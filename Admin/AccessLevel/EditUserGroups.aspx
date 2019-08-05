<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/EditPopup.master"
    AutoEventWireup="True" Inherits="UserGroups_EditUserGroups" Title="گروه های کاربران"
    CodeBehind="EditUserGroups.aspx.cs" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <div class="PopupEditArea">
        <div class="cMainEditTable">
            <div>
                <AKP:MsgBox ID="msgBox" runat="server" CssClass="cError" />
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
                                                    <AKP:Lookup jas="1" ID="lkpGroupCode" BaseID="AccessGroups" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblGroupCode" runat="server" Text="نام گروه:"></asp:Label>
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
        </div>
        <div class="cPopupToolbar">
            <table class="tblEditButtons" cellpadding="5" align="right" border="0" cellspacing="2">
                <tr>
                    <td>
                        <asp:ImageButton ID="LinkButton1" Text=" بستن پنجره " SkinID="BackButton" OnClientClick="window.close()"
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
</asp:Content>
