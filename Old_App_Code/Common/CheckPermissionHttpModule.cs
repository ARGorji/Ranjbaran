using System;
using System.Web;
using Ranjbaran.Old_App_Code.DAL;
using System.Collections.Generic;

public class CheckPermissionHttpModule : IHttpModule
{
    #region IHttpModule Members
    public void Init(HttpApplication objApplication)
    {
        objApplication.AcquireRequestState += 
            delegate(object sender, EventArgs e) 
            {
                HttpApplication Sender = (HttpApplication) sender;

                if (Sender.Context.Request.Url.AbsolutePath.EndsWith(".ASPX", StringComparison.OrdinalIgnoreCase) && !HasPermission(Sender.Context))
                {
                    Sender.Context.Session["PathAndQuery"] = Sender.Context.Request.Url.AbsolutePath.ToUpperInvariant() + GetQueryString(Sender.Context.Request.QueryString);
                    Sender.Context.Response.Redirect("~/AccessDenied.aspx");
                }
            };
    }

    public static bool QueryStringContainsIllegalCharacters(string q)
    {
        q = q.ToUpper();
        return (false);
        //(
        //    q.Contains("SELECT") ||
        //    q.Contains("DELETE") ||
        //    q.Contains("INSERT") ||
        //    q.Contains("UPDATE") ||
        //    q.Contains("SHUTDOWN") ||
        //    q.Contains("EXEC") ||
        //    q.Contains("SCRIPT") ||
        //    q.Contains("UNION") ||
        //    q.Contains("ALTER") ||
        //    q.Contains("DROP") ||
        //    q.Contains("LIKE") ||
        //    q.Contains("CHR") ||
        //    q.Contains("..") ||
        //    q.Contains("0X") ||
        //    q.Contains("%20OR") ||
        //    q.Contains(" OR") ||
        //    q.Contains("OR%20") ||
        //    q.Contains("OR ") ||
        //    q.Contains("%20AND") ||
        //    q.Contains(" AND") ||
        //    q.Contains("AND%20") ||
        //    q.Contains("AND ") ||
        //    q.Contains("WHERE") ||
        //    q.Contains("GROUP BY") ||
        //    q.Contains("HAVING") ||
        //    q.Contains("LIKE") ||
        //    q.Contains("ALERT") ||
        //    q.Contains("%00") ||
        //    q.Contains("~")
        //);
    }

    public void Dispose()
    {
        // Simply do nothing!
    }
    #endregion
    
    private static bool HasPermission(HttpContext context)
    {
     
        int i;

        int? GroupCode = int.TryParse(context.Session["GroupCode"] as string, out i) ? (int?) i : null;
        string Path = context.Request.Url.AbsolutePath.ToUpperInvariant();

        if (Path.StartsWith("/WEBSITE/"))
            Path = Path.Substring(8);

        //foreach (var Result in new UsersDataContext().GetPageAccessList(GroupCode, Path))
        //    if (SatisfiesConditions(context, Result.Conditions))
        //        return Result.AccessLevel != 0;

        return false; // ASP.NET level default value for HasPermission is false.
    }

    private static string GetQueryString(System.Collections.Specialized.NameValueCollection queryString)
    {
        string ToReturn = null;
        foreach (string Key in queryString.AllKeys)
            ToReturn += (ToReturn == null ? "?" : "&") + Key + "=" + queryString[Key];
        return ToReturn;
    }

    private static bool SatisfiesConditions(HttpContext context, string conditions)
    {
        if (conditions == null)
            return true;

        foreach (string Token in conditions.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
        {
            bool IsOperatorEqual = true;
            int i = Token.IndexOf('=');

            if (i == -1)
            {
                IsOperatorEqual = false;
                i = Token.IndexOf('~');
            }

            if (i != -1 && i != 0 && i != Token.Length - 1 && !SatisfiesCondition(context, Token.Substring(0, i), Token.Substring(i + 1), IsOperatorEqual))
                return false;
        }

        return true;
    }

    private static bool SatisfiesCondition(HttpContext context, string leftValue, string rightValue, bool isOperatorEqual)
    {
        bool AreEqual = Equals(Expression.Parse(leftValue).AcquireValue(context), Expression.Parse(rightValue).AcquireValue(context));
        return isOperatorEqual ? AreEqual : !AreEqual;
    }

    #region Nested Types
    private enum ValueType
    {
        NotNull,
        Literal,
        QueryString,
        Session,
        Method
    }

    private class Expression
    {
        #region Backing Stores
        private string _Value;
        #endregion
        #region Properties
        public ValueType Type { get; set; }
        public string Key { get; set; }
        public List<Expression> Parameters { get; set; }

        public string Value
        {
            get
            {
                if (_Value == null)
                    throw new InvalidOperationException("Call the AcquireValue(HttpContext) method before reading the value of this property.");

                return _Value;
            }

            set { _Value = value;}
        }
        #endregion
        #region Constructors
        public Expression()
        {
            Type = ValueType.NotNull;
            Value = string.Empty;
        }

        public Expression(string methodName, string parameters)
        {
            Type = ValueType.Method;
            Parameters = new List<Expression>();
            if (!string.IsNullOrEmpty(parameters))
                foreach (string Parameter in parameters.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    Parameters.Add(Parse(Parameter));
            Key = methodName;
        }

        public Expression(string value)
        {
            Type = ValueType.Literal;
            Value = value;
        }

        public Expression(ValueType type, string key)
        {
            Type = type;
            Key = key;
        }
        #endregion
        #region Static Methods
        public static Expression Parse(string s)
        {
            Expression ToReturn;
            if (TryParse(s, out ToReturn))
                return ToReturn;
            throw new ArgumentException("Cannot initialize a new instance of the Expression class using the given string.", "s");
        }

        public static bool TryParse(string s, out Expression expression)
        {
            if (s.StartsWith("q[", StringComparison.OrdinalIgnoreCase) && s.EndsWith("]", StringComparison.Ordinal))
            {
                expression = new Expression(ValueType.QueryString, s.Substring(2, s.Length - 3));
                return true;
            }

            if (s.StartsWith("s[", StringComparison.OrdinalIgnoreCase) && s.EndsWith("]", StringComparison.Ordinal))
            {
                expression = new Expression(ValueType.Session, s.Substring(2, s.Length - 3));
                return true;
            }

            if (s.StartsWith("m[", StringComparison.OrdinalIgnoreCase) && s.EndsWith("]", StringComparison.Ordinal))
            {
                s = s.Substring(2, s.Length - 3);
                int i = s.IndexOf('(');
                if (i != -1 && s.EndsWith(")", StringComparison.Ordinal))
                {
                    string MethodName = s.Substring(0, i);
                    expression = new Expression(MethodName, s.Substring(i + 1, s.Length - i - 2));
                    return true;
                }
            }

            if (s.StartsWith("[", StringComparison.Ordinal) && s.EndsWith("]", StringComparison.Ordinal))
            {
                expression = new Expression(s.Substring(1, s.Length - 2));
                return true;
            }

            if (Equals(s, "*"))
            {
                expression = new Expression();
                return true;
            }

            expression = null;
            return false;
        }
        #endregion
        #region Methods
        public Expression AcquireValue(HttpContext context)
        {
            switch (Type)
            {
                case ValueType.QueryString:
                    _Value = context.Request.QueryString[Key] ?? string.Empty;
                    break;
                case ValueType.Session:
                    object o = context.Session[Key];
                    _Value = o == null ? string.Empty : o.ToString();
                    break;
                case ValueType.Method:
                    if (Parameters != null)
                        foreach (Expression Parameter in Parameters)
                            Parameter.AcquireValue(context);
                    break;
            }

            return this;
        }
        #endregion
        #region Method Overrides
        public override bool Equals(object obj)
        {
            Expression Other = obj as Expression;
            if (Other == null)
                return false;

            if (Type == ValueType.NotNull)
                return Other.Value.Length > 0;
            if (Other.Type == ValueType.NotNull)
                return Value.Length > 0;

            return string.Equals(_Value, Other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return _Value != null ? _Value.GetHashCode() : 0;
        }
        #endregion
    }

    public static class CheckPermissionMethods
    {
    }
    #endregion
}
