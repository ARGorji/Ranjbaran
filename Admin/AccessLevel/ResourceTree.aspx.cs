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
using System.Drawing;

public partial class AccessLevel_ResourceTree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BOLResources BOLClass = new BOLResources();
            DataTable dt = BOLClass.GetAllFieldExcpetFields();

            TreeResources.DataTextField = "Name";
            TreeResources.DataFieldID = "Code";
            TreeResources.DataFieldParentID = "MasterCode";
            TreeResources.DataValueField = "Code";

            TreeResources.DataSource = dt;
            TreeResources.DataBind();
        }

    }

    protected bool CheckTextBox(Button button, TextBox textBox)
    {
        if (textBox.Text.Length == 0)
        {
            //RadAjaxManager1.Alert("Text for the node is required!");
            textBox.BackColor = Color.Yellow;
            return false;
        }
        else
        {
            textBox.BackColor = Color.White;
            return true;
        }
    }
    protected void btnAddChild_Click(object sender, System.EventArgs e)
    {
        if (TreeResources.SelectedNode == null)
        {
            //RadAjaxManager1.Alert("لطفا یک گره انتخاب کنید");
        }
        else
        {
            if (CheckTextBox(btnAddChild, tbNewNodeText))
            {
                BOLResources ResourcesBOl = new BOLResources();
                ResourcesBOl.Name = tbNewNodeText.Text;
                ResourcesBOl.MasterCode = Convert.ToInt32(TreeResources.SelectedNode.Value);
                int ReturnCode = ResourcesBOl.SaveChanges(true);

                RadTreeNode rtn = new RadTreeNode(tbNewNodeText.Text);
                rtn.Value = ReturnCode.ToString();
                TreeResources.SelectedNode.Nodes.Add(rtn);
                TreeResources.SelectedNode.ExpandChildNodes();

                tbNewNodeText.Text = string.Empty;
            }

        }
    }

    protected void TreeResources_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        tbNodeText.Text = e.Node.Text;
    }
    protected void btnRemove_Click(object sender, System.EventArgs e)
    {
        if (TreeResources.SelectedNode != null)
        {
            if (!TreeResources.SelectedNode.Equals(TreeResources.Nodes[0]))
            {
                if (TreeResources.SelectedNode.Nodes.Count != 0)
                {
                    //RadAjaxManager1.Alert("لطفا ابتدا گره های داخلی را حذف کنید");
                }
                else
                {
                    BOLResources ResourcesBOl = new BOLResources();
                    ((IBaseBOL<Resources>)ResourcesBOl).DeleteRecord(TreeResources.SelectedNode.Value);
                    TreeResources.SelectedNode.Remove();
                    tbNodeText.Text = string.Empty;
                }
            }
            else
            {
                //RadAjaxManager1.Alert("گره اصلی قابل انتقال نیست");
            }
        }
        else
        {
            //RadAjaxManager1.Alert("لطفا یک گره انتخاب کنید");
        }
    }

    protected void AddChilds(RadTreeNode dstNode, RadTreeNode srcNode)
    {
        foreach (RadTreeNode srcChildNode in srcNode.Nodes)
        {
            RadTreeNode destChildNode = new RadTreeNode(srcChildNode.Text, srcChildNode.Value);
            dstNode.Nodes.Add(destChildNode);
            AddChilds(destChildNode, srcChildNode);
        }
    }

    public void AjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
    {
        string[] args = e.Argument.Replace("\r\n", "\n").Split('\n');
        RadTreeView srcTree = null;
        RadTreeView dstTree = null;
        if (TreeResources.ClientID.Equals(args[0]))
        {
            srcTree = TreeResources;
        }
        else if (TreeResources.ClientID.Equals(args[0]))
        {
            srcTree = TreeResources;
        }

        if (TreeResources.ClientID.Equals(args[1]))
        {
            dstTree = TreeResources;
        }
        else if (TreeResources.ClientID.Equals(args[1]))
        {
            dstTree = TreeResources;
        }

        RadTreeNode sourceNode = srcTree.FindNodeByText(args[2]);
        RadTreeNode destNode = dstTree.FindNodeByText(args[3]);

        //if (sourceNode.Parent == null)
        //{
        //    RadAjaxManager1.Alert("گره اصلی قابل حذف نیست");
        //    return;
        //}
        RadTreeNode tempNode = destNode;
        while (tempNode != null)
        {
            if (!tempNode.Equals(sourceNode))
                tempNode = tempNode.ParentNode;
            else
                break;
        }
        if (tempNode != null)
        {
            //RadAjaxManager1.Alert("نمیتوان یک گره را به فرزندانش منتقل کرد");
            return;
        }

        BOLResources ResourcesBOl = new BOLResources();
        ResourcesBOl.Code = Convert.ToInt32(sourceNode.Value);
        ResourcesBOl.MasterCode = Convert.ToInt32(destNode.Value);
        ResourcesBOl.Name = sourceNode.Text;
        ResourcesBOl.SaveChanges(false);

        RadTreeNode newNode = new RadTreeNode(sourceNode.Text, sourceNode.Value);
        AddChilds(newNode, sourceNode);
        destNode.Nodes.Add(newNode);
        destNode.ExpandChildNodes();
        if (!sourceNode.Equals(srcTree.Nodes[0]))
        {
            sourceNode.Remove();
        }
    }

    protected void RadTreeView1_HandleDrop(object sender, RadTreeNodeDragDropEventArgs e)
    {
        RadTreeNode sourceNode = e.SourceDragNode;
        RadTreeNode destNode = e.DestDragNode;
        RadTreeViewDropPosition dropPosition = e.DropPosition;

        if (destNode != null) //drag&drop is performed between trees
        {
            PerformDragAndDrop(dropPosition, sourceNode, destNode);
        }
    }

    private void PerformDragAndDrop(RadTreeViewDropPosition dropPosition, RadTreeNode sourceNode, RadTreeNode destNode)
    {
        if (sourceNode.Equals(destNode) || sourceNode.IsAncestorOf(destNode))
        {
            return;
        }
        sourceNode.Owner.Nodes.Remove(sourceNode);

        switch (dropPosition)
        {
            case RadTreeViewDropPosition.Over:
                // child
                if (!sourceNode.IsAncestorOf(destNode))
                {
                    destNode.Nodes.Add(sourceNode);
                }
                break;

            case RadTreeViewDropPosition.Above:
                // sibling - above                    
                destNode.InsertBefore(sourceNode);
                break;

            case RadTreeViewDropPosition.Below:
                // sibling - below
                destNode.InsertAfter(sourceNode);
                break;
        }
        BOLResources ResourcesBOl = new BOLResources();
        ResourcesBOl.Code = Convert.ToInt32(sourceNode.Value);
        ResourcesBOl.MasterCode = Convert.ToInt32(destNode.Value);
        ResourcesBOl.Name = sourceNode.Text;
        ResourcesBOl.SaveChanges(false);

    }

    protected void btnRename_Click(object sender, System.EventArgs e)
    {
        if (CheckTextBox(btnRename, tbNodeText))
        {
            if (TreeResources.SelectedNode != null)
            {
                BOLResources ResourcesBOl = new BOLResources();
                ResourcesBOl.Code = Convert.ToInt32(TreeResources.SelectedNode.Value);
                ResourcesBOl.Name = tbNodeText.Text;
                if (TreeResources.SelectedNode.Parent != null)
                    ResourcesBOl.MasterCode = Convert.ToInt32(((RadTreeNode)TreeResources.SelectedNode.Parent).Value);
                else
                    ResourcesBOl.MasterCode = null;
                ResourcesBOl.SaveChanges(false);

                TreeResources.SelectedNode.Text = tbNodeText.Text;
            }
            else
            {
                //RadAjaxManager1.Alert("لطفا یک گره برای تغییر نام انتخاب کنید");
            }
        }
    }

    protected void btnAddRoot_Click(object sender, System.EventArgs e)
    {
        if (CheckTextBox(btnAddRoot, tbNewNodeText))
        {
            TreeResources.Nodes.Add(new RadTreeNode(tbNewNodeText.Text));
            BOLResources ResourcesBOl = new BOLResources();
            ResourcesBOl.Name = tbNewNodeText.Text;
            ResourcesBOl.MasterCode = null;
            ResourcesBOl.SaveChanges(true);
            tbNewNodeText.Text = string.Empty;
        }
    }
}
