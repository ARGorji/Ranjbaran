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
    public partial class Shipping : System.Web.UI.Page
    {
        public string strDefaultFullName = "";

        int TotalAmount = 0;
        int OtherCosts = 6000;
        bool FirstAddress = true;

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

            BOLUserAddresses UserAddressesBOL = new BOLUserAddresses(1);
            rptUserAddresses.DataSource = UserAddressesBOL.GetUserAddresses(UserCode);
            rptUserAddresses.DataBind();
            if (rptUserAddresses.Items.Count == 0)
                pnlNoAddress.Visible = true;
            else
                pnlNoAddress.Visible = false;

            if (!Page.IsPostBack)
            {

                strDefaultFullName = Session["UserFullName"].ToString();
                if (Session["dtOrders"] != null)
                {
                    DataTable dt = (DataTable)Session["dtOrders"];
                    if (dt.Rows.Count > 0)
                    {
                        CalcTotalAmount(dt);
                    }
                    else
                    {
                        pnlPayTools.Visible = false;
                    }
                }
                else
                {
                    pnlPayTools.Visible = false;

                }
            }

        }

        public string IsAddressChecked(Object objCode)
        {
            string Result = "";
            string rbAddress = Request["rbAddress"];

            if (!string.IsNullOrEmpty(rbAddress))
            {
                if (Convert.ToInt32(objCode) == Convert.ToInt32(rbAddress))
                    Result = "checked";
            }
            else if (Session["AddressCode"] != null)
            {

                if (Convert.ToInt32(objCode) == Convert.ToInt32(Session["AddressCode"]))
                    Result = "checked";
            }

            return Result;
        }

        public string GetStatus(Object obj)
        {
            try
            {
                string Result = "موجود";
                if (obj != null)
                {
                    int Code = Convert.ToInt32(obj);
                    BOLProducts ProductsBOL = new BOLProducts();
                    Result = ProductsBOL.GetProductavailStatus(Code);

                }
                return Result;
            }
            catch (Exception err)
            {
                Tools tools = new Tools();
                string errMailBody = err.Message;
                bool SenderrResult = tools.SendEmail(errMailBody, " خطا در  سایت اثبات ", "admin@Ranjbaran.com", "bidaad@gmail.com", "", "");
                return "";
            }
        }


        protected void lnkAddMoreProducts_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                msgMessage.Text = "";
                if (Session["dtOrders"] != null)
                {
                    DataTable dt = (DataTable)Session["dtOrders"];
                    CalcTotalAmount(dt);
                }
                else
                {
                    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgMessage.Text = "سبد خرید خالی است.";
                    pnlPayTools.Visible = false;
                    return;
                }

                int HCSendTypeCode = 0;// Convert.ToInt32(cboHCSendTypeCode.SelectedValue);

                BOLCities CitiesBOL = new BOLCities();

                int TotalOrderPrice = TotalAmount + SendPrice + OtherCosts;
                int UserCode = Convert.ToInt32(Session["UserCode"]);
                

                ViewState["SendPrice"] = SendPrice;
                ltrScript.Visible = false;

                btnSaveContinue.Visible = true;

            }
            catch (Exception errCalcPrice)
            {
                Tools tools = new Tools();
                string errMailBody = errCalcPrice.Message;
                bool SenderrResult = tools.SendEmail(errMailBody, " خطا در  سایت اثبات ", "admin@Ranjbaran.com", "bidaad@gmail.com", "", "");

                return;
            }
        }
        protected void btnSaveContinue_Click(object sender, EventArgs e)
        {
            msgMessage.Text = "";

            try
            {
                if (Session["dtOrders"] == null)
                {
                    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgMessage.Text = "سبد خرید خالی است.";
                    return;
                }

                string strAddressCode = Request["rbAddress"];
                if (string.IsNullOrEmpty(strAddressCode))
                {
                    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgMessage.Text = "لطفا یک آدرس انتخاب کنید.";
                    return;
                }
                else
                    Session["AddressCode"] = strAddressCode;

                if (!rbPostPishtaz.Checked && !rbSefareshi.Checked)
                {
                    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msgMessage.Text = "لطفا یک شیوه ارسال انتخاب کنید.";
                    return;
                }
                else
                {
                    string DeliverType = "";
                    if (rbPostPishtaz.Checked)
                        DeliverType = "2";
                    if (rbSefareshi.Checked)
                        DeliverType = "1";

                    Session["DeliverType"] = DeliverType;
                }

                int UserCode = Convert.ToInt32(Session["UserCode"]);
                int intAddressCode = Convert.ToInt32(strAddressCode);

                BOLUserAddresses UserAddressesBOL = new BOLUserAddresses(UserCode);
                vUserAddresses CurAddress = UserAddressesBOL.GetFullDetails(intAddressCode);



                Response.Redirect("~/Review.aspx");
                

                


            }
            catch (Exception eSave)
            {
                Tools tools = new Tools();
                string errMailBody = eSave.Message;
                //bool SendErrResult = tools.SendEmail(errMailBody, " خطا در  سایت اثبات ", "admin@Ranjbaran.com", "bidaad@gmail.com", "", "");

                msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                msgMessage.Text = "متاسفانه در ثبت سفارش شما خطایی رخ داده است." + " ERROR:" + eSave.Message;
            }
        }


        private int? GetCityCode(int ProvinceCode, int CityCode)
        {
            ZoneDataContext dc = new ZoneDataContext();
            return dc.Cities.SingleOrDefault(p => p.PayeganCode.Equals(CityCode) && p.ProvinceCode.Equals(ProvinceCode)).Code;
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

        }
       
        protected void CalcTotalOrder(object sender, EventArgs e)
        {
            if (Session["dtOrders"] != null)
            {
                DataTable dt = (DataTable)Session["dtOrders"];
                if (dt.Rows.Count > 0)
                {
                    msgMessage.Text = "";
                    CalcTotalAmount(dt);
                }
                else
                {
                    pnlPayTools.Visible = false;
                }
            }
            else
            {
                pnlPayTools.Visible = false;
            }

        }
    }
}