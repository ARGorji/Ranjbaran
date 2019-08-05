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
using Telerik.Web.UI;



public partial class AccessGroups_EditAccessGroups : EditForm<AccessGroups>
{

    protected bool ValidateInputs()
    {
        return true;
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        BOLClass = new BOLAccessGroups();


        if (Code == null)
            if (!NewMode)
                return;
        if (!Page.IsPostBack)
        {
            string InstanceName = Request["InstanceName"];
            ViewState["InstanceName"] = InstanceName;


            if (!NewMode)
            {
                LoadData((int)Code);

                IBaseBOL BOL = new BOLResources();

                DataTable dt = BOL.GetDataSource(null, string.Empty, int.MaxValue, 0);
                foreach (DataRow row in dt.Rows)
                {
                    Telerik.Web.UI.RadTreeNode node = new Telerik.Web.UI.RadTreeNode();
                    node.Text = (string)row["Name"];
                    node.Value = ((int)row["Code"]).ToString();
                    node.Category = "Some Category";
                    node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
                    RadTree1.Nodes.Add(node);
                }

                RadTree1.LoadingMessage = "( در حال بارگذاری ... )";

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
                Response.Redirect("~/Main/?BaseID=AccessGroups");
            }
        }
        catch
        {
        }
    }

    protected void RadTree1_NodeExpand(object o, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        AddChildNodes(e.Node);
    }
    private void AddChildNodes(Telerik.Web.UI.RadTreeNode node)
    {

        BOLResources ReourseBOL = new BOLResources();
        DataTable dt = ReourseBOL.GetNodeData(Convert.ToInt32(node.Value));

        foreach (DataRow row in dt.Rows)
        {
            Telerik.Web.UI.RadTreeNode childNode = new Telerik.Web.UI.RadTreeNode();
            childNode.Text = (string)row["Name"];
            childNode.Value = ((int)row["Code"]).ToString();

            DataTable dtChild = ReourseBOL.GetNodeData(Convert.ToInt32(childNode.Value));
            if (dtChild.Rows.Count > 0)
            {
                childNode.ExpandMode = TreeNodeExpandMode.ServerSideCallBack;
            }

            node.Nodes.Add(childNode);
        }
    }

}

