<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master" AutoEventWireup="true" Inherits="Orders_EditOrders" Title="Orders" Codebehind="EditOrders.aspx.cs" %>


<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">

    <div class="EditHeader">
        <asp:Label runat="server" ID="lblSysName"></asp:Label>
    </div>
    <div>
        <div>
            <table class="cTblOneRow">
                <tr>
                    <td class="cFieldLeft">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:ExTextBox id="txtID" jas="1" maxlength="50" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblID" runat="server" Text="شناسه:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:extextbox id="txtFullName" jas="1" maxlength="50" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblFullName" runat="server" Text="نام:"></asp:Label>
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
                                    <AKP:Combo id="cboHCGenderCode" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblHCGenderCode" runat="server" Text="جنسیت:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <%--<table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:lookup id="lkpProvinceCode" BaseID="Provinces" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblProvinceCode" runat="server" Text="کد استان:"></asp:Label>
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table class="cTblOneRow">
                <tr>
                    <td class="cFieldLeft">
                        <%--<table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:lookup id="lkpCityCode" BaseID="Cities" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblCityCode" runat="server" Text="کد شهر:"></asp:Label>
                                </td>
                            </tr>
                        </table>--%>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:extextbox id="txtTel" jas="1" maxlength="100" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblTel" runat="server" Text="تلفن محل کار:"></asp:Label>
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
                                    <AKP:extextbox id="txtCellPhone" jas="1" maxlength="100" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblCellPhone" runat="server" Text="موبایل:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:extextbox id="txtEmail" jas="1" maxlength="100" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblEmail" runat="server" Text="ایمیل:"></asp:Label>
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
                                    <AKP:ExTextBox id="txtPostalCode" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblPostalCode" runat="server" Text="کد پستی:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:extextbox id="txtAddress" jas="1" cssclass="cMultiLine" textmode="MultiLine" maxlength="500" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblAddress" runat="server" Text="آدرس:"></asp:Label>
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
                                    <AKP:extextbox id="txtDescription" jas="1" cssclass="cMultiLine" textmode="MultiLine" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblDescription" runat="server" Text="توضیحات:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:combo id="cboHCSendTypeCode" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblHCSendTypeCode" runat="server" Text="نوع ارسال:"></asp:Label>
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
                                    <AKP:numerictextbox id="txtDiscount" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblDiscount" runat="server" Text="تخفیف:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:farsidate id="dteCreateDate" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblCreateDate" runat="server" Text="تاریخ سفارش:"></asp:Label>
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
                                    <AKP:numerictextbox id="txtSendPrice" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblSendPrice" runat="server" Text="هزینه ارسال:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:numerictextbox id="txtOtherCosts" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblOtherCosts" runat="server" Text="سایر هزینه های سفارش:"></asp:Label>
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
                                    <AKP:numerictextbox id="txtTotalProductPrice" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblTotalProductPrice" runat="server" Text="قیمت کل:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:numerictextbox id="txtTotalOrderCost" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblTotalOrderCost" runat="server" Text="قیمت کل سفارش:"></asp:Label>
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
                                    <AKP:combo id="cboHCOrderStatusCode" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblHCOrderStatusCode" runat="server" Text="وضعیت سفارش:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:lookup id="lkpUserCode" BaseID="Users" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblUserCode" runat="server" Text="کاربر:"></asp:Label>
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
                                    <asp:Label ID="lblPayStatus" runat="server" Text="پرداخت نشده"></asp:Label>
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblHCOrderPayStatusCode" runat="server" Text="وضعیت پرداخت:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:combo id="cboHCPayMethodCode" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblHCPayMethodCode" runat="server" Text="روش پرداخت:"></asp:Label>
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
                                    <AKP:numerictextbox id="txtBuyCost" jas="1" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblBuyCost" runat="server" Text="قیمت خرید:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        
                    </td>
                </tr>
            </table>
        </div>
        <div class="TabHeaderData">
            <telerik:RadTabStrip Style="margin-right: 8px;" dir="rtl" ID="tsUsers" runat="server"
                MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Vista" SkinsPath="~/Admin/RadControls/TabStrip/Skins">
                <Tabs>
                    
                    
                    <telerik:RadTab ID="Tab1" runat="server" Text="محصولات سفارش">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <div class="cTabWrapper">
                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server">
                    
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                       
                        <div class="cBrowseArea" id="OrderProducts">
                        </div>
                        <div class="cDivSep">
                        </div>
                    </telerik:RadPageView>
                    
                </telerik:RadMultiPage>
            </div>
        </div>

    </div>

    <div style="text-align: right">
        <table class="tblEditButtons" cellpadding="2" cellspacing="4">
                <tr>
                    
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
</asp:Content>
