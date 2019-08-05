using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.ProductsFolder
{
    public partial class Default : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            int SortType = 1;
            int AscDesc = 1;
            int ItemPerPage = 24;

            if (!Page.IsPostBack)
            {
                string Keyword = Request["Keyword"];
                string strCatCode = Request["C"];
                if (!string.IsNullOrEmpty(Keyword))
                    Keyword = Keyword.Replace("'", "");
                int? CatCode = null;
                if (!string.IsNullOrEmpty(strCatCode))
                    CatCode = Convert.ToInt32(strCatCode);

                string strSortType = Request["SortType"];
                string strAscDesc = Request["AscDesc"];
                string strItemPerPage = Request["ItemPerPage"];

                if (!string.IsNullOrEmpty(strSortType))
                    SortType = Convert.ToInt32(strSortType);
                if (!string.IsNullOrEmpty(strAscDesc))
                    AscDesc = Convert.ToInt32(strAscDesc);
                if (!string.IsNullOrEmpty(strItemPerPage))
                    ItemPerPage = Convert.ToInt32(strItemPerPage);


                UCProductList1.LoadProducts(ItemPerPage, Keyword, CatCode, SortType, AscDesc);
            }
        }
    }
}