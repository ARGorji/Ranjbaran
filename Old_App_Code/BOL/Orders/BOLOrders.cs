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

public class BOLOrders : BaseBOLOrders, IBaseBOL<Orders>
{
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

    public int InsertRecord(string FullName, string Email, int CityCode, int ProvinceCode, string Tel, string CellPhone, string PostalCode, string Address,
                            int HCGenderCode, string Description, int HCSendTypeCode, int HCOrderStatusCode, int Discount, int SendPrice, int TotalAmount,
                            int OtherCosts, int TotalOrderCost, string OrderID, bool RefPayed, int? RefUserCode, int UserCode, int HCPayMethodCode)
    {
        Orders NewOrder = new Orders();
        dataContext.Orders.InsertOnSubmit(NewOrder);
        NewOrder.FullName = FullName;
        NewOrder.Email = Email;
        NewOrder.CityCode = CityCode;
        NewOrder.ProvinceCode = ProvinceCode;
        NewOrder.Tel = Tel;
        NewOrder.CellPhone = CellPhone;
        NewOrder.PostalCode = PostalCode;
        NewOrder.Address = Address;
        NewOrder.HCGenderCode = HCGenderCode;
        NewOrder.Description = Description;
        NewOrder.HCSendTypeCode = HCSendTypeCode;// HCSendTypeCode; Pishtaz
        NewOrder.Discount = Discount;
        NewOrder.TotalProductPrice = TotalAmount;
        NewOrder.CreateDate = CreateDate;
        NewOrder.HCOrderStatusCode = HCOrderStatusCode;
        NewOrder.SendPrice = SendPrice;
        NewOrder.TotalOrderCost = TotalOrderCost;
        NewOrder.OtherCosts = OtherCosts;
        NewOrder.UserCode = UserCode;
        NewOrder.ID = OrderID;
        NewOrder.HCPayMethodCode = HCPayMethodCode;
        NewOrder.HCOrderPayStatusCode = HCOrderPayStatusCode;
        NewOrder.RefPayed = RefPayed;
        NewOrder.RefUserCode = RefUserCode;
        NewOrder.CreateDate = DateTime.Now;

        dataContext.SubmitChanges();
        return NewOrder.Code;

    }

    public object GetOrderByStatus(int StatusCode)
    {
        return dataContext.vOrders.Where(p => p.HCOrderStatusCode.Equals(StatusCode));
    }

    internal void UpdateResult(int OrderCode, string PayeganOrderCode, string PostOrderCode)
    {
        Orders CurOrder = dataContext.Orders.SingleOrDefault(p => p.Code.Equals(OrderCode));
        CurOrder.PayeganOrderCode = PayeganOrderCode;
        CurOrder.PostOrderCode = PostOrderCode;
        dataContext.SubmitChanges();
    }

    internal void UpdatePayStatus(int OrderCode, int HCOrderPayStatusCode)
    {
        Orders CurOrder = dataContext.Orders.SingleOrDefault(p => p.Code.Equals(OrderCode));
        CurOrder.HCOrderPayStatusCode = HCOrderPayStatusCode;
        dataContext.SubmitChanges();
    }

    internal void UpdateOrderStatus(int Code, int HCStatusCode)
    {
        Orders CurOrder = dataContext.Orders.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurOrder != null)
        {
            CurOrder.HCOrderStatusCode = HCStatusCode;
            dataContext.SubmitChanges();
        }
    }

    internal IQueryable<vOrders> SearchOrders(string Buyer, int? HCOrderStatusCode, int ShopCode, string PostOrderCode, string PayeganOrderCode, DateTime FromDate, DateTime ToDate, int PageSize, int PageNo)
    {
        IQueryable<vOrders> Result = dataContext.vOrders.Where(p => p.CreateDate.Value.CompareTo(FromDate) >= 1 && p.CreateDate.Value.CompareTo(ToDate) <= 1);
        if (!string.IsNullOrEmpty(Buyer))
            Result = Result.Where(p => p.FullName.Contains(Buyer));
        if (HCOrderStatusCode != null)
            Result = Result.Where(p => p.HCOrderStatusCode.Equals(HCOrderStatusCode));
        if (!string.IsNullOrEmpty(PostOrderCode))
            Result = Result.Where(p => p.PostOrderCode.Equals(PostOrderCode));
        if (!string.IsNullOrEmpty(PayeganOrderCode))
            Result = Result.Where(p => p.PayeganOrderCode.Equals(PayeganOrderCode));
        if (!string.IsNullOrEmpty(PayeganOrderCode))
            Result = Result.Where(p => p.PayeganOrderCode.Equals(PayeganOrderCode));

        int SkipCount = (PageNo - 1) * PageSize;
        return Result.OrderByDescending(p => p.Code).Skip(SkipCount).Take(PageSize);
    }

    internal int GetSearchOrdersCount(string Buyer, int? HCOrderStatusCode, int ShopCode, string PostOrderCode, string PayeganOrderCode, DateTime FromDate, DateTime ToDate)
    {
        IQueryable<vOrders> Result = dataContext.vOrders.Where(p => p.CreateDate.Value.CompareTo(FromDate) >= 1 && p.CreateDate.Value.CompareTo(ToDate) <= 1);
        if (!string.IsNullOrEmpty(Buyer))
            Result = Result.Where(p => p.FullName.Contains(Buyer));
        if (HCOrderStatusCode != null)
            Result = Result.Where(p => p.HCOrderStatusCode.Equals(HCOrderStatusCode));
        if (!string.IsNullOrEmpty(PostOrderCode))
            Result = Result.Where(p => p.PostOrderCode.Equals(PostOrderCode));
        if (!string.IsNullOrEmpty(PayeganOrderCode))
            Result = Result.Where(p => p.PayeganOrderCode.Equals(PayeganOrderCode));
        if (!string.IsNullOrEmpty(PayeganOrderCode))
            Result = Result.Where(p => p.PayeganOrderCode.Equals(PayeganOrderCode));
        return Result.Count();
    }

    internal vOrders GetOrderByPostCode(string strPostCode)
    {
        return dataContext.vOrders.SingleOrDefault(p => p.PostOrderCode.Equals(strPostCode));
    }

    internal void UpdateRefPayStatus(int Code, bool RefPayed)
    {
        Orders CurOrder = dataContext.Orders.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurOrder != null)
        {
            CurOrder.RefPayed = RefPayed;
            dataContext.SubmitChanges();
        }
    }

    internal vOrders GetOrderByID(string OrderID)
    {
        return dataContext.vOrders.SingleOrDefault(p => p.ID.Equals(OrderID));
    }

    internal object GetOrdersByUserCode(int UserCode)
    {
        return dataContext.vOrders.Where(p => p.UserCode.Equals(UserCode)).OrderByDescending(p => p.Code);
    }

    internal vOrders GetFullDetails(int Code)
    {
        return dataContext.vOrders.SingleOrDefault(p => p.Code.Equals(Code));
    }

    internal string GetInternalOrderIDByTransCode(int UserTransactionCode)
    {
        vOrders CurOrder = dataContext.vOrders.SingleOrDefault(p => p.UserTransactionCode.Equals(UserTransactionCode));
        if (CurOrder != null)
            return CurOrder.ID;
        else
            return "";
    }

    public bool UpdateTransactionCode(int OrderCode, int UserTransactionCode)
    {
        try
        {
            OrdersDataContext dc = new OrdersDataContext();
            Orders CurOrder = dc.Orders.SingleOrDefault(p => p.Code.Equals(OrderCode));
            CurOrder.UserTransactionCode = UserTransactionCode;

            dc.SubmitChanges();
            return true;
        }
        catch (Exception err)
        {
            HttpContext.Current.Response.Write(err.Message);
            return false;
        }
    }
}
