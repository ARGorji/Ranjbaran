using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using Ranjbaran.Old_App_Code.DAL;

public class BOLOrderProducts : BaseBOLOrderProducts, IBaseBOL<OrderProducts>
{
    public BOLOrderProducts(params int[] MCodes) : base(MCodes)
    {

    }
    public IList CheckBusinessRules()
    {
        var messages = new List<string>();

        #region Business Rules
        //Example
        //if (string.IsNullOrEmpty(this.FirstName))
        //    messages.Add("Please fill FirstName.");

        #endregion
        return messages;
    }

    public int InsertRecord(int OrderCode, int ProductCode, int ProductPrice, int HCProductOrderStatusCode, int ItemCount)
    {
        OrderProducts NewOrderProduct = new OrderProducts();
        dataContext.OrderProducts.InsertOnSubmit(NewOrderProduct);
        NewOrderProduct.OrderCode = OrderCode;
        NewOrderProduct.ProductCode = ProductCode;
        NewOrderProduct.ProductPrice = ProductPrice;
        NewOrderProduct.ItemCount = ItemCount;
        NewOrderProduct.HCProductOrderStatusCode = HCProductOrderStatusCode;
        dataContext.SubmitChanges();
        return NewOrderProduct.Code;
    }
}
