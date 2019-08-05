<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master" AutoEventWireup="True"
    Inherits="Users_EditUsers" Title="کاربران" CodeBehind="EditUsers.aspx.cs" %>

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
                                                    <AKP:ExTextBox ID="txtFirstName" jas="1" MaxLength="100" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblFirstName" runat="server" Text="نام:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExTextBox ID="txtLastName" jas="1" MaxLength="100" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblLastName" runat="server" Text="نام خانوادگي:"></asp:Label>
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
                                                    <AKP:ExTextBox ID="txtEmail" jas="1" MaxLength="50" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblEmail" runat="server" Text="ايميل:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExTextBox ID="txtPassword" TextMode="Password" SkinID="English" jas="1" MaxLength="50" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblPassword" runat="server" Text="کلمه عبور:"></asp:Label>
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
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:Combo ID="cboHCUserTypeCode" AllowNull="false" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblHCUserTypeCode" runat="server" Text="نوع کاربر:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <fieldset class="cUpl">
                                <legend>&nbsp;<asp:Label ID="lblPicFile" Text="عکس" runat="server" />&nbsp;</legend>
                                <table class="cTblOneRow">
                                    <tr>
                                        <td class="cFieldLeft">
                                            <div class="cPic">
                                                <AKP:ExRadUpload jas="1" AllowedFileExtensions=".gif,.jpg,.jpeg,.png,.bmp" TargetFolder="~/Admin/Files/Persons/"
                                                    ID="uplPicFile" runat="server" Skin="Vista" MaxFileSize="20971520" ControlObjectsVisibility="None" />
                                                <br />
                                                <AKP:ExCheckBox ID="chkDeletePicFile" runat="server" Text="حذف فایل" />
                                            </div>
                                        </td>
                                        <td class="cFieldRight">
                                            <asp:HyperLink EnableViewState="false" Target="_blank" runat="server" ID="hplPicFile" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        
                        <div>
                            <table class="cTblOneRow">
                                <tr>
                                    <td class="cFieldLeft">
                                        
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExCheckBox ID="chkActive" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblActive" runat="server" Text="فعال:"></asp:Label>
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
                                                    <AKP:ExTextBox ID="txtContactNumber" jas="1" MaxLength="50" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblContactNumber" runat="server" Text="تلفن همراه:"></asp:Label>
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
                                                    <AKP:Combo ID="cboHCGenderCode" AllowNull="false" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblHCGenderCode" runat="server" Text="جنسيت:"></asp:Label>
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
                MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Vista" SkinsPath="~/Admin/RadControls/TabStrip/Skins">
                <Tabs>
                    
                    
                    <telerik:RadTab ID="Tab1" runat="server" Text="تراکنش های کاربر">
                    </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <div class="cTabWrapper">
                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server">
                    
                    <telerik:RadPageView ID="RadPageView1" runat="server">
                       
                        <div class="cBrowseArea" id="UserTransactions">
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
