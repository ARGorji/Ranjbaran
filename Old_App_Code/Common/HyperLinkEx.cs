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
using System.Text;

/// <summary>
/// Summary description for HyperLinkEx
/// </summary>
/// 
namespace CustomControls
{
    [ToolboxData("<{0}:HyperLinkEx runat=server></{0}:HyperLinkEx>")]
    public class HyperLinkEx : System.Web.UI.WebControls.HyperLink
    {
        private string rel;

        /// <summary>
        /// Gets or sets the rel.
        /// </summary>
        /// <value>The rel.</value>
        public string Rel
        {
            get { return rel; }
            set { rel = value; }
        }

        /// <summary>
        /// Renders the control to the specified HTML writer.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"></see> object that receives the control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<a");

            if (this.NavigateUrl.StartsWith("~"))
                this.NavigateUrl = ResolveClientUrl(this.NavigateUrl);
            sb.Append(" href=\"" + this.NavigateUrl + "\"");

            if (!string.IsNullOrEmpty(rel)) sb.Append(" rel=\"" + rel + "\"");
            if (!string.IsNullOrEmpty(this.CssClass)) sb.Append(" class=\"" + this.CssClass + "\"");
            if (!string.IsNullOrEmpty(this.Target)) sb.Append(" target=\"" + this.Target + "\"");
            if (this.Attributes["onclick"] != null) sb.Append(" onclick=\"" + this.Attributes["onclick"] + "\"");

            sb.Append(">");

            if (!string.IsNullOrEmpty(this.ImageUrl)) //Some image were found
            {
                if (this.ImageUrl.StartsWith("~"))
                    this.ImageUrl = ResolveClientUrl(this.ImageUrl);
                sb.Append("<img src=\"" + this.ImageUrl + "\" alt=\"" + this.Text + "\" title=\"" + this.Text + "\" border=\"0\" />");
            }
            else
            {
                sb.Append(this.Text);
            }

            sb.Append("</a>");
            writer.Write(sb.ToString());
        }

    }
}