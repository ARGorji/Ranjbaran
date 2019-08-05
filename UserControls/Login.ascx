<%@ Control Language="C#" AutoEventWireup="True" Inherits="UserControls_Login" Codebehind="Login.ascx.cs" %>


<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <div class="cLoginInner">
            <div>
                <AKP:MsgBox ID="msgBox" runat="server" />
            </div>
            <div class="Clear"></div>
            <asp:Panel runat="server" ID="pnlLogin" CssClass="MainLogin">
                <table border="0" class="ctblLogin" cellpadding="2" cellspacing="4" >
                    <tr>
                        <td align="right" style="width: 90px">
                            <AKP:ExTextBox ID="txtUsername" SkinID="English" runat="server" Width="116px" Height="18px" />
                        </td>
                        <td class="cLoginLabel">
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" ForeColor="#365887" Text="نام کاربر  :"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 90px;">
                            <AKP:ExTextBox ID="txtPassword" SkinID="English" runat="server" TextMode="Password"
                                Width="116px" Height="18px"  />
                        </td>
                        <td class="cLoginLabel">
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" ForeColor="#365887" Text="کلمه عبور  :"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width: 90px;">
                            <telerik:RadCaptcha ID="RadCaptcha1" CssClass="Capt" Width="300" CaptchaImage-ImageCssClass="CaptImg"
                                    CaptchaTextBoxCssClass="CaptText" ValidationGroup="ValidateLogin" runat="server"
                                    ErrorMessage="" CaptchaTextBoxLabel="">
                                    <CaptchaImage  TextChars="Numbers" />
                                </telerik:RadCaptcha>
                        </td>
                        <td class="cLoginLabel">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" ForeColor="#365887" Text="کد امنیتی  :"></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td style="text-align: left;height:40px;">
                            <asp:Button ID="imgBtnLogin" runat="server" BorderWidth="0" Width="100" Height="28" ValidationGroup="ValidateLogin" CssClass="LoginButton" OnClick="imgBtnLogin_Click" />
                        </td>
                        <td style="text-align: right">
                            
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td colspan="2" style="direction: rtl; text-align: right;">
                            <AKP:ExCheckBox ID="chkRemLoginInfo" Text="مشخصات مرا به خاطر بسپار" AutoPostBack="true"
                                runat="server"  />
                        </td>
                    </tr>
                    
                </table>
            </asp:Panel>
            <div class="Clear"></div>
        </div>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>

<script type="text/javascript" language="javascript">
    function ShowRegMemberForm() {

        document.getElementById("fraMainBody").src = 'Forms/OnlineReg/Prompt.aspx';
        //window.open('Forms/OnlineReg/Prompt.aspx', 'Prompt', 'width=380,height=200,top=200,left=400,scrollbars=no,resizable=no');
    }

    function ShowRegRealEstateForm() {

        //document.getElementById("fraMainBody").src = 'OnlineReg/EditRealEstateOnlineReg.aspx';
        window.open('Forms/OnlineReg/EditRealEstateOnlineReg.aspx', 'OnlineReg', 'width=750,height=520,top=0,scrollbars=yes,resizable=1'); return false;
    }

    function GoForSearch() {

        var fra = document.getElementById("fraMainBody");
        fra.src = s;
        //this.frames["fraMainBody"].location = s;
        //window.open(s);
    }

    function DisableButton() {
        var x = document.getElementById("<%=imgBtnLogin.ClientID%>");
        x.disabled = true;
    }


    function PKIlogin() {
        if (ValidateInputs()) {
            (wsPKIlogin(document.getElementById('<%=txtUsername.ClientID%>').value, (document.getElementById('Login1_txtPassword').value)
           , document.getElementById('captcha_txtCaptcha').value,
           document.getElementById('Login1_hidRawUrl').value, document.getElementById('Login1_hidUrlHost').value));


        }

        return false;
    }

    function ValidateInputs() {

        var username = document.getElementById('Login1_txtUsername').value;
        var pass = document.getElementById('Login1_txtPassword').value;
        var msg;
        if (username == "") {
            msg = " نام کاربری را وارد نمایید";
            document.getElementById('Login1_msgBox').innerText = msg;
            alert(msg);
            return false;
        }

        if (pass == "") {
            msg = "رمز عبور را وارد نمایید";
            document.getElementById('Login1_msgBox').innerText = msg;
            alert(msg);
            return false;
        }


        return true;
    }

    function ContainsIllegalCharacter(p) {
        p = p.toUpperCase();
        if
            (p.search('\'') != -1 ||
                p.search('"') != -1 ||
                p.search(';') != -1 ||
                p.search('<') != -1 ||
                p.search('>') != -1 ||
                p.search('%') != -1 ||
        //                p.search('\/')!=-1 ||
                p.search('&') != -1 ||
        //                p.search("..")!=-1 ||
                p.search("--") != -1 ||
                p.search("~") != -1 ||
                p.search("0X") != -1 ||
                p.search("SELECT") != -1 ||
                p.search("DELETE") != -1 ||
                p.search("SHUTDOWN") != -1 ||
                p.search("EXEC") != -1 ||
                p.search("SCRIPT") != -1 ||
                p.search("UNION") != -1 ||
                p.search("INSERT") != -1 ||
                p.search("UPDATE") != -1 ||
                p.search("ALTER") != -1 ||
                p.search("SCRIPT") != -1 ||
                p.search("DROP") != -1 ||
                p.search("LIKE") != -1 ||
                p.search("HAVING") != -1 ||
                p.search("CAST") != -1 ||
                p.search(" OR ") != -1 ||
                p.search(" AND ") != -1 ||
                p.search("CREATE") != -1 ||
                p.search("WHERE") != -1 ||
                p.search("GROUP BY") != -1 ||
                p.search("CHR") != -1
            )
            return false;

        return true;
    }

    function OpenKeyboard(controlID, target) {
        VirtualKeyboard.toggle(controlID, target);
        //return false;
    }


    
</script>

<asp:HiddenField ID="hidRawUrl" runat="server" />
<asp:HiddenField ID="hidUrlHost" runat="server" />
<asp:HiddenField ID="hidNeedCert" runat="server" />
<asp:HiddenField ID="hfPrePage" runat="server" />
