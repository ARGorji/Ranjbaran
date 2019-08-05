<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master"
    Title="وبسایت هادی رنجبران | تماس با ما" CodeBehind="ContactUs.aspx.cs" Inherits="Ranjbaran.ContactUs" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="panel panel-default Marginer20">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                تماس با ما</h3>
        </div>
        <div>
            <div style="line-height: 150%; margin-top: 50px; margin-bottom: 50px; margin-right: 10px;"
                class="RTL">
                
                
                آدرس: تهران، میدان انقلاب، خیابان کارگر شمالی، کوچه مستعلی، پلاک26، واحد2. تلفن 66908570
            </div>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
            </AKP:MsgBox>
            <div class="RegformCont" style="margin-right: 180px;">
                <div class="input-group RegField">
                    <asp:TextBox runat="server" ID="txtName" Width="250" placeholder="نام" CssClass="form-control" />
                    <span class="input-group-addon">نام: </span>
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtEmail" runat="server" Width="250" placeholder="ایمیل" CssClass="form-control LTR" />
                    <span class="input-group-addon">ایمیل: </span>
                </div>
                <div>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="cReq"
                        ValidationGroup="ValidateRegister" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ControlToValidate="txtEmail" ErrorMessage="ایمیل معتبر نیست"></asp:RegularExpressionValidator>
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtContactNumber" runat="server" Width="250" placeholder="شماره تماس"
                        CssClass="form-control LTR" />
                    <span class="input-group-addon">شماره تماس: </span>
                </div>
                <div class="input-group RegField">
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="250" Height="100"
                        placeholder="متن" CssClass="form-control" />
                    <span class="input-group-addon">متن: </span>
                </div>
                <div style="margin-right: 325px; margin-top: 5px;">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="ValidateRegister"
                        runat="server" Text="ارسال" OnClick="btnSubmit_Click" />
                </div>
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
