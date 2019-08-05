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
  
public class BOLExams : BaseBOLExams, IBaseBOL<Exams>
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

    internal object GetExams(int? HCGradeCode, int? HCStudyFieldCode,int? HCLessonCode, string Lesson, int PageNo, int PageSize)
    {
        int SkipCount = (PageNo - 1) * PageSize;
        IQueryable<vExams> Result = dataContext.vExams;

        if (HCGradeCode != null)
            Result = Result.Where(p => p.HCGradeCode.Equals(HCGradeCode));
        if (HCStudyFieldCode != null)
            Result = Result.Where(p => p.HCStudyFieldCode.Equals(HCStudyFieldCode));
        if (HCLessonCode != null)
            Result = Result.Where(p => p.HCLessonCode.Equals(HCLessonCode));
        if (!string.IsNullOrEmpty(Lesson))
            Result = Result.Where(p => p.Lesson.Equals(Lesson));

        Result = Result.Skip(SkipCount).Take(PageSize).OrderByDescending(p => p.Code);
        return Result;
    }

    internal int GetExamsCount(int? HCGradeCode, int? HCStudyFieldCode, int? HCLessonCode, string Lesson)
    {
        IQueryable<vExams> Result = dataContext.vExams;

        if (HCGradeCode != null)
            Result = Result.Where(p => p.HCGradeCode.Equals(HCGradeCode));
        if (HCStudyFieldCode != null)
            Result = Result.Where(p => p.HCStudyFieldCode.Equals(HCStudyFieldCode));
        if (HCLessonCode != null)
            Result = Result.Where(p => p.HCLessonCode.Equals(HCLessonCode));
        if (!string.IsNullOrEmpty(Lesson))
            Result = Result.Where(p => p.Lesson.Equals(Lesson));

        return Result.Count();

    }

    internal string GetPDFFile(int Code)
    {
        Exams CurExam = dataContext.Exams.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurExam != null)
        {
            return CurExam.PDFFile;

        }
        else
            return null;
    }

    internal bool IsFree(int Code)
    {
        Exams CurExam = dataContext.Exams.SingleOrDefault(p => p.Code.Equals(Code));
        if (CurExam != null)
        {
            if (CurExam.Free != null)
            {
                return (bool)CurExam.Free;
            }
            else
                return false;
        }
        else
            return false;
    }

    internal Exams GetDetail(int Code)
    {
        return dataContext.Exams.SingleOrDefault(p => p.Code.Equals(Code));
    }

    internal object GetLessons(int? HCGradeCode, int? HCStudyFieldCode)
    {
        return (from p in dataContext.vExams
                      where p.HCGradeCode.Equals(HCGradeCode) && p.HCStudyFieldCode.Equals(HCStudyFieldCode)
                      select new { p.HCLessonCode, p.Lesson }).Distinct();
    }
}
