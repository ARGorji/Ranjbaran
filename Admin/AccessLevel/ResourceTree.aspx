<%@ Page Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="True"
    Inherits="AccessLevel_ResourceTree" Title="منابع سیستم" CodeBehind="ResourceTree.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div style="text-align: center">
        <div style="width: 910;">
            <table class="cTblEdit" bordercolor="#697077" border="1" align="center" cellpadding="0"
                cellspacing="0">
                <tr>
                    <th>
                        <div style="width: 880px;">
                            <div style="width: 180px; float: left;">
                                
                            </div>
                            <div class="cSysName">
                                <asp:HyperLink runat="server" ID="hplSysName"></asp:HyperLink></div>
                        </div>
                    </th>
                </tr>
                <tr>
                    <td style="background-color: White;">
                        <div style="text-align: left">
                            <div class="cBlueBar">
                                
                                <div>
                                </div>
                            </div>
                        </div>
                        <div class="cHorSep">
                        </div>
                        <table align="center" width="99%">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="padding: 4px 2px 4px 2px;">
                                                <table class="ctblThem" width="100%">
                                                    <tr>
                                                        <td style="vertical-align: top;">
                                                            <div class="cHeaderMain">
                                                                ویرایش درخت
                                                            </div>
                                                            <div class="cEditMainData">
                                                                <table align="center" width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <div class="module" style="width: 180px; margin-left: 30px; float: left; direction: rtl;
                                                                                text-align: center">
                                                                                <asp:Label ID="Label1" runat="server" Font-Bold="True">گره انتخاب شده:</asp:Label><br />
                                                                                <AKP:ExTextBox jas="1" ID="tbNodeText" runat="server" />
                                                                                <asp:Button Width="130" ID="btnRename" runat="server" Text="تغییر نام" OnClick="btnRename_Click">
                                                                                </asp:Button>
                                                                                <br />
                                                                                <asp:Button Width="130" ID="btnRemove" runat="server" Text="حذف گره انتخاب شده" OnClick="btnRemove_Click">
                                                                                </asp:Button>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="module" style="width: 180px; margin-left: 30px; float: left; direction: rtl;">
                                                                                <asp:Label ID="Label3" runat="server" Font-Bold="True">اضافه کردن گره جدید:</asp:Label><br />
                                                                                <AKP:ExTextBox jas="1" ID="tbNewNodeText" runat="server" /><br />
                                                                                <asp:Button Width="130" ID="btnAddChild" runat="server" Text="اضافه کردن فرزند" CssClass="button"
                                                                                    OnClick="btnAddChild_Click"></asp:Button>
                                                                                <asp:Button Width="130" ID="btnAddRoot" runat="server" Text="اضافه کردن گره اصلی"
                                                                                    CssClass="button" OnClick="btnAddRoot_Click"></asp:Button>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div style="text-align: right;">
                                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <telerik:RadTreeView EnableDragAndDrop="true" AutoPostBack="true" Width="400px" OnNodeClick="TreeResources_NodeClick"
                                                                                            ID="TreeResources" runat="server" Skin="WebBlue" OnNodeDrop="RadTreeView1_HandleDrop"
                                                                                            ShowLineImages="true" ExpandDelay="3" dir="rtl">
                                                                                        </telerik:RadTreeView>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
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
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ConfigurationPanel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Panel1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>
