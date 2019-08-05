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
    public partial class Order : System.Web.UI.Page
    {
        string SMSUsername = ConfigurationManager.AppSettings["SMSUsername"];
        string SMSPassword = ConfigurationManager.AppSettings["SMSPassword"];

        int TotalAmount = 0;
        int OtherCosts = 6000;

        public int SendPrice;
        public int TotalPrice;
        public int ProductPrice;
        int TotalWeight;

        protected virtual void RepeaterItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                DropDownList MyList = (DropDownList)e.Item.FindControl("ddlItemCount");
                MyList.SelectedIndexChanged += ddlItemCount_SelectedIndexChanged;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                if (Session["UserCode"] == null)
                    pnlLoggedUser.Visible = true;
                else
                    pnlLoggedUser.Visible = false;

                int ProductCode;
                string strProductCode = Request["ProductCode"];
                Int32.TryParse(strProductCode, out ProductCode);
                if (ProductCode != 0)
                {
                    BOLProducts ProductsBOL = new BOLProducts();
                    Products CurProduct = ((IBaseBOL<Products>)ProductsBOL).GetDetails(ProductCode);
                    if (CurProduct != null)
                    {
                        int ItemCount = 1;
                        int FinalCount = AddToOrders(ProductCode, ItemCount, CurProduct.FaTitle, (int)CurProduct.Price, CurProduct.Weight, CurProduct.EnTitle, (int)CurProduct.SendPishtazPrice, (int)CurProduct.SendSefareshiPrice);

                    }
                }
            }

            //Security.Check();
            if (!Page.IsPostBack)
            {
                if (Session["dtOrders"] != null)
                {
                    DataTable dt = (DataTable)Session["dtOrders"];
                    if (dt.Rows.Count > 0)
                    {
                        CalcTotalAmount(dt);


                        rptBasket.DataSource = dt;
                        rptBasket.DataBind();

                        pnlTotal.Visible = true;

                        if (Session["UserCode"] != null)
                        {
                            int UserCode = Convert.ToInt32(Session["UserCode"]);
                            BOLUsers UsersBOL = new BOLUsers();
                            Users CurUser = ((IBaseBOL<Users>)UsersBOL).GetDetails(UserCode);
                            int AccountBalance = 0;
                        }

                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    pnlTotal.Visible = false;
                }
            }

            if (rptBasket.Items.Count == 0)
            {
                btnNext1.Visible = false;
                ltrBasketHeader.Text = "سبد خرید خالی است.";
                ltrBasketHeader.Visible = true;
                pnlTotal.Visible = false;
            }
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
                bool SenderrResult = tools.SendEmail(errMailBody, " خطا در  سایت اثبات ", "admin@Ranjbaran.ir", "bidaad@gmail.com", "", "");
                return "";
            }
        }


        protected void lnkAddMoreProducts_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void btnContinuePurchase_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Products", false);

        }


        protected void btnFinalizePurchase_Click(object sender, EventArgs e)
        {
            ltrScript.Visible = false;


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
                return;
            }

            Response.Redirect("~/Shipping.aspx");
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
                    return;
                }


                int TotalOrderPrice = TotalAmount + SendPrice + OtherCosts;
                int UserCode = Convert.ToInt32(Session["UserCode"]);



                ViewState["SendPrice"] = SendPrice;
                ltrScript.Visible = false;


            }
            catch (Exception errCalcPrice)
            {
                Tools tools = new Tools();
                string errMailBody = errCalcPrice.Message;
                bool SenderrResult = tools.SendEmail(errMailBody, " خطا در  سایت اثبات ", "admin@Ranjbaran.ir", "bidaad@gmail.com", "", "");

                return;
            }
        }


        private int? GetCityCode(int ProvinceCode, int CityCode)
        {
            ZoneDataContext dc = new ZoneDataContext();
            return dc.Cities.SingleOrDefault(p => p.PayeganCode.Equals(CityCode) && p.ProvinceCode.Equals(ProvinceCode)).Code;
        }

        protected void HandleRepeaterCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "RemoveFromBasket")
            {
                ImageButton btnRemove = (ImageButton)e.Item.FindControl("btnRemove");

                int ProductCode = Convert.ToInt32(btnRemove.Attributes["ProductCode"]);
                BOLProducts ProductsBOL = new BOLProducts();
                if (ProductCode != 0)
                {
                    msgMessage.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Info;
                    RemoveOrders(ProductCode);

                    if (Session["dtOrders"] != null)
                    {
                        DataTable dt = (DataTable)Session["dtOrders"];
                        CalcTotalAmount(dt);


                    }

                    string JSCommand = "";
                    if (!msgMessage.Visible)
                        msgMessage.Visible = true;
                    else
                        JSCommand += " $(\"#" + msgMessage.ClientID + "\").fadeTo(\"fast\",0.1);";

                    msgMessage.Text = "این محصول از سبد خرید حذف شد.";
                    JSCommand += " $(\"#" + msgMessage.ClientID + "\").fadeTo(\"slow\",0.9);";
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "SelectMediaRow", JSCommand, true);

                    if (rptBasket.Items.Count == 0)
                    {
                        btnNext1.Visible = false;
                        pnlTotal.Visible = false;
                    }
                }
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
            lblTotalAmount.Text = Tools.FormatCurrency(Tools.ChangeEnc(TotalAmount.ToString()));
        }
        private void RemoveOrders(int ProductCode)
        {
            DataTable dt;
            if (Session["dtOrders"] == null)
                return;
            dt = (DataTable)Session["dtOrders"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int CurProCode = Convert.ToInt32(dt.Rows[i]["ProductCode"]);
                if (CurProCode == ProductCode)
                {
                    dt.Rows[i].Delete();
                    break;
                }
            }


            if (dt.Rows.Count > 0)
                ltrBasketHeader.Text = "سبد خرید";
            else
            {
                rptBasket.Visible = false;
                ltrBasketHeader.Text = "سبد خرید خالی است.";
            }

            Session["dtOrders"] = dt;
            rptBasket.DataSource = dt;
            rptBasket.DataBind();

        }
        private int AddToOrders(int ProductCode, int ItemCount, string ProductName, int ProductPrice, int ProductWeight, string EnTitle, int SendPishtazPrice, int SendSefareshiPrice)
        {

            int FinalItemCount = 0;
            bool ProductAlreadyExist = false;
            DataTable dt;
            if (Session["dtOrders"] == null)
            {
                dt = new DataTable();

                DataColumn dcProductCode = new DataColumn("ProductCode", typeof(System.Int32));
                dt.Columns.Add(dcProductCode);

                DataColumn dcCount = new DataColumn("ItemCount", typeof(System.Int32));
                dt.Columns.Add(dcCount);

                DataColumn dcProductName = new DataColumn("ProductName", typeof(System.String));
                dt.Columns.Add(dcProductName);

                DataColumn dcEnTitle = new DataColumn("EnTitle", typeof(System.String));
                dt.Columns.Add(dcEnTitle);

                DataColumn dcProductPrice = new DataColumn("ProductPrice", typeof(System.Decimal));
                dt.Columns.Add(dcProductPrice);

                DataColumn dcProductTotalPrice = new DataColumn("ProductTotalPrice", typeof(System.Decimal));
                dt.Columns.Add(dcProductTotalPrice);

                DataColumn dcProductTotalWeight = new DataColumn("ProductTotalWeight", typeof(System.Decimal));
                dt.Columns.Add(dcProductTotalWeight);

                DataColumn dcSendPishtazPrice = new DataColumn("SendPishtazPrice", typeof(System.Int32));
                dt.Columns.Add(dcSendPishtazPrice);

                DataColumn dcSendSefareshiPrice = new DataColumn("SendSefareshiPrice", typeof(System.Int32));
                dt.Columns.Add(dcSendSefareshiPrice);


            }
            else
            {
                dt = (DataTable)Session["dtOrders"];
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int CurProCode = Convert.ToInt32(dt.Rows[i]["ProductCode"]);
                if (CurProCode == ProductCode)
                {
                    dt.Rows[i]["ProductTotalPrice"] = FinalItemCount * ProductPrice;
                    //FinalItemCount = Convert.ToInt32(dt.Rows[i]["ItemCount"]) + ItemCount;
                    
                    /*
                    FinalItemCount = ItemCount;
                    dt.Rows[i]["ItemCount"] = FinalItemCount;
                     */
                    ProductAlreadyExist = true;
                    break;
                }
            }

            if (!ProductAlreadyExist)
            {
                DataRow drow = dt.NewRow();
                drow["ProductCode"] = ProductCode;
                FinalItemCount = ItemCount;
                drow["ItemCount"] = FinalItemCount;
                drow["ProductName"] = ProductName;
                drow["EnTitle"] = ProductName;
                drow["ProductPrice"] = ProductPrice;
                drow["ProductTotalWeight"] = FinalItemCount * ProductWeight;
                drow["ProductTotalPrice"] = FinalItemCount * ProductPrice;

                drow["SendPishtazPrice"] = SendPishtazPrice;
                drow["SendSefareshiPrice"] = SendSefareshiPrice;
                dt.Rows.Add(drow);
            }
            Session["dtOrders"] = dt;
            rptBasket.DataSource = dt;
            rptBasket.DataBind();

            return FinalItemCount;
        }
        private int ModifyBasketItemCount(int ProductCode, int ItemCount, int ProductPrice)
        {

            bool ProductAlreadyExist = false;
            DataTable dt;
            if (Session["dtOrders"] == null)
            {
                return -1;
            }
            else
            {
                dt = (DataTable)Session["dtOrders"];
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int CurProCode = Convert.ToInt32(dt.Rows[i]["ProductCode"]);
                if (CurProCode == ProductCode)
                {
                    dt.Rows[i]["ItemCount"] = ItemCount;
                    dt.Rows[i]["ProductTotalPrice"] = ItemCount * ProductPrice;
                    ProductAlreadyExist = true;
                    break;
                }
            }

            Session["dtOrders"] = dt;
            rptBasket.DataSource = dt;
            rptBasket.DataBind();

            return ItemCount;
        }

        protected void ddlItemCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            int NewItemCount = Convert.ToInt32( ddl.SelectedValue);
            int ProductCode = Convert.ToInt32( ddl.Attributes["ProductCode"]);

            BOLProducts ProductsBOL = new BOLProducts();
            Products CurProduct = ((IBaseBOL<Products>)ProductsBOL).GetDetails(ProductCode);

            ModifyBasketItemCount(ProductCode, NewItemCount, (int)CurProduct.Price);
            DataTable dt = (DataTable)Session["dtOrders"];
            if (dt.Rows.Count > 0)
            {
                CalcTotalAmount(dt);
            }
        }


    }
}