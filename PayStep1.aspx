<%@ Page Language="C#" AutoEventWireup="true" Title="پرداخت | مرحله اول" MasterPageFile="~/MainMaster.master"
    CodeBehind="PayStep1.aspx.cs" Inherits="Ranjbaran.PayStep1" %>

<%@ Register Src="~/UserControls/NormalLogin.ascx" TagName="NormalLogin" TagPrefix="UCLogin" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    
    <div class="panel panel-default Marginer20">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                پرداخت</h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
            </AKP:MsgBox>
            <asp:Panel CssClass="RegformCont" ID="pnlPayForm" runat="server" Style="margin-right: 180px;">
                <div>
                    <span class="input-group-addon" style="white-space: nowrap;">مبلغ تراکنش: </span>
                </div>
                <div class="input-group RegField">

                    <asp:Label ID="lblAmount" CssClass="form-control" runat="server" Text=""></asp:Label>
                    
                </div>
                <div>
                    <span class="input-group-addon">عنوان: </span>
                </div>
                <div class="input-group RegField">
                    <asp:Label ID="lblTitle" CssClass="form-control" runat="server" Text=""></asp:Label>
                    
                </div>
                <div>
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="ValidateRegister"
                        runat="server" Text="پرداخت آنلاین" OnClick="btnSubmit_Click" />
                </div>
            </asp:Panel>
            <div class="clear">
            </div>
            <div class="NewsItemCont">
                <UCLogin:NormalLogin ID="Normallogin1" runat="server" />
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
