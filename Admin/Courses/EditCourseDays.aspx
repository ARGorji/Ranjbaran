<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/EditPopup.master"
    AutoEventWireup="True" Inherits="CourseDays_EditCourseDays" Title="CourseDays"
    CodeBehind="EditCourseDays.aspx.cs" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <table cellpadding="0" cellspacing="0" align="center" width="100%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="90%" class="cMainEditTable">
                    <tr>
                        <td>
                            <akp:msgbox id="msgBox" runat="server" cssclass="cError" />
                        </td>
                    </tr>
                    <div>
                        <table class="cTblOneRowPopup">
                            <tr>
                                <td class="cFieldLeft">
                                    <table class="cTblField">
                                        <tr>
                                            <td>
                                                <table class="EditRow">
                                                    <tr>
                                                        <td class="cCtrl">
                                                            <akp:combo id="cboHCDayCode" jas="1" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblHCDayCode" runat="server" Text="روز:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cFieldRight">
                                    <table class="cTblField">
                                        <tr>
                                            <td>
                                                <table class="EditRow">
                                                    <tr>
                                                        <td class="cCtrl">
                                                            <akp:extextbox id="txtStartTime" jas="1" maxlength="50" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblStartTime" runat="server" Text="زمان شروع:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <table class="cTblOneRowPopup">
                            <tr>
                                <td class="cFieldLeft">
                                    <table class="cTblField">
                                        <tr>
                                            <td>
                                                <table class="EditRow">
                                                    <tr>
                                                        <td class="cCtrl">
                                                            <akp:extextbox id="txtEndTime" jas="1" maxlength="50" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblEndTime" runat="server" Text="زمان پایان:"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="cFieldRight">
                                </td>
                            </tr>
                        </table>
                    </div>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" class="cPopupToolbar">
                <table cellpadding="5" align="right" border="0" cellspacing="2">
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgBtnBack" SkinID="BackButton" runat="server" OnClientClick="window.close()" />
                        </td>
                        <td class="cVerBar1">
                        </td>
                        <td>
                            <button onclick="ChangeLang()" class="cBtnLang" type="button">
                                <img alt="" name="langimg" border="0" src="../images/Farsibtn.gif" width="16" height="16" /></button>
                        </td>
                        <td class="cVerBar1">
                        </td>
                        <td>
                            <asp:ImageButton ID="imgBtnSave" SkinID="SaveButton" runat="server" OnClick="DoSave" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
