using Ranjbaran.Old_App_Code.DAL;
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

public partial class Orders_EditOrders : EditForm<Orders>
{
    private string BaseID = "Orders";
    IBaseBOL<Orders> BaseBOL;



    protected void Page_Load(object sender, EventArgs e)
    {
        #region Tab Pages
        //if (!NewMode)
        //     ShowDetails();
        //else
        //{
        //     RadMultiPage1.SelectedIndex = 0;
        //     tsOrders.Tabs[0].Selected = true;
        //}
        #endregion
        BOLClass = new BOLOrders();
        //lblSysName.Text = BOLClass.PageLable;

        if ((Code == null) && (!NewMode)) return;
        if (!Page.IsPostBack)
        {
            if (!NewMode) ShowDetails();


            #region Fill Combo
            cboHCGenderCode.DataSource = new BOLHardCode().GetHCDataTable("HCGenders");
            cboHCSendTypeCode.DataSource = new BOLHardCode().GetHCDataTable("HCSendTypes");
            cboHCOrderStatusCode.DataSource = new BOLHardCode().GetHCDataTable("HCOrderStatuses");
            cboHCPayMethodCode.DataSource = new BOLHardCode().GetHCDataTable("HCPayMethods");

            #endregion
            if (!NewMode)
            {
                LoadData((int)Code);
                BOLOrders OrdersBOL = new BOLOrders();
                Orders CurOrder = ((IBaseBOL<Orders>)OrdersBOL).GetDetails((int)Code);
                int? UserTransactionCode = CurOrder.UserTransactionCode;
                if (UserTransactionCode != null)
                {
                    BOLUserTransactions UserTransactionsBOL = new BOLUserTransactions(1);
                    UserTransactions CurTrans = UserTransactionsBOL.GetDetails((int)UserTransactionCode);
                    if (CurTrans.HCTransStatusCode == 2)
                        lblPayStatus.Text = "پرداخت شده";
                }
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
        string Tab1Click = "BrowseObj1.ShowDetail('OrderProducts', '" + Code
              + "', true,'BrowseObj1');";


        Tab1.Attributes.Add("onclick", Tab1Click);
        Tools.SetClientScript(this, "ActiveTab1", Tab1Click);

    }
}
