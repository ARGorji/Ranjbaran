<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="True" Inherits="Comments_EditComments" Title="Comments" CodeBehind="EditComments.aspx.cs" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <div class="cTblEdit">
        <div style="width: 700px; float: right; text-align: right; color: White;">
            <asp:Label runat="server" ID="lblSysName"></asp:Label></div>
        <div class="cTDEdit">
            <div class="cEditRight">
                <div class="cEditMain">
                    <div class="cEditMainData">
                        <div>
                            <AKP:MsgBox ID="msgBox" runat="server" CssClass="cError" />
                        </div>
                        <div>
                            <table class="cTblOneRow">
                                <tr>
                                    <td class="cFieldLeft">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExTextBox ID="txtEmail" jas="1" MaxLength="50" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblEmail" runat="server" Text="ایمیل:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExTextBox ID="txtName" jas="1" MaxLength="500" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblName" runat="server" Text="نام:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table class="cTblField">
                                <tr>
                                    <td class="cCtrl">
                                        <AKP:ExTextBox ID="txtTextComment" jas="1" Width="500" CssClass="cMultiLine" TextMode="MultiLine"
                                            MaxLength="4000" runat="server" />
                                    </td>
                                    <td class="cLabel">
                                        <asp:Label ID="lblTextComment" runat="server" Text="متن:"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table class="cTblOneRow">
                                <tr>
                                    <td class="cFieldLeft">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:FarsiDate ID="dteSendDate" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblSendDate" runat="server" Text="تاریخ ارسال:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:Combo ID="cboHCCommentStatusCode" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblHCCommentStatusCode" runat="server" Text="وضعیت:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <table class="cTblOneRow">
                                <tr>
                                    <td class="cFieldLeft">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:Combo ID="cboHCSectionCode" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblHCSectionCode" runat="server" Text="بخش:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="cHorSep">
        </div>
        <div class="TabHeaderData">
            <telerik:RadTabStrip Style="margin-right: 8px;" dir="rtl" ID="tsUsers" runat="server"
                MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Vista" SkinsPath="~/RadControls/TabStrip/Skins">
                <Tabs>
                    <telerik:RadTab ID="Tab1" runat="server" Text="گروه های کاربر">
                    </telerik:RadTab>
                    <telerik:RadTab ID="Tab2" runat="server" Text="فعالیتها های کاربر">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <div class="cTabWrapper">
                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server">
                    <telerik:RadPageView ID="PageView1" runat="server">
                        <div class="cBrowseArea" id="UserGroups">
                        </div>
                        <div class="cDivSep">
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                        <div class="cBrowseArea" id="UserLogs">
                        </div>
                        <div class="cDivSep">
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </div>
        <div style="text-align: right">
            <table class="tblEditButtons" cellpadding="2" cellspacing="4">
                <tr>
                    <td>
                        <a class="BlueButton" onclick="ChangeLang()">فارسی </a>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnBack" Text=" بازگشت " SkinID="BackButton" OnClick="GoToList"
                            runat="server" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnSave" Text=" ذخیره " SkinID="SaveButton" OnClick="DoSave"
                            runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hfPassword" runat="server" />
</asp:Content>
