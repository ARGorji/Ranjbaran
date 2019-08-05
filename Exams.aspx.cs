using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Ranjbaran
{
    public partial class Exams : System.Web.UI.Page
    {
        int PageNo = 1;
        int PageSize = 20;
        string ConcatUrl;

        protected void HandleRepeaterCommand(object source, RepeaterCommandEventArgs e)
        {

            int ExamCode = Convert.ToInt32(e.CommandArgument);


            if (e.CommandName == "StartDownload")
            {
                BOLExams ExamBOL = new BOLExams();
                string PDFFile = ExamBOL.GetPDFFile(ExamCode);
                if (!string.IsNullOrEmpty(PDFFile))
                    StartDowload(PDFFile);
                else
                {
                    msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msg.Text = "فایلی برای دانلود وجون ندارد";
                }

            }


            else if (e.CommandName == "StartPay")
            {
                BOLExams ExamsBOL = new BOLExams();
                string PDFFile = ExamsBOL.GetPDFFile(ExamCode);
                if (!string.IsNullOrEmpty(PDFFile))
                {
                    Response.Redirect("PayStep1.aspx?ItemType=Exam&Code=" + ExamCode);
                    return;

                }
                else
                {
                    msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Error;
                    msg.Text = "فایلی برای خرید وجود ندارد";
                }

            }
        }

        public bool IsFree(int Code)
        {
            BOLExams ExamsBOL = new BOLExams();
            return ExamsBOL.IsFree(Code);

        }

        private void StartDowload(string PDFFile)
        {
            byte[] pdfByte = System.IO.File.ReadAllBytes(Server.MapPath("~/" + PDFFile));

            Guid newGd = Guid.NewGuid();
            string RandFileName = "Exam" + newGd.ToString().Replace("-", "");

            Response.Clear();
            MemoryStream ms = new MemoryStream(pdfByte);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + RandFileName + ".pdf");
            Response.AddHeader("Content-Length", pdfByte.Length.ToString());
            Response.Buffer = true;


            ms.WriteTo(Response.OutputStream);
            //Response.End();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int? HCGradeCode = null;
            int? HCStudyFieldCode = null;
            int? HCLessonCode = null;

            string strHCGradeCode = Request["HCGradeCode"];
            string strHCStudyFieldCode = Request["HCStudyFieldCode"];
            string strHCLessonCode = Request["HCLessonCode"];

            if (!string.IsNullOrEmpty(strHCGradeCode))
            {
                HCGradeCode = Convert.ToInt32(strHCGradeCode);
                ConcatUrl += "&HCGradeCode=" + HCGradeCode;
            }
            else if(!string.IsNullOrEmpty( Request["HCGradeCode"]))
            {
                HCGradeCode = Convert.ToInt32(Request["HCGradeCode"]);
                ConcatUrl += "&HCGradeCode=" + HCGradeCode;
            }


            if (!string.IsNullOrEmpty(strHCStudyFieldCode))
            {
                HCStudyFieldCode = Convert.ToInt32(strHCStudyFieldCode);
                ConcatUrl += "&HCStudyFieldCode=" + HCStudyFieldCode;

            }
            if (!string.IsNullOrEmpty(strHCLessonCode))
            {
                HCLessonCode = Convert.ToInt32(strHCLessonCode);
                ConcatUrl += "&HCLessonCode=" + HCLessonCode;

            }

            string Lesson = "";
            if (!string.IsNullOrEmpty(Request["Lesson"]))
            {
                Lesson = Request["Lesson"];
                ConcatUrl += "&Lesson=" + Lesson;
            }


            string strPageNo = Request["PageNo"];
            Int32.TryParse(strPageNo, out PageNo);
            if (PageNo == 0)
                PageNo = 1;

            if (!Page.IsPostBack)
            {
                if (HCGradeCode != null && !string.IsNullOrEmpty(Lesson))
                {
                    BOLExams ExamsBOL = new BOLExams();
                    //rptExams.DataSource = ExamsBOL.GetExams(HCGradeCode, HCStudyFieldCode, HCLessonCode, PageNo, PageSize);
                    rptExams.DataSource = ExamsBOL.GetExams(HCGradeCode, null, null, Lesson, PageNo, PageSize);
                    rptExams.DataBind();

                    int ResultCount = ExamsBOL.GetExamsCount(HCGradeCode, HCStudyFieldCode, HCLessonCode, Lesson);
                    int PageCount = (int)ResultCount / PageSize;
                    if (ResultCount % PageSize > 0)
                        PageCount++;

                    ConcatUrl += "";
                    pgrToolbar.PageNo = PageNo;
                    pgrToolbar.PageCount = PageCount;
                    pgrToolbar.ConcatUrl = ConcatUrl;
                    pgrToolbar.PageBind();

                }
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

                //if (HCGradeCode != 0 && HCGradeCode != null && HCStudyFieldCode != 0 && HCStudyFieldCode != null)
                //{
                //    ddlHCGradeCode.SelectedValue = HCGradeCode.ToString();
                //    ddlHCStudyFieldCode.SelectedValue = HCStudyFieldCode.ToString();

                //    ddlHCLessonCode.DataSource = ExamsBOL.GetLessons(HCGradeCode, HCStudyFieldCode);
                //    ddlHCLessonCode.DataBind();
                //    ListItem li = new ListItem("", "");
                //    ddlHCLessonCode.Items.Insert(0, li);
                //}


                //if (HCStudyFieldCode != 0 && HCStudyFieldCode != null)
                //{
                //    ddlHCStudyFieldCode.SelectedValue = HCStudyFieldCode.ToString();
                //}
                //if (HCLessonCode != 0 && HCLessonCode != null)
                //{
                //    ddlHCLessonCode.SelectedValue = HCLessonCode.ToString();
                //}
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
        //    if (ddlHCGradeCode.SelectedValue == "" ||
        //        ddlHCStudyFieldCode.SelectedValue == "" 
        //        )
        //        return;

        //    int HCGradeCode = Convert.ToInt32(ddlHCGradeCode.SelectedValue);
        //    int HCStudyFieldCode = Convert.ToInt32(ddlHCStudyFieldCode.SelectedValue);


        //    Response.Redirect("~/Exams.aspx?HCGradeCode=" + HCGradeCode + "&HCStudyFieldCode=" + HCStudyFieldCode);

        //}

        //protected void ddlHCLessonCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlHCGradeCode.SelectedValue == "" ||
        //        ddlHCStudyFieldCode.SelectedValue == "" ||
        //        ddlHCLessonCode.SelectedValue == "")
        //        return;

        //    int HCGradeCode = Convert.ToInt32(ddlHCGradeCode.SelectedValue);
        //    int HCStudyFieldCode = Convert.ToInt32(ddlHCStudyFieldCode.SelectedValue);
        //    int HCLessonCode = Convert.ToInt32(ddlHCLessonCode.SelectedValue);


        //    Response.Redirect("~/Exams.aspx?HCGradeCode=" + HCGradeCode + "&HCStudyFieldCode=" + HCStudyFieldCode + "&HCLessonCode=" + HCLessonCode);

        //}

    }
}