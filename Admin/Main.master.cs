using System;
using System.Web.UI.HtmlControls;
using AKP.Base.Settings;

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UITools.IsUserSessionStillValid())
            Response.Redirect("~/Admin/Logout.aspx");


        lblFullName.Text = Session["FirstName"]  + " " + Session["LastName"];

        string BaseID = Request.QueryString["BaseID"];
        if (!string.IsNullOrEmpty(BaseID))
        {
            IBaseBOL iBOL = UITools.GetBOLClass(BaseID, -1);
            if ( iBOL != null)
                lblTitle.Text = iBOL.PageLable;
        }
        else
            lblTitle.Text = AKPSettings.ProjectName;
      
        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/main.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);

        script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/Browse.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);

        script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/PersianDate.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);

        script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/Site.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);

        script = new HtmlGenericControl("script");
        script.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/farsi.js"));
        script.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script);
    }

}
