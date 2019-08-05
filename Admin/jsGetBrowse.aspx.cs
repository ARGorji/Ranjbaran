using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataAccess;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using AKP.Base.Settings;

public partial class jsGetBrowse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //System.Threading.Thread.Sleep(5000);
            string BaseID = Request["BaseID"];
            string OldOrder = Request["OldOrder"];
            string Order = Request["Order"];
            string Repeat = Request["Repeat"];
            string RowsPerPage = Request["RowsPerPage"];
            string CurPage = Request["CurPage"];
            string Keyword = Request["Keyword"];
            string Condition = Request["Condition"];
            string FilterClm = Request["FilterClm"];
            string DelCode = Request["DelCode"];
            string ShowMode = Request["ShowMode"];
            string SearchOperand = Request["SearchOperand"];
            string ViewName = Request["ViewName"];
            if (Keyword != null && Keyword!="")
            Keyword = Tools.PersianTextCorrection(Keyword);
            int MasterCode = string.IsNullOrEmpty(Request["MasterCode"]) ? -1 : Convert.ToInt32(Request["MasterCode"]);



            IBaseBOL BOLClass;
            BOLClass = UITools.GetBOLClass(BaseID, MasterCode);

            #region Security check
            Tools tools = new Tools();
            tools.AccessList = tools.GetAccessList(BaseID);
            int AccessVal = 0;
            if (tools.HasAccess("New", BaseID))
                AccessVal += 1;
            if (tools.HasAccess("Edit", BaseID))
                AccessVal += 2;
            if (tools.HasAccess("Delete", BaseID))
                AccessVal += 4;
            if (tools.HasAccess("View", BaseID))
                AccessVal += 8;
            if (tools.HasAccess("Print", BaseID))
                AccessVal += 16;

            
            
            if (!tools.HasAccess("View", BaseID))
            {
                Response.Write("Message:" + " دسترسی " + BOLClass.PageLable + " برای این کاربر وجود ندارد ");
                return;
            }
            #endregion

            if (UITools.IsHardCode(BaseID))
                BOLClass.QueryObjName = BaseID;

            int TopStr = 10;
            if (CurPage != null && RowsPerPage != null)
                TopStr = Convert.ToInt32(CurPage) * Convert.ToInt32(RowsPerPage);

            if (CurPage == null || CurPage == "")
                CurPage = "1";
            if (Repeat == null)
                Repeat = "1";
            string OrderCol = Order;
            if (Order != null)
            {
                if (Order == OldOrder && Repeat == "0")
                    OrderCol = OrderCol + " DESC";
                else
                    OrderCol = Order;
            }
            else
            {
                Order = "Code DESC";
                OrderCol = "Code DESC";
            }

            if (RowsPerPage == null)
                RowsPerPage = "20";

            #region Generate SearchFilter
            SqlOperators CurOperator;
            CellCollection CellCol;
            SearchFilter sFilter;
            long? RowCount;
            DataTable dt;
            SearchFilterCollection sfCols = new SearchFilterCollection();
            if (FilterClm != null && FilterClm != "")
            {
                string[] FilterClmArray = FilterClm.Split(';');
                string[] ConditionArray = Condition.Split(';');
                string[] KeywordArray = Keyword.Split(';');
                for (int c = 0; c < ConditionArray.Length; c++)
                {
                    switch (ConditionArray[c])
                    {
                        case "0":
                            CurOperator = SqlOperators.Like;
                            break;
                        case "1":
                            CurOperator = SqlOperators.Equal;
                            break;
                        case "2":
                            CurOperator = SqlOperators.GreaterThan;
                            break;
                        case "3":
                            CurOperator = SqlOperators.GreaterThanOrEqual;
                            break;
                        case "4":
                            CurOperator = SqlOperators.LessThan;
                            break;
                        case "5":
                            CurOperator = SqlOperators.LessThanOrEqual;
                            break;
                        case "6":
                            CurOperator = SqlOperators.NotEqual;
                            break;
                        case "7":
                            CurOperator = SqlOperators.DontHave;
                            break;
                        case "9":
                            CurOperator = SqlOperators.StartsWith;
                            break;
                        default:
                            CurOperator = SqlOperators.Like;
                            break;
                    }
                    sFilter = new SearchFilter(FilterClmArray[c], CurOperator, KeywordArray[c]);
                    if (SearchOperand != "" && SearchOperand != null)
                        if (SearchOperand == "AND")
                            sFilter.CurOperand = Operands.AND;
                        else
                            sFilter.CurOperand = Operands.OR;
                    sfCols.Add(sFilter);
                }
            }
            #endregion

            if (DelCode != null)
            {
                try
                {
                    if (ViewName == "" || ViewName == null) //normal browse
                    {

                        BOLClass.DeleteRecord(DelCode);
                    }
                    else
                    {
                    }
                    Response.Write("DELETED");
                }
                catch (Exception e1)
                {
                    Response.Write(e1.Message);
                }
                return;

            }

            if (ViewName == "" || ViewName == null) //normal browse
            {
                dt = BOLClass.GetDataSource(sfCols, OrderCol, Convert.ToInt32(RowsPerPage), Convert.ToInt32(CurPage));
                RowCount = BOLClass.GetCount(sfCols);
                if (ShowMode != "List")
                    CellCol = BOLClass.GetCellCollection();
                else
                    CellCol = BOLClass.GetListCellCollection();
            }
            else //Custom Views
            {
                BrowseSchema BS = new BrowseSchema();
                CellCol = BS.CellCollection;
                dt = BS.DataTBL;
                RowCount = BS.Count;
            }

            dt.ExtendedProperties.Add("RecCount", RowCount.ToString());
            dt.ExtendedProperties.Add("CurPage", CurPage);
            dt.ExtendedProperties.Add("RowsPerPage", RowsPerPage);
            dt.ExtendedProperties.Add("Order", Order);
            dt.ExtendedProperties.Add("CurRepeat", Repeat);
            dt.ExtendedProperties.Add("AccessVal", AccessVal);
            

            int ColCount = dt.Columns.Count;
            string DelColList = "";
            for (int i = 0; i < ColCount; i++)
            {
                if (!IsInSelectedList(dt.Columns[i], CellCol))
                {
                    if (DelColList == "")
                        DelColList = dt.Columns[i].ColumnName;
                    else
                        DelColList = DelColList + "," + dt.Columns[i].ColumnName;
                }
                //dt.Columns.Remove(dt.Columns[i].ColumnName);
            }
            if (DelColList != "")
            {
                string[] DelColListArray = DelColList.Split(',');
                for (int d = 0; d < DelColListArray.Length; d++)
                    dt.Columns.Remove(DelColListArray[d]);

            }

            #region Removing Null Values
            foreach (DataRow loRow in dt.Rows)
            {
                foreach (DataColumn loColumn in dt.Columns)
                {
                    if (loRow[loColumn.ColumnName] == DBNull.Value)
                    {
                        try
                        {
                            switch (loColumn.DataType.ToString())
                            {
                                case "System.DateTime":
                                    loRow[loColumn.ColumnName] = "";
                                    break;
                                case "System.Int64":
                                case "System.Int32":
                                case "System.Int16":
                                case "System.Single":
                                case "System.Decimal":
                                case "System.Byte":
                                case "System.Double":
                                    loRow[loColumn.ColumnName] = 0;
                                    break;
                                case "System.Boolean":
                                    loRow[loColumn.ColumnName] = true;
                                    break;
                                default:
                                    loRow[loColumn.ColumnName] = "";
                                    break;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            #endregion

            for (int i = 0; i < dt.Columns.Count; i++)
                dt.Columns[i].ExtendedProperties.Add("DataType", dt.Columns[i].DataType);

            for (int i = 0; i < CellCol.Count; i++)
            {
                if (CellCol[i].DataBGCellCol.Name != "0")
                    dt.Columns[i].ExtendedProperties.Add("BgColor", CellCol[i].DataBGCellCol.Name);
                if (CellCol[i].HeaderBGCellCol.Name != "0")
                    dt.Columns[i].ExtendedProperties.Add("HeaderBgColor", CellCol[i].HeaderBGCellCol.Name);
                if (CellCol[i].Direction != Directions.None)
                    dt.Columns[i].ExtendedProperties.Add("Direction", CellCol[i].Direction.ToString());
                if (CellCol[i].Align != AlignTypes.None)
                    dt.Columns[i].ExtendedProperties.Add("Alignment", CellCol[i].Align.ToString());
                if (CellCol[i].Width != 0)
                    dt.Columns[i].ExtendedProperties.Add("Width", CellCol[i].Width.ToString());
                if (CellCol[i].IsListTitle != false)
                    dt.Columns[i].ExtendedProperties.Add("IsListTitle", "1");
                dt.Columns[i].ExtendedProperties.Add("DisplayMode", CellCol[i].DisplayMode.ToString());
                if (CellCol[i].IsKey)
                    dt.Columns[i].ExtendedProperties.Add("IsKey", "1");
                dt.Columns[i].ExtendedProperties.Add("Caption", CellCol[i].CaptionName);
            }
            dt.ExtendedProperties.Add("EditForm", BOLClass.EditForm);
            dt.ExtendedProperties.Add("ViewForm", BOLClass.ViewForm);
            dt.ExtendedProperties.Add("LabelName", BOLClass.PageLable);
            dt.ExtendedProperties.Add("EditWidth", BOLClass.EditWidth);
            dt.ExtendedProperties.Add("EditHeight", BOLClass.EditHeight);
            dt.ExtendedProperties.Add("ViewName", ViewName);


            dt.TableName = BaseID;
            dt.WriteXml(Response.OutputStream, XmlWriteMode.WriteSchema);
            //dt.WriteXml(Response.OutputStream);

        }
        catch (Exception exp)
        {
            Response.Write("Message:" + " بروز خطای غیر منتظره " + exp.Message);
            return;
        }
    }
    protected bool IsInSelectedList(DataColumn CurColumn, CellCollection CellCol)
    {
        bool Result = false;
        for (int j = 0; j < CellCol.Count; j++)
            if (CurColumn.ColumnName == CellCol[j].FieldName)
            {
                Result = true;
                break;
            }

        return Result;
    }
}
