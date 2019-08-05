using System;
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

public partial class Main_Default : System.Web.UI.Page 
{
    public string BaseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["BaseID"] != null)
            BaseID = Request["BaseID"];

        string Keyword = Request["Keyword"];
        string ConditionCode = Request["ConditionCode"];
        string FilterColumns = Request["FilterColumns"];

        string ScriptStr = "";
        if (BaseID != null)
        {
            if(FilterColumns == "")
                ScriptStr = "<script type=\"text/javascript\" language=\"javascript\">BrowseObj1.ViewEdit='Edit';BrowseObj1.CreateBrowse('" + BaseID + "','' ,'' ,'' ,'' ,'Edit', 745)</script>";
            else
                ScriptStr = string.Format("<script type=\"text/javascript\" language=\"javascript\">BrowseObj1.ViewEdit='Edit';BrowseObj1.CreateBrowse('{0}', null, '{1}', '{2}', '{3}', 'Edit', 745)</script>", BaseID, FilterColumns, Keyword, ConditionCode);

            this.RegisterStartupScript("RunBrowse", ScriptStr);
        }


    }
}
