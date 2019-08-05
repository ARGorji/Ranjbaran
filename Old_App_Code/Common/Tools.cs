using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Web.SessionState;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using AKP.Base.Settings;
using AKP.Web.Controls;
using System.Net.Mime;
using System.Net;
using DataAccess;
using System.Diagnostics;
using Ranjbaran.Old_App_Code.DAL;
using CAPICOM;


public class Tools
{

    public string EmailServer = ConfigurationManager.AppSettings["EmailServer"];
    public string EmailUserName = ConfigurationManager.AppSettings["EmailUserName"];
    public string EmailPassword = ConfigurationManager.AppSettings["EmailPassword"];

    

    public static string GET_Number_To_PersianString(string TXT)
    {
        string RET = " ", STRVA = " ";
        string[] MainStr = STR_To_Int(TXT);
        int Q = 0;
        for (int i = MainStr.Length - 1; i >= 0; i--)
        {
            STRVA = " ";
            if (RET != " " && RET != null)
                STRVA = " و ";
            RET = Convert_STR(GETCountStr(MainStr[i]), Q) + STRVA + RET;
            Q++;
        }
        if (RET == " " || RET == null || RET == "  ")
            RET = "صفر";
        return RET;
    }

    private static string[] STR_To_Int(string STR)
    {
        STR = GETCountStr(STR);
        string[] RET = new string[STR.Length / 3];
        int Q = 0;
        for (int I = 0; I < STR.Length; I += 3)
        {
            RET[Q] = STR.Substring(I, 3);
            Q++;
        }
        return RET;
    }

    public static string RemoveArabicSymbols(string MyVal)
    {
        string FinalString = "";
        char[] StringArray = MyVal.ToCharArray();
        for (int i = 0; i < StringArray.Length; i++)
        {
            int CharCode = (int)StringArray[i];
            if (CharCode != 1611 && CharCode != 1612 && CharCode != 1613 && CharCode != 1614 && CharCode != 1615 && CharCode != 1616)
            {
                FinalString += StringArray[i].ToString();
            }
        }
        return FinalString;
    }

    private static string GETCountStr(string STR)
    {
        string RET = STR;
        int LEN = (STR.Length / 3 + 1) * 3 - STR.Length;
        if (LEN < 3)
        {
            for (int i = 0; i < LEN; i++)
            {
                RET = "0" + RET;
            }
        }
        if (RET == "")
            return "000";
        return RET;
    }

    public bool IsIranianIP()
    {
        try
        {
            string IP = HttpContext.Current.Request.UserHostAddress;
            //            HttpContext.Current.Response.Write("Your IP" + IP);
            string[] IPArray = IP.Split('.');
            long IPNumber = 16777216 * Convert.ToInt32(IPArray[0]) + 65536 * Convert.ToInt32(IPArray[1]) + 256 * Convert.ToInt32(IPArray[2]) + Convert.ToInt32(IPArray[3]);
            UtilDataContext dc = new UtilDataContext();
            var ResultIPs = dc.IPToCountries.Where(p => p.StartIP <= IPNumber && p.EndIP >= IPNumber);
            if (ResultIPs.Count() == 1)
            {
                if (ResultIPs.First().CountryCode == "IR")
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        catch (Exception err)
        {
            //HttpContext.Current.Response.Write(err.Message);
            return true;
        }

    }

    public string GetCountryCode()
    {
        try
        {
            string IP = HttpContext.Current.Request.UserHostAddress;
            //            HttpContext.Current.Response.Write("Your IP" + IP);
            string[] IPArray = IP.Split('.');
            long IPNumber = 16777216 * Convert.ToInt32(IPArray[0]) + 65536 * Convert.ToInt32(IPArray[1]) + 256 * Convert.ToInt32(IPArray[2]) + Convert.ToInt32(IPArray[3]);
            UtilDataContext dc = new UtilDataContext();
            var ResultIPs = dc.IPToCountries.Where(p => p.StartIP <= IPNumber && p.EndIP >= IPNumber);
            if (ResultIPs.Count() == 1)
            {
                return ResultIPs.First().CountryCode;
            }
            else
                return "";

        }
        catch (Exception err)
        {
            return "";
        }

    }


    private static string Convert_STR(string INT, int Count)
    {
        string RET = "";
        //یک صد
        if (Count == 0)
        {
            if (INT.Substring(1, 1) == "1" && INT.Substring(2, 1) != "0")
            {
                RET = GET_Number(3, Convert.ToInt32(INT.Substring(0, 1)), " ") + GET_Number(1, Convert.ToInt32(INT.Substring(2, 1)), "");
            }
            else
            {
                string STR = GET_Number(0, Convert.ToInt32(INT.Substring(2, 1)), "");
                RET = GET_Number(3, Convert.ToInt32(INT.Substring(0, 1)), GET_Number(2, Convert.ToInt32(INT.Substring(1, 1)), "") + STR) + GET_Number(2, Convert.ToInt32(INT.Substring(1, 1)), STR) + GET_Number(0, Convert.ToInt32(INT.Substring(2, 1)), "");
            }
        }
        //هزار
        else if (Count == 1)
        {
            RET = Convert_STR(INT, 0);
            RET += " هزار";
        }
        //میلیون
        else if (Count == 2)
        {
            RET = Convert_STR(INT, 0);
            RET += " میلیون";
        }
        //میلیارد
        else if (Count == 3)
        {
            RET = Convert_STR(INT, 0);
            RET += " میلیارد";
        }
        //میلیارد
        else if (Count == 4)
        {
            RET = Convert_STR(INT, 0);
            RET += " تیلیارد";
        }
        //میلیارد
        else if (Count == 5)
        {
            RET = Convert_STR(INT, 0);
            RET += " بیلیارد";
        }
        else
        {
            RET = Convert_STR(INT, 0);
            RET += Count.ToString();
        }
        return RET;
    }

    private static string GET_Number(int Count, int Number, string VA)
    {
        string RET = "";

        if (VA != "" && VA != null)
        {
            VA = " و ";
        }
        if (Count == 0 || Count == 1)
        {
            bool IsDah = Convert.ToBoolean(Count);
            string[] MySTR = new string[10];
            MySTR[1] = IsDah ? "یازده" : "یک" + VA;
            MySTR[2] = IsDah ? "دوازده" : "دو" + VA;
            MySTR[3] = IsDah ? "سیزده" : "سه" + VA;
            MySTR[4] = IsDah ? "چهارده" : "چهار" + VA;
            MySTR[5] = IsDah ? "پانزده" : "پنج" + VA;
            MySTR[6] = IsDah ? "شانزده" : "شش" + VA;
            MySTR[7] = IsDah ? "هفده" : "هفت" + VA;
            MySTR[8] = IsDah ? "هجده" : "هشت" + VA;
            MySTR[9] = IsDah ? "نوزده" : "نه" + VA;
            return MySTR[Number];
        }
        else if (Count == 2)
        {
            string[] MySTR = new string[10];
            MySTR[1] = "ده";
            MySTR[2] = "بیست" + VA;
            MySTR[3] = "سی" + VA;
            MySTR[4] = "چهل" + VA;
            MySTR[5] = "پنجاه" + VA;
            MySTR[6] = "شصت" + VA;
            MySTR[7] = "هفتاد" + VA;
            MySTR[8] = "هشتاد" + VA;
            MySTR[9] = "نود" + VA;
            return MySTR[Number];
        }
        else if (Count == 3)
        {
            string[] MySTR = new string[10];
            MySTR[1] = "یکصد" + VA;
            MySTR[2] = "دویست" + VA;
            MySTR[3] = "سیصد" + VA;
            MySTR[4] = "چهارصد" + VA;
            MySTR[5] = "پانصد" + VA;
            MySTR[6] = "ششصد" + VA;
            MySTR[7] = "هفتصد" + VA;
            MySTR[8] = "هشتصد" + VA;
            MySTR[9] = "نهصد" + VA;
            return MySTR[Number];
        }
        return RET;
    }










    public List<AccessListStruct> AccessList;
    public Tools()
    {
    }
    public static string FormatText(object obj)
    {
        string Result = "";
        if (obj != null)
        {
            Result = obj.ToString();
            Result = Result.Replace("\n", "<br>");
        }
        return Result;

    }



    public static string ReverseDate(string str)
    {
        string Result = "";
        try
        {
            string[] strArray = str.Split('/');
            for (int i = strArray.Length; i > 0; i--)
            {
                if (Result == "")
                    Result = strArray[i - 1];
                else
                    Result += "/" + strArray[i - 1];
            }
            return Result;
        }
        catch
        {
            return "";
        }
    }
    public static DateTime GetIranDate()
    {
        DateTime ServerDate = DateTime.Now;
        string TimeDiff = ConfigurationManager.AppSettings["TimeDiff"];
        string[] TimeDiffArray = TimeDiff.Split(':');
        int HourDiff = Convert.ToInt32(TimeDiffArray[0]);
        int MinDiff = Convert.ToInt32(TimeDiffArray[1]);

        return ServerDate.AddHours(HourDiff).AddMinutes(MinDiff);
    }
    public static string HLText(object obj, string Keyword)
    {
        string Result = "";
        if (obj != null)
        {
            Result = obj.ToString();
            if (Keyword != null)
            {
                string[] KeywordArray = Keyword.Split(' ');
                for (int i = 0; i < KeywordArray.Length; i++)
                {
                    Result = Result.Replace(KeywordArray[i], "<b>" + KeywordArray[i] + "</b>");
                }
            }
        }
        return Result;


    }

    public static string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        if (sPath.ToUpper() == "/SHOWPAGE.ASPX")
        {
            sPath = System.Web.HttpContext.Current.Request.Url.PathAndQuery;
        }

        return sPath;
    }


    public static long EncryptRIC(string strPass)
    {
        long result = 0;
        if (strPass.Length > 0)
        {
            for (int i = 1; i < strPass.Length + 1; i++)
                result = result + strPass[i - 1] * 256 * i;
        }
        return result;
    }


    public static DataTable DoSearchSite(string TableName, string WhereClause, string OrderByColumn)
    {
        string cnStr = ConfigurationManager.ConnectionStrings["RanjbaranConnectionString"].ConnectionString;
        SQLServer dal = new SQLServer(cnStr);
        // ************************************** Add SP Parameters *********************************************

        dal.AddParameter("@TableName", TableName, SQLServer.SQLDataType.SQLNVarchar, 50, ParameterDirection.Input);
        dal.AddParameter("@WhereClause", WhereClause, SQLServer.SQLDataType.SQLNVarchar, 4000, ParameterDirection.Input);
        dal.AddParameter("@OrderByColumn", OrderByColumn, SQLServer.SQLDataType.SQLNVarchar, 50, ParameterDirection.Input);
        // ************************************** Add SP Parameters *********************************************
        DataSet ds = dal.runSPDataSet("dbo.spFullTextSearchSite", null);
        dal.ClearParameters();
        return ds.Tables[0];
    }

    public DataTable GetDataTable(string SqlStr)
    {
        string cnStr = ConfigurationManager.ConnectionStrings["RanjbaranConnectionString"].ConnectionString;
        SQLServer dal = new SQLServer(cnStr);
        DataSet ds = dal.runSQLDataSet(SqlStr, "dt");
        return ds.Tables[0];

    }


    public static string MakeWhereClause(string Keywords, string ColumnList)
    {
        ArrayList orSearchKeyWord = new ArrayList();
        string Result = "";
        string fullQuery = "";
        if (!string.IsNullOrEmpty(Keywords))
        {
            while (Keywords.IndexOf("  ") >= 0)
            {
                Keywords = Keywords.Replace("  ", " ");
            }
            string[] ColumnListArray = ColumnList.Split(',');
            string SearchColumn = "";

            int Counter = 0;
            foreach (var Col in ColumnListArray)
            {
                SearchColumn = Col;
                string[] arr = Keywords.Split(' ');
                foreach (string tok in arr)
                {
                    if (!string.IsNullOrEmpty(tok))
                    {
                        Result += string.Format("CONTAINS({0}, '\"*{1}*\"') AND ", SearchColumn, tok.Trim());
                        fullQuery += tok.Trim() + " ";
                    }
                }
                if (Result.EndsWith(" AND "))
                    Result = Result.Substring(0, Result.LastIndexOf(" AND "));
                Result = Result + " OR ";
                Counter++;
            }
            if (Result.EndsWith(" OR "))
                Result = Result.Substring(0, Result.LastIndexOf(" OR "));
        }

        return Result;
    }
    public static void LogException(Exception exp, ControlCollection col)
    {
        Dictionary<string, string> ControlsAndValues = new Dictionary<string, string>();
        GetControlValues(col, true, ref ControlsAndValues);
        BOLExceptions.Log(exp, ControlsAndValues);
    }


    public static bool ContainsIllegalCharacter(string p)
    {
        p = p.ToUpper();
        return
            (
                p.Contains('\'') ||
                p.Contains('"') ||
                p.Contains(';') ||
                p.Contains('<') ||
                p.Contains('>') ||
                p.Contains('%') ||
                p.Contains('\\') ||
                p.Contains('&') ||
                p.Contains("..") ||
                p.Contains("--") ||
                p.Contains("~") ||
                p.Contains("0X") ||
                p.Contains("SELECT") ||
                p.Contains("DELETE") ||
                p.Contains("SHUTDOWN") ||
                p.Contains("EXEC") ||
                p.Contains("SCRIPT") ||
                p.Contains("UNION") ||
                p.Contains("INSERT") ||
                p.Contains("UPDATE") ||
                p.Contains("ALTER") ||
                p.Contains("SCRIPT") ||
                p.Contains("DROP") ||
                p.Contains("LIKE") ||
                p.Contains("HAVING") ||
                p.Contains("CAST") ||
                p.Contains(" OR ") ||
                p.Contains(" AND ") ||
                p.Contains("CREATE") ||
                p.Contains("WHERE") ||
                p.Contains("GROUP BY") ||
                p.Contains("CHR")
            );
    }

    public static void GetControlValues(ControlCollection col, bool LoopRecursive, ref Dictionary<string, string> ControlsAndValues)
    {
        foreach (Control ctrl in col)
        {
            switch (ctrl.GetType().ToString())
            {
                case "System.Web.UI.WebControls.HiddenField":
                    HiddenField h = (HiddenField)ctrl;
                    ControlsAndValues.Add(h.ClientID, h.Value);
                    break;

                case "System.Web.UI.WebControls.RadioButtonList":
                    RadioButtonList r = (RadioButtonList)ctrl;
                    ControlsAndValues.Add(r.ClientID, r.SelectedValue);
                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList drpList = (DropDownList)ctrl;
                    ControlsAndValues.Add(drpList.ClientID, drpList.SelectedValue);
                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    CheckBox chk = (CheckBox)ctrl;
                    ControlsAndValues.Add(chk.ClientID, chk.Checked.ToString());
                    break;
                case "AKAControls.ExCheckBox":
                    CheckBox Exchk = (CheckBox)ctrl;
                    ControlsAndValues.Add(Exchk.ClientID, Exchk.Checked.ToString());
                    break;

                case "System.Web.UI.WebControls.RadioButton":
                    RadioButton rd = (RadioButton)ctrl;
                    ControlsAndValues.Add(rd.ClientID, rd.Checked.ToString());
                    break;
                case "AKAControls.FarsiDate":
                    FarsiDate dte = (FarsiDate)ctrl;
                    ControlsAndValues.Add(dte.ClientID, dte.SelectedDatePersian);
                    break;
                case "AKAControls.Combo":
                    Combo cbo = (Combo)ctrl;
                    ControlsAndValues.Add(cbo.ClientID, cbo.SelectedValue);
                    break;
                case "AKAControls.ExTextBox":
                    ExTextBox ExTB = (ExTextBox)ctrl;
                    ControlsAndValues.Add(ExTB.ClientID, ExTB.Text);
                    break;

                case "Telerik.Web.UI.RadNumericTextBox":
                    Telerik.Web.UI.RadNumericTextBox RNT = (Telerik.Web.UI.RadNumericTextBox)ctrl;
                    ControlsAndValues.Add(RNT.ClientID, RNT.Text);
                    break;

                case "Telerik.Web.UI.RadEditor":
                    Telerik.Web.UI.RadEditor Editor = (Telerik.Web.UI.RadEditor)ctrl;
                    ControlsAndValues.Add(Editor.ClientID, Editor.Text);
                    break;

                case "AKAControls.Lookup":
                    Lookup lkp = (Lookup)ctrl;
                    ControlsAndValues.Add(lkp.ClientID, lkp.Code.ToString());

                    break;
                case "AKAControls.LookupTree":
                    LookupTree lkpTree = (LookupTree)ctrl;
                    ControlsAndValues.Add(lkpTree.ClientID, lkpTree.Code.ToString());
                    break;
            }
            if (LoopRecursive && ctrl.Controls.Count > 0)
                GetControlValues(ctrl.Controls, true, ref ControlsAndValues);

        }

    }

    public static string EncryptSHA1(string str)
    {
        var Cap = new HashedDataClass();
        Cap.Algorithm = CAPICOM_HASH_ALGORITHM.CAPICOM_HASH_ALGORITHM_SHA1;

        string strToHash = ConvertToAscii(str);
        Cap.Hash(strToHash);
        return Cap.Value;
    }

    public static bool GetValue(bool? Val)
    {
        if (Val == null)
            return false;
        else
        {
            return (bool)Val;
        }
    }

    public static string GetPrettyDate(DateTime d)
    {
        // 1.
        // Get time span elapsed since the date.
        TimeSpan s = DateTime.Now.Subtract(d);

        // 2.
        // Get total number of days elapsed.
        int dayDiff = (int)s.TotalDays;

        // 3.
        // Get total number of seconds elapsed.
        int secDiff = (int)s.TotalSeconds;

        // 4.
        // Don't allow out of range values.
        if (dayDiff < 0 || dayDiff >= 31)
        {
            return null;
        }

        // 5.
        // Handle same-day times.
        if (dayDiff == 0)
        {
            // A.
            // Less than one minute ago.
            if (secDiff < 60)
            {
                return "just now";
            }
            // B.
            // Less than 2 minutes ago.
            if (secDiff < 120)
            {
                return "1 minute ago";
            }
            // C.
            // Less than one hour ago.
            if (secDiff < 3600)
            {
                return string.Format("{0} minutes ago",
                    Math.Floor((double)secDiff / 60));
            }
            // D.
            // Less than 2 hours ago.
            if (secDiff < 7200)
            {
                return "1 hour ago";
            }
            // E.
            // Less than one day ago.
            if (secDiff < 86400)
            {
                return string.Format("{0} hours ago",
                    Math.Floor((double)secDiff / 3600));
            }
        }
        // 6.
        // Handle previous days.
        if (dayDiff == 1)
        {
            return "yesterday";
        }
        if (dayDiff < 7)
        {
            return string.Format("{0} days ago",
                dayDiff);
        }
        if (dayDiff < 31)
        {
            return string.Format("{0} weeks ago",
                Math.Ceiling((double)dayDiff / 7));
        }
        return null;
    }
    public static string ChageEnc(string str)
    {
        string Result = "";
        string CurChar = "";
        for (int i = 0; i < str.Length; i++)
        {
            CurChar = str.Substring(i, 1);
            switch (CurChar)
            {
                case "0":
                    Result += "۰";
                    break;
                case "1":
                    Result += "۱";
                    break;
                case "2":
                    Result += "۲";
                    break;
                case "3":
                    Result += "۳";
                    break;
                case "4":
                    Result += "۴";
                    break;
                case "5":
                    Result += "۵";
                    break;
                case "6":
                    Result += "۶";
                    break;
                case "7":
                    Result += "۷";
                    break;
                case "8":
                    Result += "۸";
                    break;
                case "9":
                    Result += "۹";
                    break;
                default:
                    Result += CurChar;
                    break;
            }

        }
        return Result;
    }

    public static string UnChageEnc(string str)
    {
        if (str == null)
            return null;
        string Result = "";
        string CurChar = "";
        for (int i = 0; i < str.Length; i++)
        {
            CurChar = str.Substring(i, 1);
            switch (CurChar)
            {
                case "۰":
                    Result += "0";
                    break;
                case "۱":
                    Result += "1";
                    break;
                case "۲":
                    Result += "2";
                    break;
                case "۳":
                    Result += "3";
                    break;
                case "۴":
                    Result += "4";
                    break;
                case "۵":
                    Result += "5";
                    break;
                case "۶":
                    Result += "6";
                    break;
                case "۷":
                    Result += "7";
                    break;
                case "۸":
                    Result += "8";
                    break;
                case "۹":
                    Result += "9";
                    break;
                default:
                    Result += CurChar;
                    break;
            }

        }
        return Result;
    }

    public static void CheckUserAccess()
    {
        if (HttpContext.Current.Session["UserCode"] == null)
            HttpContext.Current.Response.Redirect("~/Login.aspx", true);
    }
    public static string ConvertToUnicode(object obj)
    {
        string Result = "";
        if (obj != null)
        {
            Result = obj.ToString();
            Result = Result.Replace("1", "١");
            Result = Result.Replace("2", "٢");
            Result = Result.Replace("3", "٣");
            Result = Result.Replace("4", "٤");
            Result = Result.Replace("5", "٥");
            Result = Result.Replace("6", "٦");
            Result = Result.Replace("7", "٧");
            Result = Result.Replace("8", "٨");
            Result = Result.Replace("9", "٩");
            Result = Result.Replace("0", "٠");
        }
        return Result;
    }
    public static string ShowIcon(string str)
    {
        string Ext = Tools.GetExtension(str);
        switch (Ext)
        {
            case "DOC":
                return "doc.gif";
                break;
            case "PDF":
                return "pdf.gif";
                break;
            case "TXT":
                return "txt.gif";
                break;

            default:
                return "txt.gif";
                break;
        }
        return "";
    }

    public static string ShowLargeIcon(Object obj)
    {
        string str = "";
        if (obj != null)
            str = obj.ToString();
        try
        {
            string Ext = Tools.GetExtension(str);
            switch (Ext)
            {
                case "DOC":
                    return "Word-48.png";
                case "DOCX":
                    return "Word-48.png";
                case "PDF":
                    return "File-pdf-48.png";
                case "TXT":
                    return "Text-48.png";
                case "JPG":
                    return "Jpg-48.png";
                case "JPEG":
                    return "Jpg-48.png";
                case "GIF":
                    return "Jpg-48.png";
                default:
                    return "File-48.png";
            }
        }
        catch
        {
            return "";
        }
    }

    public static string GetExtension(string str)
    {
        string Extesion = "";
        if (str.Length > 3)
        {
            Extesion = str.Substring(str.Length - 3, 3).ToUpper();
        }
        return Extesion;
    }
    public static void SetMeta(string MetaID, string MetaVal)
    {
        HtmlMeta metadesc = (HtmlMeta)((Page)(HttpContext.Current.Handler)).Master.FindControl(MetaID);
        metadesc.Attributes["content"] = MetaVal;
        metadesc.Attributes.Remove("id");
    }
    public string RemoveTags(string InputHtml)
    {
        string InnerContent = InputHtml;
        int CutLen = 0;
        int StartIndex = 0;
        int EndIndex = 0;
        string TagContent = "";
        InnerContent = InnerContent.Replace("<SCRIPT", "<script");
        InnerContent = InnerContent.Replace("</SCRIPT", "</script");

        StartIndex = InnerContent.IndexOf("<script", 0);
        while (StartIndex > 0)
        {
            EndIndex = InnerContent.IndexOf("</script>", StartIndex + 7);
            if (EndIndex > 0)
            {
                CutLen = EndIndex - StartIndex + 9;
                TagContent = InnerContent.Substring(StartIndex, CutLen);
                InnerContent = InnerContent.Replace(TagContent, "");
                StartIndex = StartIndex - TagContent.Length;
            }
            else
                break;

            StartIndex = InnerContent.IndexOf("<script", 0);
        }





        InnerContent = InnerContent.Replace("<IFRAME", "<iframe");
        InnerContent = InnerContent.Replace("</IFRAME", "</iframe");

        StartIndex = InnerContent.IndexOf("<iframe", 0);
        while (StartIndex > 0)
        {
            EndIndex = InnerContent.IndexOf("</iframe>", StartIndex + 7);
            if (EndIndex > 0)
            {
                CutLen = EndIndex - StartIndex + 9;
                TagContent = InnerContent.Substring(StartIndex, CutLen);
                InnerContent = InnerContent.Replace(TagContent, "");
                StartIndex = StartIndex - TagContent.Length;
            }
            StartIndex = InnerContent.IndexOf("<iframe", 0);
        }




        StartIndex = InnerContent.IndexOf("<", 0);
        while (StartIndex >= 0)
        {
            EndIndex = InnerContent.IndexOf(">", StartIndex + 1);
            if (EndIndex > 0)
            {
                CutLen = EndIndex - StartIndex - 1;
                TagContent = InnerContent.Substring(StartIndex + 1, CutLen);
                InnerContent = InnerContent.Replace("<" + TagContent + ">", "");
                StartIndex = StartIndex - TagContent.Length;
            }
            else
                break;
            StartIndex = InnerContent.IndexOf("<", 0);
        }


        return InnerContent;

    }
    public string GetRandString(int StrLen)
    {
        string Result = "";
        double RndIndex;
        string[] Alphabets = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9" , "0",
                               "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9" , "0",
                               "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "1", "2", "3", "4", "5", "6", "7", "8", "9" , "0"};
        RandomProportional randObj = new RandomProportional();

        Random rnd;
        for (int i = 0; i < StrLen; i++)
        {
            rnd = new Random(Alphabets.Length);

            int rndIndex = (int)(randObj.NextDouble() * 100);
            Result = Result + Alphabets[rndIndex];
        }
        return Result;
    }

    public string GetRandNumber(int StrLen)
    {
        string Result = "";
        double RndIndex;
        string[] Alphabets = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "1", "2", "3", "4", "5", "6", "7", "8", "9", "1", "2", "3", "4", "5", "6", "7", "8", "9", "1", "2", "3", "4", "5", "6", "7", "8", "9", "1", "2", "3", "4", "5", "6", "7", "8", "9", "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                             "1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9",
                             "1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9","1", "2", "3", "4", "5", "6", "7", "8", "9",};
        RandomProportional randObj = new RandomProportional();

        Random rnd;
        for (int i = 0; i < StrLen; i++)
        {
            rnd = new Random(Alphabets.Length);

            int rndIndex = (int)(randObj.NextDouble() * 100);
            Result = Result + Alphabets[rndIndex];
        }
        return Result;
    }


    public static string ShowPic(Object obj, string DefaultPic, string PicPath)
    {
        string Result = "";
        if (obj != null)
        {
            Result = ((Page)HttpContext.Current.Handler).ResolveUrl(PicPath + obj.ToString());
        }
        else
        {
            if (DefaultPic == null)
                Result = ((Page)HttpContext.Current.Handler).ResolveUrl(PicPath + "man_icon.png");
            else
                Result = ((Page)HttpContext.Current.Handler).ResolveUrl("~/" + DefaultPic);
        }
        return Result;
    }

    public bool SendMessageWithAttachment(string Body, string Subject, string FromEmail, string ToEmail, string BCC, string CC, string AttachFileList)
    {
        try
        {
            System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
            message.From = FromEmail;
            message.To = ToEmail;
            message.Bcc = BCC;
            message.Cc = CC;
            message.Subject = Subject;
            message.Body = Body;
            message.BodyFormat = System.Web.Mail.MailFormat.Html;
            message.BodyEncoding = new UTF8Encoding();
            System.Web.Mail.SmtpMail.SmtpServer = EmailServer;// "mail.nedahosting.com";

            string[] AttachFileArray = AttachFileList.Split(';');
            if (AttachFileList != "")
            {
                for (int i = 0; i < AttachFileArray.Length; i++)
                {
                    string AttachFileName = AttachFileArray[i];
                    System.Web.Mail.MailAttachment data = new System.Web.Mail.MailAttachment(AttachFileName, System.Web.Mail.MailEncoding.Base64);
                    message.Attachments.Add(data);
                }
            }

            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", EmailUserName);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", EmailPassword);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", EmailServer);
            System.Web.Mail.SmtpMail.Send(message);
            return true;
        }
        catch
        {
            return false;
        }


        // Specify the file to be attached and sent.
        // This example assumes that a file named Data.xls exists in the
        // current working directory.
        // Create a message and set up the recipients.
        //MailMessage message = new MailMessage(
        //       FromEmail,
        //       ToEmail,
        //       Subject,
        //       Body);

        //MailAddressCollection CollectionBCC = new MailAddressCollection();
        //if (BCC != "")
        //{
        //    string[] BCCList = BCC.Split(',');
        //    foreach (var item in BCCList)
        //    {
        //        message.Bcc.Add(new MailAddress(item));
        //    }
        //}

        //string[] AttachFileArray = AttachFileList.Split(';');
        //if (AttachFileList != "")
        //{
        //    for (int i = 0; i < AttachFileArray.Length; i++)
        //    {
        //        string AttachFileName = AttachFileArray[i];
        //        Attachment data = new Attachment(AttachFileName, MediaTypeNames.Application.Octet);
        //        ContentDisposition disposition = data.ContentDisposition;
        //        disposition.CreationDate = System.IO.File.GetCreationTime(AttachFileName);
        //        disposition.ModificationDate = System.IO.File.GetLastWriteTime(AttachFileName);
        //        disposition.ReadDate = System.IO.File.GetLastAccessTime(AttachFileName);
        //        message.Attachments.Add(data);
        //    }
        //}
        //message.IsBodyHtml = true;
        //message.BodyEncoding = new UTF8Encoding();


        //SmtpClient client = new SmtpClient("208.101.61.212");
        //client.Credentials = new NetworkCredential("mail@irankids.net", "ali1357");

        //try
        //{
        //    client.Send(message);
        //    return true;
        //}
        //catch (Exception ex)
        //{
        //    return false;
        //}

        //data.Dispose();
    }
    public bool SendEmail(string Body, string Subject, string FromEmail, string ToEmail, string BCC, string CC)
    {
        try
        {
            System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
            message.From = FromEmail;
            message.To = ToEmail;
            message.Bcc = BCC;
            message.Cc = CC;
            message.Subject = Subject;
            message.Body = Body;
            message.BodyFormat = System.Web.Mail.MailFormat.Html;
            message.BodyEncoding = new UTF8Encoding();
            System.Web.Mail.SmtpMail.SmtpServer = "mail.IONS.net";

            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "info@IONS.net");
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "ali1357");
            System.Web.Mail.SmtpMail.Send(message);
            return true;
        }
        catch
        {
            return false;
        }



    }



    public List<AccessListStruct> GetAccessList(string BaseID)
    {
        AccessList = new List<AccessListStruct>();
        AccessList.Clear();
        int UserCode;
        if (HttpContext.Current.Session["UserCode"] != null)
        {
            UserCode = Convert.ToInt32(HttpContext.Current.Session["UserCode"]);
            var UserAccess = new BOLUsers().GetUserAccessByBaseID(UserCode, BaseID);
            CreateAccess("New", 1, UserAccess, BaseID);
            CreateAccess("Edit", 2, UserAccess, BaseID);
            //            CreateAccess("Delete", 4, UserAccess, BaseID);
            CreateAccess("View", 8, UserAccess, BaseID);
            //            CreateAccess("Export", 16, UserAccess, BaseID);
        }
        return AccessList;
    }


    private void CreateAccess(string AccessName, int AccessCode, System.Linq.IQueryable<vUserAccess> UserAccess, string BaseID)
    {
        foreach (vUserAccess CurAccess in UserAccess)
        {
            if ((CurAccess.AccessType & AccessCode) == AccessCode)
            {
                if (CurAccess.ResName.Split('.')[0] == BaseID || BaseID == null)
                    AccessList.Add(new AccessListStruct(AccessName.ToUpper(), CurAccess.ResName.ToUpper()));
            }
        }

    }



    public static string ShowBriefText(Object obj, int CutLen)
    {
        string Result = "";
        string str = "";
        if (obj != null)
            str = obj.ToString();
        else
            return "";
        if (str.Length >= CutLen)
        {
            int CutPos = str.IndexOf(" ", CutLen);
            if (CutPos > 0)
                Result = str.Substring(0, CutPos) + "...";
            else
                Result = str;

        }
        else
            Result = str;

        return Result;
    }




    public static IBaseBOLTree GetBOLClassTree(string BaseID)
    {

        Type t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);

        if (t == null)
            throw new Exception("Invalid BaseID.");

        ConstructorInfo[] cArr = t.GetConstructors();
        return (IBaseBOLTree)Activator.CreateInstance(t);

    }

    public static bool IsUserSessionStillValid()
    {
        //bool Result = false;
        //if (HttpContext.Current.Session["New"] != null ||
        //   HttpContext.Current.Session["Edit"] != null ||
        //   HttpContext.Current.Session["Delete"] != null ||
        //   HttpContext.Current.Session["View"] != null ||
        //   HttpContext.Current.Session["Export"] != null)
        //    Result = true;
        //return Result;
        return (HttpContext.Current.Session["UserCode"] != null);
    }
    public static string ConvertToAscii(string Unistr)
    {
        Encoding ascii = Encoding.UTF32;
        Byte[] b = ascii.GetBytes(Unistr);
        return ascii.GetString((b));
    }
    public static string GetHashString(string str)
    {
        return Tools.Encode(str);
    }
    private static bool IsImageExtension(string Extension)
    {
        string[] images = { "JPG", "GIF", "JPEG", "BMP", "PNG" };
        return images.Contains(Extension);
    }
    public static string GetFileExtension(string FileName)
    {
        int DotIndex = FileName.LastIndexOf(".");
        if (DotIndex > -1)
            return FileName.Substring(DotIndex + 1, FileName.Length - DotIndex - 1).ToUpper();
        else
            return "";
    }
    public static string GetRandomFileName(string FileName)
    {
        Guid newGd = Guid.NewGuid();
        string Extenstion = GetFileExtension(FileName);
        return newGd.ToString().Replace("-", "") + "." + Extenstion;
    }

    public static void CloseWin(Page page, System.Web.UI.MasterPage MP, string BaseID, string InstanceName)
    {
        HtmlGenericControl CloseScript = new HtmlGenericControl("script");
        CloseScript.Attributes.Add("type", "text/javascript");
        StringBuilder strClose = new StringBuilder();
        if (InstanceName != null)
            strClose.Append("opener." + InstanceName + ".ShowDetail(\"" + BaseID + "\", undefined,\"true,\",\"" + InstanceName + "\" );\n");
        strClose.Append("window.close();");
        //CloseScript.InnerText = strClose.ToString();
        //page.Header.Controls.Add(CloseScript);
        ((Literal)MP.FindControl("ltrHeaderScript")).Text = "<script type=\"text/javascript\">" + strClose.ToString() + "</script>";

        MP.FindControl("cphMain").Visible = false;
    }
    public static string GetCondition(SearchFilterCollection sFilterCols)
    {
        string WhereCond = "";
        if (sFilterCols != null)
        {
            foreach (SearchFilter sf in sFilterCols)
            {
                if (sf.Value != "")
                {
                    string CurrentVal = sf.Value;
                    CurrentVal = CurrentVal.Replace("'", "''");
                    switch (sf.Operator)
                    {
                        case SqlOperators.Equal:
                            WhereCond = sf.ColumnName + " = N''" + CurrentVal + "'' " + sf.CurOperand.ToString() + " " + WhereCond;
                            break;
                        case SqlOperators.Like:
                            WhereCond = sf.ColumnName + " LIKE N''%" + CurrentVal + "%'' " + sf.CurOperand.ToString() + " " + WhereCond;
                            break;
                        case SqlOperators.StartsWith:
                            WhereCond = sf.ColumnName + " LIKE N''" + CurrentVal + "%'' " + sf.CurOperand.ToString() + " " + WhereCond;
                            break;
                        case SqlOperators.EndWith:
                            WhereCond = sf.ColumnName + " LIKE N''%" + CurrentVal + "'' " + sf.CurOperand.ToString() + " " + WhereCond;
                            break;
                        case SqlOperators.GreaterThan:
                            WhereCond = sf.ColumnName + " > " + CurrentVal + " " + sf.CurOperand.ToString() + " " + WhereCond;
                            break;
                        case SqlOperators.GreaterThanOrEqual:
                            WhereCond = sf.ColumnName + " >= " + CurrentVal + " " + sf.CurOperand.ToString() + " " + WhereCond;
                            break;
                        case SqlOperators.LessThan:
                            WhereCond = sf.ColumnName + " < " + CurrentVal + " " + sf.CurOperand.ToString() + " " + WhereCond;
                            break;
                        case SqlOperators.LessThanOrEqual:
                            WhereCond = sf.ColumnName + " <= " + CurrentVal + " " + sf.CurOperand.ToString() + " " + WhereCond;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (WhereCond.Length > 0)
            {
                if (WhereCond.Trim().Substring(WhereCond.Length - 4, 3) == "AND")
                    WhereCond = WhereCond.Substring(0, WhereCond.Length - 4); //Remove AND
                else if (WhereCond.Trim().Substring(WhereCond.Length - 3, 2) == "OR")
                    WhereCond = WhereCond.Substring(0, WhereCond.Length - 3); //Remove OR
            }
        }
        return WhereCond;
    }
    public bool HasAccess(string AccessName, string FieldName)
    {
        //bool Result = false;
        //HttpSessionState Session = HttpContext.Current.Session;

        //if (Session[AccessName] != null)
        //{
        //    string[] AccessArray = Session[AccessName].ToString().Split(',');
        //    for (int i = 0; i < AccessArray.Length; i++)
        //    {
        //        if (AccessArray[i].ToLower() == FieldName.ToLower())
        //        {
        //            Result = true;
        //            break;
        //        }
        //    }
        //}
        //return Result;

        if (AccessList != null)
        {
            if (AccessList.Contains(new AccessListStruct(AccessName.ToUpper(), FieldName.ToUpper())))
                return true;
        }
        return false;

    }
    public static string ChangeEnc(Object Objstr)
    {
        if (Objstr == null)
            return "";
        string str = Objstr.ToString();
        string Result = "";
        string CurChar = "";

        Result = str.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
        return Result;
    }

    public static string ChangeEncArabic(Object Objstr)
    {
        if (Objstr == null)
            return "";
        string str = Objstr.ToString();
        string Result = "";
        string CurChar = "";
        for (int i = 0; i < str.Length; i++)
        {
            CurChar = str.Substring(i, 1);
            switch (CurChar)
            {
                case "0":
                    Result += "۰";
                    break;
                case "1":
                    Result += "۱";
                    break;
                case "2":
                    Result += "٢";
                    break;
                case "3":
                    Result += "۳";
                    break;
                case "4":
                    Result += "٤";
                    break;
                case "5":
                    Result += "٥";
                    break;
                case "6":
                    Result += "٦";
                    break;
                case "7":
                    Result += "۷";
                    break;
                case "8":
                    Result += "۸";
                    break;
                case "9":
                    Result += "٩";
                    break;
                default:
                    Result += CurChar;
                    break;
            }

        }
        return Result;
    }
    public static string UploadPath()
    {
        //String savePath = @"D:\Projects\Mabsa\WebSite\Files\";
        string savePath = AppDomain.CurrentDomain.BaseDirectory + "Files\\";
        return savePath;

    }
    public static string Encode(string MyString)
    {
        string result;
        try
        {

            byte[] IV = new byte[8] { 240, 32, 45, 29, 0, 76, 173, 59 };
            string cryptoKey = "All you need is Love and money";
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(MyString);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            des.Key = MD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(cryptoKey));
            des.IV = IV;
            byte[] CodedBuffer = des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length);
            result = System.Convert.ToBase64String(CodedBuffer, 0, CodedBuffer.Length);
        }
        catch
        {
            result = null;
        }
        return result;

    }
    public static string Decode(string CodedString64)
    {
        string result;
        try
        {
            byte[] IV = new byte[8] { 240, 32, 45, 29, 0, 76, 173, 59 };
            string cryptoKey = "All you need is Love and money";
            byte[] buffer = Convert.FromBase64String(CodedString64);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            des.Key = MD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(cryptoKey));
            des.IV = IV;
            byte[] CodedBuffer = des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length);
            result = System.Text.Encoding.ASCII.GetString(CodedBuffer);
        }
        catch
        {
            result = "";
        }
        return result;
    }
    public static string FormatShamsiDate(string unfDate)
    {
        string Result = "";
        if (unfDate != null)
        {
            if (unfDate.Length == 8)
                Result = unfDate.Substring(0, 4) + "/" + unfDate.Substring(4, 2) + "/" + unfDate.Substring(unfDate.Length - 2, 2);
            else
                Result = "";
        }
        else
            Result = "";
        return Result;
    }
    public string[] GetHeaderArray(string SqlStr)
    {
        int SelectIndex = 0;
        int FromIndex = 0;
        string ColStr = "";
        string CutFullHedaer = "";
        string[] Result;
        string TempResult = "";
        string MySeperator = "|SEP|";
        char[] charSeparators1 = new char[] { ',' };
        string[] stringSeparators1 = new string[] { "AS" };
        string[] stringSeparators2 = new string[] { MySeperator };

        try
        {
            SelectIndex = SqlStr.ToUpper().IndexOf("SELECT");
            FromIndex = SqlStr.ToUpper().IndexOf("FROM");
            if (FromIndex > SelectIndex)
            {
                ColStr = SqlStr.Substring(SelectIndex + 7, FromIndex - SelectIndex - 7);
                string[] ColStrArray = ColStr.Split(charSeparators1);

                for (int i = 0; i < ColStrArray.Length; i++)
                {
                    CutFullHedaer = ColStrArray[i];
                    if (CutFullHedaer.ToUpper().IndexOf("AS") >= 0)
                    {
                        string[] ColNameAliasArray = CutFullHedaer.Split(stringSeparators1, StringSplitOptions.None);
                        if (i == 0)
                            TempResult = ColNameAliasArray[0].Trim();
                        else
                            TempResult = TempResult + MySeperator + ColNameAliasArray[0].Trim();
                    }
                    else
                    {
                        if (i == 0)
                            TempResult = CutFullHedaer.Trim();
                        else
                            TempResult = TempResult + MySeperator + CutFullHedaer.Trim();
                    }
                }
                Result = TempResult.Split(stringSeparators2, StringSplitOptions.None);

                return Result;
            }
            else
                return null;

        }
        catch
        {
            return null;
        }


    }
    public static string GetXMLData(DataSet dsxml, string FilterKeyword, int FilterClm, int PageNo, string BtnType, int PageSize, int PageCount, int OldOrder, int Order, int Repeat)
    {
        string XMLColumnName = "";
        if (BtnType == "Next" || BtnType == "Back")
        {
            switch (BtnType)
            {
                case "Back":
                    if (PageNo > 1)
                        PageNo = PageNo - 1;
                    else
                        PageNo = 1;
                    break;
                case "Next":
                    if (PageNo < PageCount)
                        PageNo = PageNo + 1;
                    else
                        PageNo = PageCount;
                    break;

                default:
                    PageNo = 1;
                    break;
            }
        }

        int StartRecord = (PageNo - 1) * PageSize;
        int EndRecord = StartRecord + PageSize - 1;
        string strResult = "";

        strResult = "<?xml version='1.0' encoding='utf-8'?><dataroot>";
        DataView dv = new DataView(dsxml.Tables[0]);
        DataTable dt = new DataTable();

        string ColName = "";
        ColName = dv.Table.Columns[Order].ColumnName;
        if (Order == OldOrder && Repeat == 0)
            ColName = ColName + " DESC";
        else
            Repeat = 1;
        dv.Sort = ColName;
        OldOrder = Order;

        dt = dv.ToTable();

        //start data cells
        for (int j = StartRecord; j < EndRecord; j++)
        {
            if (j >= dt.Rows.Count)
                break;
            strResult += "<item>";
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                XMLColumnName = dt.Columns[k].ColumnName;
                XMLColumnName = XMLColumnName.Replace(" ", "_x0020_");
                strResult += "<" + XMLColumnName + ">" + dt.Rows[j][k].ToString() + "</" + XMLColumnName + ">";
            }
            strResult += "</item>";
        }
        //end data cells
        strResult += "</dataroot>";


        return strResult;

    }
    public static string GetListXMLData(DataSet dsxml, CellCollection CellCol, string FilterKeyword, int FilterClm, int PageNo, string BtnType, int PageSize, int PageCount, int OldOrder, int Order, int Repeat)
    {
        string XMLColumnName = "";
        if (BtnType == "Next" || BtnType == "Back")
        {
            switch (BtnType)
            {
                case "Back":
                    if (PageNo > 1)
                        PageNo = PageNo - 1;
                    else
                        PageNo = 1;
                    break;
                case "Next":
                    if (PageNo < PageCount)
                        PageNo = PageNo + 1;
                    else
                        PageNo = PageCount;
                    break;

                default:
                    PageNo = 1;
                    break;
            }
        }

        int StartRecord = (PageNo - 1) * PageSize;
        int EndRecord = StartRecord + PageSize - 1;
        string strResult = "";

        strResult = "<?xml version='1.0' encoding='utf-8'?><dataroot>";
        DataView dv = new DataView(dsxml.Tables[0]);
        DataTable dt = new DataTable();

        string ColName = "";
        ColName = dv.Table.Columns[Order].ColumnName;
        if (Order == OldOrder && Repeat == 0)
            ColName = ColName + " DESC";
        else
            Repeat = 1;
        dv.Sort = ColName;
        OldOrder = Order;

        dt = dv.ToTable();

        //start data cells
        for (int j = StartRecord; j < EndRecord; j++)
        {
            if (j >= dt.Rows.Count)
                break;
            strResult += "<item>";
            foreach (DataCell dc in CellCol)
            {
                XMLColumnName = dc.CaptionName;
                XMLColumnName = XMLColumnName.Replace(" ", "_x0020_");
                strResult += "<" + XMLColumnName + ">" + dt.Rows[j][dc.FieldName].ToString() + "</" + XMLColumnName + ">";
            }

            strResult += "</item>";
        }
        //end data cells
        strResult += "</dataroot>";
        /*
        for (int k = 0; k < dt.Columns.Count; k++)
        {
            XMLColumnName = dt.Columns[k].ColumnName;
            XMLColumnName = XMLColumnName.Replace(" ", "_x0020_");
            strResult += "<" + XMLColumnName + ">" + dt.Rows[j][k].ToString() + "</" + XMLColumnName + ">";
        }
         */

        return strResult;

    }
    private static string GetTableName(string BaseID)
    {
        string Result = "";
        switch (BaseID)
        {
            case "Companies":
                {
                    Result = "PersonCompanies";
                    break;
                }
            default:
                {
                    Result = BaseID;
                    break;
                }
        }
        return Result;
    }
    public static string FormatCurrency(Object objPrice)
    {
        if (objPrice == null)
            return "";
        string Price = objPrice.ToString();
        Price = Price.Replace(" ", "");
        string Result = "";
        if (Price != "")
        {
            //Price = Price.Substring(0, Price.Length - 1);//convert rial to toman
            while (Price.Length > 3)
            {
                if (Result == "")
                    Result = Price.Substring(Price.Length - 3, 3);
                else
                    Result = Price.Substring(Price.Length - 3, 3) + "," + Result;
                Price = Price.Substring(0, Price.Length - 3);
            }
        }
        if (Result != "")
            Result = Price + "," + Result;

        if (Result == "")
            Result = Price;
        return Result;

    }

    public static string FormatCurrency2(Object objPrice)
    {
        string Price = "";
        if (objPrice == null)
            return "";
        else
            Price = objPrice.ToString();
        try
        {
            Price = Price.Replace(" ", "");
            string Result = "";
            if (Price != "")
            {
                //Price = Price.Substring(0, Price.Length - 1);//convert rial to toman
                while (Price.Length > 3)
                {
                    if (Result == "")
                        Result = Price.Substring(Price.Length - 3, 3);
                    else
                        Result = Price.Substring(Price.Length - 3, 3) + "," + Result;
                    Price = Price.Substring(0, Price.Length - 3);
                }
            }
            if (Result != "")
                Result = Price + "," + Result;

            if (Result == "")
                Result = Price;
            Result = ChangeEnc(Result);
            return Result;

        }
        catch
        {
            return "";
        }

    }

    //public static string LogError(HttpRequest Req , Exception exp)
    //{
    //    string Result = "بروز خطای غیر منتظره";
    //    string ErrorMessage = exp.Message;
    //    BOLErrorLog ErrorLogBOL = new BOLErrorLog();
    //    ErrorLogBOL.Insert(exp.Message, DateTime.Now, Req.Url.AbsolutePath, Req.QueryString.ToString());
    //    if (ErrorMessage.IndexOf("DELETE statement conflicted") >= 0)
    //        Result = Messages.ShowMessage(MessagesEnum.ErrorWhileDelete);
    //    else if (ErrorMessage.IndexOf("Cannot insert duplicate key") >= 0)
    //        Result = Messages.ShowMessage(MessagesEnum.ErrorInsertDuplicate);


    //    return Result;

    //}
    public static string GetAppPath()
    {
        return "http://localhost:4300/WebSite";
        //return "http://www.hamshahrimahalleh.net";
    }
    public static string GetListTitle(string BaseID, params int[] CodeArray)
    {
        string Result = "";
        IBaseBOL BOLClass = UITools.GetBOLClass(BaseID);
        if (UITools.IsHardCode(BaseID))
            BOLClass.QueryObjName = BaseID;
        #region Generate SearchFilter
        SqlOperators CurOperator = SqlOperators.Equal; ;
        SearchFilter sFilter;
        SearchFilterCollection sfCols = new SearchFilterCollection();
        sFilter = new SearchFilter("CODE", CurOperator, CodeArray[0].ToString());
        sfCols.Add(sFilter);
        #endregion
        DataTable dt = BOLClass.GetDataSource(sfCols, "Code", 1, 1);

        CellCollection cellCol = BOLClass.GetListCellCollection();
        int Counter = 0;
        bool BracketAdded = false;
        if (dt.Rows.Count > 0)
        {
            foreach (DataCell dataCell in cellCol)
            {
                if (dataCell.IsListTitle)
                {
                    if (Counter == 1)
                    {
                        Result = Result + " (" + dt.Rows[0][dataCell.FieldName].ToString();
                        BracketAdded = true;
                    }
                    else
                        Result = Result + " " + dt.Rows[0][dataCell.FieldName].ToString();
                    Counter++;
                }

            }
            if (Counter > 1 && BracketAdded)
                Result = Result + ")";
            //else
            //    Result = Result.Substring(0, Result.Length - 1);
            if (Result.EndsWith("()"))
                Result = Result.Substring(0, Result.Length - 2);
            Result = Result.Trim();
        }
        return Result;
    }

    public static string GetListTitle(string BaseID, string CodeArray)
    {
        string Result = "";
        if (CodeArray.Trim() == "")
            return Result;
        IBaseBOL BOLClass = UITools.GetBOLClass(BaseID);
        if (UITools.IsHardCode(BaseID))
            BOLClass.QueryObjName = BaseID;
        #region Generate SearchFilter
        SqlOperators CurOperator = SqlOperators.Equal; ;
        SearchFilter sFilter;
        SearchFilterCollection sfCols = new SearchFilterCollection();
        sFilter = new SearchFilter("CODE", CurOperator, CodeArray.ToString());
        sfCols.Add(sFilter);
        #endregion
        DataTable dt = BOLClass.GetDataSource(sfCols, "Code", 1, 1);

        CellCollection cellCol = BOLClass.GetListCellCollection();
        if (dt.Rows.Count > 0)
        {
            foreach (DataCell dataCell in cellCol)
            {
                if (dataCell.IsListTitle)
                    Result = Result + " " + dt.Rows[0][dataCell.FieldName].ToString();
            }
            Result = Result.Trim();
        }
        return Result;
    }

    public static string GetListTreeTitle(string BaseID, params int[] CodeArray)
    {
        string Result = "";
        IBaseBOLTree BOLClass = GetBOLClassTree(BaseID);
        if (UITools.IsHardCode(BaseID))
            BOLClass.QueryObjName = BaseID;
        #region Generate SearchFilter
        SqlOperators CurOperator = SqlOperators.Equal; ;
        SearchFilter sFilter;
        SearchFilterCollection sfCols = new SearchFilterCollection();
        sFilter = new SearchFilter("CODE", CurOperator, CodeArray[0].ToString());
        sfCols.Add(sFilter);
        #endregion
        DataTable dt = BOLClass.GetDataSource(sfCols, "Code", 1, 1);

        CellCollection cellCol = BOLClass.GetListCellCollection();
        if (dt.Rows.Count > 0)
        {
            foreach (DataCell dataCell in cellCol)
            {
                if (dataCell.IsListTitle)
                    Result = Result + " " + dt.Rows[0][dataCell.FieldName].ToString();
            }
            Result = Result.Trim();
        }
        return Result;
    }
    //public bool SendMail(string MailText, string MailSubject, string Email, string BCC, string SenderAccount)
    //{
    //    string mailServerName = "mail.hamshahrimahalleh.net";//"smtp.gmail.com";
    //    string from = "\"Subject Board\" <info@hamshahrimahalleh.net>";
    //    string to = "";
    //    string subject = "";
    //    string body = "";

    //    string Sender = SenderAccount;
    //    body = MailText;
    //    subject = MailSubject;
    //    to = Email;

    //    try
    //    {
    //        using (MailMessage message =
    //            new MailMessage(from, to, subject, body))
    //        {
    //            message.IsBodyHtml = true;
    //            if (BCC != "")
    //            {
    //                MailAddressCollection SendBCC = new MailAddressCollection();
    //                SendBCC.Add(BCC);
    //                foreach (MailAddress BCCEmail in SendBCC)
    //                    message.Bcc.Add(BCCEmail);
    //            }
    //            SmtpClient mailClient = new SmtpClient(mailServerName);

    //            //mailClient.EnableSsl = true;
    //            mailClient.UseDefaultCredentials = false;
    //            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
    //            //mailClient.Credentials = new System.Net.NetworkCredential(Sender, "ads123");
    //            mailClient.Credentials = new System.Net.NetworkCredential("", "");
    //            mailClient.Send(message);
    //        }
    //        return true;
    //    }
    //    catch (FormatException)
    //    {
    //        return false;
    //    }
    //    catch (SmtpException)
    //    {
    //        return false;
    //    }

    //}

    public static void AddToDic(Control ctrl, string BOLName, IList list)
    {
        var dic = new Dictionary<Control, object>();
        string PropertyFullName = BOLName + "." + ctrl.ID.Substring(3, ctrl.ID.Length - 3);
        dic.Add(ctrl, PropertyFullName);
        list.Add(dic);
    }
    public static string PersianTextCorrection(string str)
    {
        //ي U+FEF1 Arabic Letter Yeh Isolated Form
        //ي U+064A Arabic Letter Yeh
        //ى U+0649 Arabic Letter Alef Maksura
        //ی U+06CC Arabic Letter Farsi Yeh

        //ك U+0643 Arabic Letter Kaf
        //ک U+069A Arabic Letter Keh

        //U+0660 	٠ 	Arabic-Indic Digit Zero
        //U+0661 	١ 	Arabic-Indic Digit One
        //U+0662 	٢ 	Arabic-Indic Digit Two
        //U+0663 	٣ 	Arabic-Indic Digit Three
        //U+0664 	٤ 	Arabic-Indic Digit Four
        //U+0665 	٥ 	Arabic-Indic Digit Five
        //U+0666 	٦ 	Arabic-Indic Digit Six
        //U+0667 	٧ 	Arabic-Indic Digit Seven
        //U+0668 	٨ 	Arabic-Indic Digit Eight
        //U+0669 	٩ 	Arabic-Indic Digit Nine

        //U+06F0 	۰ 	Extended Arabic-Indic Digit Zero
        //U+06F1 	۱ 	Extended Arabic-Indic Digit One
        //U+06F2 	۲ 	Extended Arabic-Indic Digit Two
        //U+06F3 	۳ 	Extended Arabic-Indic Digit Three
        //U+06F4 	۴ 	Extended Arabic-Indic Digit Four
        //U+06F5 	۵ 	Extended Arabic-Indic Digit Five
        //U+06F6 	۶ 	Extended Arabic-Indic Digit Six
        //U+06F7 	۷ 	Extended Arabic-Indic Digit Seven
        //U+06F8 	۸ 	Extended Arabic-Indic Digit Eight
        //U+06F9 	۹ 	Extended Arabic-Indic Digit Nine
        return str.Replace('ي', 'ی').Replace('ي', 'ی').Replace('ى', 'ی').Replace('ﻳ', 'ی').Replace('ﻱ', 'ی').Replace('ﻲ', 'ی').Replace('ﻰ', 'ی').Replace('ﻯ', 'ی').Replace('ك', 'ک')
            .Replace('٠', '۰').Replace('١', '۱').Replace('٢', '۲').Replace('٣', '۳').Replace('٤', '۴')
            .Replace('٥', '۵').Replace('٦', '۶').Replace('٧', '۷').Replace('٨', '۸').Replace('٩', '۹');
    }

    public string PersianTextCorrection2(string str)
    {
        //ي U+FEF1 Arabic Letter Yeh Isolated Form
        //ي U+064A Arabic Letter Yeh
        //ى U+0649 Arabic Letter Alef Maksura
        //ی U+06CC Arabic Letter Farsi Yeh

        //ك U+0643 Arabic Letter Kaf
        //ک U+069A Arabic Letter Keh

        //U+0660 	٠ 	Arabic-Indic Digit Zero
        //U+0661 	١ 	Arabic-Indic Digit One
        //U+0662 	٢ 	Arabic-Indic Digit Two
        //U+0663 	٣ 	Arabic-Indic Digit Three
        //U+0664 	٤ 	Arabic-Indic Digit Four
        //U+0665 	٥ 	Arabic-Indic Digit Five
        //U+0666 	٦ 	Arabic-Indic Digit Six
        //U+0667 	٧ 	Arabic-Indic Digit Seven
        //U+0668 	٨ 	Arabic-Indic Digit Eight
        //U+0669 	٩ 	Arabic-Indic Digit Nine

        //U+06F0 	۰ 	Extended Arabic-Indic Digit Zero
        //U+06F1 	۱ 	Extended Arabic-Indic Digit One
        //U+06F2 	۲ 	Extended Arabic-Indic Digit Two
        //U+06F3 	۳ 	Extended Arabic-Indic Digit Three
        //U+06F4 	۴ 	Extended Arabic-Indic Digit Four
        //U+06F5 	۵ 	Extended Arabic-Indic Digit Five
        //U+06F6 	۶ 	Extended Arabic-Indic Digit Six
        //U+06F7 	۷ 	Extended Arabic-Indic Digit Seven
        //U+06F8 	۸ 	Extended Arabic-Indic Digit Eight
        //U+06F9 	۹ 	Extended Arabic-Indic Digit Nine
        return str.Replace('ي', 'ی').Replace('ي', 'ی').Replace('ى', 'ی').Replace('ﻳ', 'ی').Replace('ﻱ', 'ی').Replace('ﻲ', 'ی').Replace('ﻰ', 'ی').Replace('ﻯ', 'ی').Replace('ك', 'ک')
            .Replace('٠', '۰').Replace('١', '۱').Replace('٢', '۲').Replace('٣', '۳').Replace('٤', '۴')
            .Replace('٥', '۵').Replace('٦', '۶').Replace('٧', '۷').Replace('٨', '۸').Replace('٩', '۹');
    }

    private List<WebControl> listControls = new List<WebControl>();
    public List<WebControl> GetControls()
    {
        ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
        RecursiveFindControl(cph.Controls);
        return listControls;
        //RecursiveFindControl(ControlCollection RootCol)
    }
    private void RecursiveFindControl(ControlCollection RootCol)
    {
        foreach (Control c in RootCol)
        {
            if (c is WebControl)
            {
                WebControl wc = (WebControl)c;
                string wcAtt = wc.Attributes["jas"];
                if (!string.IsNullOrEmpty(wcAtt) && wcAtt == "1")
                    listControls.Add(wc);
            }
            if (c.GetType().ToString() != "System.Web.UI.WebControls.Repeater")
            {
                if (c.Controls.Count > 0)
                    RecursiveFindControl(c.Controls);
            }
        }
    }
    public static IList TryGet(IList<Dictionary<Control, object>> ControlsAndValues, IBaseBOL CurObj)
    {

        try
        {
            ArrayList arr = new ArrayList();
            int count = ControlsAndValues.Count;

            for (int i = 0; i < count; i++)
            {
                string message = ValidateControls(ControlsAndValues[i].Keys.First(), ControlsAndValues[i], CurObj);
                if (!string.IsNullOrEmpty(message))
                {
                    arr.Add(message);
                }
            }
            return arr;
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }

    public static IList TryGet(IList<Dictionary<Control, object>> ControlsAndValues, IBaseBOLTree CurObj)
    {

        try
        {
            ArrayList arr = new ArrayList();
            int count = ControlsAndValues.Count;

            for (int i = 0; i < count; i++)
            {
                string message = ValidateControls(ControlsAndValues[i].Keys.First(), ControlsAndValues[i], CurObj);
                if (!string.IsNullOrEmpty(message))
                {
                    arr.Add(message);
                }
            }
            return arr;
        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }
    public static string[] SplitValue(ColumnAttribute att)
    {
        string dbt = att.DbType;
        string MaxLen = "", AllowNull, Dbtype;


        if ((dbt.IndexOf("NOT NULL") > 0) || (att.CanBeNull = false))
        {
            AllowNull = "FALSE";
            dbt = dbt.Replace("NOT NULL", "");
        }
        else
            AllowNull = "TRUE";

        if (dbt.IndexOf("(") > 0)
        {
            MaxLen = dbt.Substring(dbt.IndexOf("(") + 1, dbt.IndexOf(")") - (dbt.IndexOf("(") + 1));
            dbt = dbt.Replace("(" + MaxLen + ")", "");
        }

        Dbtype = dbt.Trim().ToUpper();

        return new string[] { Dbtype, MaxLen, AllowNull };

    }

    public static object GetControlValue(Control ctrl)
    {
        ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));

        object retVal = "";
        try
        {
            switch (ctrl.GetType().ToString())
            {
                //case "System.Web.UI.WebControls.TextBox":
                //    TextBox t = (TextBox)ctrl;
                //    retVal = t.Text;
                //    break;
                case "AKP.Web.Controls.ExTextBox":
                    AKP.Web.Controls.ExTextBox ExTB = (AKP.Web.Controls.ExTextBox)ctrl;
                    retVal = ExTB.Text;
                    break;

                case "System.Web.UI.WebControls.HiddenField":
                    HiddenField h = (HiddenField)ctrl;
                    retVal = h.Value;

                    break;
                case "System.Web.UI.WebControls.Label":
                    Label lbl = (Label)ctrl;
                    retVal = lbl.Text;

                    break;
                case "System.Web.UI.WebControls.RadioButtonList":
                    RadioButtonList r = (RadioButtonList)ctrl;
                    retVal = r.SelectedValue;

                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList drpList = (DropDownList)ctrl;
                    retVal = drpList.SelectedValue;

                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    CheckBox chk = (CheckBox)ctrl;
                    retVal = chk.Checked;

                    break;
                case "AKP.Web.Controls.ExCheckBox":
                    CheckBox Exchk = (CheckBox)ctrl;
                    retVal = Exchk.Checked;

                    break;
                case "System.Web.UI.WebControls.RadioButton":
                    RadioButton rd = (RadioButton)ctrl;
                    retVal = rd.Checked;

                    break;
                case "AKP.Web.Controls.FarsiDate":
                    AKP.Web.Controls.FarsiDate dte = (AKP.Web.Controls.FarsiDate)ctrl;
                    retVal = dte.SelectedDateChristian;

                    break;
                case "AKP.Web.Controls.Combo":
                    AKP.Web.Controls.Combo cbo = (AKP.Web.Controls.Combo)ctrl;
                    retVal = cbo.Value;

                    break;


                case "AKP.Web.Controls.NumericTextBox":
                    AKP.Web.Controls.NumericTextBox ntxtBox = (AKP.Web.Controls.NumericTextBox)ctrl;
                    retVal = ntxtBox.Text;

                    break;
                case "System.Web.UI.WebControls.HyperLink":
                    //System.Web.UI.WebControls.HyperLink hl = (System.Web.UI.WebControls.HyperLink)ctrl;
                    //hl.ImageUrl = "~/Imager.aspx?ImgFilePath=" + HttpUtility.UrlEncode(Tools.Encode(dic.ToString())) + "&StaticHW=150";
                    //dic[ctrl] = hl.NavigateUrl;

                    break;
                case "Telerik.Web.UI.RadUpload":
                    Telerik.Web.UI.RadUpload Upload = (Telerik.Web.UI.RadUpload)ctrl;
                    string UploadPath = Upload.TargetFolder;
                    if (!string.IsNullOrEmpty(UploadPath))
                    {
                        if (Upload.UploadedFiles.Count > 0)
                        {
                            string FileExtension = GetFileExtension(Upload.UploadedFiles[0].FileName);
                            if (FileExtension.ToUpper() != "ASP" && FileExtension.ToUpper() != "ASPX" && FileExtension.ToUpper() != "JS" || FileExtension.ToUpper() == "PHP")
                            {
                                string RandName = GetRandomFileName(Upload.UploadedFiles[0].FileName);
                                Upload.UploadedFiles[0].SaveAs(HttpContext.Current.Server.MapPath(UploadPath + RandName));

                                string FileNameID = Upload.ID.Replace("upl", "hpl");
                                HyperLink hpl = (HyperLink)cph.FindControl(FileNameID);
                                hpl.ImageUrl = "~/Imager.aspx?ImgFilePath=" + HttpUtility.UrlEncode(Encode(UploadPath + RandName)) + "&StaticHW=150";

                                retVal = RandName;
                                Upload.Attributes.Add("FileName", RandName);
                                return retVal;
                            }
                        }
                        else
                            if (!string.IsNullOrEmpty(Upload.Attributes["FileName"]))
                                retVal = Upload.Attributes["FileName"];
                    }


                    string UploadID = Upload.ID;
                    string DelCheckBoxID = UploadID.Replace("upl", "chkDelete");
                    CheckBox chkDelUpload = (CheckBox)cph.FindControl(DelCheckBoxID);
                    if (chkDelUpload != null)
                    {
                        if (chkDelUpload.Checked)
                        {
                            retVal = null;
                        }

                    }

                    break;
                case "AKP.Web.Controls.ExRadUpload":
                    AKP.Web.Controls.ExRadUpload ExUpload = (AKP.Web.Controls.ExRadUpload)ctrl;
                    string ExUploadPath = ExUpload.TargetFolder;
                    if (!string.IsNullOrEmpty(ExUploadPath))
                    {
                        if (ExUpload.UploadedFiles.Count > 0)
                        {
                            string FileExtension = GetFileExtension(ExUpload.UploadedFiles[0].FileName);
                            if (FileExtension.ToUpper() != "ASP" && FileExtension.ToUpper() != "ASPX" && FileExtension.ToUpper() != "JS" || FileExtension.ToUpper() == "PHP")
                            {
                                //string RandName = GetRandomFileName(ExUpload.UploadedFiles[0].FileName);
                                string RandName = ExUpload.UploadedFiles[0].GetName();
                                if (!ExUploadPath.EndsWith("/"))
                                    ExUploadPath = ExUploadPath + "/";
                                ExUpload.UploadedFiles[0].SaveAs(HttpContext.Current.Server.MapPath(ExUploadPath + RandName));
                                string FileNameID = ExUpload.ID.Replace("upl", "hpl");
                                HyperLink hpl = (HyperLink)cph.FindControl(FileNameID);
                                hpl.ImageUrl = "~/Imager.aspx?ImgFilePath=" + HttpUtility.UrlEncode(Encode(ExUploadPath + RandName)) + "&StaticHW=150";

                                //hl.ImageUrl = "~/Imager.aspx?ImgFilePath=" + HttpUtility.UrlEncode(Encode(FullNameObj.ToString())) + "&StaticHW=150";
                                ExUpload.Attributes.Add("FileName", ExUploadPath.Replace("~","") + RandName);
                                retVal = ExUploadPath.Replace("~/", "/") + RandName;
                                return retVal;
                            }
                        }
                        else
                            retVal = ExUpload.Attributes["FileName"];
                    }



                    string ExDelCheckBoxID = ExUpload.ID.Replace("upl", "chkDelete");
                    CheckBox ExchkDelUpload = (CheckBox)cph.FindControl(ExDelCheckBoxID);
                    if (ExchkDelUpload != null)
                    {
                        if (ExchkDelUpload.Checked)
                        {
                            retVal = null;
                        }

                    }

                    break;
                case "AKP.Web.Controls.Lookup":
                    AKP.Web.Controls.Lookup lkp = (AKP.Web.Controls.Lookup)ctrl;
                    retVal = lkp.Value;
                    break;

                case "AKP.Web.Controls.LookupTree":
                    AKP.Web.Controls.LookupTree lkpTree = (AKP.Web.Controls.LookupTree)ctrl;
                    retVal = lkpTree.Value;
                    break;
                case "Telerik.Web.UI.RadEditor":
                    Telerik.Web.UI.RadEditor Editor = (Telerik.Web.UI.RadEditor)ctrl;
                    retVal = PersianTextCorrection(Editor.Html).Replace("\n", "");
                    break;
                case "AKP.Web.Controls.ExRadEditor":
                    AKP.Web.Controls.ExRadEditor ExEditor = (AKP.Web.Controls.ExRadEditor)ctrl;
                    retVal = PersianTextCorrection(ExEditor.Html).Replace("\n", "");
                    //retVal = PersianTextCorrection(ExEditor.Html);
                    break;

                default:
                    retVal = "نوع کنترل غیر مجاز است.";
                    break;
            }

            return retVal;
        }
        catch (Exception exp)
        {
            return exp.Message;
        }


    }

    public static string ValidateControls(Control ctrl, Dictionary<Control, object> dic, IBaseBOL CurObj)
    {
        try
        {
            string Property = dic[ctrl].ToString().Remove(0, dic[ctrl].ToString().IndexOf(".") + 1);

            //if (!Tools.HasAccess(AccessList,AccessList,"Edit", BaseID + "." + Property))
            //    return "";

            string ErrorMessage = "";
            ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));


            MemberInfo mi = CurObj.GetType().GetMember(Property)[0];
            ColumnAttribute att = (ColumnAttribute)System.Attribute.GetCustomAttribute(mi, typeof(ColumnAttribute));

            string[] sv = new string[] { };
            sv = SplitValue(att);

            Label l;
            string LabelText;
            switch (ctrl.GetType().ToString())
            {
                case "AKP.Web.Controls.ExTextBox":
                    AKP.Web.Controls.ExTextBox ExTB = (AKP.Web.Controls.ExTextBox)ctrl;
                    l = cph.FindControl(ExTB.ID.Replace("txt", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if (sv[2] == "FALSE")
                        if (ExTB.DisplayMode == AKP.Web.Controls.Common.EnmDisplayMode.EditMode)
                        {
                            if (string.IsNullOrEmpty(ExTB.Text))
                            {
                                ErrorMessage = LabelText + "نباید خالی باشد";
                                return (ErrorMessage);
                            }
                        }

                    if (sv[0].ToUpper() != "NTEXT" && sv[0].ToUpper() != "TEXT")
                    {
                        if (ExTB.DisplayMode == AKP.Web.Controls.Common.EnmDisplayMode.EditMode)
                        {
                            int MaxAllowedLen = 0;
                            string strMaxAllowedLen = sv[1];
                            if (strMaxAllowedLen == "MAX")
                                MaxAllowedLen = 999999999;
                            else
                                MaxAllowedLen = Convert.ToInt32(sv[1]);
                            if (ExTB.Text.Length > MaxAllowedLen)
                                ErrorMessage = string.Format("طول {0} بیشتر از حد مجاز است", LabelText);
                        }
                    }
                    break;

                case "System.Web.UI.WebControls.HiddenField":
                    HiddenField h = (HiddenField)ctrl;

                    break;
                case "System.Web.UI.WebControls.Label":
                    Label lbl = (Label)ctrl;

                    break;
                case "System.Web.UI.WebControls.RadioButtonList":
                    RadioButtonList r = (RadioButtonList)ctrl;

                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList drpList = (DropDownList)ctrl;

                    l = cph.FindControl(drpList.ID.Replace("cbo", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if (!(drpList.SelectedValue.ToString() != string.Empty))
                    {
                        if (sv[2] == "FALSE")
                            ErrorMessage = LabelText + "نباید خالی باشد";
                        else
                            dic[ctrl] = null;
                    }

                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    CheckBox chk = (CheckBox)ctrl;

                    break;
                case "AKP.Web.Controls.ExCheckBox":
                    CheckBox Exchk = (CheckBox)ctrl;

                    break;
                case "System.Web.UI.WebControls.RadioButton":
                    RadioButton rd = (RadioButton)ctrl;

                    break;
                case "AKP.Web.Controls.FarsiDate":
                    AKP.Web.Controls.FarsiDate dte = (AKP.Web.Controls.FarsiDate)ctrl;
                    l = cph.FindControl(dte.ID.Replace("dte", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";
                    if (sv[2] == "FALSE" && dte.SelectedDateChristian == null)
                    {
                        ErrorMessage = LabelText + "نباید خالی باشد";

                    }

                    break;
                case "AKP.Web.Controls.Combo":
                    AKP.Web.Controls.Combo cbo = (AKP.Web.Controls.Combo)ctrl;
                    l = cph.FindControl(cbo.ID.Replace("cbo", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if (cbo.Value != null && cbo.Value.ToString() == string.Empty)
                    {
                        if (sv[2] == "FALSE")
                            ErrorMessage = LabelText + "نباید خالی باشد";
                    }

                    break;

                case "Telerik.Web.UI.RadEditor":
                    Telerik.Web.UI.RadEditor Editor = (Telerik.Web.UI.RadEditor)ctrl;
                    break;
                case "AKP.Web.Controls.ExRadEditor":
                    AKP.Web.Controls.ExRadEditor ExEditor = (AKP.Web.Controls.ExRadEditor)ctrl;
                    break;

                case "AKP.Web.Controls.NumericTextBox":
                    AKP.Web.Controls.NumericTextBox ntxtBox = (AKP.Web.Controls.NumericTextBox)ctrl;
                    l = cph.FindControl(ntxtBox.ID.Replace("txt", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if (ntxtBox.Text != null)
                    {
                        if (ntxtBox.HasError)
                        {
                            ErrorMessage = string.Format("عدد وارد شده برای {0} بزرگتر از حد مجاز است", LabelText);
                            return (ErrorMessage);
                        }
                    }
                    if (sv[2] == "FALSE")
                        if (ntxtBox.Text == null || string.IsNullOrEmpty(ntxtBox.Text.ToString()))
                        {
                            ErrorMessage = LabelText + "نباید خالی باشد";
                            return (ErrorMessage);
                        }
                    break;
                case "System.Web.UI.WebControls.HyperLink":
                    //System.Web.UI.WebControls.HyperLink hl = (System.Web.UI.WebControls.HyperLink)ctrl;
                    //hl.ImageUrl = "~/Imager.aspx?ImgFilePath=" + HttpUtility.UrlEncode(Tools.Encode(dic.ToString())) + "&StaticHW=150";
                    //dic[ctrl] = hl.NavigateUrl;
                    break;

                case "Telerik.Web.UI.RadUpload":
                    Telerik.Web.UI.RadUpload Upload = (Telerik.Web.UI.RadUpload)ctrl;
                    string UploadPath = Upload.TargetFolder;
                    l = cph.FindControl(Upload.ID.Replace("upl", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";
                    if (!string.IsNullOrEmpty(UploadPath))
                    {
                        if (Upload.UploadedFiles.Count > 0)
                        {
                            string FileExtension = GetFileExtension(Upload.UploadedFiles[0].FileName);
                            if (FileExtension.ToUpper() == "ASP" ||
                                FileExtension.ToUpper() == "ASPX" ||
                                FileExtension.ToUpper() == "JS" ||
                                FileExtension.ToUpper() == "PHP" ||
                                FileExtension.ToUpper() == "EXE" ||
                                FileExtension.ToUpper() == "COM" ||
                                FileExtension.ToUpper() == "BAT" ||
                                FileExtension.ToUpper() == "REG")
                            {
                                ErrorMessage = " مجاز به بارگذاری " + LabelText + " با پسوند " + FileExtension.ToUpper() + " نیستید ";
                                return (ErrorMessage);
                            }
                        }
                        else if (Upload.InvalidFiles.Count > 0)
                        {
                            int MaxSize = Upload.MaxFileSize / 1048576;
                            if (Upload.InvalidFiles[0].ContentLength > Upload.MaxFileSize)
                                ErrorMessage = LabelText + " وارد شده نباید بزرگتر از " + Convert.ToString(MaxSize) + " مگابایت باشد ";
                            else
                                ErrorMessage = LabelText + "وارد شده تصویری نیست";
                            return (ErrorMessage);
                        }
                    }
                    break;

                case "AKP.Web.Controls.ExRadUpload":
                    AKP.Web.Controls.ExRadUpload RadUpload = (AKP.Web.Controls.ExRadUpload)ctrl;
                    string RadUploadPath = RadUpload.TargetFolder;
                    l = cph.FindControl(RadUpload.ID.Replace("upl", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    HyperLink hplFile = cph.FindControl(RadUpload.ID.Replace("upl", "hpl")) as HyperLink;

                    if (sv[2] == "FALSE" && string.IsNullOrEmpty(RadUpload.Attributes["FileName"]))
                    {
                        if (!string.IsNullOrEmpty(LabelText) && RadUpload.UploadedFiles.Count == 0)
                        {
                            ErrorMessage = LabelText + "نباید خالی باشد";
                            return (ErrorMessage);
                        }
                    }
                    if (!string.IsNullOrEmpty(RadUploadPath))
                    {
                        if (RadUpload.UploadedFiles.Count > 0)
                        {
                            string FileExtension = GetFileExtension(RadUpload.UploadedFiles[0].FileName);
                            if (FileExtension.ToUpper() == "ASP" ||
                                FileExtension.ToUpper() == "ASPX" ||
                                FileExtension.ToUpper() == "JS" ||
                                FileExtension.ToUpper() == "PHP" ||
                                FileExtension.ToUpper() == "EXE" ||
                                FileExtension.ToUpper() == "COM" ||
                                FileExtension.ToUpper() == "BAT" ||
                                FileExtension.ToUpper() == "REG")
                            {
                                ErrorMessage = " مجاز به بارگذاری " + LabelText + " با پسوند " + FileExtension.ToUpper() + " نیستید ";
                                return (ErrorMessage);
                            }
                        }
                        else if (RadUpload.InvalidFiles.Count > 0)
                        {
                            int MaxSize;
                            MaxSize = RadUpload.MaxFileSize / 1048576;
                            if (RadUpload.InvalidFiles[0].ContentLength > RadUpload.MaxFileSize)
                                ErrorMessage = LabelText + " وارد شده نباید بزرگتر از " + Convert.ToString(MaxSize) + " مگابایت باشد ";
                            else
                                ErrorMessage = LabelText + "وارد شده تصویری نیست";
                            return (ErrorMessage);
                        }
                    }
                    break;
                case "AKP.Web.Controls.Lookup":
                    AKP.Web.Controls.Lookup lkp = (AKP.Web.Controls.Lookup)ctrl;
                    l = cph.FindControl(lkp.ID.Replace("lkp", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if ((lkp.Code != null && lkp.Code.ToString() == string.Empty))
                    {
                        if (sv[2] == "FALSE")
                            ErrorMessage = LabelText + "نباید خالی باشد";
                    }

                    break;


                case "AKP.Web.Controls.LookupTree":
                    AKP.Web.Controls.LookupTree lkpTree = (AKP.Web.Controls.LookupTree)ctrl;
                    l = cph.FindControl(lkpTree.ID.Replace("tre", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if ((lkpTree.Code.ToString() == string.Empty))
                    {
                        if (sv[2] == "FALSE")
                            ErrorMessage = LabelText + "نباید خالی باشد";
                    }
                    break;
                default:
                    ErrorMessage = "نوع کنترل غیر مجاز است.";
                    break;
            }


            //if ((pi != null) && (string.IsNullOrEmpty(ErrorMessage)))
            //    pi.SetValue(CurObj, dic[ctrl], new object[] { });

            return ErrorMessage;
        }
        catch (Exception exp)
        {
            return exp.Message;
        }


    }
    public static string ValidateControls(Control ctrl, Dictionary<Control, object> dic, IBaseBOLTree CurObj)
    {
        try
        {
            string Property = dic[ctrl].ToString().Remove(0, dic[ctrl].ToString().IndexOf(".") + 1);

            //if (!Tools.HasAccess(AccessList,AccessList,"Edit", BaseID + "." + Property))
            //    return "";

            string ErrorMessage = "";
            ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));


            MemberInfo mi = CurObj.GetType().GetMember(Property)[0];
            ColumnAttribute att = (ColumnAttribute)System.Attribute.GetCustomAttribute(mi, typeof(ColumnAttribute));

            string[] sv = new string[] { };
            sv = SplitValue(att);

            Label l;
            string LabelText;
            switch (ctrl.GetType().ToString())
            {
                case "AKP.Web.Controls.ExTextBox":
                    AKP.Web.Controls.ExTextBox ExTB = (AKP.Web.Controls.ExTextBox)ctrl;
                    l = cph.FindControl(ExTB.ID.Replace("txt", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if (sv[2] == "FALSE")
                        if (string.IsNullOrEmpty(ExTB.Text))
                        {
                            ErrorMessage = LabelText + "نباید خالی باشد";
                            return (ErrorMessage);
                        }

                    if (sv[0].ToUpper() != "NTEXT" && sv[0].ToUpper() != "TEXT")
                    {
                        if (ExTB.Text.Length > Convert.ToInt32(sv[1]))
                            ErrorMessage = string.Format("طول {0} بیشتر از حد مجاز است", LabelText);
                    }
                    break;

                case "System.Web.UI.WebControls.HiddenField":
                    HiddenField h = (HiddenField)ctrl;

                    break;
                case "System.Web.UI.WebControls.Label":
                    Label lbl = (Label)ctrl;

                    break;
                case "System.Web.UI.WebControls.RadioButtonList":
                    RadioButtonList r = (RadioButtonList)ctrl;

                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList drpList = (DropDownList)ctrl;

                    l = cph.FindControl(drpList.ID.Replace("cbo", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if (!(drpList.SelectedValue.ToString() != string.Empty))
                    {
                        if (sv[2] == "FALSE")
                            ErrorMessage = LabelText + "نباید خالی باشد";
                        else
                            dic[ctrl] = null;
                    }

                    break;
                case "System.Web.UI.WebControls.CheckBox":
                    CheckBox chk = (CheckBox)ctrl;

                    break;
                case "AKP.Web.Controls.ExCheckBox":
                    CheckBox Exchk = (CheckBox)ctrl;

                    break;
                case "System.Web.UI.WebControls.RadioButton":
                    RadioButton rd = (RadioButton)ctrl;

                    break;
                case "AKP.Web.Controls.FarsiDate":
                    AKP.Web.Controls.FarsiDate dte = (AKP.Web.Controls.FarsiDate)ctrl;
                    l = cph.FindControl(dte.ID.Replace("dte", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";
                    if (sv[2] == "FALSE" && dte.SelectedDateChristian == null)
                    {
                        ErrorMessage = LabelText + "نباید خالی باشد";

                    }

                    break;
                case "AKP.Web.Controls.Combo":
                    AKP.Web.Controls.Combo cbo = (AKP.Web.Controls.Combo)ctrl;
                    l = cph.FindControl(cbo.ID.Replace("cbo", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if (cbo.Value != null && cbo.Value.ToString() == string.Empty)
                    {
                        if (sv[2] == "FALSE")
                            ErrorMessage = LabelText + "نباید خالی باشد";
                    }

                    break;

                case "Telerik.Web.UI.RadEditor":
                    Telerik.Web.UI.RadEditor Editor = (Telerik.Web.UI.RadEditor)ctrl;
                    break;
                case "AKP.Web.Controls.ExRadEditor":
                    AKP.Web.Controls.ExRadEditor ExEditor = (AKP.Web.Controls.ExRadEditor)ctrl;
                    break;

                case "AKP.Web.Controls.NumericTextBox":
                    AKP.Web.Controls.NumericTextBox ntxtBox = (AKP.Web.Controls.NumericTextBox)ctrl;
                    l = cph.FindControl(ntxtBox.ID.Replace("txt", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if (ntxtBox.Text != null)
                    {
                        if (ntxtBox.HasError)
                        {
                            ErrorMessage = string.Format("عدد وارد شده برای {0} بزرگتر از حد مجاز است", LabelText);
                            return (ErrorMessage);
                        }
                    }
                    if (sv[2] == "FALSE")
                        if (ntxtBox.Text == null || string.IsNullOrEmpty(ntxtBox.Text.ToString()))
                        {
                            ErrorMessage = LabelText + "نباید خالی باشد";
                            return (ErrorMessage);
                        }
                    break;
                case "System.Web.UI.WebControls.HyperLink":
                    //System.Web.UI.WebControls.HyperLink hl = (System.Web.UI.WebControls.HyperLink)ctrl;
                    //hl.ImageUrl = "~/Imager.aspx?ImgFilePath=" + HttpUtility.UrlEncode(Tools.Encode(dic.ToString())) + "&StaticHW=150";
                    //dic[ctrl] = hl.NavigateUrl;
                    break;

                case "Telerik.Web.UI.RadUpload":
                    Telerik.Web.UI.RadUpload Upload = (Telerik.Web.UI.RadUpload)ctrl;
                    string UploadPath = Upload.TargetFolder;
                    l = cph.FindControl(Upload.ID.Replace("upl", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";
                    if (!string.IsNullOrEmpty(UploadPath))
                    {
                        if (Upload.UploadedFiles.Count > 0)
                        {
                            string FileExtension = GetFileExtension(Upload.UploadedFiles[0].FileName);
                            if (FileExtension.ToUpper() == "ASP" ||
                                FileExtension.ToUpper() == "ASPX" ||
                                FileExtension.ToUpper() == "JS" ||
                                FileExtension.ToUpper() == "PHP" ||
                                FileExtension.ToUpper() == "EXE" ||
                                FileExtension.ToUpper() == "COM" ||
                                FileExtension.ToUpper() == "BAT" ||
                                FileExtension.ToUpper() == "REG")
                            {
                                ErrorMessage = " مجاز به بارگذاری " + LabelText + " با پسوند " + FileExtension.ToUpper() + " نیستید ";
                                return (ErrorMessage);
                            }
                        }
                        else if (Upload.InvalidFiles.Count > 0)
                        {
                            int MaxSize = Upload.MaxFileSize / 1048576;
                            if (Upload.InvalidFiles[0].ContentLength > Upload.MaxFileSize)
                                ErrorMessage = LabelText + " وارد شده نباید بزرگتر از " + Convert.ToString(MaxSize) + " مگابایت باشد ";
                            else
                                ErrorMessage = LabelText + "وارد شده تصویری نیست";
                            return (ErrorMessage);
                        }
                    }
                    break;

                case "AKP.Web.Controls.ExRadUpload":
                    AKP.Web.Controls.ExRadUpload RadUpload = (AKP.Web.Controls.ExRadUpload)ctrl;
                    string RadUploadPath = RadUpload.TargetFolder;
                    l = cph.FindControl(RadUpload.ID.Replace("upl", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";
                    if (!string.IsNullOrEmpty(RadUploadPath))
                    {
                        if (RadUpload.UploadedFiles.Count > 0)
                        {
                            string FileExtension = GetFileExtension(RadUpload.UploadedFiles[0].FileName);
                            if (FileExtension.ToUpper() == "ASP" ||
                                FileExtension.ToUpper() == "ASPX" ||
                                FileExtension.ToUpper() == "JS" ||
                                FileExtension.ToUpper() == "PHP" ||
                                FileExtension.ToUpper() == "EXE" ||
                                FileExtension.ToUpper() == "COM" ||
                                FileExtension.ToUpper() == "BAT" ||
                                FileExtension.ToUpper() == "REG")
                            {
                                ErrorMessage = " مجاز به بارگذاری " + LabelText + " با پسوند " + FileExtension.ToUpper() + " نیستید ";
                                return (ErrorMessage);
                            }
                        }
                        else if (RadUpload.InvalidFiles.Count > 0)
                        {
                            int MaxSize;
                            MaxSize = RadUpload.MaxFileSize / 1024;
                            if (RadUpload.InvalidFiles[0].ContentLength > RadUpload.MaxFileSize)
                                ErrorMessage = LabelText + " وارد شده نباید بزرگتر از " + Convert.ToString(MaxSize) + " کیلو بایت باشد ";
                            else
                                ErrorMessage = LabelText + "وارد شده تصویری نیست";
                            return (ErrorMessage);
                        }
                    }
                    break;
                case "AKP.Web.Controls.Lookup":
                    AKP.Web.Controls.Lookup lkp = (AKP.Web.Controls.Lookup)ctrl;
                    l = cph.FindControl(lkp.ID.Replace("lkp", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if ((lkp.Code != null && lkp.Code.ToString() == string.Empty))
                    {
                        if (sv[2] == "FALSE")
                            ErrorMessage = LabelText + "نباید خالی باشد";
                    }

                    break;


                case "AKP.Web.Controls.LookupTree":
                    AKP.Web.Controls.LookupTree lkpTree = (AKP.Web.Controls.LookupTree)ctrl;
                    l = cph.FindControl(lkpTree.ID.Replace("lkp", "lbl")) as Label;
                    LabelText = l != null ? l.Text : "";
                    LabelText = "<u>" + LabelText.Replace(":", "") + "</u>" + " ";

                    if ((lkpTree.Code.ToString() == string.Empty))
                    {
                        if (sv[2] == "FALSE")
                            ErrorMessage = LabelText + "نباید خالی باشد";
                    }
                    break;
                default:
                    ErrorMessage = "نوع کنترل غیر مجاز است.";
                    break;
            }


            //if ((pi != null) && (string.IsNullOrEmpty(ErrorMessage)))
            //    pi.SetValue(CurObj, dic[ctrl], new object[] { });

            return ErrorMessage;
        }
        catch (Exception exp)
        {
            return exp.Message;
        }


    }

    public static string ShowPersianDate(Object obj)
    {
        string Result = "";
        try
        {

            if (obj != null)
            {
                DateTimeMethods dtm = new DateTimeMethods();
                Result = dtm.GetPersianDate(Convert.ToDateTime(obj));
                Result = Tools.ChangeEnc(Result);
            }
        }
        catch
        {
        }
        return Result;
    }


    public void ShowControl(string FieldName, Control ctrl, object obj, IBaseBOL CurObj)
    {

        if (this.HasAccess("View", FieldName))
            Tools.SetControlValue(ctrl, obj, CurObj);
        //else
        //ctrl.SkinID = "NoViewAccess";
    }
    public void ShowControl(string FieldName, Control ctrl, object obj, IBaseBOLTree CurObj)
    {

        if (this.HasAccess("View", FieldName))
            Tools.SetControlValue(ctrl, obj, CurObj);
        //else
        //ctrl.SkinID = "NoViewAccess";
    }

    public static void SetControlValue(Control ctrl, object obj, IBaseBOLTree CurObj)
    {
        #region Display Mandatory -- Moved EditForm.cs
        //Label lbl;
        //string ctrID;
        //string[] sv = new string[] { };

        //ctrID = ctrl.ID.Substring(3, ctrl.ID.Length - 3);
        //ContentPlaceHolder cphMandatory = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
        //lbl = cphMandatory.FindControl("lbl" + ctrID) as Label;

        //PropertyInfo piMandatory = CurObj.GetType().GetProperty(ctrID);
        //MemberInfo miMandatory = CurObj.GetType().GetMember(ctrID)[0];
        //ColumnAttribute attMandatory = (ColumnAttribute)System.Attribute.GetCustomAttribute(miMandatory, typeof(ColumnAttribute));

        //sv = SplitValue(attMandatory);
        //if (sv[2] == "FALSE" && lbl != null)
        //    lbl.Text = lbl.Text + " * ";

        #endregion Display Mandatory

        switch (ctrl.GetType().ToString())
        {
            case "System.Web.UI.WebControls.HiddenField":
                HiddenField h = (HiddenField)ctrl;
                h.Value = obj.ToString();
                break;
            case "System.Web.UI.WebControls.Label":
                Label l = (Label)ctrl;
                if (obj != null)
                    l.Text = obj.ToString();
                break;
            case "System.Web.UI.WebControls.RadioButtonList":
                RadioButtonList r = (RadioButtonList)ctrl;
                r.SelectedValue = obj.ToString();
                break;
            case "System.Web.UI.WebControls.DropDownList":
                DropDownList drpList = (DropDownList)ctrl;
                if ((obj.ToString() != string.Empty))
                    drpList.SelectedValue = obj.ToString();
                else
                    obj = DBNull.Value;
                break;
            case "System.Web.UI.WebControls.CheckBox":
                CheckBox chk = (CheckBox)ctrl;
                if (obj != DBNull.Value)
                    chk.Checked = Convert.ToBoolean(obj);
                break;
            case "AKP.Web.Controls.ExCheckBox":
                CheckBox Exchk = (CheckBox)ctrl;
                if (obj != DBNull.Value)
                    Exchk.Checked = Convert.ToBoolean(obj);
                break;

            case "System.Web.UI.WebControls.RadioButton":
                RadioButton rd = (RadioButton)ctrl;
                if (obj != DBNull.Value)
                    rd.Checked = Convert.ToBoolean(obj);
                break;
            case "AKP.Web.Controls.FarsiDate":
                AKP.Web.Controls.FarsiDate dte = (AKP.Web.Controls.FarsiDate)ctrl;
                if (obj != null)
                    dte.SelectedDateChristian = Convert.ToDateTime(obj);
                break;
            case "AKP.Web.Controls.Combo":
                AKP.Web.Controls.Combo cbo = (AKP.Web.Controls.Combo)ctrl;
                if (obj != null)
                    cbo.Value = Convert.ToInt32(obj);
                break;
            case "AKP.Web.Controls.ExTextBox":
                AKP.Web.Controls.ExTextBox ExTB = (AKP.Web.Controls.ExTextBox)ctrl;
                if (obj != null)
                {
                    if ((ExTB.DisplayMode == AKP.Web.Controls.Common.EnmDisplayMode.ViewMode))
                    {
                        ExTB.Text = new Tools().ShowMoreText(obj.ToString(), ExTB, ExTB.MoreLinkLength, !ExTB.HasMoreLink, HttpContext.Current.Request.QueryString["BaseID"] + "." + ExTB.ID.Remove(0, 3), ExTB.MoreLinkText);
                    }
                    else
                        ExTB.Text = obj.ToString();
                }

                break;

            case "AKP.Web.Controls.NumericTextBox":
                AKP.Web.Controls.NumericTextBox ntxtBox = (AKP.Web.Controls.NumericTextBox)ctrl;
                if (obj != null)
                    ntxtBox.Text = Convert.ToInt32(obj);
                break;


            case "Telerik.Web.UI.RadEditor":
                Telerik.Web.UI.RadEditor Editor = (Telerik.Web.UI.RadEditor)ctrl;
                if (obj != null)
                    Editor.Html = PersianTextCorrection(obj.ToString());
                break;
            case "AKP.Web.Controls.ExRadEditor":
                AKP.Web.Controls.ExRadEditor ExEditor = (AKP.Web.Controls.ExRadEditor)ctrl;
                if (obj != null)
                    ExEditor.Html = PersianTextCorrection(obj.ToString());
                break;

            case "System.Web.UI.WebControls.HyperLink":
                break;

            case "Telerik.Web.UI.RadUpload":
                Telerik.Web.UI.RadUpload wc = (Telerik.Web.UI.RadUpload)ctrl;
                string DBFieldName = wc.ID.Substring(3, wc.ID.Length - 3);
                ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
                HyperLink hpl = (HyperLink)cph.FindControl("hpl" + DBFieldName);
                wc.Attributes.Add("FileName", obj != null ? obj.ToString() : "");

                SetControlValue(hpl, obj, wc.TargetFolder);
                if (HttpContext.Current.Request["ViewMode"] == "1")
                {
                    CheckBox chkDelete = (CheckBox)cph.FindControl("chkDelete" + wc.ID.Replace("upl", ""));
                    chkDelete.Visible = false;
                    wc.Visible = false;
                }

                break;
            case "AKP.Web.Controls.ExRadUpload":
                AKP.Web.Controls.ExRadUpload upl = (AKP.Web.Controls.ExRadUpload)ctrl;
                string ExDBFieldName = upl.ID.Substring(3, upl.ID.Length - 3);
                ContentPlaceHolder Excph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
                HyperLink Exhpl = (HyperLink)Excph.FindControl("hpl" + ExDBFieldName);
                upl.Attributes.Add("FileName", obj != null ? obj.ToString() : "");
                SetControlValue(Exhpl, obj, upl.TargetFolder);

                break;
            case "AKP.Web.Controls.Lookup":
                AKP.Web.Controls.Lookup lkp = (AKP.Web.Controls.Lookup)ctrl;
                if (obj != null)
                {
                    lkp.Code = Convert.ToInt32(obj);
                    lkp.Title = GetListTitle(lkp.BaseID, Convert.ToInt32(obj));

                }
                break;
            case "AKP.Web.Controls.LookupTree":
                AKP.Web.Controls.LookupTree lkpTree = (AKP.Web.Controls.LookupTree)ctrl;
                if (obj != null)
                {
                    lkpTree.Code = Convert.ToInt32(obj);
                    lkpTree.Title = GetListTreeTitle(lkpTree.BaseID, Convert.ToInt32(obj));
                }
                break;


            default:
                throw new Exception("نوع کنترل غیر مجاز است.");
        }
    }
    #region Show Label and Set Control Value
    public void ShowLabel(string FieldName, Label lbl, object obj, ShamsiDateModes DateMode)
    {
        if (this.HasAccess("View", FieldName))
            if (obj != null)
            {
                DateTimeMethods dtm = new DateTimeMethods();
                if (obj.ToString().Trim() != "")
                    lbl.Text = dtm.GetPersianDate(Convert.ToDateTime(obj));
            }
    }
    public void ShowLabel(string FieldName, Label lbl, object obj, ControlTypes ctrlType, params string[] args)
    {
        if (this.HasAccess("View", FieldName))
            if (obj != null)
            {
                switch (ctrlType)
                {
                    case ControlTypes.ExTextBox:
                        break;
                    case ControlTypes.ComboBox:
                        break;
                    case ControlTypes.Lookup:
                        break;
                    case ControlTypes.LookupTree:
                        lbl.Text = Tools.GetListTreeTitle(args[0], Convert.ToInt32(obj));
                        break;
                    case ControlTypes.PersianDate:
                        break;
                    case ControlTypes.RadUpload:
                        break;
                    case ControlTypes.NumericTextBox:
                        break;
                    case ControlTypes.Label:
                        break;
                    case ControlTypes.CheckBox:
                        break;
                    case ControlTypes.RadioButton:
                        break;
                    case ControlTypes.RadEditor:
                        break;
                    case ControlTypes.HyperLink:
                        break;
                    default:
                        break;
                }
            }
    }
    public void ShowLabel(string FieldName, HyperLink hl, object obj)
    {

        if (this.HasAccess("View", FieldName))
            if (obj != null)
            {
                hl.Text = Tools.FormatString(obj.ToString());
                hl.NavigateUrl = obj.ToString();
            }
    }
    public void ShowLabel(string FieldName, Label lbl, object obj)
    {
        if (this.HasAccess("View", FieldName))
            if (obj != null)
            {
                if (obj.GetType() == typeof(bool))
                {
                    if (Convert.ToBoolean(obj))
                        lbl.Text = "بله";
                    else
                        lbl.Text = "خیر";
                }
                else
                {
                    //lbl.Text = Tools.FormatString(obj.ToString());
                    //HyperLink hplLabel = (HyperLink)lbl;
                    if (obj != null)
                    {
                        string Result = "";
                        string ShowAllTextAtr = lbl.Attributes["ShowAllText"];
                        bool ShowAllText = ShowAllTextAtr == "1";
                        if (ShowAllText)
                            Result = obj.ToString();
                        else
                            Result = ShowMoreText(obj.ToString(), lbl, 200, ShowAllText, FieldName, "متن کامل");

                        Result = Tools.FormatString(Result);
                        lbl.Text = Result;

                    }
                }
            }
    }
    public void ShowLabel(string FieldName, Label lbl, object obj, string BaseID)
    {
        if (this.HasAccess("View", FieldName))
            if (obj != null)
            {
                lbl.Text = Tools.GetListTitle(BaseID, Convert.ToInt32(obj));
            }
    }
    public void ShowLabel(string FieldName, HyperLink hl, object obj, string UploadPath)
    {
        if (this.HasAccess("View", FieldName))
            if (obj != null)
            {
                if (obj.ToString() != "")
                {
                    if (IsImageExtension(GetFileExtension(obj.ToString().ToUpper())))
                    {
                        hl.ImageUrl = "~/Imager.aspx?ImgFilePath=" + HttpUtility.UrlEncode(Tools.Encode(UploadPath + obj.ToString())) + "&StaticHW=150";
                    }
                    else
                    {
                        hl.ImageUrl = "~/Images/SampleFile.png";
                        hl.ToolTip = "نمایش فایل";
                    }
                    hl.NavigateUrl = UploadPath + obj.ToString();
                }

            }
    }
    public static void SetControlValue(Control ctrl, object obj, IBaseBOL CurObj)
    {
        #region Display Mandatory -- Moved EditForm.cs
        //Label lbl;
        //string ctrID;
        //string[] sv = new string[] { };

        //ctrID = ctrl.ID.Substring(3, ctrl.ID.Length - 3);
        //ContentPlaceHolder cphMandatory = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
        //lbl = cphMandatory.FindControl("lbl" + ctrID) as Label;

        //PropertyInfo piMandatory = CurObj.GetType().GetProperty(ctrID);
        //MemberInfo miMandatory = CurObj.GetType().GetMember(ctrID)[0];
        //ColumnAttribute attMandatory = (ColumnAttribute)System.Attribute.GetCustomAttribute(miMandatory, typeof(ColumnAttribute));

        //sv = SplitValue(attMandatory);
        //if (sv[2] == "FALSE" && lbl != null)
        //    lbl.Text = lbl.Text + " * ";

        #endregion Display Mandatory

        switch (ctrl.GetType().ToString())
        {
            case "System.Web.UI.WebControls.HiddenField":
                HiddenField h = (HiddenField)ctrl;
                h.Value = obj.ToString();
                break;
            case "System.Web.UI.WebControls.Label":
                Label l = (Label)ctrl;
                if (obj != null)
                    l.Text = obj.ToString();
                break;
            case "System.Web.UI.WebControls.RadioButtonList":
                RadioButtonList r = (RadioButtonList)ctrl;
                r.SelectedValue = obj.ToString();
                break;
            case "System.Web.UI.WebControls.DropDownList":
                DropDownList drpList = (DropDownList)ctrl;
                if ((obj.ToString() != string.Empty))
                    drpList.SelectedValue = obj.ToString();
                else
                    obj = DBNull.Value;
                break;
            case "System.Web.UI.WebControls.CheckBox":
                CheckBox chk = (CheckBox)ctrl;
                if (obj != DBNull.Value)
                    chk.Checked = Convert.ToBoolean(obj);
                break;
            case "AKP.Web.Controls.ExCheckBox":
                CheckBox Exchk = (CheckBox)ctrl;
                if (obj != DBNull.Value)
                    Exchk.Checked = Convert.ToBoolean(obj);
                break;

            case "System.Web.UI.WebControls.RadioButton":
                RadioButton rd = (RadioButton)ctrl;
                if (obj != DBNull.Value)
                    rd.Checked = Convert.ToBoolean(obj);
                break;
            case "AKP.Web.Controls.FarsiDate":
                AKP.Web.Controls.FarsiDate dte = (AKP.Web.Controls.FarsiDate)ctrl;
                if (obj != null)
                    dte.SelectedDateChristian = Convert.ToDateTime(obj);
                break;
            case "AKP.Web.Controls.Combo":
                AKP.Web.Controls.Combo cbo = (AKP.Web.Controls.Combo)ctrl;
                if (obj != null)
                    cbo.Value = Convert.ToInt32(obj);
                break;
            case "AKP.Web.Controls.ExTextBox":
                AKP.Web.Controls.ExTextBox ExTB = (AKP.Web.Controls.ExTextBox)ctrl;
                if (obj != null)
                {
                    if ((ExTB.DisplayMode == AKP.Web.Controls.Common.EnmDisplayMode.ViewMode))
                    {
                        ExTB.Text = new Tools().ShowMoreText(obj.ToString(), ExTB, ExTB.MoreLinkLength, !ExTB.HasMoreLink, HttpContext.Current.Request.QueryString["BaseID"] + "." + ExTB.ID.Remove(0, 3), ExTB.MoreLinkText);
                    }
                    else
                        ExTB.Text = obj.ToString();
                }

                break;

            case "AKP.Web.Controls.NumericTextBox":
                AKP.Web.Controls.NumericTextBox ntxtBox = (AKP.Web.Controls.NumericTextBox)ctrl;
                if (obj != null)
                    ntxtBox.Text = Convert.ToInt32(obj);
                break;


            case "Telerik.Web.UI.RadEditor":
                Telerik.Web.UI.RadEditor Editor = (Telerik.Web.UI.RadEditor)ctrl;
                if (obj != null)
                    Editor.Html = PersianTextCorrection(obj.ToString());
                break;
            case "AKP.Web.Controls.ExRadEditor":
                AKP.Web.Controls.ExRadEditor ExEditor = (AKP.Web.Controls.ExRadEditor)ctrl;
                if (obj != null)
                    ExEditor.Html = PersianTextCorrection(obj.ToString());
                break;

            case "System.Web.UI.WebControls.HyperLink":
                break;

            case "Telerik.Web.UI.RadUpload":
                Telerik.Web.UI.RadUpload wc = (Telerik.Web.UI.RadUpload)ctrl;
                string DBFieldName = wc.ID.Substring(3, wc.ID.Length - 3);
                ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
                HyperLink hpl = (HyperLink)cph.FindControl("hpl" + DBFieldName);
                wc.Attributes.Add("FileName", obj != null ? obj.ToString() : "");

                SetControlValue(hpl, obj, wc.TargetFolder);
                if (HttpContext.Current.Request["ViewMode"] == "1")
                {
                    CheckBox chkDelete = (CheckBox)cph.FindControl("chkDelete" + wc.ID.Replace("upl", ""));
                    chkDelete.Visible = false;
                    wc.Visible = false;
                }

                break;
            case "AKP.Web.Controls.ExRadUpload":
                AKP.Web.Controls.ExRadUpload upl = (AKP.Web.Controls.ExRadUpload)ctrl;
                string ExDBFieldName = upl.ID.Substring(3, upl.ID.Length - 3);
                ContentPlaceHolder Excph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
                HyperLink Exhpl = (HyperLink)Excph.FindControl("hpl" + ExDBFieldName);
                upl.Attributes.Add("FileName", obj != null ? obj.ToString() : "");
                //SetControlValue(Exhpl, obj, upl.TargetFolder);
                SetControlValue(Exhpl, obj, "");

                break;
            case "AKP.Web.Controls.Lookup":
                AKP.Web.Controls.Lookup lkp = (AKP.Web.Controls.Lookup)ctrl;
                if (obj != null)
                {
                    lkp.Code = Convert.ToInt32(obj);
                    lkp.Title = GetListTitle(lkp.BaseID, Convert.ToInt32(obj));

                }
                break;
            case "AKP.Web.Controls.LookupTree":
                AKP.Web.Controls.LookupTree lkpTree = (AKP.Web.Controls.LookupTree)ctrl;
                if (obj != null)
                {
                    lkpTree.Code = Convert.ToInt32(obj);
                    lkpTree.Title = GetListTreeTitle(lkpTree.BaseID, Convert.ToInt32(obj));
                }
                break;


            default:
                throw new Exception("نوع کنترل غیر مجاز است.");
        }
    }
    public static void SetControlValue(Control ctrl, object obj, string UploadPath)
    {

        HyperLink hl = (HyperLink)ctrl;
        if (obj != null)
        {
            object FullNameObj = UploadPath + obj;
            //object FullNameObj = obj;
            if (obj.ToString().IndexOf(".") < 0)
                hl.Visible = false;
            else
            {
                if (IsImageExtension(GetFileExtension(obj.ToString().ToUpper())))
                {
                    hl.ImageUrl = "~/Imager.aspx?ImgFilePath=" + HttpUtility.UrlEncode(Encode(FullNameObj.ToString())) + "&StaticHW=150";
                    hl.Attributes.Add("rel", "lightbox");
                    hl.ToolTip = "برای مشاهده اندازه واقعی روی عکس کلیک کنید";
                }
                else
                {
                    hl.ImageUrl = "~/Images/SampleFile.png";
                    hl.ToolTip = "نمایش فایل";
                }

                hl.NavigateUrl = FullNameObj.ToString();
            }
        }
    }
    #endregion

    public void ShowFullLabel(string FieldName, Label lbl, object obj)
    {
        //if (this.HasAccess("View", FieldName))
        if (obj != null)
        {
            if (obj.GetType() == typeof(bool))
            {
                if (Convert.ToBoolean(obj))
                    lbl.Text = "بله";
                else
                    lbl.Text = "خیر";
            }
            else
            {
                //lbl.Text = Tools.FormatString(obj.ToString());
                //HyperLink hplLabel = (HyperLink)lbl;
                if (obj != null)
                {
                    string Result = "";
                    string strText = obj.ToString();

                    Result = strText;
                    Result = Tools.FormatString(Result);
                    lbl.Text = Result;

                }
            }
        }
    }
    private string ShowMoreText(string text, WebControl lbl, int Length, bool ShowAllText, string FieldName, string MoreLinkText)
    {
        string Result = "";
        string strText = text;

        if (strText.Length > Length && !ShowAllText)
        {
            int BlankPos = strText.IndexOf(" ", Length);
            if (BlankPos == -1) if (strText.Length > 30) BlankPos = 30;

            string url = new HyperLink().ResolveClientUrl(string.Format("~/ShowLabel.aspx?FullFieldName={0}&DetailCode={1}", FieldName, HttpContext.Current.Request.Params["Code"]));
            url = "window.open('" + url + "','','width=640,height=420,menubar=no,status=no,titlebar=no,scrollbars,resizable=yes,top=200,left=150');return false; ";
            string moreLink = string.Format(string.Format("<a style=\"cursor:pointer;color:Blue;\" onclick=\"{0}\" target='_blank'>{1}</a>", url, MoreLinkText));
            Result = Tools.FormatString(strText.Substring(0, BlankPos)) + "..." + moreLink;
        }
        else
            Result = strText;

        return Result;
    }

    public static string FormatString(Object objstr)
    {
        if (objstr == null)
            return "";

        string str = objstr.ToString();
        str = str.Replace("\n", "<br />");
        return str;
    }
    public static void SetClientScript(Page p, string Key, string ScriptBody)
    {
        string ScriptStr;
        ScriptStr = "<script language='javascript'>";
        ScriptStr = ScriptStr + ScriptBody + "</script>";
        p.RegisterStartupScript(Key, ScriptStr);
    }

    private static ArrayList CreateList(string _pattern, string Result, int WordCount)
    {
        ArrayList ResultList = new ArrayList();
        Regex r = new Regex(_pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        Match m = r.Match(Result);
        string CurKeyword = "";
        while (m.Success)
        {
            CurKeyword = m.Groups[0].Captures[0].ToString();
            if (CurKeyword.Length > 2)
            {
                if (!ResultList.Contains(CurKeyword))
                {
                    if (WordCount > 1)
                        ResultList.Add(CurKeyword);
                    else
                    {
                        //if (!IsInGarbageWords(CurKeyword))
                        ResultList.Add(CurKeyword);
                    }
                }
            }
            m = m.NextMatch();
        }
        return ResultList;
    }
    protected int FindKeyNums(string Content, string Keyword)
    {
        int Count = 0;
        int IndexPos;
        IndexPos = Content.IndexOf(Keyword);
        while (IndexPos >= 0)
        {
            Count++;
            IndexPos = Content.IndexOf(Keyword, IndexPos + 1);
        }
        return Count;
    }
    internal void CheckEditButtnAccess(MasterPage mp, string BaseID)
    {
        AccessList = GetAccessList(BaseID);
        if (!HasAccess("Edit", BaseID))
        {
            try
            {
                ((ContentPlaceHolder)mp.FindControl("cphMain")).FindControl("hplEdit").Visible = false;
            }
            catch
            {
            }
        }
    }

    public static string GetRandID()
    {
        Random RandomNumber = new Random();
        double dblRandRowIndex = RandomNumber.NextDouble();
        string strRandNum = dblRandRowIndex.ToString();
        string GenID = strRandNum.Substring(2, strRandNum.Length - 2);
        return GenID;
    }

    public static object ShowDate(object obj)
    {
        string Result = "";
        try
        {

            if (obj != null)
            {
                DateTime CurDT = Convert.ToDateTime(obj);
                Result = ChangeEnc(CurDT.Hour + ":" + CurDT.Minute);
            }
        }
        catch
        {
        }
        return Result;
    }

    internal string GetModuleStyle(int? TopMargin, int? BottomMargin, int? LeftMargin, int? RightMargin)
    {
        string Result = "";
        if (TopMargin != null)
            Result += "margin-top:" + TopMargin + "px;";
        if (BottomMargin != null)
            Result += "margin-bottom:" + BottomMargin + "px;";
        if (LeftMargin != null)
            Result += "margin-left:" + LeftMargin + "px;";
        if (RightMargin != null)
            Result += "margin-right:" + RightMargin + "px;";
        return Result;
    }

    public static int GetLang()
    {
        try
        {
            int Result = 1;
            if (System.Web.HttpContext.Current.Request["Lang"] != null)
            {
                Result = Convert.ToInt32(System.Web.HttpContext.Current.Request["Lang"]);
                System.Web.HttpContext.Current.Session["Lang"] = Result.ToString();
            }
            else if (System.Web.HttpContext.Current.Session["Lang"] != null)
                Result = Convert.ToInt32(System.Web.HttpContext.Current.Session["Lang"]);

            if (Result != null)
                System.Web.HttpContext.Current.Session["Lang"] = Result.ToString();
            return Result;
        }
        catch
        {
            return 1;
        }
    }

    public static string FormatArabic(object obj )
    {
        string Result = "";
        if(obj == null)
            return "";
        Result = obj.ToString();
        int LangCode = Tools.GetLang();
        if (LangCode == 1)
        {
            Result = Result.Replace("ک", "ك");
        }
        return Result;
    }

    public static string GetMessage(System.Exception exe)
    {
        StringBuilder sb = new StringBuilder();
        do
        {
            StackTrace st = new StackTrace(exe, true);
            for (int i = 0; i < st.FrameCount; i++)
            {
                StackFrame sf = st.GetFrame(i);
                if (i == 0)
                    sb.AppendLine(string.Format("Exception: {0}", exe.Message));
                sb.AppendLine(string.Format("At File: {0} L:{1},C:{2}", sf.GetFileName(), sf.GetFileLineNumber(), sf.GetFileColumnNumber()));
                sb.AppendLine(string.Format("In Method: {0}", sf.GetMethod()));
            }
            exe = exe.InnerException;
            if (exe != null)
                sb.AppendLine("--------------------------------------------------------------------------------------");
            else
                sb.AppendLine("---------------------------BaseException-----------------------------------");
            sb.Append(Environment.NewLine);
        } while (exe != null);
        return sb.ToString();
    }

    internal static string Translate(int LangCode, string Word)
    {
        Dictionary<string, string> FaDic = new Dictionary<string, string>();
        Dictionary<string, string> ArDic = new Dictionary<string, string>();
        Dictionary<string, string> EnDic = new Dictionary<string, string>();

        FaDic.Add("Home", "صفحه اصلی");
        FaDic.Add("Contact Us", "تماس با ما");
        FaDic.Add("About Us", "درباره ما");
        FaDic.Add("News Titles", "عناوین اخبار");
        FaDic.Add("Most Viewed", "پربازدید ترین ها");
        FaDic.Add("Galleries", "تصاویر");
        FaDic.Add("Latest", "آخرین ها");
        FaDic.Add("Cases", "پرونده ها");
        FaDic.Add("Movies", "فیلم");
        FaDic.Add("Sounds", "صدا");

        FaDic.Add("First Name", "نام");
        FaDic.Add("Last Name", "نام خانوادگی");
        FaDic.Add("Tel", "تلفن");
        FaDic.Add("Job", "شغل");
        FaDic.Add("Degree", "تحصیلات");
        FaDic.Add("Email", "ایمیل");
        FaDic.Add("Address", "آدرس");
        FaDic.Add("Captcha", "کد امنیتی");
        FaDic.Add("Search", "جستجو");
        FaDic.Add("SendArticleDesc", @"\n1.خلاصه فشرده ای از مقاله ارسالی در صفحه جداگانه ضمیمه باشد؛
<br>2.طول مقاله ها از 20 صفحه تایپ شده تجاوز نکند؛
<br>3.ارائه متن اصلی مقاله ترجمه شده و فهرست کامل منابع م ماخذ مقاله های تالیف شده با مشخصات دقیق کتاب شناسی آنها؛
<br>4.سایت الوقت در حک و اصلاح و ویرایش مقالات پذیرفت شده آزاد خواهد بود؛");
        FaDic.Add("RelatedNews", "اخبار مرتبط");
        FaDic.Add("Comment", "نظر");
        FaDic.Add("BackToTop", "بازگشت به بالای صفحه");
        FaDic.Add("Comments", "نظرات");
        FaDic.Add("Search Results", "نتایج جستجو");
        FaDic.Add("No Result Found", "هیچ نتیجه ای پیدا نشد");

        FaDic.Add("Security Code is not Valid", "کد امنیتی معتبر نیست");
        FaDic.Add("Please Enter Name", "لطفا نام را وارد کنید");
        FaDic.Add("Please Enter Last Name", "لطفا نام خانوادگی را وارد کنید");
        FaDic.Add("Please Enter Email", "لطفا ایمیل را وارد کنید");
        FaDic.Add("Please enter Comment", "لطفا متن را وارد کنید");
        FaDic.Add("Information Successfully Submitted", "اطلاعات با موفقیت ارسال شد");

        FaDic.Add("Please Enter Article File", "لطفا فایل مقاله را وارد کنید");
        FaDic.Add("Article File Successfully Submitted.", "فایل با موفقیت ارسال شد.");
        FaDic.Add("Error Sending file", "خطا در ارسال فایل.");
        FaDic.Add("File Extenction is not Valid", "پسوند فایل وارد شده معتبر نیست");
        FaDic.Add("Error Sending Comment", "خطا در ارسال نظر");

        FaDic.Add("Your Comment Successfully Submitted.", "نظر شما دریافت شد");
        FaDic.Add("Unfortunately there was an error submitting your comment.", "متاسفانه در ثبت نظر شما خطایی رخ داده است.");
        FaDic.Add("Published", "انتشار یافته");
        FaDic.Add("Non Released", "غیر قابل انتشار");
        FaDic.Add("Search Results for", "نتایج جستجو برای ");

        FaDic.Add("Print Version", "نسخه چاپی");
        FaDic.Add("Send To Email", "ارسال به ایمیل");
        FaDic.Add("PDF Version", "نسخه PDF");
        FaDic.Add("Make Font Smaller", "کوچکتر کردن اندازه فونت");
        FaDic.Add("Make Font Greater", "بزرگتر کردن اندازه فونت");
        FaDic.Add("News Attachments", "ضمائم تصویری");
        FaDic.Add("More", "ادامه");
        FaDic.Add("Visitor", "بازدید");
        FaDic.Add("CopyrightContent", "تمامی حقوق برای سایت \"الوقت\" محفوظ است");

        FaDic.Add("News", "خبر");
        FaDic.Add("Articles", "مقاله");
        FaDic.Add("Notes", "یادداشت");
        FaDic.Add("Analyzes", "تحلیل");
        FaDic.Add("Interviews", "مصاحبه");
        FaDic.Add("Reports", "گزارش");

        FaDic.Add("All", "همه");



        ///////////////////////////////////////////////////////
        ArDic.Add("Home", "الصفحة الرئيسية");
        ArDic.Add("Contact Us", "إتصل بنا");
        ArDic.Add("About Us", "من نحن");
        ArDic.Add("News Titles", "عناوين الأخبار");
        ArDic.Add("Most Viewed", "الاکثر قراءة");
        ArDic.Add("Galleries", "الصـور");
        ArDic.Add("Latest", "آخر المستجدات");
        ArDic.Add("Cases", "الملفات");
        ArDic.Add("Movies", "افلام");
        ArDic.Add("Sounds", "الصوت");

        ArDic.Add("First Name", "الاسم");
        ArDic.Add("Last Name", "العائلة");
        ArDic.Add("Tel", "الهاتف");
        ArDic.Add("Job", "العمل");
        ArDic.Add("Degree", "الدراسة");
        ArDic.Add("Email", "البريد الالكتروني");
        ArDic.Add("Address", "العنوان");
        ArDic.Add("Captcha", "الرمز");
        ArDic.Add("Search", "إبحث");
        ArDic.Add("SendArticleDesc", @"1-يرجى وضع ملخص المقال في صفحة مستقلة
                                <br />2-حجم المقال لايتعدي الـ 20 صفحة
                                <br />3-وضع نص المقال وقائمة بفهرسة كامل للمصادر والمآخذ كتباته
                                <br />4-الموقع يتمتع بالصلاحية لإصلاح او تعديل المقال
                                <br />5-النصوص المرسلة لاتعاد الى اصحابها
                                ");
        ArDic.Add("RelatedNews", "الأخبار المتعلقة");
        ArDic.Add("Comment", "النص");
        ArDic.Add("BackToTop", "الرجوع الی اعلی الصفحه");
        ArDic.Add("Comments", "التعلیقات");
        ArDic.Add("Search Results", "نتائج البحث");
        ArDic.Add("No Result Found", "لم یعثر علی أی تطابق");

        ArDic.Add("Security Code is not Valid", "رمز الامان غیر صحیح");
        ArDic.Add("Please Enter Name", "الرجاء کتابة الاسم");
        ArDic.Add("Please Enter Last Name", "الرجاء کتابة إسم العائلة");
        ArDic.Add("Please Enter Email", "الرجاء کتابة البرید الإلکتروني");
        ArDic.Add("Please enter Comment", "الرجاء إدخال النص");
        ArDic.Add("Information Successfully Submitted", "تم إرسال المعلومات بنجاح");

        ArDic.Add("Please Enter Article File", "الرجاء درج ملف المقال");
        ArDic.Add("Article File Successfully Submitted.", "تم إرسال الملف بنجاح");
        ArDic.Add("Error Sending file", "حدوث خطأ حین إرسال الملف");
        ArDic.Add("File Extenction is not Valid", "إن صفة الملف الذي تم رفعه غیر صحیحة");
        ArDic.Add("Error Sending Comment", "حدوث خطأ حین إرسال الأراء");

        ArDic.Add("Your Comment Successfully Submitted.", "تمّ إستلام آراؤکم");
        ArDic.Add("Unfortunately there was an error submitting your comment.", "للأسف حدث خطأ حین إستلام آراؤکم");
        ArDic.Add("Published", "تم نشره");
        ArDic.Add("Non Released", "غیر صالح للنشر");
        ArDic.Add("Search Results for", "نتائج البحث ل");

        ArDic.Add("Print Version", "نسخة الطبع");
        ArDic.Add("Send To Email", "إرسال عبر البرید الإلکتروني");
        ArDic.Add("PDF Version", "نسخه PDF");
        ArDic.Add("Make Font Smaller", "تصغیر حجم الخط");
        ArDic.Add("Make Font Greater", "تکبیر حجم الخط");
        ArDic.Add("News Attachments", "ملحقات تصویریة");
        ArDic.Add("More", "المزید");
        ArDic.Add("Visitor", "زائر");
        ArDic.Add("CopyrightContent", "جميع الحقوق محفوظة لموقع \"الوقت\"");

        ArDic.Add("News", "خبر");
        ArDic.Add("Articles", "مقال");
        ArDic.Add("Notes", "آراء");
        ArDic.Add("Analyzes", "تحلیل");
        ArDic.Add("Interviews", "مقابلة");
        ArDic.Add("Reports", "تقریر");

        ArDic.Add("All", "کل التصنیفات");

        ///////////////////////////////////////////////////////////////////////////////

        EnDic.Add("Home", "Home");
        EnDic.Add("Contact Us", "Contact Us");
        EnDic.Add("About Us", "About Us");
        EnDic.Add("News Titles", "News Titles");
        EnDic.Add("Most Viewed", "Most Viewed");
        EnDic.Add("Galleries", "Galleries");
        EnDic.Add("Latest", "Latest");
        EnDic.Add("Cases", "In Focus");
        EnDic.Add("Movies", "Video");
        EnDic.Add("Sounds", "Voice");

        EnDic.Add("First Name", "First Name");
        EnDic.Add("Last Name", "Last Name");
        EnDic.Add("Tel", "Phone Number");
        EnDic.Add("Job", "Occupation");
        EnDic.Add("Degree", "Education");
        EnDic.Add("Email", "Email");
        EnDic.Add("Address", "Address");
        EnDic.Add("Captcha", "Enter Code");
        EnDic.Add("Search", "Search");
        EnDic.Add("SendArticleDesc", @"1. The abstract of your manuscript should be sent in a separate file.
                                      <br />2. A manuscript of not more than twenty pages is required.
                                      <br />3. Original texts and references of your manuscript in full detail are required.
                                      <br />4. Any editing in your manuscript by Alwaqt is allowed.");

        EnDic.Add("Comment", "Comment");
        EnDic.Add("BackToTop", "Back To Top");
        EnDic.Add("Comments", "Comments");
        EnDic.Add("Search Results", "Search Results");
        EnDic.Add("No Result Found", "No Result Found");

        EnDic.Add("Security Code is not Valid", "Security Code is not Valid");
        EnDic.Add("Please Enter Name", "Please Enter Name");
        EnDic.Add("Please Enter Last Name", "Please Enter Last Name");
        EnDic.Add("Please Enter Email", "Please Enter Email");
        EnDic.Add("Please enter Comment", "Please enter Comment");
        EnDic.Add("Information Successfully Submitted", "Information Successfully Submitted");

        EnDic.Add("Please Enter Article File", "Please Enter Article File");
        EnDic.Add("Article File Successfully Submitted.", "Article File Successfully Submitted.");
        EnDic.Add("Error Sending file", "Error Sending file.");
        EnDic.Add("File Extenction is not Valid", "File Extenction is not Valid");
        EnDic.Add("Error Sending Comment", "Error Sending Comment");

        EnDic.Add("Your Comment Successfully Submitted.", "Your Comment Successfully Submitted");
        EnDic.Add("Unfortunately there was an error submitting your comment.", "متاسفانه در ثبت نظر شما خطایی رخ داده است.");
        EnDic.Add("Published", "Published");
        EnDic.Add("Non Released", "Non Released");
        EnDic.Add("Search Results for", "Search Results for");

        EnDic.Add("Print Version", "Print Version");
        EnDic.Add("Send To Email", "Send To Email");
        EnDic.Add("PDF Version", "PDF Version");
        EnDic.Add("Make Font Smaller", "Make Font Smaller");
        EnDic.Add("Make Font Greater", "Make Font Greater");
        EnDic.Add("News Attachments", "News Attachments");
        EnDic.Add("More", "more");
        EnDic.Add("Visitor", "Visitor");
        EnDic.Add("CopyrightContent", "Copyright Alwaght");

        EnDic.Add("News", "News");
        EnDic.Add("Articles", "Articles");
        EnDic.Add("Notes", "Notes");
        EnDic.Add("Analyzes", "Analyzes");
        EnDic.Add("Interviews", "Interviews");
        EnDic.Add("Reports", "Reports");

        EnDic.Add("All", "All");

        ///////////////////////////////////////////////////////

        switch (LangCode)
        {
            case 1://Arabic
                if (ArDic.ContainsKey(Word))
                {
                    return ArDic[Word];
                }
                break;
            case 2://Farsi
                if (FaDic.ContainsKey(Word))
                {
                    return FaDic[Word];
                }
                break;
            case 3://English
                if (EnDic.ContainsKey(Word))
                {
                    return EnDic[Word];
                }
                break;

            default:
                break;
        }
        return "";
    }
}

public class op_result
{
    private string _result;
    public string result
    {
        set
        {
            _result = value;
        }
        get
        {
            return _result;
        }
    }

    private string _success;
    public string success
    {
        set
        {
            _success = value;
        }
        get
        {
            return _success;
        }
    }
}

public class RandomProportional : Random
{
    // Sample generates a distribution proportional to the value 
    // of the random numbers, in the range [ 0.0, 1.0 ).
    protected override double Sample()
    {
        return Math.Sqrt(base.Sample());
    }
}

public class SearchFilter
{
    private string _columnName;
    private SqlOperators _operator = SqlOperators.Like;
    private string _value;
    public Operands CurOperand = Operands.AND;
    public SqlOperators Operator
    {
        get
        {
            return _operator;
        }
        set
        {
            _operator = value;
        }
    }
    public string ColumnName
    {
        get
        {
            return _columnName;
        }
        set
        {
            _columnName = value;
        }
    }
    public string Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }

    public SearchFilter(string Col, SqlOperators Op, string Val)
    {
        _columnName = Col;
        _operator = Op;
        _value = Val;
    }
}
public class DataCell
{
    public Color DataBGCellCol;
    public Color HeaderBGCellCol;
    public Directions Direction = Directions.None;
    public CellTypes CellType = CellTypes.grdTextBox;
    public bool IsKey = false;
    public bool IsDate = false;
    public bool IsListTitle = false;
    public string CaptionName = "";
    public int MaxLength;
    public AlignTypes Align = AlignTypes.None;
    public string FieldName;
    public Unit Width;
    public DisplayModes DisplayMode = DisplayModes.Visible;
    public string ExtraAttribute;
    public bool IsImage = false;

    public DataCell(string _FieldName, string _CaptionName, AlignTypes _Align, Unit _Width)
    {
        FieldName = _FieldName;
        CaptionName = _CaptionName;
        Align = _Align;
        Width = _Width;
    }
    public DataCell()
    {
    }

}
public class SearchFilterCollection : CollectionBase
{
    public SearchFilter this[int index]
    {
        get
        {
            if (index <= List.Count - 1)
                return (SearchFilter)List[index];
            else
                return null;
        }
        set
        {
            if (index <= List.Count - 1)
                List[index] = value;
        }
    }
    public void Add(SearchFilter newCell)
    {
        List.Add(newCell);
    }
    public void Remove(SearchFilter newCell)
    {
        List.Remove(newCell);
    }
    public void Contains(SearchFilter newCell)
    {
        List.Contains(newCell);
    }
}
public class CellCollection : CollectionBase
{
    public DataCell this[int index]
    {
        get
        {
            if (index <= List.Count - 1)
                return (DataCell)List[index];
            else
                return null;
        }
        set
        {
            if (index <= List.Count - 1)
                List[index] = value;
        }
    }
    public void Add(DataCell newCell)
    {
        List.Add(newCell);
    }
    public void Remove(DataCell newCell)
    {
        List.Remove(newCell);
    }
    public void Contains(DataCell newCell)
    {
        List.Contains(newCell);
    }
}

public enum Operands
{
    AND,
    OR
}
public enum SqlOperators
{
    Equal,
    Like,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    NotEqual,
    DontHave,
    Between,
    StartsWith,
    EndWith
}
public enum CellTypes
{
    grdCheckBox,
    grdOptionBox,
    grdImageBox,
    grdTextBox,
    grdLabelBox,
    grdComboBox,
    grdListBox,
    grdDateText
}
public enum DisplayModes
{
    Visible,
    Hidden,
    HiddenDefault
}
public enum Directions
{
    LeftToRight,
    RightToLeft,
    None
}
public enum AlignTypes
{
    Left,
    Right,
    Center,
    Justify,
    None
}
public enum ListRoles
{
    Browse,
    List
}
public enum AccessNameTypes
{
    News,
    Edit,
    Delete,
    View,
    Export
}
public enum ShamsiDateModes
{
    Simple,
    Long,
    FullDesc
}

public enum ControlTypes
{
    ExTextBox,
    ComboBox,
    Lookup,
    LookupTree,
    PersianDate,
    RadUpload,
    NumericTextBox,
    Label,
    CheckBox,
    RadioButton,
    RadEditor,
    HyperLink
}

public enum MessageTypes
{
    Info,
    Error
}

public enum ServiceTypes
{
    SMS,
    Email,
    Other
}

