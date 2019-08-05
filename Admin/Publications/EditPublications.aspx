<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" Inherits="Publications_EditPublications" Title="Publications"
    CodeBehind="EditPublications.aspx.cs" %>

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
                                                    <AKP:ExTextBox ID="txtTitle" jas="1" width="530"
                                                        runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblTitle" runat="server" Text="عنوان:"></asp:Label>
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
                                                    <AKP:ExTextBox ID="txtDescription" TextMode="MultiLine" Height="100" jas="1" width="510"
                                                        runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="Label1" runat="server" Text="توضیحات:"></asp:Label>
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
                                                    <AKP:ExTextBox ID="txtEntesharat" jas="1" 
                                                        MaxLength="200" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblEntesharat" runat="server" Text="انتشارات:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:NumericTextBox ID="txtVisitCount" jas="1" NumericType="IntType" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblVisitCount" runat="server" Text="تعداد بازدید:"></asp:Label>
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
                                                    <AKP:NumericTextBox ID="txtPrice" jas="1" NumericType="IntType" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblPrice" runat="server" Text="قیمت:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExTextBox ID="txtPublicationTurn" jas="1" 
                                                        MaxLength="200" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblPublicationTurn" runat="server" Text="نوبت چاپ:"></asp:Label>
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
                                                    <AKP:NumericTextBox ID="txtCiculation" jas="1" NumericType="IntType" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblCiculation" runat="server" Text="شمارگان:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="UploaderContainer">
                            <fieldset style="direction: rtl">
                                <legend>&nbsp;<asp:Label runat="server" Text="عکس کوچک" ID="lblSmallPic" />&nbsp;</legend>
                                <div>
                                    <table class="cTblOneRow2">
                                        <tr>
                                            <td class="cFieldLeft2">
                                                <AKP:ExRadUpload ID="uplSmallPic" jas="1" AutoSave="false" runat="server" AllowedFileExtensions=".gif,.jpg,.jpeg,.png,.bmp"
                                                    TargetFolder="~/Files/Publications/" Skin="Vista" MaxFileSize="99990000" ControlObjectsVisibility="None" />
                                                <br />
                                                <asp:CheckBox ID="chkDeleteSmallPic" runat="server" Text="حذف فایل" />
                                            </td>
                                            <td class="cFieldRight2">
                                                <div class="cPic">
                                                    <asp:HyperLink rel="lightbox" EnableViewState="false" Target="_blank" runat="server"
                                                        ID="hplSmallPic" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>

                        <div class="UploaderContainer">
                            <fieldset style="direction: rtl">
                                <legend>&nbsp;<asp:Label runat="server" Text="عکس بزرگ" ID="lblLargePic" />&nbsp;</legend>
                                <div>
                                    <table class="cTblOneRow2">
                                        <tr>
                                            <td class="cFieldLeft2">
                                                <AKP:ExRadUpload ID="uplLargePic" jas="1" AutoSave="false" runat="server" AllowedFileExtensions=".gif,.jpg,.jpeg,.png,.bmp"
                                                    TargetFolder="~/Files/Publications/" Skin="Vista" MaxFileSize="99990000" ControlObjectsVisibility="None" />
                                                <br />
                                                <asp:CheckBox ID="chkDeleteLargePic" runat="server" Text="حذف فایل" />
                                            </td>
                                            <td class="cFieldRight2">
                                                <div class="cPic">
                                                    <asp:HyperLink rel="lightbox" EnableViewState="false" Target="_blank" runat="server"
                                                        ID="hplLargePic" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>


                        <div class="UploaderContainer">
                            <fieldset style="direction: rtl">
                                <legend>&nbsp;<asp:Label runat="server" Text="فایل PDF" ID="lblPDFFile" />&nbsp;</legend>
                                <div>
                                    <table class="cTblOneRow2">
                                        <tr>
                                            <td class="cFieldLeft2">
                                                <AKP:ExRadUpload ID="uplPDFFile" jas="1" AutoSave="false" runat="server" AllowedFileExtensions=".gif,.jpg,.jpeg,.png,.bmp"
                                                    TargetFolder="~/Files/Publications/" Skin="Vista" MaxFileSize="99990000" ControlObjectsVisibility="None" />
                                                <br />
                                                <asp:CheckBox ID="chkDeletePDFFile" runat="server" Text="حذف فایل" />
                                            </td>
                                            <td class="cFieldRight2">
                                                <div class="cPic">
                                                    <asp:HyperLink rel="lightbox" EnableViewState="false" Target="_blank" runat="server"
                                                        ID="hplPDFFile" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </fieldset>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="cHorSep">
        </div>
        <div class="TabHeaderData">
            <telerik:RadTabStrip Style="margin-right: 8px;" dir="rtl" ID="tsPublications" runat="server"
                MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Vista" SkinsPath="~/RadControls/TabStrip/Skins">
                <Tabs>
                    <telerik:RadTab ID="Tab1" runat="server" Text="گروه های تالیف">
                    </telerik:RadTab>
                    
                </Tabs>
            </telerik:RadTabStrip>
            <div class="cTabWrapper">
                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server">
                    <telerik:RadPageView ID="PageView1" runat="server">
                        <div class="cBrowseArea" id="PublicationGroups">
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
