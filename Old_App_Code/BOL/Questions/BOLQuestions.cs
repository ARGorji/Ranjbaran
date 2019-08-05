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
  
public class BOLQuestions : BaseBOLQuestions, IBaseBOL<Questions>
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

    internal object GetQuestions()
    {
        return dataContext.vQuestionsFulls.Where(p=> p.HCQuestionStatusCode.Equals(2)).OrderByDescending(p => p.Code);
    }

    public bool Insert(string Question, int UserCode)
    {
        try
        {
            Questions NewQuestion = new Questions();
            NewQuestion.UserCode = UserCode;
            NewQuestion.Ques = Question;
            NewQuestion.SendDate = DateTime.Now;
            NewQuestion.HCQuestionStatusCode = 1;

            dataContext.Questions.InsertOnSubmit(NewQuestion);
            dataContext.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
