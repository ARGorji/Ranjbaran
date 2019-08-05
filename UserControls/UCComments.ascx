<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCComments.ascx.cs"
    Inherits="ASNoor.UserControls.UCComments" %>
<div class="NewsCommentContainer">
    <div class="NewsComments">
        <div class="CommentFooter">
            <div class="FooterLabel">
                <asp:Literal ID="ltrTitle" runat="server">التعلیقات</asp:Literal>
            </div>
        </div>
        <asp:Panel ID="PublishInfo" runat="server" CssClass="CommentHeader">
            <div class="MainLabel">
                
            </div>
            <div class="PublishedCount">
                <asp:Label ID="lblPublishedCount" runat="server" Text=""></asp:Label>
            </div>
            <div class="WillNotBePublishedCount">
                <asp:Label ID="lblWillNotBePublishedCount" runat="server" Text=""></asp:Label>
            </div>
        </asp:Panel>
        <div>
            <asp:Repeater ID="rptComments" runat="server">
                <ItemTemplate>
                    <div class="CommentReplyHeader">
                        <div class="Name">
                            <%#Eval("Name") %>
                        </div>
                        <div class="SendDate">
                            <%#PersianDate(Eval("SendDate")) %>
                        </div>
                    </div>
                    <div class="Comment">
                        <img alt="" src="/images/site/comments.gif" style="padding-left: 3px;">
                        <%#Eval("TextComment") %>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div style="margin-top: 5px;">
                        <AKP:MsgBox runat="server" ID="msgBox">
                        </AKP:MsgBox>
                    </div>
                    <table class="tblComment">
                        <tr>
                            <td style="padding-right:30px;">
                                <AKP:ExTextBox ID="txtComment" Text="نظر" onclick="this.className='input-text'; this.value='';" onblur="this.className='GrayText';if(this.value == '') this.value= 'نظر';" CssClass="CommentText" TextMode="MultiLine" Width="350"
                                    Height="200" runat="server"></AKP:ExTextBox>
                            </td>
                            <td>
                                <div class="TextContainer" style="margin-top:0px;">
                                    <AKP:ExTextBox ID="txtName" Text="الاسم" onclick="this.className='input-text';this.value='';" onblur="if(this.value == '') this.value= 'نام';" Width="220" CssClass="CommentText" runat="server"></AKP:ExTextBox>
                                </div>
                                <div class="TextContainer">
                                    <AKP:ExTextBox ID="txtEmail" Width="220" onclick="this.className='input-text';this.value='';" Text="ایمیل" onblur="if(this.value == '') this.value= 'ایمیل';" SkinID="English"  CssClass="CommentText" runat="server"></AKP:ExTextBox>
                                </div>
                                <div class="TextContainer">
                                    <asp:Label ID="lblCaptcha" runat="server" Text="کد امنیتی را وارد کنید"></asp:Label>
                                </div>
                                <div>
                                    <telerik:RadCaptcha ID="RadCaptcha1" ValidationGroup="ContactCenter" CssClass="Capt"
                                        Width="200" CaptchaImage-ImageCssClass="CaptImg" CaptchaTextBoxCssClass="CaptText"
                                        runat="server" ErrorMessage="" CaptchaTextBoxLabel="">
                                    </telerik:RadCaptcha>
                                </div>
                                <div class="btnSendCommentCont">
                                    <asp:ImageButton ImageUrl="~/images/btnUCSendComment.png" ID="btnSendComment" CssClass="btnSendComment" Text="ارسال" 
                                        runat="server" OnClick="btnSendComment_Click"></asp:ImageButton>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
