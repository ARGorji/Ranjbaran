using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ranjbaran.Old_App_Code.DAL;
using System.Configuration;

namespace Ranjbaran
{
    public partial class Payment : System.Web.UI.Page
    {
        int TotalAmount = 0;
        int OtherCosts = 0;
        int CouponDiscount = 0;
        public int SendPrice;
        public int TotalPrice;
        public int ProductPrice;
        int TotalWeight;

        protected string FormBody = "";
        public string tranKey = ConfigurationManager.AppSettings["BMITransactionKey"];
        public string CardAcqID = ConfigurationManager.AppSettings["BMIMerchantID"];
        public string TerminalId = ConfigurationManager.AppSettings["BMITerminalID"];
        public string ReturnURL = ConfigurationManager.AppSettings["BMIShopReturnURL"];
        public string ServiceURL = ConfigurationManager.AppSettings["BMIServiceURL"];


        protected void Page_Load(object sender, EventArgs e)
        {
            msgMessage.Text = "";
            Security.Check();
            int UserCode = Convert.ToInt32(Session["UserCode"]);
            string OrderID = Request["OrderID"];

            if (string.IsNullOrEmpty(OrderID))
            {
                if (Session["dtOrders"] == null)
                {
                    Response.Redirect("~/");
                    return;
                }
                else
                {
                    DataTable dt = (DataTable)Session["dtOrders"];
                    if (dt.Rows.Count > 0)
                    {
                        CalcTotalAmount(dt);

                        BOLUsers UsersBOL = new BOLUsers();
                        Users CurUser = ((IBaseBOL<Users>)UsersBOL).GetDetails(UserCode);

                    }
                }
            }

            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(OrderID))
                {
                    int intAddressCode = Convert.ToInt32(Session["AddressCode"]);

                    BOLUserAddresses UserAddressesBOL = new BOLUserAddresses(UserCode);
                    vUserAddresses CurAddress = UserAddressesBOL.GetFullDetails(intAddressCode);
                    if (CurAddress.CityCode != 124)
                    {
                        //rbPayOnline.Checked = true;
                        //rbPayInPlace.Enabled = false;
                    }
                }
                else
                {
                    ViewState["OrderID"] = OrderID;
                    BOLOrders OrdersBOL = new BOLOrders();
                    vOrders CurOrder = OrdersBOL.GetOrderByID(OrderID);
                    lblTotalOrderPrice.Text = Tools.FormatCurrency(Tools.ChangeEnc((CurOrder.TotalOrderCost / 10 ).ToString()));
                }

            }

        }

        private int CalcTotalAmount(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int Amount = Convert.ToInt32(dt.Rows[i]["ProductPrice"]);
                int Quantity = Convert.ToInt32(dt.Rows[i]["ItemCount"]);
                int CurWeight = Convert.ToInt32(dt.Rows[i]["ProductTotalWeight"]);

                TotalAmount += (Amount * Quantity);
                TotalWeight += CurWeight;
            }


            int intAddressCode = Convert.ToInt32(Session["AddressCode"]);

            int UserCode = Convert.ToInt32(Session["UserCode"]);
            BOLUserAddresses UserAddressesBOL = new BOLUserAddresses(UserCode);
            vUserAddresses CurAddress = UserAddressesBOL.GetFullDetails(intAddressCode);

            string strDeliverType = "1";
            if (Session["DeliverType"] != null)
                strDeliverType = Session["DeliverType"].ToString();

            //if (strDeliverType == "2")//Sefareshi
            //{
            //    OtherCosts = 65000;
            //}
            //else
            //{
            //    OtherCosts = 75000;
            //}
            OtherCosts = 0;
            if (Session["dtOrders"] != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int ItemCount = Convert.ToInt32(dt.Rows[i]["ItemCount"]);
                    int SendPishtazPrice = Convert.ToInt32(dt.Rows[i]["SendPishtazPrice"]);
                    int SendSefareshiPrice = Convert.ToInt32(dt.Rows[i]["SendSefareshiPrice"]);

                    if (strDeliverType == "2")//Sefareshi
                        OtherCosts += SendSefareshiPrice * ItemCount;
                    else
                        OtherCosts += SendPishtazPrice * ItemCount;
                }
            }


            lblTotalOrderPrice.Text = Tools.FormatCurrency(Tools.ChangeEnc((TotalAmount / 10 + OtherCosts / 10).ToString()));
            return TotalAmount + OtherCosts;
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            int OrderCode = 0;
            int TotalOrderCost = 0;
            string OrderID = "";
            int UserCode = Convert.ToInt32(Session["UserCode"]);
            int HCPayMethodCode = 1;
            if (rbPayOnline.Checked)
                HCPayMethodCode = 1;
            else
                HCPayMethodCode = 2;

            string strDeliverType = "1";
            if (Session["DeliverType"] != null)
                strDeliverType = Session["DeliverType"].ToString();

            //if (strDeliverType == "2")//Sefareshi
            //{
            //    OtherCosts = 65000;
            //}
            //else
            //{
            //    OtherCosts = 75000;
            //}


            Tools tools = new Tools();

            SendPrice = 0;

            BOLOrders OrdersBOL = new BOLOrders();


            if (ViewState["OrderID"] == null)
            {
                if (Session["dtOrders"] != null)
                {
                    DataTable dt = (DataTable)Session["dtOrders"];
                    if (dt.Rows.Count > 0)
                    {
                        TotalAmount = 0;
                        int NetTotal = CalcTotalAmount(dt);

                    }
                }
                int intDeliverType = Convert.ToInt32(Session["DeliverType"]);
                int intAddressCode = Convert.ToInt32(Session["AddressCode"]);

                OtherCosts = 0;
                if (Session["dtOrders"] != null)
                {
                    DataTable dt = (DataTable)Session["dtOrders"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int ItemCount = Convert.ToInt32(dt.Rows[i]["ItemCount"]);
                        int SendPishtazPrice = Convert.ToInt32(dt.Rows[i]["SendPishtazPrice"]);
                        int SendSefareshiPrice = Convert.ToInt32(dt.Rows[i]["SendSefareshiPrice"]);

                        if (strDeliverType == "2")//Sefareshi
                            OtherCosts += SendSefareshiPrice * ItemCount;
                        else
                            OtherCosts += SendPishtazPrice * ItemCount;
                    }
                }

                //BOLHardCode HardCodeBOL = new BOLHardCode();
                //HardCodeBOL.TableOrViewName = "HCSendTypes";
                BOLUserAddresses UserAddressesBOL = new BOLUserAddresses(UserCode);
                vUserAddresses CurAddress = UserAddressesBOL.GetFullDetails(intAddressCode);

                string FullAddress = CurAddress.Province + " " + CurAddress.City + " " + CurAddress.Address;

                TotalOrderCost = TotalAmount + SendPrice + OtherCosts - CouponDiscount;

                string FullName = CurAddress.FullName;
                string Tel = CurAddress.Tel;
                string PostalCode = CurAddress.PostalCode;
                string Description = "";
                string PostOrderCode = "";
                string CellPhone = CurAddress.CellPhone;
                string Email = Session["Email"].ToString();

                string Address = CurAddress.Province + " " + CurAddress.City + " " + CurAddress.Address;
                int HCSendTypeCode = 1;
                int HCGenderCode = 1;

                int CityCode = CurAddress.CityCode;
                int ProvinceCode = CurAddress.ProvinceCode;



                #region Save To Orders
                OrderID = tools.GetRandString(10).ToUpper();

                int? RefUserCode = null;
                if (Session["RefUserCode"] != null)
                    RefUserCode = Convert.ToInt32(Session["RefUserCode"]);
                if (Session["UserCode"] != null)
                    UserCode = Convert.ToInt32(Session["UserCode"]);
                OrderCode = OrdersBOL.InsertRecord(FullName, Email, CityCode, ProvinceCode, Tel, CellPhone, PostalCode, Address, HCGenderCode, Description,
                    HCSendTypeCode, 1, 0, SendPrice, TotalAmount, OtherCosts, TotalOrderCost, OrderID, false, RefUserCode, UserCode, HCPayMethodCode);

                #endregion

                #region Save to Order Products
                if (Session["dtOrders"] != null)
                {
                    BOLOrderProducts OrderProductsBOL = new BOLOrderProducts(OrderCode);
                    DataTable dt = (DataTable)Session["dtOrders"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int ProductCode = Convert.ToInt32(dt.Rows[i]["ProductCode"]);
                        int ProductPrice = Convert.ToInt32(dt.Rows[i]["ProductPrice"]);
                        int ItemCount = Convert.ToInt32(dt.Rows[i]["ItemCount"]);

                        OrderProductsBOL.InsertRecord(OrderCode, ProductCode, ProductPrice, 1, ItemCount);

                    }
                }
                #endregion
            }
            else
            {
                vOrders CurOrder = OrdersBOL.GetOrderByID(ViewState["OrderID"].ToString());
                if (CurOrder == null)
                {
                    msgMessage.Text = "کد سفارش معتبر نیست";
                    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    return;
                }
                else
                {
                    OrderCode = CurOrder.Code;
                    TotalOrderCost = Convert.ToInt32( CurOrder.TotalOrderCost);
                }
                OrderID = CurOrder.ID;
            }


            //if (OrderCode != -1 && !string.IsNullOrEmpty( CellPhone) )
            //{
            //    try
            //    {
            //        long intCelPhone = 0;
            //        if(CellPhone.Length == 11)
            //        {
            //            if(CellPhone.StartsWith("0"))
            //            {
            //                intCelPhone  = Convert.ToInt64( CellPhone.Substring(1, CellPhone.Length - 1));
            //                SMSHelper sHelper = new SMSHelper();
            //                sHelper.SendSingleSMS(intCelPhone, "خرید شما در سایت سایت اثبات با موفقیت انجام شد. کد پیگیری:" + );
            //            }
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}



            pnlPayTools.Visible = false;

            Session["dtOrders"] = null;

            if (rbPayOnline.Checked)
            {
                string UserIP = "";

                try
                {
                    ///////////////////////bank
                    long Authority = 0;
                    //byte Status = 1;
                    int BankCode = 1;//Parsian
                    string errMessage = "";

                    BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(UserCode);
                    //int UserTransactionCode = 1;// UserTransactionsBOL.Insert(UserCode, DateTime.Now, 1, 1, UserIP, TotalOrderCost, 1);
                    int UserTransactionCode = UserTransactionsBOL.Insert(UserCode, DateTime.Now, 1, 1, UserIP, TotalOrderCost, 1, BankCode, "", 0, out errMessage);
                    bool UpdateTransCodeResult  = OrdersBOL.UpdateTransactionCode(OrderCode, UserTransactionCode);
                    
                    if(!UpdateTransCodeResult)
                    {
                        msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                        msgMessage.Text = "بروز خطا";
                        return;
                    }

                    long BankOrderId = Convert.ToInt64(UserTransactionCode);
                    string AdditionalData = "";
                    string requestKey; // request key





                    string AfterBuyUrl = "http://www.hadiranjbaran.com/Checkout.aspx";

                    //Ranjbaran.ParsianBankWS.EShopService ParsianService = new Ranjbaran.ParsianBankWS.EShopService();
                    //ParsianService.PinPaymentRequest(ConfigurationManager.AppSettings["ParsianPin"], TotalOrderCost, UserTransactionCode, AfterBuyUrl, ref Authority, ref Status);
                    ir.shaparak.pec.SaleService SaleService = new ir.shaparak.pec.SaleService();
                    ir.shaparak.pec.ClientSaleRequestData DataInfo = new ir.shaparak.pec.ClientSaleRequestData();

                    DataInfo.LoginAccount = "EAi722c7td6881cPnysp";
                    DataInfo.Amount = TotalOrderCost;
                    DataInfo.OrderId = UserTransactionCode;
                    DataInfo.CallBackUrl = AfterBuyUrl;


                    ir.shaparak.pec.ClientSaleResponseData ResponseData = SaleService.SalePaymentRequest(DataInfo);
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
                }
                catch (Exception err)
                {
                    msgMessage.Text = "خطا در برقراری ارتباط با سرور بانک ملی" + err.Message;
                    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    return;
                }

            }
            else
            {
                Response.Redirect("~/Checkout.aspx?ID=" + OrderID);
                return;
            }


        }

       

     
    }
}