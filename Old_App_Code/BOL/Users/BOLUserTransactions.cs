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

public class BOLUserTransactions : BaseBOLUserTransactions, IBaseBOL<UserTransactions>
{
    public BOLUserTransactions(params int[] MCodes)
        : base(MCodes)
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

    internal int Insert(int? UserCode, DateTime? PayDate, int HCTransStatusCode, int? HCPayMethodCode, string UserIP, int Amount, int HCPayReasonCode, int? BankCode, string ItemType, int ItemCode, out string errMessage)
    {
        try
        {

            UserTransactions NewRecord = new UserTransactions();
            dataContext.UserTransactions.InsertOnSubmit(NewRecord);
            if (UserCode != null)
                NewRecord.UserCode = (int)UserCode;
            if (PayDate != null)
                NewRecord.PayDate = (DateTime)PayDate;
            NewRecord.HCTransStatusCode = HCTransStatusCode;
            NewRecord.UserIP = UserIP;
            NewRecord.Amount = Amount;
            NewRecord.BankCode = BankCode;
            NewRecord.ItemType = ItemType;
            NewRecord.ItemCode = ItemCode;

            if (HCPayMethodCode != null)
                NewRecord.HCPayMethodCode = (int)HCPayMethodCode;
            NewRecord.HCPayReasonCode = (int)HCPayReasonCode;
            dataContext.SubmitChanges();
            errMessage = "";

            return NewRecord.Code;
        }
        catch (Exception err)
        {
            errMessage = err.Message;
            //HttpContext.Current.Response.Write(err.Message);
            return -1;
        }
    }


    internal int Insert(int? UserCode, DateTime? PayDate, int HCTransStatusCode, int? HCPayMethodCode, string UserIP, int Amount, int HCPayReasonCode, int? BankCode, string ItemType, int ItemCode, out string errMessage, string CardNumberMasked, long RRN)
    {
        try
        {

            UserTransactions NewRecord = new UserTransactions();
            dataContext.UserTransactions.InsertOnSubmit(NewRecord);
            if (UserCode != null)
                NewRecord.UserCode = (int)UserCode;
            if (PayDate != null)
                NewRecord.PayDate = (DateTime)PayDate;
            NewRecord.HCTransStatusCode = HCTransStatusCode;
            NewRecord.UserIP = UserIP;
            NewRecord.Amount = Amount;
            NewRecord.BankCode = BankCode;
            NewRecord.ItemType = ItemType;
            NewRecord.ItemCode = ItemCode;
            NewRecord.CardNumberMasked = CardNumberMasked;
            NewRecord.RRN = RRN;

            if (HCPayMethodCode != null)
                NewRecord.HCPayMethodCode = (int)HCPayMethodCode;
            NewRecord.HCPayReasonCode = (int)HCPayReasonCode;
            dataContext.SubmitChanges();
            errMessage = "";

            return NewRecord.Code;
        }
        catch (Exception err)
        {
            errMessage = err.Message;
            //HttpContext.Current.Response.Write(err.Message);
            return -1;
        }
    }

    internal UserTransactions GetTransactionByOrderID(long OrderId)
    {
        return dataContext.UserTransactions.SingleOrDefault(p => p.Code.Equals(OrderId));
    }

    internal UserTransactions GetTransactionByOrderID(long OrderId, int UserCode)
    {
        return dataContext.UserTransactions.SingleOrDefault(p => p.Code.Equals(OrderId) && p.UserCode.Equals(UserCode));
    }
    internal int UpdateAuthority(int UserTransactionCode, string Authority, out string err)
    {
        try
        {
            UserTransactions CurTrans = dataContext.UserTransactions.SingleOrDefault(p => p.Code.Equals(UserTransactionCode));
            if (CurTrans != null)
            {
                CurTrans.Authority = Authority;
                dataContext.SubmitChanges();
                err = "";
                return 0;
            }
            else
            {
                err = "";
                return -1;
            }
        }
        catch(Exception errmsg)
        {
            err = errmsg.Message;
            return -1;
        }
    }

    internal vUserTransactions GetTransByAuthority(string strAuthority)
    {
        return dataContext.vUserTransactions.SingleOrDefault(p => p.Authority.Equals(strAuthority));
    }

    internal bool ChangeStatus(int Code, int HCTransStatusCode)
    {
        try
        {
            UserTransactions CurTrans = dataContext.UserTransactions.SingleOrDefault(p => p.Code.Equals(Code));
            if (CurTrans != null)
            {
                CurTrans.HCTransStatusCode = HCTransStatusCode;
                dataContext.SubmitChanges();
                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }

    }

    internal object GetUserTrans(int UserCode, int HCTransStatusCode)
    {
        return dataContext.vUserTransactions.Where(p => p.UserCode.Equals(UserCode) && p.HCTransStatusCode.Equals(HCTransStatusCode) && p.Amount > 0).OrderBy(p => p.Code);
    }
}
