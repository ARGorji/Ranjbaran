using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.UserControls
{
    public partial class UCLinks : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BOLLinks LinksBOL = new BOLLinks();
            rptLinks.DataSource = LinksBOL.GetLinks();
            rptLinks.DataBind();
        }
    }
}