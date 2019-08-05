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

public partial class HardCode_EditHardCode : System.Web.UI.Page
{
    IBaseBOL<DataTable> BOLClass;

    protected string BaseID
    {
        get
        {
            object o = this.ViewState["_BaseID"];
            if (o == null)
            {
                return Request["BaseID"];
            }
            else
                return (string)o;
        }
        set
        {
            this.ViewState["_BaseID"] = value;
        }
    }

    public int Code
    {
        get
        {
            object o = this.ViewState["_Code"];
            if (o == null)
            {
                string strCode = Request["Code"];
                if (strCode == "")
                    return -1;
                else
                    return Convert.ToInt32(strCode);
            }
            else
                return (int)o;
        }

        set
        {
            this.ViewState["_Code"] = value;
        }
    }
    public bool NewMode
    {
        get
        {
            object o = this.ViewState["_NewMode"];
            if (o == null)
            {
                string CodeList = Request["Code"];
                if (CodeList == "")
                    return true;
                else
                    return false;
            }
            else
                return (bool)o;
        }

        set
        {
            this.ViewState["_NewMode"] = value;
        }
    }
    protected bool ValidateInputs()
    {
        return true;
    }
    protected void LoadData(string TableName, int DetailCode)
    {
        BOLClass.QueryObjName = TableName;
        DataTable dt = BOLClass.GetDetails(DetailCode);
        if (dt != null)
        {
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtDescription.Text = dt.Rows[0]["Description"].ToString();
            txtDescArshad.Content = dt.Rows[0]["DescArshad"].ToString();
            txtDescDoc.Content = dt.Rows[0]["DescDoc"].ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        BOLClass = new BOLHardCode();

        Label MasterPageTitle = (Label)Master.FindControl("lblTitle");
        //MasterPageTitle.Text = BOLClass.PageLable;

        if (Code == -1)
            return;
        if (!Page.IsPostBack)
        {
            if (NewMode)
            {
            }
            else
            {
                LoadData(BaseID, Code);
            }
        }
    }
    protected void DoSave(object sender, ImageClickEventArgs e)
    {
        if (!ValidateInputs())
            return;

        BOLHardCode CurObj = new BOLHardCode();
        CurObj.TableOrViewName = BaseID;
        if (!NewMode) CurObj.Code = Convert.ToInt32(Code);
        CurObj.Name = txtName.Text;
        CurObj.Description = txtDescription.Text;
        CurObj.DescArshad = txtDescArshad.Content;
        CurObj.DescDoc = txtDescDoc.Content;
        CurObj.SaveChanges(NewMode);

        GoToList(null, null);
    }
    protected void GoToList(object sender, EventArgs e)
    {
        Response.Redirect("../Main/Default.aspx?BaseID=" + BaseID);
    }
}
