using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ranjbaran
{
    public partial class PayStep1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            string ItemType = Request["ItemType"];
            string strCode = Request["Code"];
            int Code;
            Int32.TryParse(strCode, out Code);
            if (Code == 0)
                return;
            if (ItemType != "Booklet" && ItemType != "Exam" && ItemType != "Course")
                return;

            ViewState["Code"] = Code;
            ViewState["ItemType"] = ItemType;

            if (Session["UserCode"] == null)
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text = "برای پرداخت مبلغ لازم است ابتدا عضو سایت شوید.";
                pnlPayForm.Visible = false;
                Normallogin1.Visible = true;
                return;
            }
            else
                Normallogin1.Visible = false;

            if (ItemType == "Booklet")
            {
                BOLBooklets BookletsBOL = new BOLBooklets();
                Ranjbaran.Old_App_Code.DAL.Booklets CurBooklet = BookletsBOL.GetDetail(Code);
                if (CurBooklet != null)
                {
                    lblAmount.Text = Tools.FormatCurrency( CurBooklet.Price.ToString()) + " ریال ";
                    lblTitle.Text = CurBooklet.Title.ToString();
                }
            }
            else if (ItemType == "Exam")
            {
                BOLExams ExamsBOL = new BOLExams();
                Ranjbaran.Old_App_Code.DAL.Exams CurExam = ExamsBOL.GetDetail(Code);
                if (CurExam != null)
                {
                    lblAmount.Text = CurExam.Price.ToString();
                    lblTitle.Text = CurExam.Title.ToString();
                }
            }
            else if (ItemType == "Course")
            {
                BOLCourses CoursesBOL = new BOLCourses();
                Ranjbaran.Old_App_Code.DAL.Courses CurExam = CoursesBOL.GetDetail(Code);
                if (CurExam != null)
                {
                    lblAmount.Text = CurExam.Fee.ToString();
                    lblTitle.Text = CurExam.Title.ToString();
                }
            }


            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ViewState["ItemType"] == null || ViewState["Code"] == null)
            {
                return;
            }

            string ItemType = ViewState["ItemType"].ToString();
            int Code = Convert.ToInt32(ViewState["Code"]);

            int TotalCost = 0;

            #region Parsian

            long Authority = 0;
            //byte Status = 1;
            int BankCode = 1;//Parsian
            string UserIP = "";

            if (ItemType == "Booklet")
            {
                BOLBooklets BookletsBOL = new BOLBooklets();
                Ranjbaran.Old_App_Code.DAL.Booklets CurBooklet = BookletsBOL.GetDetail(Code);
                if (CurBooklet != null)
                {
                    TotalCost = (int)CurBooklet.Price;
                }
            }
            else if (ItemType == "Exam")
            {
                BOLExams ExamsBOL = new BOLExams();
                Ranjbaran.Old_App_Code.DAL.Exams CurExam = ExamsBOL.GetDetail(Code);
                if (CurExam != null)
                {
                    TotalCost = (int)CurExam.Price;
                }
            }
            else if (ItemType == "Course")
            {
                BOLCourses CoursesBOL = new BOLCourses();
                Ranjbaran.Old_App_Code.DAL.Courses CurExam = CoursesBOL.GetDetail(Code);
                if (CurExam != null)
                {
                    TotalCost = (int)CurExam.Fee;
                }
            }

            int UserCode = Convert.ToInt32(Session["UserCode"]);
            string errMessage = "";

            string AfterBuyUrl = "http://www.hadiranjbaran.com/PayStep2.aspx";
            BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(UserCode);
            int UserTransactionCode = UserTransactionsBOL.Insert(UserCode, DateTime.Now, 1, 1, UserIP, TotalCost, 1, BankCode, ItemType, Code, out errMessage);

            if (UserTransactionCode == -1)
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text = "بروز خطا" + errMessage;
                return;
            }
            else
                Response.Write(UserTransactionCode);

            ir.shaparak.pec.SaleService SaleService = new ir.shaparak.pec.SaleService();
            //Ranjbaran.ParsianBankWS.EShopService ParsianService = new Ranjbaran.ParsianBankWS.EShopService();
            //ParsianService.PinPaymentRequest(ConfigurationManager.AppSettings["ParsianPin"], TotalCost, UserTransactionCode, AfterBuyUrl, ref Authority, ref Status);
            ir.shaparak.pec.ClientSaleRequestData DataInfo = new ir.shaparak.pec.ClientSaleRequestData();

            DataInfo.LoginAccount = "EAi722c7td6881cPnysp";
            DataInfo.Amount = TotalCost;
            DataInfo.OrderId = UserTransactionCode;
            DataInfo.CallBackUrl = AfterBuyUrl;


            ir.shaparak.pec.ClientSaleResponseData ResponseData =  SaleService.SalePaymentRequest(DataInfo);
            Authority = ResponseData.Token;
            short Status = ResponseData.Status;
            if (Status == 0)
            {
                int UpdateResult = UserTransactionsBOL.UpdateAuthority(UserTransactionCode, Authority.ToString(), out errMessage);
                if (UpdateResult == 0)
                {
                    Response.Redirect("https://pec.shaparak.ir/NewIPG/?Token=" + Authority);
                    return;
                }
                else
                {
                    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgMessage.Text = "بروز خطا در ذخیره داده های تراکنش بانک پارسیان" + " کد خطا: " + errMessage;
                    return;
                }
            }
            else
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text = "خطا در برقراری ارتباط با بانک پارسیان" + " کد خطا: " + Status;
                return;
            }

            #endregion
        }
    }
}