<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="ASNoor.Admin.UserLogin" %>

<%@ Register Src="~/UserControls/Login.ascx" TagName="Login" TagPrefix="UCLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>وبسایت شخصی هادی رنجبران</title>
    <link id="Link1" runat="server" href="~/Admin/Styles/main.css" rel="stylesheet" type="text/css" />
    <link id="Link2" runat="server" href="~/Admin/Styles/PersianDate.css" rel="stylesheet"
        type="text/css" />
    <link id="Link3" runat="server" href="~/Admin/Styles/Browse.css" rel="stylesheet"
        type="text/css" />
    <link id="Link4" runat="server" href="~/Admin/Styles/TreeView.Outlook.css" rel="stylesheet"
        type="text/css" />
    <link id="Link5" runat="server" href="~/Admin/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="wrap">
        <div class="header">
            <div class="banner">
                <asp:HyperLink ID="imgBanner" NavigateUrl="~/Admin/" runat="server" ImageUrl="~/Admin/images/AdminHeader.jpg"
                    alt="" /></div>
        </div>
        <br class="clearfloat" />
        <br class="clearfloat" />
        <div class="container">
            <div class="main">
                <h1 class="title">
                    <asp:Label runat="server" ID="lblTitle" Text="" /></h1>
                <div class="pageBody">
                    <div class="homeBox" style="text-align: center;margin-right:100px;margin-top:30px;margin-bottom:30px;">
                        <UCLogin:Login runat="server" ID="Login1" />
                        <br class="clearfloat" />
                    </div>
                </div>
            </div>
            <div>
                <asp:Image ID="Image5" runat="server" ImageUrl="~/Admin/images/spacer.gif" />
            </div>
        </div>
        <div class="homeLable">
            <div style="float: right">
                <asp:HyperLink ID="Image1" Width="25px" ToolTip="خروج از سامانه" runat="server" ImageUrl="~/Admin/images/Site/logout.gif"
                    NavigateUrl="~/Admin/logout.aspx" />
            </div>
            <div>
                کاربر :
                <asp:Label runat="server" ID="lblFullName"></asp:Label>
                <br />
            </div>
        </div>
        <div class="homeLable">
            <span id="ctl00_lblFooter1">&copy; تمامی حقوق این سایت محفوظ می باشد. </span>
            <br />
            <span id="ctl00_lblFooter2">طراحی و پیاده سازی:</span> <a href="http://parset.com"
                target="_blank" id="ctl00_HyperLink5">شرکت پیشرو عصر دانش</a>
        </div>
        <div class="homeLable">
        </div>
        <br class="clearfloat" />
    </div>
    </form>
</body>
</html>
