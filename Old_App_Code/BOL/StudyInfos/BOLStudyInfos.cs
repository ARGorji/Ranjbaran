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
  
public class BOLStudyInfos : BaseBOLStudyInfos, IBaseBOL<StudyInfos>
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



    internal object GetItems(int HCGradeCode, int HCStudyFieldCode, int PageNo, int PageSize)
    {
        int SkipCount = (PageNo - 1) * PageSize;

        return dataContext.vStudyInfos.Where(p => p.HCStudyFieldCode.Equals(HCStudyFieldCode) && p.HCGradeCode.Equals(HCGradeCode)).Skip(SkipCount).Take(PageSize);
    }

    internal int GetItemCount(int HCGradeCode, int HCStudyFieldCode)
    {
        return dataContext.vStudyInfos.Where(p => p.HCStudyFieldCode.Equals(HCStudyFieldCode) && p.HCGradeCode.Equals(HCGradeCode)).Count();

    }

    internal object GetActiveStudyFields(int OrVal)
    {
        return (from p in dataContext.vStudyInfos2s
                where (Convert.ToInt32(p.GradeVal) & OrVal) != 0
                select new { p.HCStudyFieldCode, p.HCStudyField, p.ShowOrder}).Distinct().OrderBy(p => p.ShowOrder);
    }

    internal vStudyInfos GetDetail(int Code)
    {
        return dataContext.vStudyInfos.SingleOrDefault(p => p.Code.Equals(Code));
    }
}
