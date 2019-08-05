using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.UserControls
{
    public partial class UCCourses : System.Web.UI.UserControl
    {
        protected int _hCGradeCode;
        protected int _hCMainFieldCode;
        protected string _title;
        public int HCGradeCode
        {
            get
            {
                return _hCGradeCode;
            }
            set
            {
                _hCGradeCode = value;
            }
        }

        public int HCMainFieldCode
        {
            get 
            {
                return _hCMainFieldCode;
            }
            set
            {
                _hCMainFieldCode = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            BOLHardCode HardCodeBOL = new BOLHardCode();
            HardCodeBOL.TableOrViewName = "HCMainFields";
            string HCMainField = HardCodeBOL.GetNameByCode(_hCMainFieldCode);

            HardCodeBOL.TableOrViewName = "HCGrades";
            string GradeName = HardCodeBOL.GetNameByCode(_hCGradeCode);

            lblHeader.Text = "کلاس " + HCMainField + " / " + GradeName + " " + _title;

            BOLCourses CoursesBOL = new BOLCourses();
            rptCourses.DataSource = CoursesBOL.GetCourses(_hCGradeCode, _hCMainFieldCode);
            rptCourses.DataBind();

        }
    }
}