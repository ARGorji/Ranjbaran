using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;
using System.IO;

namespace Ranjbaran.UsersFolder
{
    public partial class UserTrans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserCode"] == null)
            {
                Response.Redirect("~/Users/Login.aspx");
                return;
            }

            int UserCode = Convert.ToInt32(Session["UserCode"]);

            if (!Page.IsPostBack)
            {
                BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(1);
                rptUserTrans.DataSource = UserTransactionsBOL.GetUserTrans(UserCode, 2);
                rptUserTrans.DataBind();
            }

        }

        public bool IsVisible(int Code)
        {
            BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(1);
            UserTransactions CurTrans = UserTransactionsBOL.GetDetails(Code);
            if (CurTrans.PayDate.Value.CompareTo(DateTime.Now.AddDays(1)) >= 1)
                return false;
            else
                return true;

        }

        public string GetItemName(Object obj)
        {
            string Result = "";
            string objItemType = obj.ToString();
            switch (objItemType)
            {
                case "Booklet":
                    Result = "جزوه";
                    break;
                case "Exam":
                    Result = "آزمون آزمایشی";
                    break;
                default:
                    break;
            }

            return Result;
        }

        private void StartDowload(string ItemType, string PDFFile)
        {
            byte[] pdfByte = System.IO.File.ReadAllBytes(Server.MapPath("~/" + PDFFile));

            Guid newGd = Guid.NewGuid();
            string RandFileName = ItemType + newGd.ToString().Replace("-", "");

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

            int UserTransCode = Convert.ToInt32(e.CommandArgument);
            BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(1);
            UserTransactions CurTrans = UserTransactionsBOL.GetDetails(UserTransCode);


            if (e.CommandName == "StartDownload")
            {

                if (CurTrans.ItemType == "Booklet")
                {
                    BOLExams ExamBOL = new BOLExams();
                    string PDFFile = ExamBOL.GetPDFFile((int)CurTrans.ItemCode);
                    if (!string.IsNullOrEmpty(PDFFile))
                        StartDowload(CurTrans.ItemType, PDFFile);
                    else
                    {
                        msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                        msgMessage.Text = "فایلی برای دانلود وجون ندارد";
                    }
                }
                else if (CurTrans.ItemType == "Exam")
                {
                    int ExamCode = (int)CurTrans.ItemCode;
                    BOLExams ExamsBOL = new BOLExams();
                    string PDFFile = ExamsBOL.GetPDFFile(ExamCode);
                    if (!string.IsNullOrEmpty(PDFFile))
                        StartDowload(CurTrans.ItemType, PDFFile);
                    else
                    {
                        msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                        msgMessage.Text = "فایلی برای دانلود وجود ندارد";
                    }
                }

            }


        }

        public string GetItemTitle(Object objItemType, Object objItemCode)
        {
            string ItemType = objItemType.ToString();
            int ItemCode = Convert.ToInt32(objItemCode);
            if (ItemType == "Booklet")
            {
                int BookletCode = ItemCode;
                BOLBooklets BookletsBOL = new BOLBooklets();
                Ranjbaran.Old_App_Code.DAL.Booklets CurBooklet = BookletsBOL.GetDetail(BookletCode);
                return CurBooklet.Title;
            }
            else if (ItemType == "Exam")
            {
                int ExamCode = ItemCode;
                BOLExams ExamsBOL = new BOLExams();
                Ranjbaran.Old_App_Code.DAL.Exams CurExam = ExamsBOL.GetDetail(ExamCode);
                return CurExam.Title;
            }
            else
                return "";

        }
    }
}