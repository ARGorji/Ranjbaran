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
  
public class BOLSpeeches : BaseBOLSpeeches, IBaseBOL<Speeches>
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

    internal object GetSpeeches(int PageNo, int PageSize)
    {
        int SkipCount = (PageNo - 1) * PageSize;

        return dataContext.vSpeeches.Skip(SkipCount).Take(PageSize).OrderByDescending(p => p.Code);
    }

    internal int GetSpeechesCount()
    {
        return dataContext.vSpeeches.Count();

    }

    internal Ranjbaran.Old_App_Code.DAL.vSpeeches GetLatest()
    {
        var Result = dataContext.vSpeeches.OrderByDescending(p => p.Code);
        if (Result.Count() > 0)
            return Result.First();
        else
            return null;
    }
}
