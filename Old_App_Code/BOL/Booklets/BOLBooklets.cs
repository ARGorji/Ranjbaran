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
  
public class BOLBooklets : BaseBOLBooklets, IBaseBOL<Booklets>
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

    internal object GetBooklets(int PageNo, int PageSize)
    {
        int SkipCount = (PageNo - 1) * PageSize;

        return dataContext.vBooklets.Skip(SkipCount).Take(PageSize).OrderByDescending(p=> p.Code);
    }

    public bool IsFree(int Code)
    {
        Booklets CurBooklet = dataContext.Booklets.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurBooklet != null)
        {
            if (CurBooklet.Free != null)
            {
                return (bool)CurBooklet.Free;
            }
            else
                return false;
        }
        else
            return false;
    }

    public string GetPDFFile(int Code)
    {
        Booklets CurBooklet = dataContext.Booklets.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurBooklet != null)
        {
                return CurBooklet.BookletFile;

        }
        else
            return null;
    }

    internal int GetBookletsCount()
    {
        return dataContext.vBooklets.Count();
    }

    internal Booklets GetDetail(int Code)
    {
        return dataContext.Booklets.SingleOrDefault(p => p.Code.Equals(Code));
    }
}
