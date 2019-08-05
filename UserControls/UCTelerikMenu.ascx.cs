using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IONS.UserControls
{
    public partial class UCTelerikMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BOLResources BOL = new BOLResources();
            RadMenu1.DataTextField = "Name";
            RadMenu1.DataFieldID = "Code";
            RadMenu1.DataFieldParentID = "MasterCode";
            //RadMenu1.DataNavigateUrlField = "BaseID";
            RadMenu1.DataValueField = "BaseID";


            Tools tools = new Tools();
            System.Collections.Generic.List<AccessListStruct> AccessList = new Tools().GetAccessList(null);
            DataTable dt = BOL.GetValidAccess(AccessList, null, string.Empty, int.MaxValue, 0);
            if (dt.Rows.Count > 0)
            {
                RadMenu1.DataSource = dt;
                RadMenu1.DataBind();
            }

        }
    }
}