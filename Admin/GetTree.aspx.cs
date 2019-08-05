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

public partial class GetTree : System.Web.UI.Page
{
    public string FFN;
    public string FFC;
    protected void Page_Load(object sender, EventArgs e)
    {
        FFN = Request["FFN"];
        FFC = Request["FFC"];
        string BaseID = Request["BaseID"];
        IBaseBOLTree BOLClass;
        BOLClass = Tools.GetBOLClassTree(BaseID);
        DataTable dt = BOLClass.GetDataSource(null, "Code", 10000, 1);
        DataView dv = dt.DefaultView;
        if(BaseID == "UserNewsArchiveGroups")
            dv.RowFilter = " UserCode =" + Convert.ToInt32(Session["UserCode"]);

        RadTree1.DataTextField = "Name";
        RadTree1.DataFieldID = "Code";
        RadTree1.DataFieldParentID = "MasterCode";
        RadTree1.DataValueField = "Code";

        RadTree1.DataSource = dv.Table;
        RadTree1.DataBind();

    }
}
