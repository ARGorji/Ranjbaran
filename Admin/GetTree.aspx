<%@ Page Language="C#" AutoEventWireup="true" Inherits="GetTree" Codebehind="GetTree.aspx.cs" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tree</title>
    <%--<link id="Link1" runat="server" href="~/Admin/styles/main.css" rel="stylesheet" type="text/css" />--%>

</head>
<body >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div >
        <telerik:RadTreeView ID="RadTree1" OnClientDoubleClick="SelectNode" Width="400px" runat="server" dir="rtl" Skin="WebBlue" >
        </telerik:RadTreeView>
    </div>
    <div style="clear:both"></div>
    
    </form>
        <script language="javascript" type="text/javascript">
function SelectNode(sender, eventArgs)
{
    var node = eventArgs.get_node();    
    NodeCode = node.get_value();
    NodeText = node.get_text();

    opener.aspnetForm.<%=FFC%>.value = NodeCode;
    opener.aspnetForm.<%=FFN%>.value = NodeText;
    window.close();

}
    
    </script>
</body>
</html>
