using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;
using System.Configuration;
using Ranjbaran.ir.shaparak.pec1;

namespace Ranjbaran
{
    public partial class Checkout : System.Web.UI.Page
    {

        string strOrderId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string OrderID = Request["ID"];

            if (!string.IsNullOrEmpty(OrderID))
            {
                ShowOrderInfo(OrderID);

            }
            else
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


                if (status == "0") //Parsian Bank
                {
                    CheckRequestStatus(Token);
                }
            }
        }

        private void ShowOrderInfo(string OrderID)
        {
            BOLOrders OrdersBOL = new BOLOrders();
            vOrders CurOrder = OrdersBOL.GetOrderByID(OrderID);

            if (CurOrder != null)
            {
                lblOrderStatus.Text = CurOrder.OrderStatus;

                if (CurOrder.HCTransStatusCode== 2)
                    lblPaymentStatus.Text = "پرداخت شده";
                else
                    lblPaymentStatus.Text = "پرداخت نشده";

                lblTotalAmount.Text = Tools.ChangeEnc(Tools.FormatCurrency(Convert.ToInt32(CurOrder.TotalOrderCost / 10).ToString())) + " تومان ";
                lblOrderID.Text = OrderID;

                lblFullName.Text = CurOrder.FullName;
                lblAddress.Text = Tools.FormatString(CurOrder.Address.Replace("\n", " "));
                lblSendType.Text = CurOrder.SendType;

            }
            else
                mainMessage.Visible = false;
        }

        private void CheckRequestStatus(string strAuthority)
        {
            try
            {
                int UserCode = 0;
                int BankCode = 1;
                BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(1);
                vUserTransactions CurTransaction = UserTransactionsBOL.GetTransByAuthority(strAuthority);

                if (CurTransaction != null)
                {
                    if (CurTransaction.HCTransStatusCode == 2)
                    {
                        msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Warning;
                        msgMessage.Text = "این تراکنش قبلا تایید شده است.";
                        return;
                    }
                    //byte Status = 1;
                    //Ranjbaran.ParsianBankWS.EShopService ParsianService = new Ranjbaran.ParsianBankWS.EShopService();
                    //ParsianService.PinPaymentEnquiry(ConfigurationManager.AppSettings["ParsianPin"], Convert.ToInt64(strAuthority), ref Status);

                    ir.shaparak.pec1.ConfirmService ConfirmClass = new ir.shaparak.pec1.ConfirmService();
                    ClientConfirmRequestData CCR = new ClientConfirmRequestData();
                    CCR.Token = Convert.ToInt64(strAuthority);
                    CCR.LoginAccount = ConfigurationManager.AppSettings["ParsianPin"];
                    ClientConfirmResponseData ClientResponse = ConfirmClass.ConfirmPayment(CCR);


                    if (ClientResponse.Status == 0)
                    {
                        string errMessage = "";
                        UserTransactionsBOL.ChangeStatus(CurTransaction.Code, 2);
                        int UserTransactionCode = UserTransactionsBOL.Insert(CurTransaction.UserCode, DateTime.Now, 2, 1, "", -1 * (int)CurTransaction.Amount, 1, BankCode, "", 0, out errMessage, ClientResponse.CardNumberMasked, ClientResponse.RRN);
                        Response.Write(errMessage);

                        lblTotalAmount.Text = CurTransaction.Amount.ToString();
                        lblPaymentStatus.Text = "پرداخت شده";

                        DateTimeMethods dtm = new DateTimeMethods();

                        msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.OK;
                        msgMessage.Text = "پرداخت با موفقیت انجام شد.";
                        ViewState["TransactionCode"] = CurTransaction.Code;

                        ltrMessage.Text = "آقای/خانم " + CurTransaction.FirstName + " " + CurTransaction.LastName + " خرید شما انجام پذیرفت. شماره پیگیری:" + CurTransaction.Code + " تاریخ:" + CurTransaction.ChrgDate + "<br /> با تشکر از شما <br />انتشارات اثبات";
                        return;
                    }
                    else
                    {
                        msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                        msgMessage.Text = "مشترک گرامی، پرداخت الکترونیک شما با موفقیت انجام نشد، این مشکل معمولاً در مواردی رخ می‌دهد که شما در صفحه بانک پرداخت را تایید نمی‌کنید، در حساب خود به اندازه کافی موجودی ندارید، مشکلی در برقرار ارتباط با بانک بوجود آمده و ... در هر صورت جای نگرانی وجود ندارد، چرا که هیچ وجهی از حساب شما کسر نشده است.. کد خطا:" + ClientResponse.Status;
                    }
                }


                }
            catch (Exception ex)
            {
                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text += "بروز خطا : " + ex.Message + "<BR>";
                return;
            }
        }
    }
}