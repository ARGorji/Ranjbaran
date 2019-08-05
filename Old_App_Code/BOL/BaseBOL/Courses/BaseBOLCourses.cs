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
/// BaseBOLCourses is the base class of Courses
/// </summary>
public class BaseBOLCourses : Courses, IBaseBOL<Courses>
{
    
    
    public BaseBOLCourses()
    {
        dataContext = new CoursesDataContext();
    }
    private string TableOrViewName="vCourses";
    public string BaseID = "Courses";
    protected CoursesDataContext dataContext;
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

    
    Courses IBaseBOL<Courses>.GetDetails(int Code)
    {
        return dataContext.Courses.Single(p => p.Code.Equals(Code));
    }

    public int SaveChanges(bool IsNewRecord)
    {
        string NewsTitle = "";
        Courses ObjTable;
        if (IsNewRecord)
        {
            ObjTable = new Courses();
            dataContext.Courses.InsertOnSubmit(ObjTable);
        }
        else
        {
            ObjTable = dataContext.Courses.Single(p => p.Code.Equals(this.Code));
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

                if (Property == "Title")
                    NewsTitle = ((TextBox)wc).Text;
            }
            #endregion

        if (tools.HasAccess("Edit", "Courses"))
            	dataContext.SubmitChanges();
        }
        catch (Exception exp)
        {
            throw exp;
        }

        if (IsNewRecord)
        {
            BOLNews NewsBOL = new BOLNews();
            NewsBOL.Insert(NewsTitle, "Courses.aspx");
        }

        return ObjTable.Code;
    }
    #region IBaseBOL Members
    string IBaseBOL.EditForm
    {
        get { return "Courses/EditCourses.aspx"; }
    }
    string IBaseBOL.ViewForm
    {
        get { return "Courses/ViewCourses.aspx";  }
    }
    string IBaseBOL.PageLable
    {
        get { return "کلاسها"; }
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
        var ListResult = dataContext.ExecuteQuery<vCourses>(string.Format("exec spGetPaged '{0}','{1}','{2}','{3}',N'{4}'", TableOrViewName, SortString, PageSize, CurrentPage, WhereCond));
        DataTable dt = new Converter<vCourses>().ToDataTable(ListResult);
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
     if (tools.HasAccess("Edit", "Courses"))
        {
			Courses ObjTable = dataContext.Courses.Single(p => p.Code.Equals(DelParam[0]));
			dataContext.Courses.DeleteOnSubmit(ObjTable);
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
        dataCell = new DataCell("DurationLen", "طول", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Fee", "شهریه", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("GradeName", "مقطع تحصیلی", AlignTypes.Right, 200);
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
        dataCell = new DataCell("DurationLen", "طول", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("Fee", "شهریه", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
        CellCol.Add(dataCell);
        dataCell = new DataCell("GradeName", "مقطع تحصیلی", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode=DisplayModes.Visible;
        CellCol.Add(dataCell);

        dataCell = new DataCell("UserCount", "تعداد ثبت نام", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);

        dataCell = new DataCell("MainField", "فیلد", AlignTypes.Right, 200);
        dataCell.IsListTitle = true;
        dataCell.DisplayMode = DisplayModes.Visible;
        CellCol.Add(dataCell);


        #endregion
        return CellCol;
    }
}
