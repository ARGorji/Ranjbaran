using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.UserControls
{
    public partial class UCNews : System.Web.UI.UserControl
    {
        private int _sectionCode;
        public int SectionCode
        {
            get
            {
                return _sectionCode;
            }
            set
            {
                _sectionCode = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                BOLNews NewsBOL = new BOLNews();
                rptNews.DataSource = NewsBOL.GetNewsBySectionCode(_sectionCode);
                rptNews.DataBind();

            }
        }
    }
}