<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Edit.master"
    AutoEventWireup="True" Inherits="Resources_EditResources"
    Title="Resources" Codebehind="EditResources.aspx.cs" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <div style="text-align: center">
        <div style="width: 910;">
            <table class="cTblEdit" bordercolor="#697077" border="1" align="center" cellpadding="0"
                cellspacing="0">
                <tr>
                    <th>
                        <div style="width: 880px;">
                            <div style="width: 180px; float: left;">
                                <table align="left">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="imgBtnReport" runat="server" ImageUrl="~/Admin/images/Report.gif" />
                                        </td>
                                        <td class="cSepVer">
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnSearch" runat="server" ImageUrl="~/Admin/images/Search.gif" />
                                        </td>
                                        <td class="cSepVer">
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnOpenFol" runat="server" ImageUrl="~/Admin/images/OpenFol.gif" />
                                        </td>
                                        <td class="cSepVer">
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Admin/images/Help.gif" />
                                        </td>
                                        <td class="cSepVer">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="cSysName">
                                <asp:HyperLink runat="server" ID="hplSysName"></asp:HyperLink></div>
                        </div>
                    </th>
                </tr>
                <tr>
                    <td class="cTDEdit">
                        <table cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <td class="cEditRight">
                                    <table class="cEditMain" width="100%">
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <div class="cHeaderEditMain">
                                                    <table align="left" width="150" dir="ltr">
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="imgBtnDeleteTop" runat="server" ImageUrl="~/Admin/images/Delete.gif" />
                                                            </td>
                                                            <td class="cSepVer">
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgBtnView" runat="server" ImageUrl="~/Admin/images/View.gif" />
                                                            </td>
                                                            <td class="cSepVer">
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgBtnEdit" runat="server" ImageUrl="~/Admin/images/Edit.gif" />
                                                            </td>
                                                            <td class="cSepVer">
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="imgBtnPrint" runat="server" ImageUrl="~/Admin/images/Print.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="cEditMainData">
                                                    <table align="center" width="100%">
                                                        <tr>
                                                            <td>
                                                                <div>
                                                                    <div>
                                                                        <table class="cTblOneRow">
                                                                            <tr>
                                                                                <td class="cFieldLeft">
                                                                                    <table class="cTblField">
                                                                                        <tr>
                                                                                            <td class="cCtrl">
                                                                                                <AKP:ExTextBox jas="1" ID="txtName" CssClass="cMultiLine" TextMode="MultiLine" MaxLength="1000"
                                                                                                    runat="server" />
                                                                                            </td>
                                                                                            <td class="cLabel">
                                                                                                <asp:Label ID="lblName" runat="server" Text="نام فارسي:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td class="cFieldRight">
                                                                                    <table class="cTblField">
                                                                                        <tr>
                                                                                            <td class="cCtrl">
                                                                                                <AKP:ExTextBox jas="1" ID="txtEngName" CssClass="cMultiLine" TextMode="MultiLine"
                                                                                                    MaxLength="1000" runat="server" />
                                                                                            </td>
                                                                                            <td class="cLabel">
                                                                                                <asp:Label ID="lblEngName" runat="server" Text="نام لاتين:"></asp:Label>
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
                                                                                                <AKP:Lookup jas="1" ID="lkpTypeCode" BaseID="HCResourceTypes" runat="server" />
                                                                                            </td>
                                                                                            <td class="cLabel">
                                                                                                <asp:Label ID="lblTypeCode" runat="server" Text="نوع:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td class="cFieldRight">
                                                                                    <table class="cTblField">
                                                                                        <tr>
                                                                                            <td class="cCtrl">
                                                                                                <AKP:Lookup jas="1" ID="lkpMasterCode" BaseID="Resources" runat="server" />
                                                                                            </td>
                                                                                            <td class="cLabel">
                                                                                                <asp:Label ID="lblMasterCode" runat="server" Text="کد پدر:"></asp:Label>
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
                                                                                                <AKP:ExTextBox jas="1" ID="txtEditPath" CssClass="cMultiLine" TextMode="MultiLine"
                                                                                                    MaxLength="200" runat="server" />
                                                                                            </td>
                                                                                            <td class="cLabel">
                                                                                                <asp:Label ID="lblEditPath" runat="server" Text="مسير:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td class="cFieldRight">
                                                                                    <table class="cTblField">
                                                                                        <tr>
                                                                                            <td class="cCtrl">
                                                                                                <AKP:ExTextBox jas="1" ID="txtBaseID" runat="server" />
                                                                                            </td>
                                                                                            <td class="cLabel">
                                                                                                <asp:Label ID="lblBaseID" runat="server" Text="BaseID:"></asp:Label>
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
                                                                                                <AKP:ExTextBox jas="1" ID="txtBasicAccessType" runat="server" />
                                                                                            </td>
                                                                                            <td class="cLabel">
                                                                                                <asp:Label ID="lblBasicAccessType" runat="server" Text="دسترسي پيش فرض:"></asp:Label>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td class="cFieldRight">
                                                                                    <table class="cTblField">
                                                                                        <tr>
                                                                                            <td class="cCtrl">
                                                                                                <AKP:ExTextBox jas="1" ID="txtResName" runat="server" />
                                                                                            </td>
                                                                                            <td class="cLabel">
                                                                                                <asp:Label ID="lblResName" runat="server" Text="نام موجوديت:"></asp:Label>
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
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="cHorSep">
                                    </div>
                                    <table class="cEditTabs" width="100%">
                                        <tr>
                                            <td>
                                                <div>
                                                    <telerik:RadTabStrip Style="margin-right: 8px;" dir="rtl" ID="tsNews" runat="server"
                                                        MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Vista" SkinsPath="~/Admin/RadControls/TabStrip/Skins">
                                                        <Tabs>
                                                            <telerik:RadTab ID="Tab1" runat="server" Text="اطلاعات اصلی">
                                                            </telerik:RadTab>
                                                            <telerik:RadTab ID="Tab2" runat="server" Text="Tab1">
                                                            </telerik:RadTab>
                                                            <telerik:RadTab ID="Tab3" runat="server" Text="Tab2">
                                                            </telerik:RadTab>
                                                            <telerik:RadTab ID="Tab4" runat="server" Text="Tab3">
                                                            </telerik:RadTab>
                                                            <telerik:RadTab ID="Tab5" runat="server" Text="Tab4">
                                                            </telerik:RadTab>
                                                            <telerik:RadTab ID="Tab6" runat="server" Text="Tab5">
                                                            </telerik:RadTab>
                                                            <telerik:RadTab ID="Tab7" runat="server" Text="Tab16">
                                                            </telerik:RadTab>
                                                        </Tabs>
                                                    </telerik:RadTabStrip>
                                                    <div class="cTabWrapper">
                                                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server">
                                                            <telerik:RadPageView ID="PageView1" runat="server">
                                                                Content 1
                                                            </telerik:RadPageView>
                                                            <telerik:RadPageView ID="PageView2" runat="server">
                                                                <div id="Detail1">
                                                                </div>
                                                            </telerik:RadPageView>
                                                            <telerik:RadPageView ID="PageView3" runat="server">
                                                                <div id="Detail2">
                                                                </div>
                                                            </telerik:RadPageView>
                                                            <telerik:RadPageView ID="PageView4" runat="server">
                                                                <div id="Detail13">
                                                                </div>
                                                            </telerik:RadPageView>
                                                        </telerik:RadMultiPage>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="cHorSep">
                                    </div>
                                    <div style="text-align: left">
                                        <table cellpadding="2" cellspacing="4">
                                            <tr>
                                                <td>
                                                    <a id="imgBtnDeleteDown" href='#' onclick="DeleteFromEditForm('<%=BaseID %>','<%=Code %>')">
                                                        <img alt='(F3) حذف' class="cDelRec" src='../images/spacer.gif' /></a>
                                                </td>
                                                <td class="cVerBar1">
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgBtnBack" SkinID="BackButton" OnClick="GoToList" runat="server" />
                                                </td>
                                                <td class="cVerBar1">
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgBtnSave" SkinID="SaveButton" OnClick="DoSave" runat="server" />
                                                </td>
                                                <td class="cVerBar1">
                                                </td>
                                                <td>
                                                    <a id="imgBtnLang" href='#' onclick='ChangeLang()'>
                                                        <img alt="(F5) تغییر زبان" name="langimg" border="0" src="../images/Farsibtn.gif"
                                                            class="cBtnImage2" width="16" height="16" /></a>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
