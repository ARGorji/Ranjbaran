<%@ Page Language="C#" AutoEventWireup="true" Title="وبسایت هادی رنجبران | عضویت"
    MasterPageFile="~/MainMaster.master" CodeBehind="Register.aspx.cs" Inherits="Ranjbaran.UsersFolder.Register" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="panel panel-default Marginer20">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                عضویت</h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
            </AKP:MsgBox>
            <div class="RegformCont" >
                <div class="input-group RegField">
                    <asp:DropDownList ID="cboGenderCode" DataTextField="Name" DataValueField="Code" Width="149"
                        CssClass="form-control" runat="server">
                    </asp:DropDownList>
                    
                </div>
                <div class="input-group RegField">
                    <asp:TextBox runat="server" ID="txtFirstName" placeholder="نام" CssClass="form-control" />
                    
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtLastName" runat="server" placeholder="نام خانوادگی" CssClass="form-control" />
                    
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="ایمیل" CssClass="form-control LTR" />
                    
                </div>
                <div>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="cReq"
                        ValidationGroup="ValidateRegister" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="txtEmail" ErrorMessage="ایمیل معتبر نیست"></asp:RegularExpressionValidator>
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtContactNumber" runat="server" placeholder="شماره تماس" CssClass="form-control LTR" />
                    
                </div>
                <div class="input-group RegField">
                    <asp:DropDownList ID="cboHCStudyFieldCode" DataTextField="Name" DataValueField="Code"
                        Width="149" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                    
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="کلمه عبور"
                        CssClass="form-control" />
                    
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" placeholder="تکرار کلمه عبور"
                        CssClass="form-control" />
                    
                </div>
                
                <div style="margin-right: 200px;">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="ValidateRegister"
                        runat="server" Text="ارسال" OnClick="btnSubmit_Click" />
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
