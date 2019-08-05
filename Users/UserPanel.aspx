<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master" Title="صفحه شخصی" CodeBehind="UserPanel.aspx.cs" Inherits="Ranjbaran.UsersFolder.UserPanel" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="panel panel-default Marginer20">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                پنل کاربر</h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
            </AKP:MsgBox>
            <div class="RegformCont" style="margin-right: 180px;">
                <div class="input-group" style="width:200px;">
                    <asp:HyperLink CssClass="input-group-addon" runat="server">تغییر مشخصات</asp:HyperLink>
                </div>
                <div class="input-group" style="width:200px;">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Users/UserTrans.aspx" CssClass="input-group-addon" runat="server">خریدهای من</asp:HyperLink>
                </div>
                <div class="input-group" style="width:200px;">
                    <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Users/Logout.aspx" CssClass="input-group-addon" runat="server">خروج</asp:HyperLink>
                </div>
                
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>