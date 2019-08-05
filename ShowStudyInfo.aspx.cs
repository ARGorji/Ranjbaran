using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ranjbaran.Old_App_Code.DAL;

namespace Ranjbaran
{
    public partial class ShowStudyInfo : System.Web.UI.Page
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

            string strHCGradeCode = Request["HCGradeCode"];
            int HCGradeCode;
            Int32.TryParse(strHCGradeCode, out HCGradeCode);

            string strHCStudyFieldCode = Request["HCStudyFieldCode"];
            int HCStudyFieldCode;
            Int32.TryParse(strHCStudyFieldCode, out HCStudyFieldCode);

            if (HCStudyFieldCode != 0 && HCGradeCode != 0)
            {
                BOLHardCode HardCodeBOL = new BOLHardCode();
                HardCodeBOL.TableOrViewName = "HCGrades";
                string GradeName =  HardCodeBOL.GetNameByCode(HCGradeCode);

                HardCodeBOL.TableOrViewName = "HCStudyFields";
                string StudyField = HardCodeBOL.GetNameByCode(HCStudyFieldCode);
                Page.Title= ltrTitle.Text = "رشته " + StudyField + " | مقطع " + GradeName;

                HardCodeDataContext dcHardCode = new HardCodeDataContext();
                HCStudyFields CurStudyField = dcHardCode.HCStudyFields.SingleOrDefault(p=> p.Code.Equals(HCStudyFieldCode));
                if(CurStudyField != null)
                {
                    if (HCGradeCode == 4)
                    {
                        if (!string.IsNullOrEmpty(CurStudyField.DescArshad))
                            ltrDesc.Text = CurStudyField.DescArshad;
                        else
                            pnlDesc.Visible = false;
                    }
                    else if (HCGradeCode == 5)
                    {
                        if (!string.IsNullOrEmpty(CurStudyField.DescDoc))
                            ltrDesc.Text = CurStudyField.DescDoc;
                        else
                            pnlDesc.Visible = false;
                    }
                }

            }
            
            if (!Page.IsPostBack)
            {
                BOLStudyInfos StudyInfosBOL = new BOLStudyInfos();
                rptStudyInfos.DataSource = StudyInfosBOL.GetItems(HCGradeCode, HCStudyFieldCode, PageNo, PageSize);
                rptStudyInfos.DataBind();

                if (rptStudyInfos.Items.Count > 0)
                {

                    int ResultCount = StudyInfosBOL.GetItemCount(HCGradeCode, HCStudyFieldCode);
                    int PageCount = (int)ResultCount / PageSize;
                    if (ResultCount % PageSize > 0)
                        PageCount++;

                    ConcatUrl += "&HCGradeCode=" + HCGradeCode + "&HCStudyFieldCode=" + HCStudyFieldCode;
                    pgrToolbar.PageNo = PageNo;
                    pgrToolbar.PageCount = PageCount;
                    pgrToolbar.ConcatUrl = ConcatUrl;
                    pgrToolbar.PageBind();
                }
                else
                {
                    msg.MessageTextMode = AKP.Web.Controls.Common.MessageMode.Warning;
                    msg.Text = "مطلبی برای این موضوع وارد نشده است.";
                }
            }

        }
    }
}