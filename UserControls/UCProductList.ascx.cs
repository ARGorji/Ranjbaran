using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;

namespace Ranjbaran.UserControls
{
    public partial class UCProductList : System.Web.UI.UserControl
    {
        private bool IsHomeProduct = false;
        private bool _showSearchTools = false;
        public bool ShowSearchTools
        {
            get
            {
                return _showSearchTools;
            }
            set
            {
                _showSearchTools = value;
            }
        }

        private bool _showPagination = true;
        public bool ShowPagination
        {
            get
            {
                return _showPagination;
            }
            set
            {
                _showPagination = value;
            }
        }

        private int _maxPerPage = 0;
        public int MaxPerPage
        {
            get
            {
                return _maxPerPage;
            }
            set
            {
                _maxPerPage = value;
            }
        }

        string ConcatUrl;

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string ShowClass(Object objCode)
        {
            string Result = "";
//            if (IsHomeProduct)
//                Result = "hide ";
            if (objCode != null)
            {
                int Code = Convert.ToInt32(objCode);
                BOLProducts ProductsBOL = new BOLProducts();
                Products CurProduct = ProductsBOL.GetDetails(Code);

                if (CurProduct.Special != null)
                {
                    if ((bool)CurProduct.Special)
                        Result += " product-featured";
                }
                if (CurProduct.IsNew != null)
                {
                    if ((bool)CurProduct.IsNew)
                        Result += " product-new";
                }
                if (CurProduct.IsMostSold != null)
                {
                    if ((bool)CurProduct.IsMostSold)
                        Result += " product-bestsales";
                }
                if (CurProduct.IsDiscount != null)
                {
                    if ((bool)CurProduct.IsDiscount)
                        Result += " product-sale";
                }

            }
            return Result;

        }


        public bool ShowMarketPrice(Object objPrice, Object objMarketProce)
        {
            if (objPrice == null || objMarketProce == null)
                return false;

            int Price = Convert.ToInt32(objPrice);
            int MarketPrice = Convert.ToInt32(objMarketProce);
            if (MarketPrice > Price)
                return true;
            else
                return false;
        }

        public string ISpecial(Object objSpecial)
        {
            if (objSpecial == null)
                return "";
            else
            {
                bool Special = Convert.ToBoolean(objSpecial);
                if (Special)
                    return "<div class=\"Special\"></div>";
                else
                    return "";
            }
        }

        public void LoadProducts(int ItemPerPage, string Keyword, int? CatCode, int SortType, int AscDesc)
        {
            try
            {
                ViewState["Keyword"] = Keyword;
                ViewState["CatCode"] = CatCode;
                ViewState["SortType"] = SortType;
                ViewState["AscDesc"] = AscDesc;
                if (_maxPerPage != 0)
                    ItemPerPage = _maxPerPage;

                ddlItemPerPage.SelectedValue = ItemPerPage.ToString();
                ddlAscDesc.SelectedValue = AscDesc.ToString();
                ddlSort.SelectedValue = SortType.ToString();

                int PageNo = 1;
                BOLHardCode HardCodeBOL = new BOLHardCode();

                string strPageNo = Request["PageNo"];
                if (strPageNo != "" && strPageNo != null)
                    PageNo = Convert.ToInt32(strPageNo);

                int? CompanyCode = null;
                string strCompanyCode = Request["CompanyCode"];
                if (!string.IsNullOrEmpty(strCompanyCode))
                {
                    CompanyCode = Convert.ToInt32(strCompanyCode);
                    HardCodeBOL.TableOrViewName = "HCCompanies";
                    ltrHeader.Text = "محصولات شرکت " + HardCodeBOL.GetNameByCode((int)CompanyCode);
                }

                BOLProducts ProductsBOL = new BOLProducts();
                IQueryable<vProducts> ItemList = ProductsBOL.GetList(PageNo, ItemPerPage, Keyword, CatCode, CompanyCode, SortType, AscDesc);
                rptProducts.DataSource = ItemList;
                rptProducts.DataBind();

                int ResultCount = ProductsBOL.GetListCount(Keyword, CatCode, CompanyCode);
                if (ResultCount == 0)
                {
                    msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msg.Text = "هیچ محصولی یافت نشد";
                }


                int PageCount = ResultCount / ItemPerPage;
                if (ResultCount % ItemPerPage > 0)
                    PageCount++;

                if (!string.IsNullOrEmpty(Keyword))
                    Page.Title = ltrHeader.Text = "نتایج جستجو برای " + Keyword;

                ConcatUrl += "&CatCode=" + Request["CatCode"] + "&Keyword=" + Keyword + "&SortType=" + SortType + "&AscDesc=" + AscDesc + "&ItemPerPage=" + ItemPerPage;
                pgrToolbar.PageNo = PageNo;
                pgrToolbar.PageCount = PageCount;
                pgrToolbar.ConcatUrl = ConcatUrl;
                pgrToolbar.PageBind();

                if (!_showPagination)
                    pgrToolbar.Visible = false;

            }
            catch(Exception err)
            {
                Response.Write(err.Message);
            }
        }

        

        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Keyword = null;
            if(ViewState["Keyword"] != null)
                Keyword = ViewState["Keyword"].ToString();
            int? CatCode = null;
            if(ViewState["CatCode"] != null)
                CatCode = Convert.ToInt32( ViewState["CatCode"]);

            int SortType = Convert.ToInt32( ddlSort.SelectedValue);
            int ItemPerPage = Convert.ToInt32(ViewState["ItemPerPage"]);

            ViewState["SortType"] = SortType;
            int AscDesc = Convert.ToInt32( ViewState["AscDesc"]) ;

            LoadProducts(ItemPerPage, Keyword, CatCode, SortType, AscDesc);
        }

        protected void ddlAscDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Keyword = null;
            if (ViewState["Keyword"] != null)
                Keyword = ViewState["Keyword"].ToString();
            int? CatCode = null;
            if (ViewState["CatCode"] != null)
                CatCode = Convert.ToInt32(ViewState["CatCode"]);

            int SortType = Convert.ToInt32(ViewState["SortType"]);

            int AscDesc = Convert.ToInt32(ddlAscDesc.SelectedValue);
            ViewState["AscDesc"] = AscDesc;
            int ItemPerPage = Convert.ToInt32(ViewState["ItemPerPage"]);

            LoadProducts(ItemPerPage, Keyword, CatCode, SortType, AscDesc);

        }

        protected void ddlItemPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Keyword = null;
            if (ViewState["Keyword"] != null)
                Keyword = ViewState["Keyword"].ToString();
            int? CatCode = null;
            if (ViewState["CatCode"] != null)
                CatCode = Convert.ToInt32(ViewState["CatCode"]);

            int SortType = Convert.ToInt32(ViewState["SortType"]);

            int AscDesc = Convert.ToInt32(ddlAscDesc.SelectedValue);
            ViewState["AscDesc"] = AscDesc;
            int ItemPerPage = Convert.ToInt32(ddlItemPerPage.SelectedValue);
            ViewState["ItemPerPage"] = ItemPerPage;

            LoadProducts(ItemPerPage, Keyword, CatCode, SortType, AscDesc);

        }

        public void ShowHomeProducts()
        {
            BOLProducts ProductsBOL = new BOLProducts();
            rptProducts.DataSource = ProductsBOL.ShowHomeProducts();
            rptProducts.DataBind();
        }

        public void ShowSpecialProducts()
        {
            IsHomeProduct = true;
            BOLProducts ProductsBOL = new BOLProducts();
            rptProducts.DataSource = ProductsBOL.ShowSpecialProducts();
            rptProducts.DataBind();
            ltrHeader.Text = "پیشنهادات ویژه";
        }
        public void ShowNewProducts()
        {
            IsHomeProduct = true;
            BOLProducts ProductsBOL = new BOLProducts();
            rptProducts.DataSource = ProductsBOL.ShowNewProducts();
            rptProducts.DataBind();
            ltrHeader.Text = "جدیدترین ها";
        }
        public void ShowDiscountProducts()
        {
            IsHomeProduct = true;
            BOLProducts ProductsBOL = new BOLProducts();
            rptProducts.DataSource = ProductsBOL.ShowDiscountProducts();
            rptProducts.DataBind();
            ltrHeader.Text = "تخفیف ها";
        }
        public void ShowMostSoldProducts()
        {
            IsHomeProduct = true;
            BOLProducts ProductsBOL = new BOLProducts();
            rptProducts.DataSource = ProductsBOL.ShowMostSoldProducts();
            rptProducts.DataBind();
            ltrHeader.Text = "پرفروش ترین ها";
        }


    }
}