<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCRegister.ascx.cs"
    Inherits="Ranjbaran.UserControls.UCRegister" %>
<div class="panel panel-default Marginer20">
    <div class="panel-heading ListTitle">
        <h3 class="panel-title BulletList text-center">
            عضویت در سایت اثبات</h3>
    </div>
    <div class="Padder20">
        <AKP:MsgBox runat="server" ID="msgMessage">
        </AKP:MsgBox>
        <div class="RegformCont form-horizontal">
            
            <div class="form-group RegField">
                <asp:TextBox runat="server" ID="txtFirstName" placeholder="نام" CssClass="form-control" />
            </div>
            <div class="form-group RegField">
                <asp:TextBox ID="txtLastName" runat="server" placeholder="نام خانوادگی" CssClass="form-control" />
            </div>
            <div class="form-group RegField">
                <asp:TextBox ID="txtEmail" runat="server" placeholder="ایمیل" CssClass="form-control LTR" />
            </div>
            <div>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="cReq"
                    ValidationGroup="ValidateRegister" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txtEmail" ErrorMessage="ایمیل معتبر نیست"></asp:RegularExpressionValidator>
            </div>
            <div class="form-group RegField">
                <asp:TextBox ID="txtPassword" runat="server" SkinID="English" TextMode="Password"
                    placeholder="کلمه عبور" CssClass="form-control LTR" />
            </div>
            <div class="form-group RegField Relative">
                <asp:TextBox ID="txtConfirmPassword" runat="server" SkinID="English" TextMode="Password"
                    placeholder="تکرار کلمه عبور" CssClass="form-control LTR" />
                <span onmouseup="hidePass($(this))" onmousedown="showPass($(this))" class="viewpass">
                    <img src="../images/viewpassword.png"></span>
            </div>
            <div style="margin-right: 0px;">
                <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="ValidateRegister"
                    runat="server" Text="ارسال" OnClick="btnSubmit_Click" />
            </div>
            <div style="margin-top: 10px;">
                <asp:Button ID="btnBackToRefPage" Visible="false" CssClass="btn btn-primary" ValidationGroup="ValidateRegister"
                    runat="server" Text="بازگشت به صفحه پیشین" OnClick="btnBackToRefPage_Click" />
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
</div>
