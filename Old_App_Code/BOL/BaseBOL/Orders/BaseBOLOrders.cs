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
using System.Web.SessionState;
using System.Collections.Generic;
using System.Reflection;
using Ranjbaran.Old_App_Code.DAL;
/// <summary>
/// BaseBOLOrders is the base class of Orders
/// </summary>
public class BaseBOLOrders : Orders, IBaseBOL<Orders>
{


    public BaseBOLOrders()
    {
        dataContext = new OrdersDataContext();
    }
    private string TableOrViewName = "vOrders";
    public string BaseID = "Orders";
    protected OrdersDataContext dataContext;
    public List<WebControl> ObjectList;
    public string QueryObjName
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


    Orders IBaseBOL<Orders>.GetDetails(int Code)
    {
        return dataContext.Orders.Single(p => p.Code.Equals(Code));
    }

    public int SaveChanges(bool IsNewRecord)
    {

        Orders ObjTable;
        if (IsNewRecord)
        {
            ObjTable = new Orders();
            dataContext.Orders.InsertOnSubmit(ObjTable);
        }
        else
        {
            ObjTable = dataContext.Orders.Single(p => p.Code.Equals(this.Code));
        }
        try
        {
            #region Save Controls
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

            if (tools.HasAccess("Edit", "Orders"))
                dataContext.SubmitChanges();
        }
        catch (Exception exp)
        {
            throw exp;
        }

        return ObjTable.Code;
    }
    #region IBaseBOL Members
    public string EditForm
    {
        get { return "Orders/EditOrders.aspx"; }
    }
    public string ViewForm
    {
        get { return "Orders/ViewOrders.aspx"; }
    }
    public string PageLable
    {
        get { return "سفارشات"; }
    }
    public int EditWidth
    {
        get { return 500; }
    }
    public int EditHeight
    {
        get { return 180; }
    }
    public DataTable GetDataSource(SearchFilterCollection sFilterCols, string SortString, int PageSize, int CurrentPage)
    {
        string WhereCond = Tools.GetCondition(sFilterCols);
        var ListResult = dataContext.ExecuteQuery<vOrders>(string.Format("exec spGetPaged '{0}','{1}','{2}','{3}',N'{4}'", TableOrViewName, SortString, PageSize, CurrentPage, WhereCond));
        DataTable dt = new Converter<vOrders>().ToDataTable(ListResult);
        return dt;
    }
    public int GetCount(SearchFilterCollection sFilterCols)
    {
        int RecordCount = 1;
        string WhereCond = Tools.GetCondition(sFilterCols).Replace("''", "'");
        var ResultQuery = new DBToolsDataContext().spGetCount(TableOrViewName, WhereCond);

        RecordCount = (int)ResultQuery.ReturnValue;
        return RecordCount;
    }

    public void DeleteRecord(params string[] DelParam)
    {
        Tools tools = new Tools();
        tools.AccessList = tools.GetAccessList(BaseID);
        if (tools.HasAccess("Edit", "Orders"))
        {
            Orders ObjTable = dataContext.Orders.Single(p => p.Code.Equals(DelParam[0]));
            dataContext.Orders.DeleteOnSubmit(ObjTable);
            dataContext.SubmitChanges();
        }
    }

    public CellCollection GetListCellCollection()
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
        dataCell = new DataCell("CDate", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("FullName", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("CellPhone", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("City", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("OrderStatus", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("SendType", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("PostalCode", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("OrderPayStatus", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);

        #endregion
        return CellCol;
    }
    #endregion
    public CellCollection GetCellCollection()
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
        dataCell = new DataCell("CDate", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("FullName", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("CellPhone", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("City", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("OrderStatus", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("SendType", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("PostalCode", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("OrderPayStatus", "", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);

        #endregion
        return CellCol;
    }
}
