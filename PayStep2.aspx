<%@ Page Language="C#" AutoEventWireup="true" Title="پرداخت آنلاین | مرحله دوم" MasterPageFile="~/MainMaster.master" CodeBehind="PayStep2.aspx.cs" Inherits="Ranjbaran.PayStep2" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    

    <div class="panel panel-default Marginer20">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                پرداخت مرحله دوم</h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgBox"></AKP:MsgBox>
            <asp:Panel CssClass="RegformCont" ID="pnlPayForm" runat="server" style="margin-right: 180px;">
                <div class="input-group RegField">
                    <span class="input-group-addon"  style="white-space:nowrap;">تاریخ: </span><br />
                    <asp:Label ID="lblDate" CssClass="form-control" runat="server" Text=""></asp:Label>
                    
                </div>
                <div class="input-group RegField">
                    <span class="input-group-addon"  style="white-space:nowrap;">مبلغ تراکنش: </span><br />
                    <asp:Label ID="lblAmount" CssClass="form-control" runat="server" Text=""></asp:Label>
                    
                </div>
                <div class="input-group RegField">
                    <span class="input-group-addon">عنوان: </span><br />
                    <asp:Label ID="lblTitle" CssClass="form-control" runat="server" Text=""></asp:Label>
                    
                </div>
                <div >
                    <asp:Button ID="btnDownload" Visible="false" CssClass="btn btn-primary" ValidationGroup="ValidateRegister"
                        runat="server" Text="دریافت فایل" OnClick="btnDownload_Click" />
                </div>
            </asp:Panel>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
