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
using Ranjbaran.Old_App_Code.DAL;
using System.Collections.Generic;
using System.Reflection;
/// <summary>
/// BaseBOLGalleries is the base class of Galleries
/// </summary>
public class BaseBOLGalleries : Galleries, IBaseBOL<Galleries>
{
    
    
    public BaseBOLGalleries()
    {
        dataContext = new GalleriesDataContext();
    }
    private string TableOrViewName="vGalleries";
    public string BaseID = "Galleries";
    protected GalleriesDataContext dataContext;
    public List<WebControl> ObjectList;
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

    
    Galleries IBaseBOL<Galleries>.GetDetails(int Code)
    {
        return dataContext.Galleries.Single(p => p.Code.Equals(Code));
    }

    public int SaveChanges(bool IsNewRecord)
    {
     
        Galleries ObjTable;
        if (IsNewRecord)
        {
            ObjTable = new Galleries();
            dataContext.Galleries.InsertOnSubmit(ObjTable);
        }
        else
        {
            ObjTable = dataContext.Galleries.Single(p => p.Code.Equals(this.Code));
        }
        try
        {
            #region Save Controls
            string BaseID = this.ToString().Substring(3,this.ToString().Length - 3);
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

        if (tools.HasAccess("Edit", "Galleries"))
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
        get { return "Galleries/EditGalleries.aspx"; }
    }
    string IBaseBOL.ViewForm
    {
        get { return "Galleries/ViewGalleries.aspx";  }
    }
    string IBaseBOL.PageLable
    {
        get { return "گالری تصاویر"; }
    }
    int IBaseBOL.EditWidth
    {
        get { return 500; }
    }
    int IBaseBOL.EditHeight
    {
        get { return 180; }
    }
    DataTable IBaseBOL.GetDataSource(SearchFilterCollection sFilterCols, string SortString, int PageSize, int CurrentPage)
    {
        string WhereCond = Tools.GetCondition(sFilterCols);
        var ListResult = dataContext.ExecuteQuery<vGalleries>(string.Format("exec spGetPaged '{0}','{1}','{2}','{3}',N'{4}'", TableOrViewName, SortString, PageSize, CurrentPage, WhereCond));
        DataTable dt = new Converter<vGalleries>().ToDataTable(ListResult);
        return dt;
    }
    int IBaseBOL.GetCount(SearchFilterCollection sFilterCols)
    {
        int RecordCount = 1;
        string WhereCond = Tools.GetCondition(sFilterCols).Replace("''", "'");
        var ResultQuery = new DBToolsDataContext().spGetCount(TableOrViewName, WhereCond);
        
        RecordCount = (int)ResultQuery.ReturnValue;
        return RecordCount;
    }

    void IBaseBOL.DeleteRecord(params string[] DelParam)
    {
     Tools tools = new Tools();
     tools.AccessList = tools.GetAccessList(BaseID);
     if (tools.HasAccess("Edit", "Galleries"))
        {
			Galleries ObjTable = dataContext.Galleries.Single(p => p.Code.Equals(DelParam[0]));
			dataContext.Galleries.DeleteOnSubmit(ObjTable);
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
        dataCell = new DataCell("Title", "عنوان", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("PicFile", "عکس", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
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
        dataCell = new DataCell("Title", "عنوان", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("PicFile", "عکس", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
        CellCol.Add(dataCell);
                
        #endregion
        return CellCol;
    }
}
