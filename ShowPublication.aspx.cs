using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;

namespace Ranjbaran
{
    public partial class ShowPublication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCode = Request["Code"];
            int Code;
            Int32.TryParse(strCode, out Code);
            if (Code != 0)
            {
                BOLPublications PublicationsBOL = new BOLPublications();
                vPublications CurRecord = PublicationsBOL.GetDetail(Code);
                if (CurRecord != null)
                {
                    Page.Title = lblTitle.Text = CurRecord.Title;
                    lblDescription.Text = Tools.ChangeEnc( Tools.FormatString( CurRecord.Description));
                    imgPubLargePic.ImageUrl = "~/" + CurRecord.LargePic;

                    lblEntesharat.Text = CurRecord.Entesharat;
                    lblPrice.Text = Tools.ChangeEnc( Tools.FormatCurrency( CurRecord.Price.ToString()));
                    lblPublicationTurn.Text = CurRecord.PublicationTurn;
                }
            }

        }
    }
}