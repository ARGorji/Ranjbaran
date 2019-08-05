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
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.Data.Linq.Mapping;
using AKP.Web.Controls.Common;


/// <summary>
/// Summary description for EditFormDetail
/// </summary>
public class EditFormDetail<T> : System.Web.UI.Page
    where T : class
{

    protected string BaseID;
    protected string MasterFieldName;
    protected IBaseBOL<T> BOLClass;
    //List<Dictionary<Control, object>> ObjectList;
    //List<WebControl> listControls = new List<WebControl>();
    //string ControlList = "";

    public EditFormDetail()
    {
        string t = typeof(T).ToString();
        BaseID = t.Substring(t.LastIndexOf(".") + 1);
        this.Init += new EventHandler(EditFormDetail_Init);
    }

    void EditFormDetail_Init(object sender, EventArgs e)
    {
        #region Display Mandatory
        Label lbl;
        string ctrID, Property, FullPropName;
        string[] sv = new string[] { };
        ContentPlaceHolder cphMandatory = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
        PropertyInfo pi, piMandatory;

        Type t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);
        int[] MasterCodes = { -1 };
        object[] oArr = { MasterCodes };
        IBaseBOL CurObj = (IBaseBOL)Activator.CreateInstance(t, oArr);
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
    }

    #region Properties
    public int? Code
    {
        get
        {
            if (ViewState["_Code"] == null)
            {
                int intCode;
                if (int.TryParse(Request["Code"], out intCode))
                {
                    ViewState["_Code"] = intCode;
                    return intCode;
                }
                return null;
            }
            return int.Parse(ViewState["_Code"].ToString());
        }
        set
        {
            ViewState["_Code"] = value;
        }
    }
    public bool NewMode
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
    public int? MasterCode
    {
        get
        {
            if (ViewState["_MasterCode"] == null)
            {
                try
                {
                    int intMasterCode = Int32.Parse(Request["MasterCode"]);
                    ViewState["_MasterCode"] = intMasterCode;
                    return intMasterCode;
                }
                catch
                {
                    return null;
                }
            }
            else
                return Int32.Parse(ViewState["_MasterCode"].ToString());
        }
        set
        {
            ViewState["_MasterCode"] = value;
        }
    }
    #endregion

    protected virtual void LoadData(int DetailCode)
    {
        Tools tools = new Tools();
        tools.AccessList = tools.GetAccessList(this.BaseID);

        if (!tools.HasAccess("Edit", BaseID) || Request["ViewMode"] == "1")
        {
            ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
            ImageButton SaveBtn = (ImageButton)cph.FindControl("imgBtnSave");
            SaveBtn.Visible = false;

        }
        var ObjBaseID = BOLClass.GetDetails(DetailCode);
        if (ObjBaseID != null)
        {
            #region Full Load Data
            #region Automatic Load Data
            Type t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);
            IBaseBOL CurObj = (IBaseBOL)Activator.CreateInstance(t, new object[] { (int)MasterCode });
            List<WebControl> listControls = new Tools().GetControls();
            foreach (WebControl wc in listControls)
            {
                string Property = wc.ID.Substring(3, wc.ID.Length - 3);
                PropertyInfo pi = ObjBaseID.GetType().GetProperty(Property);
                string FullPropName = BaseID + "." + Property;
                #region Cast controls to label if view mode
                //if (!string.IsNullOrEmpty(wc.Attributes["jas"]) && wc.Attributes["jas"] == "1")
                if (Request["ViewMode"] == "1")
                {
                    if (wc is ICustomControlsBase)
                    {
                        ((ICustomControlsBase)wc).DisplayMode = EnmDisplayMode.ViewMode;
                    }
                }
                //else
                //    wc.Enabled = false;

                #endregion
                tools.ShowControl(FullPropName, wc, pi.GetValue(ObjBaseID, new object[] { }), CurObj);

            }
            #endregion
            #endregion

        }
    }
    protected int SaveControls(string AfterSaveUrl)
    {
        object ReturnValue = -1;

        try
        {
            //BOLPersonAcademicDegrees CurObj = new BOLPersonAcademicDegrees((int)MasterCode);

            Type t = System.Web.Compilation.BuildManager.GetType("BOL" + BaseID, true);
            IBaseBOL CurObj = (IBaseBOL)Activator.CreateInstance(t, new object[] { (int)MasterCode });

            #region Full Save Data
            if (!NewMode)
            {
                //CurObj.Code = Convert.ToInt32(Code);vvv
                PropertyInfo CodeProperty = CurObj.GetType().GetProperty("Code");
                CodeProperty.SetValue(CurObj, Convert.ToInt32(Code), new object[] { });
            }
            FieldInfo MasterField = CurObj.GetType().GetField("MasterCode");
            MasterField.SetValue(CurObj, (int)MasterCode);

            //listControls = new Tools().GetControls();
            //foreach (WebControl wc in listControls)
            //    Tools.AddToDic(wc, BOLClass.ToString(), ObjectList);
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
            ShowResultMessages(messages);
            ObjectList.Clear();
            #endregion
            #endregion

        }
        catch (Exception cexp)
        {
            //TODO: Handle
            //if (exp.Message == "NotValidFileExtenstion")
            //    msgBox.Text = Messages.ShowMessage(MessagesEnum.ErrorNotValidFileName);
        }
        return (int)ReturnValue;

    }
    private void ShowResultMessages(IList messages)
    {
        if (messages.Count == 0)
        {
            Tools.CloseWin(this.Page , this.Master, BaseID, (string)ViewState["InstanceName"]);
        }
        else
        {
            AKP.Web.Controls.MsgBox msgBox = new AKP.Web.Controls.MsgBox();
            ContentPlaceHolder cph = ((ContentPlaceHolder)((Page)(HttpContext.Current.Handler)).Master.FindControl("cphMain"));
            msgBox = (AKP.Web.Controls.MsgBox)(cph.FindControl("msgBox"));
            msgBox.Text = "";
            foreach (var msg in messages)
                msgBox.Text = msgBox.Text + "<li>" + msg + "</li>";

        }
    }

}
