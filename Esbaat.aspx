<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master"
    Title="وبسایت هادی رنجبران | نشر اثبات" CodeBehind="Esbaat.aspx.cs" Inherits="Ranjbaran.Esbaat" %>
<%@ Register Src="~/UserControls/Banner.ascx" TagName="Banner" TagPrefix="UCB" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="panel panel-default Marginer20">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                درباره انتشارات اثبات</h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
            </AKP:MsgBox>
            <UCB:Banner ID="Banner4" runat="server" PositionCode="5" />
            
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
