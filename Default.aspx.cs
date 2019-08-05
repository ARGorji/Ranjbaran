using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;

namespace Ranjbaran
{
    public partial class _Default : System.Web.UI.Page
    {
        protected string _showedCode = "";
        public string ShowedCodes
        {
            get
            {
                return _showedCode;
            }
            set
            {
                _showedCode = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            BOLNews NewsBOL = new BOLNews();
            rptNewsTicker.DataSource = NewsBOL.GetLatestTelexNews(10);
            rptNewsTicker.DataBind();

            BOLLinks LinksBOLD = new BOLLinks();
            rptLinks.DataSource = LinksBOLD.GetLinks();
            rptLinks.DataBind();

        }


    }
}
