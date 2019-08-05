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
  
public class BOLCourses : BaseBOLCourses, IBaseBOL<Courses>
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

    internal object GetCourses(int? HCGradeCode, int? HCStudyFieldCode, int PageNo, int PageSize)
    {
        int SkipCount = (PageNo - 1) * PageSize;
        IQueryable<vCourses> Result = dataContext.vCourses;

        if (HCGradeCode != null)
            Result = Result.Where(p => p.HCGradeCode.Equals(HCGradeCode));
        if (HCStudyFieldCode != null)
            Result = Result.Where(p => p.HCStudyFieldCode.Equals(HCStudyFieldCode));

        Result = Result.Skip(SkipCount).Take(PageSize).OrderByDescending(p => p.StartTime);
        return Result;
    }

    internal object GetCourses(int HCGradeCode, int HCMainFieldCode)
    {
        IQueryable<vCourses> Result = dataContext.vCourses;
        if (HCGradeCode != 0)
            Result = Result.Where(p => p.HCGradeCode.Equals(HCGradeCode));
        if (HCMainFieldCode != 0)
            Result = Result.Where(p => p.HCMainFieldCode.Equals(HCMainFieldCode));

        return Result;
    }

    internal int GetCoursesCount(int? HCGradeCode, int? HCStudyFieldCode)
    {
        IQueryable<vCourses> Result = dataContext.vCourses;

        if (HCGradeCode != null)
            Result = Result.Where(p => p.HCGradeCode.Equals(HCGradeCode));
        if (HCStudyFieldCode != null)
            Result = Result.Where(p => p.HCStudyFieldCode.Equals(HCStudyFieldCode));

        return Result.Count();
    }

    internal Courses GetDetail(int Code)
    {
        return dataContext.Courses.SingleOrDefault(p => p.Code.Equals(Code));
    }
}
