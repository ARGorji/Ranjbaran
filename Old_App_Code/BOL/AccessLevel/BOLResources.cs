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
using Ranjbaran.Old_App_Code.DAL;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
/// <summary>
/// Summary description for BOLResources
/// </summary>
public class BOLResources : BaseBOLResources, IBaseBOL<Resources>
{

    public DataTable GetValidAccess(List<AccessListStruct> AccessList,SearchFilterCollection sFilterCols, string SortString, int PageSize, int CurrentPage)
    {
        DataTable dt=new DataTable();
        UsersDataContext dataContext = new UsersDataContext();
        string WhereCond = Tools.GetCondition(sFilterCols);
        if (HttpContext.Current.Session["UserCode"] != null)
        {
            
            //AccessListStruct s=new AccessListStruct("Edit",
            ArrayList EditAccess =new ArrayList();
            ArrayList ViewAccess = new ArrayList();

            for (int i = 0; i < AccessList.Count; i++)
            {
                if (AccessList[i].AccessName == "EDIT")
                    EditAccess.Add(AccessList[i].FieldName);
                else if (AccessList[i].AccessName == "VIEW")
                    ViewAccess.Add(AccessList[i].FieldName);
            }
            //string[] EditAccess = HttpContext.Current.Session["Edit"].ToString().Split(',');
            //string[] ViewAccess = HttpContext.Current.Session["View"].ToString().Split(',');
            string[] arrEditAccess = (string[])EditAccess.ToArray(typeof(string));
            string[] arrViewAccess = (string[])ViewAccess.ToArray(typeof(string));
            System.Collections.Generic.IEnumerable<String> AllAccess = arrEditAccess.Union(arrViewAccess);
            if (AllAccess.Count<string>() > 0)
            {
                var ListResult = from v in dataContext.Resources
                                 orderby v.Ordering
                                 where (v.HCResourceTypeCode.Equals(1) || v.HCResourceTypeCode.Equals(2))
                                  && (AllAccess.Contains(v.ResName))
                                 select v
                                 
                                 ;
                dt = new Converter<Resources>().ToDataTable(ListResult);
            }
        }
        return dt;
    }
    public int? GetCodeByEngName(string EngName)
    {
        UsersDataContext dataContext = new UsersDataContext();
        var Result = dataContext.Resources.SingleOrDefault(p => p.EngName.Equals(EngName));
        if (Result == null)
            return null;
        else
            return Result.Code;
    }

    public IQueryable<Resources> GetByMasterCode(int MasterCode)
    {
        UsersDataContext dataContext = new UsersDataContext();
        var Result = from p in dataContext.Resources
                     where p.MasterCode.Equals(MasterCode)
                     select p;
        return Result;
    }
    public DataTable GetNodeData(int NodeValue)
    {
        UsersDataContext dataContext = new UsersDataContext();
        //string WhereCond = Tools.GetCondition(sFilterCols);
        var ListResult = from v in dataContext.Resources
                         where v.MasterCode.Equals(NodeValue)
                         select v;
        DataTable dt = new Converter<Resources>().ToDataTable(ListResult);
        return dt;
    }
    public IList CheckBusinessRules()
    {
        var messages = new List<string>();
        //Business rules here

        if (false)
            messages.Add("");

        return messages;
    }


    public DataTable GetAllFieldExcpetFields()
    {
        UsersDataContext dataContext = new UsersDataContext();
        DataTable dt = new DataTable();
        var ListResult = from r in dataContext.Resources
                         where (!r.HCResourceTypeCode.Equals(3))
                         select r;
        ListResult = ListResult.OrderBy(p => p.Name);
        dt = new Converter<Resources>().ToDataTable(ListResult);
        return dt;
    }
}
