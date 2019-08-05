<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master" Title="وبسایت هادی رنجبران | بازیابی کلمه عبور" CodeBehind="ForgotPassword2.aspx.cs" Inherits="Ranjbaran.UsersFolder.ForgotPassword2" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    
    <div class="panel panel-default Marginer20 EditForm">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                بازیابی کلمه عبور | قسمت نهایی </h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
                    </AKP:MsgBox>
            
            <asp:Panel runat="server" ID="pnlNewPassword" CssClass="RegformCont">
                
                <div class="input-group RegField">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="کلمه عبور"
                        CssClass="form-control LTR" />
                    <span class="input-group-addon">کلمه عبور: </span>
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="تکرار کلمه عبور"
                        CssClass="form-control LTR" />
                    <span class="input-group-addon">تکرار کلمه عبور: </span>
                </div>

                <div style="margin-right:200px;">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="ارسال" OnClick="btnSetNewPassword_Click" />
                </div>
            </asp:Panel>
            <div class="clear"></div>
        </div>
    </div>
    <div class="clear"></div>
</asp:Content>
