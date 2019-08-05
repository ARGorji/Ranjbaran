using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Data.Linq.Mapping;

/// <summary>
/// Summary description for EditForm
/// </summary>
public class EditForm<T> : System.Web.UI.Page
where T : class
{
    protected IBaseBOL<T> BOLClass;
    protected string BaseID;
    public EditForm()
    {

        string t = typeof(T).ToString();
        BaseID = t.Substring(t.LastIndexOf(".") + 1);
        //this.Init += new EventHandler(EditForm_Init);
    }

    protected override void  OnInit(EventArgs e)
    {
        Tools tools = new Tools();
        tools.AccessList = tools.GetAccessList(this.BaseID);
        if (!tools.HasAccess("Edit", this.BaseID))
            Response.Redirect("~/Admin/UserLogin.aspx");


        #region Display Mandatory
        Label lbl;
        string ctrID, Property, FullPropName;
        string[] sv = new string[] { };
        ContentPlaceHolder cphMandatory = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
        PropertyInfo pi, piMandatory;

        Type t;
        if (!BaseID.StartsWith("HC"))
            t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);
        else
            t = System.Web.Compilation.BuildManager.GetType("BOLHardCode", true);
        IBaseBOL CurObj = (IBaseBOL)Activator.CreateInstance(t);
        MemberInfo miMandatory;
        ColumnAttribute attMandatory;
        Type type = typeof(T).GetType();
        //List<WebControl> listControls = new Tools().GetControls();
        //foreach (WebControl wc in listControls)
        //{
        //    bool IsJASControl = wc.Attributes["jas"] != null;
        //    if (!IsJASControl)
        //        continue;
        //    Property = wc.ID.Substring(3, wc.ID.Length - 3);
        //    ctrID = wc.ID.Substring(3, wc.ID.Length - 3);
        //    pi = type.GetProperty(Property);
        //    lbl = cphMandatory.FindControl("lbl" + ctrID) as Label;

        //    FullPropName = BaseID + "." + Property;
        //    piMandatory = CurObj.GetType().GetProperty(ctrID);
        //    miMandatory = CurObj.GetType().GetMember(ctrID)[0];
        //    attMandatory = (ColumnAttribute)System.Attribute.GetCustomAttribute(miMandatory, typeof(ColumnAttribute));
        //    sv = Tools.SplitValue(attMandatory);
        //    if (sv[2] == "FALSE" && lbl != null)
        //        lbl.Text = "<font style='color:red' title=\"اجباری\"> * </font> " + lbl.Text;
        //}

        #endregion Display Mandatory
        base.OnInit(e);

    }
    void EditForm_Init(object sender, EventArgs e)
    {
        Tools tools = new Tools();
        tools.AccessList = tools.GetAccessList(this.BaseID);
        if (!tools.HasAccess("Edit", this.BaseID))
            Response.Redirect("~/Default.aspx");


        #region Display Mandatory
        Label lbl;
        string ctrID, Property, FullPropName;
        string[] sv = new string[] { };
        ContentPlaceHolder cphMandatory = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
        PropertyInfo pi, piMandatory;

        Type t;
        if (!BaseID.StartsWith("HC"))
            t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);
        else
            t = System.Web.Compilation.BuildManager.GetType("BOLHardCode", true);
        IBaseBOL CurObj = (IBaseBOL)Activator.CreateInstance(t);
        MemberInfo miMandatory;
        ColumnAttribute attMandatory;
        Type type = typeof(T).GetType();
        List<WebControl> listControls = new Tools().GetControls();
        foreach (WebControl wc in listControls)
        {
            Property = wc.ID.Substring(3, wc.ID.Length - 3);
            ctrID = wc.ID.Substring(3, wc.ID.Length - 3);
            pi = type.GetProperty(Property);
            lbl = cphMandatory.FindControl("lbl" + ctrID) as Label;

            FullPropName = BaseID + "." + Property;
            piMandatory = CurObj.GetType().GetProperty(ctrID);
            miMandatory = CurObj.GetType().GetMember(ctrID)[0];
            attMandatory = (ColumnAttribute)System.Attribute.GetCustomAttribute(miMandatory, typeof(ColumnAttribute));
            sv = Tools.SplitValue(attMandatory);
            if (sv[2] == "FALSE" && lbl != null)
                lbl.Text = "<font style='color:red' title=\"اجباری\"> * </font> " + lbl.Text;
        }

        #endregion Display Mandatory
       // base.OnInit(e);
    }
    public int SelectedTabIndex = 0;


    #region Properties
    protected string Keyword
    {
        get
        {
            try
            {
                if (ViewState["_Keyword"] == null)
                {
                    ViewState["_Keyword"] = "";
                    return "";
                }
                else
                    return (string)ViewState["_Keyword"].ToString();
            }
            catch
            {
                return "";
            }
        }
        set
        {
            ViewState["_Keyword"] = value;
        }
    }
    protected string ConditionCode
    {
        get
        {
            try
            {
                if (ViewState["_ConditionCode"] == null)
                {
                    ViewState["_ConditionCode"] = "";
                    return "";
                }

                else
                    return (string)ViewState["_ConditionCode"].ToString();
            }
            catch
            {
                return "";
            }

        }
        set
        {
            ViewState["_ConditionCode"] = value;
        }
    }
    protected string FilterColumns
    {
        get
        {
            try
            {
                if (ViewState["_FilterColumns"] == null)
                {
                    ViewState["_FilterColumns"] = "";
                    return "";
                }

                else
                    return (string)ViewState["_FilterColumns"].ToString();
            }
            catch
            {
                return "";
            }

        }
        set
        {
            ViewState["_FilterColumns"] = value;
        }
    }


    protected int? Code
    {
        get
        {
            if (ViewState["_Code"] == null)
            {
                try
                {
                    Keyword = Request["Keyword"];
                    ConditionCode = Request["ConditionCode"];
                    FilterColumns = Request["FilterColumns"];
                    int intCode = Int32.Parse(Request["Code"]);
                    ViewState["_Code"] = intCode;
                    return intCode;
                }
                catch
                {
                    return null;
                }
            }
            else
                return Int32.Parse(ViewState["_Code"].ToString());
        }
        set
        {
            ViewState["_Code"] = value;
        }
    }

    protected bool NewMode
    {
        get
        {
            if (ViewState["_NewMode"] == null)
            {
                if (Code == null)
                    ViewState["_NewMode"] = true;
                else
                    ViewState["_NewMode"] = false;
            }

            return (bool)ViewState["_NewMode"];
        }

        set
        {
            ViewState["_NewMode"] = value;
        }
    }
    #endregion

    //protected void EditForm_Page_Load(object sender, EventArgs e)
    //{
    //    int aa = 1;
    //}
    protected virtual object LoadData(int DetailCode)
    {
        int HCLevelCode = 0;
        int DBHCLevelCode = 0;
        if (Session["HCLevelCode"] != null)
            HCLevelCode = Convert.ToInt32(Session["HCLevelCode"]);

        Tools tools = new Tools();
        tools.AccessList = tools.GetAccessList(this.BaseID);
        var ObjBaseID = BOLClass.GetDetails(DetailCode);
        if (ObjBaseID != null)
        {

            #region Full Load Data
            #region Automatic Load Data
            Type t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);
            IBaseBOL CurObj = (IBaseBOL)Activator.CreateInstance(t);

            List<WebControl> listControls = new Tools().GetControls();
            foreach (WebControl wc in listControls)
            {
                string Property = wc.ID.Substring(3, wc.ID.Length - 3);
                PropertyInfo pi = ObjBaseID.GetType().GetProperty(Property);
                string FullPropName = BaseID + "." + Property;
                if (Property == "HCLevelCode")
                    DBHCLevelCode = Convert.ToInt32(pi.GetValue(ObjBaseID, new object[] { }));
                tools.ShowControl(FullPropName, wc, pi.GetValue(ObjBaseID, new object[] { }), CurObj);
            }
            #endregion
            #endregion

        }
        if (HCLevelCode < DBHCLevelCode)
            Response.Redirect("~/Main/?BaseID=" + BaseID);

        return ObjBaseID;
    }
    protected int SaveControls(string AfterSaveUrl)
    {
        object ReturnValue = -1;
        try
        {
            Type t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);
            IBaseBOL CurObj = (IBaseBOL)Activator.CreateInstance(t);

            #region Full Save Data
            if (!NewMode)
            {
                //CurObj.Code = Convert.ToInt32(Code);vvvv
                PropertyInfo CodeProperty = CurObj.GetType().GetProperty("Code");
                CodeProperty.SetValue(CurObj, Convert.ToInt32(Code), new object[] { });
            }
            #region Automatica Save Data
            List<Dictionary<Control, object>> ObjectList = new List<Dictionary<Control, object>>();
            List<WebControl> listControls = new Tools().GetControls();
            foreach (WebControl wc in listControls)
                Tools.AddToDic(wc, "BOL" + BaseID, ObjectList);
            #endregion
            #region Show Messages and save
            IList messages = Tools.TryGet(ObjectList, CurObj);
            if (messages.Count == 0)
            {
                //messages = CurObj.CheckBusinessRules();
                MethodInfo BusinessRulesMethod = CurObj.GetType().GetMethod("CheckBusinessRules");
                BusinessRulesMethod.Invoke(CurObj, new object[] { });

                if (messages.Count == 0)
                {
                    //CurObj.ObjectList = listControls;vvvvv
                    FieldInfo ObjectListField = CurObj.GetType().GetField("ObjectList");
                    ObjectListField.SetValue(CurObj, listControls);

                    //CurObj.SaveChanges(NewMode);vvvvvv
                    MethodInfo SaveChangesMethod = CurObj.GetType().GetMethod("SaveChanges");
                    ReturnValue = SaveChangesMethod.Invoke(CurObj, new object[] { NewMode });
                }
            }
            if (messages.Count == 0)
            {
                if (NewMode)
                    messages.Add(Messages.ShowMessage(MessagesEnum.PrimaryInfomationSavedSuccessfuly));
                else
                    messages.Add(Messages.ShowMessage(MessagesEnum.SavedSuccessfuly));
            }

            ShowResultMessages(messages, AfterSaveUrl);
            ObjectList.Clear();
            #endregion
            #endregion
        }
        catch (Exception exp)
        {
            //TODO: Handle
            //if (exp.Message == "NotValidFileExtenstion")
            throw (exp);
        }
        return (int)ReturnValue;

    }
    private void ShowResultMessages(IList messages, string AfterSaveUrl)
    {
        AKP.Web.Controls.MsgBox msgBox = new AKP.Web.Controls.MsgBox();
        ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
        msgBox = (AKP.Web.Controls.MsgBox)(cph.FindControl("msgBox"));
        msgBox.Text = "";
        if (messages.Count == 0)
        {
            if (!NewMode)
            {
                //if (AfterSaveUrl != "")
                //    Response.Redirect(AfterSaveUrl, false);
            }

        }
        else
            foreach (var msg in messages)
                msgBox.Text = msgBox.Text + "<li>" + msg + "</li>";
    }

    protected void GoToList(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/Main/Default.aspx?BaseID=" + BaseID + "&Keyword=" + Keyword + "&ConditionCode=" + ConditionCode + "&FilterColumns=" + FilterColumns, false);
    }
}
