using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;
using System.Data;
using System.Configuration;

namespace Ranjbaran
{
    public partial class Review : System.Web.UI.Page
    {

        int TotalAmount = 0;
        int OtherCosts = 0;

        public int SendPrice;
        public int TotalPrice;
        public int ProductPrice;
        int TotalWeight;

        protected void Page_Load(object sender, EventArgs e)
        {

            Security.Check();
            int UserCode = Convert.ToInt32(Session["UserCode"]);

            if (Session["dtOrders"] == null)
            {
                Response.Redirect("~/");
                return;
            }


            if (!Page.IsPostBack)
            {
                int intDeliverType = Convert.ToInt32(Session["DeliverType"]);
                int intAddressCode = Convert.ToInt32(Session["AddressCode"]);

                BOLHardCode HardCodeBOL = new BOLHardCode();
                HardCodeBOL.TableOrViewName = "HCSendTypes";
                lblSendType.Text = HardCodeBOL.GetNameByCode(intDeliverType);
                BOLUserAddresses UserAddressesBOL = new BOLUserAddresses(UserCode);
                vUserAddresses CurAddress = UserAddressesBOL.GetFullDetails(intAddressCode);
                lblFullName.Text = CurAddress.FullName;
                lblAddress.Text = CurAddress.Province + " " + CurAddress.City + " " + CurAddress.Address ;
                lblContactNumber.Text = CurAddress.CellPhone + " " + CurAddress.Tel;
                lblDiscount.Text = Tools.ChangeEnc("0");

                string strDeliverType = "1";
                if (Session["DeliverType"] != null)
                    strDeliverType = Session["DeliverType"].ToString();

                //if (strDeliverType == "2")//Sefareshi
                //{
                //    OtherCosts = 65000;
                //    lblOtherCosts.Text = Tools.ChangeEnc("65000");
                //    lblSendCost.Text = Tools.ChangeEnc("65000");
                //}
                //else
                //{
                //    OtherCosts = 75000;
                //    lblOtherCosts.Text = Tools.ChangeEnc("75000");
                //    lblSendCost.Text = Tools.ChangeEnc("75000");
                //}



                if (Session["dtOrders"] != null)
                {
                    DataTable dt = (DataTable)Session["dtOrders"];


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int ItemCount = Convert.ToInt32(dt.Rows[i]["ItemCount"]);
                        int SendPishtazPrice = Convert.ToInt32(dt.Rows[i]["SendPishtazPrice"]);
                        int SendSefareshiPrice = Convert.ToInt32(dt.Rows[i]["SendSefareshiPrice"]);

                        if (strDeliverType == "1")//Sefareshi
                            OtherCosts += SendSefareshiPrice * ItemCount;
                        else
                            OtherCosts += SendPishtazPrice * ItemCount;
                    }

                    lblOtherCosts.Text = Tools.ChangeEnc( (OtherCosts/10).ToString());
                    lblSendCost.Text = Tools.ChangeEnc( (OtherCosts/10).ToString());


                    if (dt.Rows.Count > 0)
                    {
                        CalcTotalAmount(dt);

                        lblBasketHeader.Text = "سبد خرید";
                        rptBasket.DataSource = dt;
                        rptBasket.DataBind();

                        if (Session["UserCode"] != null)
                        {
                            BOLUsers UsersBOL = new BOLUsers();
                            Users CurUser = ((IBaseBOL<Users>)UsersBOL).GetDetails(UserCode);
                        }
                    }
                    else
                    {
                        lblBasketHeader.Text = "سبد خرید خالی است.";
                        
                    }
                }

            }

        }



        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Payment.aspx");

            }
            catch (Exception errCalcPrice)
            {
                Tools tools = new Tools();
                string errMailBody = errCalcPrice.Message;
                //bool SenderrResult = tools.SendEmail(errMailBody, " خطا در  سایت اثبات ", "admin@Ranjbaran.com", "bidaad@gmail.com", "", "");

                return;
            }
        }
        
        private void CalcTotalAmount(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int Amount = Convert.ToInt32(dt.Rows[i]["ProductPrice"]);
                int Quantity = Convert.ToInt32(dt.Rows[i]["ItemCount"]);
                int CurWeight = Convert.ToInt32(dt.Rows[i]["ProductTotalWeight"]);

                TotalAmount += (Amount * Quantity);
                TotalWeight += CurWeight;
            }
            //TotalAmount += OtherCosts;
            lblTotalAmount.Text = Tools.FormatCurrency(Tools.ChangeEnc((TotalAmount / 10 ).ToString()));
            lblTotalOrderPrice.Text = Tools.FormatCurrency(Tools.ChangeEnc((TotalAmount / 10 + OtherCosts / 10).ToString()));
        }
        
        
    }
}