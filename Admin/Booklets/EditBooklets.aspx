<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="True" Inherits="Booklets_EditBooklets" Title="Booklets" CodeBehind="EditBooklets.aspx.cs" %>

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
                                                    <AKP:ExTextBox ID="txtTitle" jas="1" MaxLength="500"
                                                        runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblTitle" runat="server" Text="عنوان:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
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
                                                    <AKP:ExTextBox ID="txtDescription" jas="1" CssClass="cMultiLine" TextMode="MultiLine"
                                                        MaxLength="500" runat="server" />
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
                                                    <AKP:FarsiDate ID="dteCreateDate" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblCreateDate" runat="server" Text="تاریخ ایجاد:"></asp:Label>
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
                                                    <AKP:ExCheckBox ID="chkFree" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="Label1" runat="server" Text="مجانی:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:NumericTextBox ID="txtDownloadCount" jas="1" NumericType="IntType" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblDownloadCount" runat="server" Text="تعداد دفعات دانلود:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="UploaderContainer">
                                <fieldset style="direction: rtl">
                                    <legend>&nbsp;<asp:Label runat="server" Text="فایل" ID="lblBookletFile" />&nbsp;</legend>
                                    <div>
                                        <table class="cTblOneRow2">
                                            <tr>
                                                <td class="cFieldLeft2">
                                                    <AKP:ExRadUpload ID="uplBookletFile" jas="1" AutoSave="false" runat="server" AllowedFileExtensions=".gif,.jpg,.jpeg,.png,.bmp,.pdf"
                                                        TargetFolder="~/Files/Booklets/" Skin="Vista" MaxFileSize="99990000" ControlObjectsVisibility="None" />
                                                    <br />
                                                    <asp:CheckBox ID="chkDeleteBookletFile" runat="server" Text="حذف فایل" />
                                                </td>
                                                <td class="cFieldRight2">
                                                    <div class="cPic">
                                                        <asp:HyperLink rel="lightbox" EnableViewState="false" Target="_blank" runat="server"
                                                            ID="hplBookletFile" />
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
        <div class="clear">
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
