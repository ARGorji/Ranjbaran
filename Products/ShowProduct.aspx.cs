using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;
using System.Data;
using AKP.Web.Controls;

namespace Ranjbaran.ProductsFolder
{
    public partial class ShowProduct : System.Web.UI.Page
    {
        public string strHirarchy = "";
        public string ProTitle = "";
        public string strCode = "";
        public string strEnTitle = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserCode"] == null)
            {
            }
            else
            {
            }

            if (!Page.IsPostBack)
            {


                if (Request["RefUserCode"] != null)
                    Session["RefUserCode"] = Request["RefUserCode"];

                strCode = Request["Code"];
                int Code;
                Int32.TryParse(strCode, out Code);
                if (Code != 0)
                {
                    

                    BOLProducts ProductsBOL = new BOLProducts();
                    Products CurProduct = ((IBaseBOL<Products>)ProductsBOL).GetDetails(Code);
                    if (CurProduct != null)
                    {
                    
                        
                        ViewState["Code"] = Code.ToString();
                        if (Request.UserAgent == "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)" ||
                            Request.UserAgent == "Mozilla/5.0 (compatible; Yahoo! Slurp; http://help.yahoo.com/help/us/ysearch/slurp)" ||
                            Request.UserAgent == "msnbot/2.0b (+http://search.msn.com/msnbot.htm)._" ||
                            Request.UserAgent == "Mozilla/5.0 (compatible; bingbot/2.0; +http://www.bing.com/bingbot.htm)" ||
                            Request.UserAgent == "Mozilla/5.0 (en-us) AppleWebKit/525.13 (KHTML, like Gecko; Google Web Preview) Version/3.1 Safari/525.13" ||
                            Request.UserAgent == "Mozilla/5.0 (compatible; MJ12bot/v1.3.3; http://www.majestic12.co.uk/bot.php?+)"
                            )
                        {
                            int ff = 1;
                        }
                        else
                        {
                            ProductsBOL.IncrementView(CurProduct.Code);
                            ProductsBOL.IncProVisits(CurProduct.Code);
                        }


                        hplBuy.NavigateUrl = "~/Cart.aspx?ProductCode=" + Code;
                        lblEnTitle.Text = CurProduct.EnTitle;
                        lblFaTitle.Text = CurProduct.FaTitle;

                        strEnTitle = CurProduct.EnTitle;
                        lblDescription.Text = Tools.FormatString(CurProduct.Description);
                        lblPrice.Text = Tools.ChangeEnc(Tools.FormatCurrency( (Convert.ToInt32( CurProduct.Price) / 10).ToString())) + " تومان";

                        int? MarketPrice = CurProduct.MarketPrice;
                        if (MarketPrice != null)
                        {
                            if (MarketPrice > CurProduct.Price)
                            {
                                lblMarketPrice.Text = Tools.ChangeEnc(Tools.FormatCurrency( (Convert.ToInt32( CurProduct.MarketPrice) / 10).ToString())) + " تومان";
                                pnlMarketPrice.Visible = true;
                            }
                        }

                        ProTitle = CurProduct.FaTitle;
                        if (string.IsNullOrEmpty(CurProduct.FaTitle))
                            ProTitle = CurProduct.EnTitle;

                        ReqUtils Utils = new ReqUtils();
                        string FullDesc = CurProduct.Description;
                        string BriefDesc = Tools.ShowBriefText(Utils.RemoveTags(FullDesc), 300);



                        Page.Title = ProTitle;


                        //if (CurProduct.ProductCatCode != null)
                        //{
                        //    SelectedProducts1.CatCode = (int)CurProduct.ProductCatCode;
                        //    RelatedProducts1.ProductCode = CurProduct.Code;
                        //    RelatedProducts1.ShowSelectedProducts();

                        //}
                        //else
                        //    SelectedProducts1.Visible = false;




                        BOLHardCode HardCodeBOL = new BOLHardCode();

                        ltrHirarchy.Text = strHirarchy;

                        #region Fill Fields
                        #endregion


                        #region Visibility

                        #endregion


                        #region Box Visibility




                        #endregion



                        //string[] ProTitleArray = ProTitle.Split(' ');


                    }

                }



            }

        }

 
        public string ShowItem(Object obj)
        {
            string Result = "";
            try
            {
                if (obj != null)
                {
                    Result = Convert.ToString(obj);
                    if (Result.Trim() == "")
                        Result = "نامشخص";
                }
                return Result;
            }
            catch
            {
                return "";
            }
        }
        public string ShowDate(Object obj)
        {
            string Result = "";
            try
            {
                if (obj != null)
                {
                    DateTime dtCommentDate = Convert.ToDateTime(obj);
                    DateTimeMethods dtm = new DateTimeMethods();
                    Result = dtm.GetPersianDate(dtCommentDate);
                    Result = Tools.ChangeEnc(Result);
                }
                return Result;
            }
            catch
            {
                return "";
            }
        }
        

        public string FormatText(Object obj)
        {
            if (obj == null)
                return "";
            ReqUtils rUtil = new ReqUtils();
            return Tools.ShowBriefText(rUtil.RemoveTags(obj.ToString()), 15);

        }

        public string FormatText2(Object obj, int CharCount)
        {
            if (obj == null)
                return "";
            ReqUtils rUtil = new ReqUtils();
            return Tools.ShowBriefText(rUtil.RemoveTags(obj.ToString()), CharCount);

        }

        public string ShowPic(object Title, object PicName)
        {
            string Result = "";
            if (PicName != null && PicName != "")
                Result = "<img class=\"cPic3\" alt=\"" + Title + "" + Title + "\" src=\"" + ((Page)HttpContext.Current.Handler).ResolveUrl("~/" + PicName) + "\" />";
            return Result;
        }

        public string ShowNum(Object obj)
        {
            string Result = "";
            if (obj != null)
            {
                int intCount = Convert.ToInt32(obj);
                if (intCount != 0)
                    Result = "(" + Tools.ChangeEnc(intCount.ToString()) + ")";
            }
            return Result;
        }

        

        
    }
}