<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NormalLogin.ascx.cs"
    Inherits="IONS.UserControls.NormalLogin" %>

    <div class="panel panel-default Marginer20" >
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                ورود به حساب کاربری</h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
                    </AKP:MsgBox>
            <div class="RegformCont" style="margin-right:200px;">
                <div class="input-group RegField">
                     
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="ایمیل" CssClass="form-control LTR" />
                   
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="کلمه عبور"
                        CssClass="form-control LTR" />
                    
                </div>
                <div class="input-group RegField">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Users/ForgotPassword.aspx" runat="server">کلمه عبور را فراموش کرده ام</asp:HyperLink>
                </div>
                <div class="input-group RegField">
                    <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Users/Register.aspx" runat="server">عضویت</asp:HyperLink>
                </div>
                <div style="margin-right:200px;">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="ورود" OnClick="btnSubmit_Click" />
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>


