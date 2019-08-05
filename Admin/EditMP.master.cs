using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class UI_EditMP : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/scripts/main.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);


        BOLResources BOL = new BOLResources();
        RadMenu1.DataTextField = "Name";
        RadMenu1.DataFieldID = "Code";
        RadMenu1.DataFieldParentID = "MasterCode";
        //RadMenu1.DataNavigateUrlField = "BaseID";
        RadMenu1.DataValueField = "BaseID";

        System.Collections.Generic.List<AccessListStruct> AccessList = new Tools().GetAccessList(null);
        DataTable dt = BOL.GetValidAccess(AccessList,null, string.Empty, int.MaxValue, 0);
        RadMenu1.DataSource = dt;
        RadMenu1.DataBind();
    }
    protected void RadMenu1_ItemDataBound(object sender, Telerik.Web.UI.RadMenuEventArgs e)
    {
    }
}
