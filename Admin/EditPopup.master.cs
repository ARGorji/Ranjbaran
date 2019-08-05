using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using AKP.Web.Controls.Common;
using System.ComponentModel;

public partial class Admin_EditPopup : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Tools.IsUserSessionStillValid())
        {
            Tools.SetClientScript(this.Page, "LogoutUser", "window.opener.location.href='../Default.aspx';window.close()");
            //Response.End();
            (this.FindControl("cphMain")).Visible = false;
            return;
        }

        HtmlGenericControl script1 = new HtmlGenericControl("script");
        script1.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/main.js"));
        script1.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script1);

        HtmlGenericControl script2 = new HtmlGenericControl("script");
        script2.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/farsi.js"));
        script2.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script2);

        HtmlGenericControl script3 = new HtmlGenericControl("script");
        script3.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/Browse.js"));
        script3.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script3);

        HtmlGenericControl script4 = new HtmlGenericControl("script");
        script4.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/PersianDate.js"));
        script4.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script4);

        HtmlGenericControl script6 = new HtmlGenericControl("script");
        script6.Attributes.Add("src", this.ResolveClientUrl("~/Admin/Scripts/Lookup.js"));
        script6.Attributes.Add("type", "text/javascript");
        Page.Header.Controls.Add(script6);

        #region Cast controls to label if the parent is in View mode
        if (!IsPostBack)
        {
            //RecursiveFindControl(cphMain.Controls);
        }
        #endregion
    }

    private void RecursiveFindControl(ControlCollection col)
    {
        int ControlsCount = col.Count;
        for (int i = 0; i < ControlsCount; i++)
        {
            Control c = col[i];
            if ((c is WebControl) && !(c is Label) && (c is IContainer))
            {

                WebControl wc = (WebControl)c;
                //string wcAtt = wc.Attributes["jas"];
                //if (!string.IsNullOrEmpty(wcAtt) && wcAtt == "1")
                if (wc is ICustomControlsBase)
                {
                    #region Main process to cast controls to label
                    Label l = new Label();
                    object val = Tools.GetControlValue(wc);
                    l.Text = val == null ? "" : val.ToString();
                    IContainer ic;

                    c.Parent.Controls.Add(l);
                    #endregion
                }
            }
            if (c.Controls.Count > 0)
                RecursiveFindControl(c.Controls);
        }
    }
}
