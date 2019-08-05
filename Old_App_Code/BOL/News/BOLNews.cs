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
  
public class BOLNews : BaseBOLNews, IBaseBOL<News>
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

    internal IQueryable<vNews> GetLatestTelexNews(int TakeCount)
    {
        return dataContext.vNews.Where(p => p.Teletex.Equals(true)).OrderByDescending(p => p.NDate).Take(TakeCount);
    }

    internal vNews GetLatestNews()
    {
        return dataContext.vNews.OrderByDescending(p => p.Code).FirstOrDefault();
    }

    internal IQueryable<vNews> GetLatestNews(int TakeCount)
    {
        return dataContext.vNews.Take(TakeCount).OrderByDescending(p => p.Code);
    }

    internal object GetNews(int PageNo, int PageSize)
    {
        int SkipCount = (PageNo - 1) * PageSize;

        return dataContext.vNews.Skip(SkipCount).Take(PageSize).OrderByDescending(p => p.NDate);
    }

    internal int GetNewsCount()
    {
        return dataContext.vNews.Count();
    }

    internal vNews GetDetail(int Code)
    {
        return dataContext.vNews.SingleOrDefault(p => p.Code.Equals(Code));
    }

    internal void Insert(string NewsTitle, string Link)
    {
        News NewRecord = new News();
        dataContext.News.InsertOnSubmit(NewRecord);
        NewRecord.Title = NewsTitle;
        NewRecord.Link = Link;
        NewRecord.NewsDate = DateTime.Now;

        dataContext.SubmitChanges();
    }

    internal object GetNewsBySectionCode(int HCSectionCode)
    {
        return dataContext.vNews.Where(p => p.HCSectionCode.Equals(HCSectionCode)).OrderByDescending(p => p.NDate);
    }
}
