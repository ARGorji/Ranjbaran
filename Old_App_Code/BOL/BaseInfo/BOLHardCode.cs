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
/// Summary description for BOLHardCode
/// </summary>
/// 
public class BOLHardCode : BaseBOLHardCode, IBaseBOL<DataTable>
{
    public int GetCodeByName(string Name)
    {
        int Result = 0;
        SearchFilterCollection sfcol = new SearchFilterCollection();
        SearchFilter sf = new SearchFilter("Name", SqlOperators.Equal, Name);
        sfcol.Add(sf);

        DataTable dt = GetDataSource(sfcol, "Code", 10, 1);
        if (dt.Rows.Count > 0)
        {
            Result = Convert.ToInt32(dt.Rows[0]["Code"]);
        }
        return Result;
    }
    public IList CheckBusinessRules()
    {
        var messages = new List<string>();
        //Business rules here

        if (false)
            messages.Add("");

        return messages;
    }
    public DataTable GetDetails(int Code)
    {
        var ListResult = dataContext.ExecuteQuery<HCYesNo>(string.Format("exec spGetDetail '{0}','{1}'", TableOrViewName, Code));
        DataTable dt = new Converter<HCYesNo>().ToDataTable(ListResult);
        return dt;
    }


    internal string GetNameByCode(int Code)
    {
        string Result = "";
        SearchFilterCollection sfcol = new SearchFilterCollection();
        SearchFilter sf = new SearchFilter("Code", SqlOperators.Equal, Code.ToString());
        sfcol.Add(sf);

        DataTable dt = GetDataSource(sfcol, "Code", 10, 1);
        if (dt.Rows.Count > 0)
        {
            Result = Convert.ToString(dt.Rows[0]["Name"]);
        }
        return Result;
    }
}
