using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran
{
    public partial class Courses : System.Web.UI.Page
    {
        int PageNo = 1;
        int PageSize = 20;
        string ConcatUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            string strPageNo = Request["PageNo"];
            Int32.TryParse(strPageNo, out PageNo);
            if (PageNo == 0)
                PageNo = 1;

            if (!Page.IsPostBack)
            {
                //int? HCGradeCode = null;
                //int? HCStudyFieldCode = null;

                //string strHCGradeCode = Request["HCGradeCode"];
                //string strHCStudyFieldCode = Request["HCStudyFieldCode"];
                //if (!string.IsNullOrEmpty(strHCGradeCode))
                //{
                //    HCGradeCode = Convert.ToInt32(strHCGradeCode);
                //    ConcatUrl += "&HCGradeCode=" + HCGradeCode;
                //}
                //if (!string.IsNullOrEmpty(strHCStudyFieldCode))
                //{
                //    HCStudyFieldCode = Convert.ToInt32(strHCStudyFieldCode);
                //    ConcatUrl += "&HCStudyFieldCode=" + HCStudyFieldCode;

                //}

                //BOLCourses CoursesBOL = new BOLCourses();
                //rptCourses.DataSource = CoursesBOL.GetCourses(HCGradeCode, HCStudyFieldCode, PageNo, PageSize);
                //rptCourses.DataBind();

                //int ResultCount = CoursesBOL.GetCoursesCount(HCGradeCode, HCStudyFieldCode);
                //int PageCount = (int)ResultCount / PageSize;
                //if (ResultCount % PageSize > 0)
                //    PageCount++;

                
                //pgrToolbar.PageNo = PageNo;
                //pgrToolbar.PageCount = PageCount;
                //pgrToolbar.ConcatUrl = ConcatUrl;
                //pgrToolbar.PageBind();

                //BOLHardCode HardCodeBOL = new BOLHardCode();
                //BOLStudyInfos StudyInfosBOL = new BOLStudyInfos();

                //HardCodeBOL.TableOrViewName = "HCGrades";
                //ddlHCGradeCode.DataSource = HardCodeBOL.GetDataSource(new SearchFilterCollection(), "Code", 10, 1);
                //ddlHCGradeCode.DataBind();

                //ListItem li1 = new ListItem("", "");
                //ddlHCGradeCode.Items.Insert(0, li1);


                //if (HCGradeCode != 0 && HCGradeCode != null)
                //{
                //    int LoadTypeCode = 1;
                //    if (HCGradeCode == 4)
                //        LoadTypeCode = 1;
                //    else
                //        LoadTypeCode = 2;

                //    ddlHCGradeCode.SelectedValue = HCGradeCode.ToString();
                //    ddlHCStudyFieldCode.DataSource = StudyInfosBOL.GetActiveStudyFields(LoadTypeCode);
                //    ddlHCStudyFieldCode.DataBind();
                //    ListItem li = new ListItem("", "");
                //    ddlHCStudyFieldCode.Items.Insert(0, li);
                //}


                //if (HCStudyFieldCode != 0 && HCStudyFieldCode != null)
                //{
                //    ddlHCStudyFieldCode.SelectedValue = HCStudyFieldCode.ToString();
                //}



                //HardCodeBOL.TableOrViewName = "HCStudyFields";
                //ddlHCStudyFieldCode.DataSource = StudyInfosBOL.GetActiveStudyFields(1);
                //ddlHCStudyFieldCode.DataBind();

            }
        }

        //protected void ddlHCGradeCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int HCGradeCode = Convert.ToInt32(ddlHCGradeCode.SelectedValue);
        //    BOLStudyInfos StudyInfosBOL = new BOLStudyInfos();

        //    int LoadTypeCode = 1;
        //    if (HCGradeCode == 4)
        //        LoadTypeCode = 1;
        //    else
        //        LoadTypeCode = 2;

        //    ddlHCStudyFieldCode.DataSource = StudyInfosBOL.GetActiveStudyFields(LoadTypeCode);
        //    ddlHCStudyFieldCode.DataBind();
        //    ListItem li = new ListItem("", "");
        //    ddlHCStudyFieldCode.Items.Insert(0, li);
        //}

        //protected void ddlHCStudyFieldCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int HCGradeCode = Convert.ToInt32(ddlHCGradeCode.SelectedValue);
        //    int HCStudyFieldCode = Convert.ToInt32(ddlHCStudyFieldCode.SelectedValue);


        //    //BOLCourses CoursesBOL = new BOLCourses();
        //    //rptCourses.DataSource = CoursesBOL.GetCourses(HCGradeCode, HCStudyFieldCode, PageNo, PageSize);
        //    //rptCourses.DataBind();

        //    Response.Redirect("~/Courses.aspx?HCGradeCode=" + HCGradeCode + "&HCStudyFieldCode=" + HCStudyFieldCode);

        //}
    }
}