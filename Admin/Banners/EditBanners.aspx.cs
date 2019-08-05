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


public partial class Banners_EditBanners : EditForm<Banners>
{
    private string BaseID = "Banners";
    IBaseBOL<Banners> BaseBOL;

    protected void Page_Load(object sender, EventArgs e)
    {
        #region Tab Pages
        #endregion
        BOLClass = new BOLBanners();
        lblSysName.Text = BOLClass.PageLable;

        if ((Code == null) && (!NewMode)) return;
        if (!Page.IsPostBack)
        {
            #region Fill Combo
            cboHCTypeCode.DataSource = new BOLHardCode().GetHCDataTable("HCBannerTypes");
            cboPositionCode.DataSource = new BOLHardCode().GetHCDataTable("BannerPositions");
            
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
            }
        }
        catch
        {
        }
    }
}
