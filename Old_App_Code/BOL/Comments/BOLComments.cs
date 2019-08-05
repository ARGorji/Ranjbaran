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
  
public class BOLComments : BaseBOLComments, IBaseBOL<Comments>
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

    internal object GetCommentsByStatusCode(int ItemCode, int HCCommentStatusCode)
    {
        return dataContext.Comments.Where(p => p.ItemCode.Equals(ItemCode) && p.HCCommentStatusCode.Equals(HCCommentStatusCode)).OrderByDescending(p => p.Code);

    }

    internal int GetCommentByStatusCodeCount(int ItemCode, int HCCommentstatusCode)
    {
        return dataContext.Comments.Where(p => p.ItemCode.Equals(ItemCode) && p.HCCommentStatusCode.Equals(HCCommentstatusCode)).Count();
    }


    internal bool SaveComment(int HCSectionCode, int ItemCode, string Name, string Email, string Comment)
    {
        try
        {
            Comments CurComment = new Comments();
            dataContext.Comments.InsertOnSubmit(CurComment);
            CurComment.ItemCode = ItemCode;
            CurComment.Name = Name;
            CurComment.Email = Email;
            CurComment.TextComment = Comment;
            CurComment.HCCommentStatusCode = 1;
            CurComment.HCSectionCode = HCSectionCode;
            CurComment.SendDate = DateTime.Now;
            dataContext.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }

    }
}
