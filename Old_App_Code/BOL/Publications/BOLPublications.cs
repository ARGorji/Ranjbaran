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
  
public class BOLPublications : BaseBOLPublications, IBaseBOL<Publications>
{
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

    internal object GetPublications(int PageNo, int PageSize)
    {
        int SkipCount = (PageNo - 1) * PageSize;

        return dataContext.vPublications.Skip(SkipCount).Take(PageSize).OrderBy(p => p.ShowOrder);
    }

    internal vPublications GetDetail(int Code)
    {
        return dataContext.vPublications.SingleOrDefault(p => p.Code.Equals(Code));
    }

    internal int GetPublicationsCount()
    {
        return dataContext.vPublications.Count();
    }
}
