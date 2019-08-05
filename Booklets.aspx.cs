using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Ranjbaran
{
    public partial class Booklets : System.Web.UI.Page
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
                BOLBooklets BookletsBOL = new BOLBooklets();
                rptBooklets.DataSource = BookletsBOL.GetBooklets(PageNo, PageSize);
                rptBooklets.DataBind();

                int ResultCount = BookletsBOL.GetBookletsCount();
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

        public bool IsFree(int Code)
        {
            BOLBooklets BookletsBOL = new BOLBooklets();
            return BookletsBOL.IsFree(Code);

        }

        private void StartDowload(string PDFFile)
        {
            byte[] pdfByte = System.IO.File.ReadAllBytes(Server.MapPath("~/" + PDFFile));

            Guid newGd = Guid.NewGuid();
            string RandFileName = "Booklet" + newGd.ToString().Replace("-", "");

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdfByte);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + RandFileName + ".pdf");
            Response.AddHeader("Content-Length", pdfByte.Length.ToString());
            Response.Buffer = true;


            ms.WriteTo(Response.OutputStream);
            //Response.End();
        }

        protected void HandleRepeaterCommand(object source, RepeaterCommandEventArgs e)
        {

            int BookletCode = Convert.ToInt32(e.CommandArgument);

            #region StartDownload
            if (e.CommandName == "StartDownload")
            {
                BOLBooklets BookletsBOL = new BOLBooklets();
                string PDFFile = BookletsBOL.GetPDFFile(BookletCode);
                if (!string.IsNullOrEmpty(PDFFile))
                    StartDowload(PDFFile);
                else
                {
                    msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msg.Text = "فایلی برای دانلود وجود ندارد";
                }

            }
            else if (e.CommandName == "StartPay")
            {
                BOLBooklets BookletsBOL = new BOLBooklets();
                string PDFFile = BookletsBOL.GetPDFFile(BookletCode);
                if (!string.IsNullOrEmpty(PDFFile))
                {
                    Response.Redirect("PayStep1.aspx?ItemType=Booklet&Code=" + BookletCode);
                    return;
                }
                else
                {
                    msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msg.Text = "فایلی برای خرید وجود ندارد";
                }

            }
            #endregion

        }
    }
}