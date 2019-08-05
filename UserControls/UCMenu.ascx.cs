using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ranjbaran.UserControls
{
    public partial class UCMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BOLStudyInfos StudyInfosBOL = new BOLStudyInfos();

            BOLHardCode HardCodeBOL = new BOLHardCode();
            HardCodeBOL.TableOrViewName = "HCStudyFields";
            rptMasterStudyFields.DataSource = StudyInfosBOL.GetActiveStudyFields(1);
            rptMasterStudyFields.DataBind();

            rptPHDStudyFields.DataSource = StudyInfosBOL.GetActiveStudyFields(2);
            rptPHDStudyFields.DataBind();

        }
    }
}