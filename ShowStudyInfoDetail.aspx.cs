using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;

namespace Ranjbaran
{
    public partial class ShowStudyInfoDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCode = Request["Code"];
            int Code;

            Int32.TryParse(strCode, out Code);

            if (Code != 0)
            {
                BOLStudyInfos StudyInfosBOL = new BOLStudyInfos();
                vStudyInfos CurNews = StudyInfosBOL.GetDetail(Code);
                if (CurNews != null)
                {

                    Page.Title = ltrTitle.Text = CurNews.Title;
                    ltrDec.Text = Tools.FormatString(CurNews.Description);
                    if (CurNews.CDate != null)
                        ltrDate.Text = Tools.ChageEnc(CurNews.CDate);
                }

            }
        }
    }
}