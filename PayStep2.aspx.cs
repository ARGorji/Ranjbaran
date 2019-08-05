using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Ranjbaran.Old_App_Code.DAL;
using System.IO;
using Ranjbaran.ir.shaparak.pec1;

namespace Ranjbaran
{
    public partial class PayStep2 : System.Web.UI.Page
    {

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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (ViewState["TransactionCode"] == null)
                return;

            int UserTransactionCode = Convert.ToInt32(ViewState["TransactionCode"]);

            BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(1);
            UserTransactions CurTransaction = UserTransactionsBOL.GetDetails(UserTransactionCode);
            if (CurTransaction.ItemType == "Booklet")
            {
                int BookletCode = (int)CurTransaction.ItemCode;
                BOLBooklets BookletsBOL = new BOLBooklets();
                string PDFFile = BookletsBOL.GetPDFFile(BookletCode);
                if (!string.IsNullOrEmpty(PDFFile))
                    StartDowload(CurTransaction.ItemType, PDFFile);
                else
                {
                    msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgBox.Text = "فایلی برای دانلود وجود ندارد";
                }
            }
            else if (CurTransaction.ItemType == "Exam")
            {
                int ExamCode = (int)CurTransaction.ItemCode;
                BOLExams ExamsBOL = new BOLExams();
                string PDFFile = ExamsBOL.GetPDFFile(ExamCode);
                if (!string.IsNullOrEmpty(PDFFile))
                    StartDowload(CurTransaction.ItemType, PDFFile);
                else
                {
                    msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgBox.Text = "فایلی برای دانلود وجود ندارد";
                }
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //string strAuthority = Request["au"];
            //string strStatus = Request["rs"];

            string Token = Request.Form["Token"];
            string status = Request.Form["status"];
            string OrderId = Request.Form["OrderId"];
            string TerminalNo = Request.Form["TerminalNo"];
            string RRN = Request.Form["RRN"];
            string HashCardNumber = Request.Form["HashCardNumber "];
            string Amount = Request.Form["Amount"];



            #region Parsian
            //if (strAuthority != "" && strAuthority != null) //Parsian Bank
            if(status == "0")
            {
                int BankCode = 1;
                BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(1);
                vUserTransactions CurTransaction = UserTransactionsBOL.GetTransByAuthority(Token);

                if (CurTransaction != null)
                {
                    if (CurTransaction.HCTransStatusCode == 2)
                    {
                        msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Warning;
                        msgBox.Text = "این تراکنش قبلا تایید شده است.";
                        return;
                    }
                    byte Status = 1;
                    //Ranjbaran.ParsianBankWS.EShopService ParsianService = new Ranjbaran.ParsianBankWS.EShopService();
                    //ParsianService.PinPaymentEnquiry(ConfigurationManager.AppSettings["ParsianPin"], Convert.ToInt64(strAuthority), ref Status);

                    ir.shaparak.pec1.ConfirmService ConfirmClass = new ir.shaparak.pec1.ConfirmService();
                    ClientConfirmRequestData CCR = new ClientConfirmRequestData();
                    CCR.Token = Convert.ToInt64(Token);
                    CCR.LoginAccount = ConfigurationManager.AppSettings["ParsianPin"];
                    ClientConfirmResponseData ClientResponse =  ConfirmClass.ConfirmPayment(CCR);

                    if (ClientResponse.Status == 0)
                    {
                        string errMessage = "";
                        UserTransactionsBOL.ChangeStatus(CurTransaction.Code, 2);
                        int UserTransactionCode = UserTransactionsBOL.Insert(CurTransaction.UserCode, DateTime.Now, 2, 1, "", -1 * (int)CurTransaction.Amount, 1, BankCode, CurTransaction.ItemType, (int)CurTransaction.ItemCode, out errMessage, ClientResponse.CardNumberMasked, ClientResponse.RRN);
                        Response.Write(errMessage);

                        lblAmount.Text = CurTransaction.Amount.ToString();
                        if (CurTransaction.ItemType == "Booklet")
                        {
                            int BookletCode = (int)CurTransaction.ItemCode;
                            BOLBooklets BookletsBOL = new BOLBooklets();
                            Ranjbaran.Old_App_Code.DAL.Booklets CurBooklet = BookletsBOL.GetDetail(BookletCode);
                            lblTitle.Text = CurBooklet.Title;
                            msgBox.Text = "آقای/خانم " + CurTransaction.FirstName + " " + CurTransaction.LastName + " خرید شما انجام پذیرفت. شماره پیگیری:" + CurTransaction.Code + " تاریخ:" + CurTransaction.ChrgDate + "<br /> با تشکر از شما <br />انتشارات اثبات";

                        }
                        else if (CurTransaction.ItemType == "Exam")
                        {
                            int ExamCode = (int)CurTransaction.ItemCode;
                            BOLExams ExamsBOL = new BOLExams();
                            Ranjbaran.Old_App_Code.DAL.Exams CurExam = ExamsBOL.GetDetail(ExamCode);
                            lblTitle.Text = CurExam.Title;
                            msgBox.Text = "آقای/خانم " + CurTransaction.FirstName + " " + CurTransaction.LastName + " خرید شما انجام پذیرفت. شماره پیگیری:" + CurTransaction.Code + " تاریخ:" + CurTransaction.ChrgDate + "<br /> با تشکر از شما <br />انتشارات اثبات";

                        }
                        else if (CurTransaction.ItemType == "Course")
                        {
                            int CourseCode = (int)CurTransaction.ItemCode;
                            BOLCourses CoursesBOL = new BOLCourses();
                            Ranjbaran.Old_App_Code.DAL.Courses CurCourse = CoursesBOL.GetDetail(CourseCode);
                            lblTitle.Text = CurCourse.Title;
                            msgBox.Text = "آقای/خانم " + CurTransaction.FirstName + " " + CurTransaction.LastName + " ثبت نام شما در این کلاس با انجام پذیرفت. شماره پیگیری:" + CurTransaction.Code + " تاریخ:" + CurTransaction.ChrgDate + "<br /> با تشکر از شما <br />انتشارات اثبات";


                            BOLCourseUsers CourseUsersBOL = new BOLCourseUsers(1);
                            int Result = CourseUsersBOL.Insert(CourseCode, (int)CurTransaction.UserCode);
                            if (Result != -1)
                            {
                                msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;
                                msgBox.Text = "ثبت نام شما با موفقیت انجام شد";
                            }
                            else
                            {
                                msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                                msgBox.Text = "بروز خطا در فرایند ثبت نام";
                            }

                        }
                        DateTimeMethods dtm = new DateTimeMethods();
                        lblDate.Text = dtm.GetPersianDate(DateTime.Now) + " | " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;

                        msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;

                        //msgBox.Text = "پرداخت با موفقیت انجام شد.";
                        ViewState["TransactionCode"] = CurTransaction.Code;
                        btnDownload.Visible = true;
                        return;
                    }
                    else
                    {
                        msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                        msgBox.Text = "مشترک گرامی، پرداخت الکترونیک شما با موفقیت انجام نشد، این مشکل معمولاً در مواردی رخ می‌دهد که شما در صفحه بانک پرداخت را تایید نمی‌کنید، در حساب خود به اندازه کافی موجودی ندارید، مشکلی در برقرار ارتباط با بانک بوجود آمده و ... در هر صورت جای نگرانی وجود ندارد، چرا که هیچ وجهی از حساب شما کسر نشده است.. کد خطا:" + ClientResponse.Status;
                    }

                }
                else
                {
                    msgBox.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgBox.Text = "مشترک گرامی، پرداخت الکترونیک شما با موفقیت انجام نشد، این مشکل معمولاً در مواردی رخ می‌دهد که شما در صفحه بانک پرداخت را تایید نمی‌کنید، در حساب خود به اندازه کافی موجودی ندارید، مشکلی در برقرار ارتباط با بانک بوجود آمده و ... در هر صورت جای نگرانی وجود ندارد، چرا که هیچ وجهی از حساب شما کسر نشده است.. کد خطا:";
                }
            }
            #endregion
        }
    }
}