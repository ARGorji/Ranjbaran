using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Ranjbaran.Old_App_Code.DAL;


public partial class Courses_EditCourses : EditForm<Courses>
{
    private string BaseID = "Courses";
    IBaseBOL<Courses> BaseBOL;
    


    protected void Page_Load(object sender, EventArgs e)
    {
        #region Tab Pages
        if (!NewMode)
            ShowDetails();
        else
        {
            RadMultiPage1.SelectedIndex = 0;
            tsCourses.Tabs[0].Selected = true;
        }
        #endregion
        BOLClass = new BOLCourses();
        lblSysName.Text = BOLClass.PageLable;
        
        if ((Code == null) && (!NewMode)) return;
        if (!Page.IsPostBack)
        {
           //if (!NewMode) ShowDetails();
                       

        #region Fill Combo
	    cboHCGradeCode.DataSource = new BOLHardCode().GetHCDataTable("HCGrades");
            cboHCMainFieldCode.DataSource = new BOLHardCode().GetHCDataTable("HCMainFields");
            
	    
	    #endregion
            if (!NewMode)
            {
                LoadData((int)Code);
            }
        }


    }
 
    protected void DoSave(object sender, ImageClickEventArgs e)
    {
        try
        {
            int ReturnCode = SaveControls("~/Main/Default.aspx?BaseID=" + BaseID);
            if (NewMode && ReturnCode != -1)
            {
                NewMode = false;
                Code = ReturnCode;
                ShowDetails();
            }
        }
        catch
        {
        }
    }
    private void ShowDetails()
    {
        string Tab1Click = "BrowseObj1.ShowDetail('CourseDays', '" + Code + "', true,'BrowseObj1');BrowseObj2.ShowDetail('CourseUsers', '" + Code + "', true,'BrowseObj2');";


        Tab1.Attributes.Add("onclick", Tab1Click);
        Tools.SetClientScript(this, "ActiveTab1", Tab1Click);

        
    }
}
