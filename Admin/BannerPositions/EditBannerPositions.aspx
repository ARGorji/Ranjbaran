<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master" AutoEventWireup="True"
    Inherits="BannerPositions_EditBannerPositions" Title="BannerPositions" CodeBehind="EditBannerPositions.aspx.cs" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <div class="cTblEdit">
        <div style="width: 700px; float: right; text-align: right; color: White;">
            <asp:Label runat="server" ID="lblSysName"></asp:Label></div>
        <div class="cTDEdit">
            <div class="cEditRight">
                <div class="cEditMain">
                    <div class="cEditMainData">
                        <div>
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
                                                        <AKP:ExTextBox ID="txtName" jas="1" MaxLength="500" runat="server" />
                                                    </td>
                                                    <td class="cLabel">
                                                        <asp:Label ID="lblName" runat="server" Text="نام:"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cFieldRight">
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
                                                        <AKP:NumericTextBox ID="txtWidth" jas="1" NumericType="IntType" runat="server" />
                                                    </td>
                                                    <td class="cLabel">
                                                        <asp:Label ID="lblWidth" runat="server" Text="طول:"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cFieldRight">
                                            <table class="cTblField">
                                                <tr>
                                                    <td class="cCtrl">
                                                        <AKP:NumericTextBox ID="txtHeight" jas="1" NumericType="IntType" runat="server" />
                                                    </td>
                                                    <td class="cLabel">
                                                        <asp:Label ID="lblHeight" runat="server" Text="عرض:"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="cHorSep">
        </div>
        <div class="clear">
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
</asp:Content>
