<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/EditPopup.master" AutoEventWireup="true" Inherits="CourseUsers_EditCourseUsers" Title="CourseUsers" Codebehind="EditCourseUsers.aspx.cs" %>


<asp:content runat="server" id="content1" contentplaceholderid="cphMain">
<table cellpadding="0" cellspacing="0" align="center" width="100%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="90%" class="cMainEditTable">
                    <tr>
                        <td>
                            <AKP:MsgBox ID="msgBox" runat="server" CssClass="cError" />
                        </td>
                    </tr>
                    <div>
                                                                        <table class="cTblOneRowPopup">
                                                                            <tr>
                                                                                <td class="cFieldLeft">
                                                                                    <table class="cTblField"><tr>
                                            <td>
                                                <table class="EditRow">
                                                    <tr>
                                                        <td class="cCtrl">
                                                                <AKP:Lookup ID="lkpUserCode" jas="1"  runat="server"/>
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblUserCode" runat="server" Text="کد کاربر:"></asp:Label>
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
</asp:content>
