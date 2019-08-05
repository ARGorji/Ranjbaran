using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;

namespace Ranjbaran
{
    public partial class ShowNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCode = Request["Code"];
            int Code;

            Int32.TryParse(strCode, out Code);

            if (Code != 0)
            {
                BOLNews NewsBOL = new BOLNews();
                vNews CurNews = NewsBOL.GetDetail(Code);
                if (CurNews != null)
                {
                    if (CurNews.Link != null)
                    {
                        Response.Redirect("~/" + CurNews.Link);
                        return;
                    }
                    Page.Title =  ltrTitle.Text = CurNews.Title;
                    ltrDec.Text = Tools.FormatString( CurNews.NewsBody);
                    if(CurNews.NDate != null)
                        ltrDate.Text = Tools.ChageEnc( CurNews.NDate);
                }

            }
        }
    }
}