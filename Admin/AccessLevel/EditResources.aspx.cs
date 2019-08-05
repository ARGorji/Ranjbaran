using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Ranjbaran.Old_App_Code.DAL;
using System.Web.SessionState;


public partial class Resources_EditResources : EditForm<Resources>
{
    private string BaseID = "Resources";
    IBaseBOL<Resources> BOLClass;
    #region Properties
    public int? Code
    {
        get
        {
            if (ViewState["_Code"] == null)
            {
                try
                {
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
    #endregion
    protected bool ValidateInputs()
    {
        return true;
    }
    protected void LoadData(int DetailCode)
    {
        Resources ObjBaseID = BOLClass.GetDetails(DetailCode);
        Tools tools = new Tools();
        if (ObjBaseID != null)
        {
            tools.ShowControl("Resources.Name", txtName, ObjBaseID.Name, BOLClass);
            tools.ShowControl("Resources.EngName", txtEngName, ObjBaseID.EngName, BOLClass);
            tools.ShowControl("Resources.TypeCode", lkpTypeCode, ObjBaseID.HCResourceTypeCode, BOLClass);
            tools.ShowControl("Resources.MasterCode", lkpMasterCode, ObjBaseID.MasterCode, BOLClass);
            tools.ShowControl("Resources.EditPath", txtEditPath, ObjBaseID.EditPath, BOLClass);
            tools.ShowControl("Resources.BaseID", txtBaseID, ObjBaseID.BaseID, BOLClass);
            tools.ShowControl("Resources.BasicAccessType", txtBasicAccessType, ObjBaseID.BasicAccessType, BOLClass);
            tools.ShowControl("Resources.ResName", txtResName, ObjBaseID.ResName, BOLClass);

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        BOLClass = new BOLResources();

        hplSysName.Text = BOLClass.PageLable;
hplSysName.NavigateUrl = "~/" + BaseID;


        if (Code == null)
            if (!NewMode)
                return;
        if (!Page.IsPostBack)
        {

            if (!NewMode)
            {
                LoadData((int)Code);
            }
        }


    }
    protected void DoSave(object sender, ImageClickEventArgs e)
    {
        if (!ValidateInputs())
            return;

        BOLResources CurObj = new BOLResources();
        if (!NewMode) CurObj.Code = Convert.ToInt32(Code);
        CurObj.Name = txtName.Text;
        CurObj.EngName = txtEngName.Text;
        CurObj.HCResourceTypeCode = (int)lkpTypeCode.Code;
        CurObj.MasterCode = (int)lkpMasterCode.Code;
        CurObj.EditPath = txtEditPath.Text;
        CurObj.BaseID = txtBaseID.Text;
        CurObj.BasicAccessType = Convert.ToInt32(txtBasicAccessType.Text);
        CurObj.ResName = txtResName.Text;



        CurObj.SaveChanges(NewMode);

        GoToList(null, null);
    }

}
