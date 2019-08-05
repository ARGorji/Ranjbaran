<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/EditPopup.master" AutoEventWireup="true" Inherits="OrderProducts_EditOrderProducts" Title="OrderProducts" Codebehind="EditOrderProducts.aspx.cs" %>


<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <table cellpadding="0" cellspacing="0" align="center" width="100%">
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="90%" class="cMainEditTable">
                    <tr>
                        <td>
                            <msg:msgbox id="msgBox" runat="server" cssclass="cError" />
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
                                                            <AKP:lookup id="lkpProductCode" BaseID="Products" jas="1" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblProductCode" runat="server" Text="کد محصول:"></asp:Label>
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
                                                            <AKP:NumericTextBox id="txtWeight" jas="1" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblWeight" runat="server" Text="وزن:"></asp:Label>
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
                                                            <AKP:numerictextbox id="txtProductPrice" jas="1" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblProductPrice" runat="server" Text="قیمت محصول:"></asp:Label>
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
                                                            <AKP:numerictextbox id="txtSendPrice" jas="1" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblSendPrice" runat="server" Text="هزینه ارسال:"></asp:Label>
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
                                                            <AKP:combo id="cboHCProductOrderStatusCode" jas="1" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblHCProductOrderStatusCode" runat="server" Text="وضعیت محصول سفارش داده شده:"></asp:Label>
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
                                                            <AKP:numerictextbox id="txtItemCount" jas="1" runat="server" />
                                                        </td>
                                                        <td class="cLabel">
                                                            <asp:Label ID="lblItemCount" runat="server" Text="Not Defined:"></asp:Label>
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
                        <td class="cVerBar1"></td>
                        <td>
                            <button onclick="ChangeLang()" class="cBtnLang" type="button">
                                <img alt="" name="langimg" border="0" src="../images/Farsibtn.gif" width="16" height="16" /></button>
                        </td>
                        <td class="cVerBar1"></td>
                        <td>
                            <asp:LinkButton ID="imgBtnSave" Text="Save" SkinID="SaveButton" runat="server" OnClick="DoSave" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
