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
using System.Collections.Generic;
using System.Reflection;
using Ranjbaran.Old_App_Code.DAL;


public class BaseBOLUserTransactions : UserTransactions, IBaseBOL<UserTransactions>
{
    public int MasterCode;
    protected UsersDataContext dataContext;
    protected string TableOrViewName = "vUserTransactions";
    public string BaseID = "UserTransactions";
    public BaseBOLUserTransactions(params int[] MCodes)
    {
        MasterCode = Convert.ToInt32(MCodes[0]);
        dataContext = new UsersDataContext();
    }

    string IBaseBOL.QueryObjName
    {
        get
        {
            return TableOrViewName;
        }
        set
        {
            TableOrViewName = value;
        }
    }

    public List<WebControl> ObjectList;

    public UserTransactions GetDetails(int Code)
    {
        return dataContext.UserTransactions.Single(p => p.Code.Equals(Code));
    }
    public int SaveChanges(bool IsNewRecord)
    {
        UserTransactions ObjTable;
        if (IsNewRecord)
        {
            ObjTable = new UserTransactions();
            dataContext.UserTransactions.InsertOnSubmit(ObjTable);
        }
        else
        {
            ObjTable = dataContext.UserTransactions.Single(p => p.Code.Equals(this.Code));
        }
        try
        {

            #region Save Detail Controls
            PropertyInfo piMasterCode = ObjTable.GetType().GetProperty("UserCode");
            piMasterCode.SetValue(ObjTable, MasterCode, new object[] { });

            string BaseID = this.ToString().Substring(3, this.ToString().Length - 3);
            Tools tools = new Tools();
            tools.AccessList = tools.GetAccessList(BaseID);
            foreach (WebControl wc in ObjectList)
            {
                string Property = wc.ID.Substring(3, wc.ID.Length - 3);
                PropertyInfo pi = ObjTable.GetType().GetProperty(Property);
                string FullPropName = BaseID + "." + Property;
                if (tools.HasAccess("Edit", BaseID + "." + Property))
                    pi.SetValue(ObjTable, Tools.GetControlValue(wc), new object[] { });
            }
            #endregion

            dataContext.SubmitChanges();
        }
        catch (Exception exp)
        {
            throw exp;
        }

        return ObjTable.Code;
    }
    #region IBaseBOL Members
    string IBaseBOL.EditForm
    {
        get { return "Users/EditUserTransactions.aspx"; }
    }
    string IBaseBOL.ViewForm
    {
        get { return ""; }
    }
    string IBaseBOL.PageLable
    {
        get { return "تراکنش های کاربر"; }
    }
    int IBaseBOL.EditWidth
    {
        get { return 700; }
    }
    int IBaseBOL.EditHeight
    {
        get { return 380; }
    }

    DataTable IBaseBOL.GetDataSource(SearchFilterCollection sFilterCols, string SortString, int PageSize, int CurrentPage)
    {
        string WhereCond;
        WhereCond = Tools.GetCondition(sFilterCols);
        if (WhereCond == "")
            WhereCond = " UserCode = " + MasterCode;
        else
            WhereCond = " UserCode = " + MasterCode + " and " + WhereCond;

        var ListResult = dataContext.ExecuteQuery<vUserTransactions>(string.Format("exec spGetPaged '{0}','{1}','{2}','{3}','{4}'", TableOrViewName, SortString, PageSize, CurrentPage, WhereCond));
        DataTable dt = new Converter<vUserTransactions>().ToDataTable(ListResult);
        return dt;
    }
    public int GetCount(SearchFilterCollection sFilterCols)
    {
        string WhereCond;
        int RecordCount = 1;
        DBToolsDataContext dcTools = new DBToolsDataContext();
        WhereCond = Tools.GetCondition(sFilterCols);
        if (WhereCond == "")
            WhereCond = " UserCode = " + MasterCode;
        else
            WhereCond = " UserCode = " + MasterCode + " and " + WhereCond;

        WhereCond = WhereCond.Replace("''", "'");
        var ResultQuery = dcTools.spGetCount(TableOrViewName, WhereCond);

        RecordCount = (int)ResultQuery.ReturnValue;
        return RecordCount;
    }

    void IBaseBOL.DeleteRecord(params string[] DelParam)
    {
        Tools tools = new Tools();
        tools.AccessList = tools.GetAccessList(BaseID);
        if (tools.HasAccess("Edit", "UserTransactions"))
        {
            UserTransactions ObjTable = dataContext.UserTransactions.Single(p => p.Code.Equals(DelParam[0]));
            dataContext.UserTransactions.DeleteOnSubmit(ObjTable);
            dataContext.SubmitChanges();
        }
    }

    CellCollection IBaseBOL.GetCellCollection()
    {
        return GetCellCollection();
    }
    CellCollection IBaseBOL.GetListCellCollection()
    {
        DataCell dataCell;
        CellCollection CellCol = new CellCollection();

        #region Code Cell
        dataCell = new DataCell();
        dataCell.CaptionName = "Code";
        dataCell.IsKey = true;
        dataCell.DisplayMode = DisplayModes.Hidden;
        dataCell.Align = AlignTypes.Right;
        dataCell.FieldName = "Code";
        dataCell.MaxLength = 100;
        dataCell.Width = 50;
        CellCol.Add(dataCell);
        #endregion

        #region Data Cells
        dataCell = new DataCell("UserCode", "کد کاربر", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Hidden;
        CellCol.Add(dataCell);
        dataCell = new DataCell("HCGenderCode", "کد جنسیت", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("FirstName", "نام", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("LastName", "نام خانوادگی", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Email", "ایمیل", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("TransStatus", "وضعیت", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("BankName", "بانک", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("DigitalSignature", "امضا", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("UserIP", "آی پی", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Authority", "کد پارسیان", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("HCTransStatusCode", "کد وضعیت", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("ChrgDate", "تاریخ شمسی", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("HCPayReasonCode", "کد دلیل پرداخت", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("HCPayMethodCode", "کد روش پرداخت", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Amount", "مبلغ", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("PayMethod", "روش پرداخت", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("PayReason", "دلیل پرداخت", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);

        #endregion
        return CellCol;
    }
    #endregion
    protected CellCollection GetCellCollection()
    {
        DataCell dataCell;
        CellCollection CellCol = new CellCollection();

        #region Code Cell
        dataCell = new DataCell();
        dataCell.CaptionName = "Code";
        dataCell.IsKey = true;
        dataCell.DisplayMode = DisplayModes.Hidden;
        dataCell.Align = AlignTypes.Right;
        dataCell.FieldName = "Code";
        dataCell.Width = 50;
        CellCol.Add(dataCell);
        #endregion

        #region Data Cells
        dataCell = new DataCell("UserCode", "کد کاربر", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Hidden;
        CellCol.Add(dataCell);
        dataCell = new DataCell("FirstName", "نام", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("LastName", "نام خانوادگی", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Email", "ایمیل", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("TransStatus", "وضعیت", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Amount", "مبلغ", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("ChrgDate", "تاریخ", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);

        #endregion
        return CellCol;
    }
}
