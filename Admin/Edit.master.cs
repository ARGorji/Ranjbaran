using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class Edit : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Tools.IsUserSessionStillValid())
            Response.Redirect("~/Default.aspx");


        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/js/main.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);

        script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/js/farsi.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);

        script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/js/Browse.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);

        script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/js/PersianDate.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);

        //script = new HtmlGenericControl("script");
        //script.Attributes.Add("src", this.ResolveClientUrl("~/js/prototype.js"));
        //script.Attributes.Add("type", "text/javascript");
        //Page.Header.Controls.Add(script);

        //script = new HtmlGenericControl("script");
        //script.Attributes.Add("src", this.ResolveClientUrl("~/js/scriptaculous.js?load=effects"));
        //script.Attributes.Add("type", "text/javascript");
        //Page.Header.Controls.Add(script);


        //HtmlGenericControl script5 = new HtmlGenericControl("script");
        //script5.Attributes.Add("src", this.ResolveClientUrl("~/js/lightbox.js"));
        //script5.Attributes.Add("type", "text/javascript");
        //Page.Header.Controls.Add(script5);

        HtmlGenericControl script6 = new HtmlGenericControl("script");
        script6.Attributes.Add("src", this.ResolveClientUrl("~/js/Lookup.js"));
        script6.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script6);

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

        if (!Page.IsPostBack)
        {
            DateTime dt1 = DateTimeMethods.GetIranChristianDT;
            lblTime.Text = Tools.ChangeEnc(dt1.ToShortTimeString());
            DateTimeMethods dtm2 = new DateTimeMethods();
            lblPersianDate.Text = Tools.ChangeEnc(dtm2.GetPersianLongDate(dt1));

            
        }

    }
    

}
