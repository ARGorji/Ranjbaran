<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/MainMaster.master" Title="وبسایت هادی رنجبران | بازیابی کلمه عبور" CodeBehind="ForgotPassword.aspx.cs" Inherits="Ranjbaran.UsersFolder.ForgotPassword" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    
    <div class="panel panel-default Marginer20 EditForm">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                بازیابی کلمه عبور </h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
                    </AKP:MsgBox>
            <asp:Panel runat="server" ID="pnlSend" CssClass="RegformCont">
                <div class="input-group RegField">
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="ایمیل" CssClass="form-control LTR" />
                    
                </div>
                <div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" CssClass="cReq" ValidationGroup="ForgotPass" ControlToValidate="txtEmail"
                                        runat="server" ErrorMessage="ایمیل را وارد کنید"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="cReq" Display="Dynamic"
                                        runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ControlToValidate="txtEmail" ValidationGroup="ForgotPass" ErrorMessage="ایمیل معتبر نیست"></asp:RegularExpressionValidator>
                </div>
                <div style="margin-right:200px;">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="ارسال" OnClick="btnSend_Click" />
                </div>
            </asp:Panel>
            <div class="clear"></div>
        </div>
    </div>
    <div class="clear"></div>
</asp:Content>