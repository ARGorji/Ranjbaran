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


public partial class Publications_EditPublications : EditForm<Publications>
{
    private string BaseID = "Publications";
    IBaseBOL<Publications> BaseBOL;
    


    protected void Page_Load(object sender, EventArgs e)
    {
        #region Tab Pages
        if (!NewMode)
            ShowDetails();
        else
        {
            RadMultiPage1.SelectedIndex = 0;
            tsPublications.Tabs[0].Selected = true;
        }
        #endregion
        BOLClass = new BOLPublications();
        lblSysName.Text = BOLClass.PageLable;
        
        if ((Code == null) && (!NewMode)) return;
        if (!Page.IsPostBack)
        {
           //if (!NewMode) ShowDetails();
                       

        #region Fill Combo
	    
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
        string Tab1Click = "BrowseObj1.ShowDetail('PublicationGroups', '" + Code + "', true,'BrowseObj1');";


        Tab1.Attributes.Add("onclick", Tab1Click);
        Tools.SetClientScript(this, "ActiveTab1", Tab1Click);

        
    }
}
