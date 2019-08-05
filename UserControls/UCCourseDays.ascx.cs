using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.UserControls
{
    public partial class UCCourseDays : System.Web.UI.UserControl
    {
        public string strCourseCode;
        protected int _courseCode;
        public int CourseCode
        {
            set
            {
                _courseCode = value;
            }
            get
            {
                return _courseCode;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

                strCourseCode = _courseCode.ToString();
                BOLCourseDays CourseDaysBOL = new BOLCourseDays(1);
                rptCourseDays.DataSource = CourseDaysBOL.GetDays(_courseCode);
                rptCourseDays.DataBind();

                if (rptCourseDays.Items.Count == 0)
                {
                    pnlNoData.Visible = true;
                    rptCourseDays.Visible = false;
                }


        }
    }
}