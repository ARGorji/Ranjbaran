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


public class BaseBOLUserAddresses : UserAddresses, IBaseBOL<UserAddresses>
{
    public int MasterCode;
    protected UsersDataContext dataContext;
    protected string TableOrViewName = "vUserAddresses";
    public string BaseID = "UserAddresses";
    public BaseBOLUserAddresses(params int[] MCodes)
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

    public UserAddresses GetDetails(int Code)
    {
        return dataContext.UserAddresses.Single(p => p.Code.Equals(Code));
    }
    public int SaveChanges(bool IsNewRecord)
    {
        UserAddresses ObjTable;
        if (IsNewRecord)
        {
            ObjTable = new UserAddresses();
            dataContext.UserAddresses.InsertOnSubmit(ObjTable);
        }
        else
        {
            ObjTable = dataContext.UserAddresses.Single(p => p.Code.Equals(this.Code));
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
        get { return "AccessLevel/EditUserAddresses.aspx"; }
    }
    string IBaseBOL.ViewForm
    {
        get { return ""; }
    }
    string IBaseBOL.PageLable
    {
        get { return "آدرس های کاربر"; }
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

        var ListResult = dataContext.ExecuteQuery<vUserAddresses>(string.Format("exec spGetPaged '{0}','{1}','{2}','{3}','{4}'", TableOrViewName, SortString, PageSize, CurrentPage, WhereCond));
        DataTable dt = new Converter<vUserAddresses>().ToDataTable(ListResult);
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
        if (tools.HasAccess("Edit", "UserAddresses"))
        {
            UserAddresses ObjTable = dataContext.UserAddresses.Single(p => p.Code.Equals(DelParam[0]));
            dataContext.UserAddresses.DeleteOnSubmit(ObjTable);
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
        dataCell = new DataCell("UserCode", "کد پدر", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Hidden;
        CellCol.Add(dataCell);
        dataCell = new DataCell("FullName", "نام گیرنده", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Address", "آدرس", AlignTypes.Right, 200);
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
        dataCell = new DataCell("UserCode", "کد پدر", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Hidden;
        CellCol.Add(dataCell);
        dataCell = new DataCell("FullName", "نام گیرنده", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Address", "آدرس", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);

        #endregion
        return CellCol;
    }
}
