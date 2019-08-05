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
using System.Data.Linq;
using System.Collections;

public class BOLGroupResources : BaseBOLGroupResources, IBaseBOL<GroupResources>
{
    public BOLGroupResources(params int[] MCodes)
        : base(MCodes)
    {
        
    }

    public GroupResources GetByGroupandResourceCode(int GroupCode, int ResourceCode)
    {
        UsersDataContext dataContext = new UsersDataContext();
        return dataContext.GroupResources.SingleOrDefault(p => p.GroupCode.Equals(GroupCode) && p.ResourceCode.Equals(ResourceCode) );
    }

    public void GetAccess(int AccessType, int GroupCode, int MasterCode)
    {
        int ResourceCode;
        UsersDataContext dataContext = new UsersDataContext();
        var Result = dataContext.sp_GetMultipleAccess(AccessType, GroupCode, MasterCode);
        var ListResult = dataContext.ExecuteQuery<GroupResources>(string.Format("exec sp_GetMultipleAccess '{0}','{1}','{2}'", AccessType, GroupCode, MasterCode));

        foreach (var r in ListResult)
        {
            ResourceCode = (int)((GroupResources)r).ResourceCode;
            GetAccess(AccessType, GroupCode, ResourceCode);
        }
    }
    public IList CheckBusinessRules()
    {
        var messages = new List<string>();
        //Business rules here

        if (false)
            messages.Add("");

        return messages;
    }

}
