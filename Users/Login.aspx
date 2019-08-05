<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master"
    Title="وبسایت هادی رنجبران | ورود اعضا" CodeBehind="Login.aspx.cs" Inherits="Ranjbaran.UsersFolder.Login" %>

<%@ Register Src="~/UserControls/NormalLogin.ascx" TagName="NormalLogin" TagPrefix="UCLogin" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    
    <h1 class="PageTitle">
        <asp:Image ID="Image1" ImageUrl="~/images/lblLogin.png" ToolTip="ورود اعضا" runat="server" />
    </h1>
    <div class="NewsItemCont">
        <uclogin:normallogin runat="server" />
    </div>
    <div class="SendQuestion">
        <AKP:MsgBox runat="server" ID="msg">
        </AKP:MsgBox>
    </div>
</asp:Content>
