<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master" Title="وبسایت هادی رنجبران | پرسش و پاسخ" CodeBehind="Questions.aspx.cs" Inherits="Ranjbaran.Questions" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <h1 class="PageTitle">
        <asp:Image ID="Image1" ImageUrl="~/images/lblQuestions.png" ToolTip="پرسش و پاسخ" runat="server" />
    </h1>
    <div class="NewsItemCont">
        <asp:Repeater ID="rptQuestions" runat="server">
            <ItemTemplate>
                <div class="QuestionItem">
                    <div>
                        <ul class="NewsItemList">
                            <li>
                                <div class="Question">
                                    <%#Eval("Ques") %>
                                </div>
                            </li>
                            <li>
                                <div class="ViewNews" onclick="ToggleQuestion('<%#Eval("Code")%>')">
                                    <%#Tools.ChangeEnc( Eval("SDate")) %>
                                </div>
                            </li>
                            <li>
                                <div class="NewsDate">
                                    <div class="AnswerCont" onclick="ToggleQuestion('<%#Eval("Code")%>')">
                                        مشاهده پاسخ
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="LineShadow">
                    </div>
                    <div class="Clear">
                    </div>
                    <div id="Answer<%#Eval("Code") %>" class="NewsBody hide">
                        <%#Tools.FormatString( Eval("Answer") )%>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="SendQuestion">
        <AKP:MsgBox runat="server" ID="msg">
    </AKP:MsgBox>
        <table class="tblSendQuestion">
            <tr>
                <td>
                    <AKP:ExTextBox runat="server" CssClass="RTL" Width="620" TextMode="MultiLine" ID="txtQuestion"></AKP:ExTextBox>
                </td>
                <td class="lbl">
                    <asp:Label ID="Label1" runat="server" Text="پرسش:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" 
                        Text="ارسال" onclick="btnSubmit_Click"
                         />
                </td>
                
            </tr>
        </table>
    </div>
</asp:Content>