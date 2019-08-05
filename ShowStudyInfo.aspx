<%@ Page Language="C#" AutoEventWireup="true" Title="" MasterPageFile="~/MainMaster.master"
    CodeBehind="ShowStudyInfo.aspx.cs" Inherits="Ranjbaran.ShowStudyInfo" %>

<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<%@ Register Src="~/UserControls/UCNews.ascx" TagName="UCNews" TagPrefix="UCN" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="panel panel-default MainArea">
        <div class="Padder5">
            <div class="panel panel-default Marginer20">
                <div class="panel-heading ListTitle">
                    <h3 class="panel-title BulletList">
                        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                    </h3>
                </div>
                <div class="Padder5">
                    <AKP:MsgBox runat="server" ID="msg">
                    </AKP:MsgBox>
                    <asp:Panel runat="server" ID="pnlDesc" CssClass="center MarginTopBot">
                        <table style="width: 720px;" align="center" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img src="/images/Templates/WinShadow_01.gif" width="29" height="23" alt="" />
                                </td>
                                <td style="background-image: url(/images/Templates/WinShadowTop.jpg); height: 23px;">
                                </td>
                                <td>
                                    <img src="/images/Templates/WinShadow_03.gif" width="28" height="23" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td style="background-image: url(/images/Templates/WinShadowLeft.jpg); width: 29px;">
                                </td>
                                <td style="text-align: justify; direction: rtl; line-height: 150%; ">
                                    <asp:Literal ID="ltrDesc" runat="server"></asp:Literal>
                                </td>
                                <td style="background-image: url(/images/Templates/WinShadowRight.jpg); width: 28px;">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src="/images/Templates/WinShadow_07.gif" width="29" height="27" alt="" />
                                </td>
                                <td style="background-image: url(/images/Templates/WinShadowBottom.jpg); height: 27px;">
                                </td>
                                <td>
                                    <img src="/images/Templates/WinShadow_09.gif" width="28" height="27" alt="" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <%--<UCN:UCNews runat="server" SectionCode="3" />--%>
                    <asp:Repeater ID="rptStudyInfos" runat="server">
                        <ItemTemplate>
                            <div class="StudyInfoBrief">
                                <ul class="StudyInfoItemList">
                                    <li>
                                        <div class="StudyInfoTitle">
                                            <%#Eval("Title") %>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="ViewDate">
                                            <%#Tools.ChangeEnc(Eval("CDate"))%>
                                        </div>
                                    </li>
                                    <li>
                                        <div class="ViewButton">
                                            <div class="CourseDayCont" onclick="ToggleStudyInfo('<%#Eval("Code")%>')">
                                                مشاهده
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <div class="clear">
                            </div>
                            <div id="StudyInfo<%#Eval("Code") %>" class="StudyInfoBody hide">
                                <%#Eval("Description") %>
                            </div>
                            <div class="clear">
                            </div>
                            <div class="LineShadow">
                            </div>
                            <div class="clear">
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="Clear">
                    </div>
                    <asp:Panel runat="server" ID="pnlPaging">
                        <Tlb:PagerToolbar runat="server" ID="pgrToolbar" />
                    </asp:Panel>
                </div>
            </div>
            <div id="divLinks">
            </div>
        </div>
    </div>
</asp:Content>
