using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran
{
    public partial class Galleries : System.Web.UI.Page
    {
        int PageNo = 1;
        int PageSize = 20;
        string ConcatUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            string strPageNo = Request["PageNo"];
            Int32.TryParse(strPageNo, out PageNo);
            if (PageNo == 0)
                PageNo = 1;

            if (!Page.IsPostBack)
            {
                BOLGalleries GalleriesBOL = new BOLGalleries();
                rptGalleries.DataSource = GalleriesBOL.GetGalleries(PageNo, PageSize);
                rptGalleries.DataBind();

                int ResultCount = GalleriesBOL.GetGalleriesCount();
                int PageCount = (int)ResultCount / PageSize;
                if (ResultCount % PageSize > 0)
                    PageCount++;

                ConcatUrl += "";
                pgrToolbar.PageNo = PageNo;
                pgrToolbar.PageCount = PageCount;
                pgrToolbar.ConcatUrl = ConcatUrl;
                pgrToolbar.PageBind();
            }
        }
    }
}