<%@ Page Language="C#" StylesheetTheme="Edit" ValidateRequest="false" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" Inherits="Products_EditProducts" Title="Products" CodeBehind="EditProducts.aspx.cs" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <div>
        <AKP:MsgBox runat="server" ID="msgBox" />
    </div>
    <asp:Panel runat="server" ID="pnlDuplicateProducts" Visible="false">
        <div class="DuplProCont">
            <asp:Repeater ID="rptDuplicateProducts" EnableViewState="false" runat="server">
                <ItemTemplate>
                    <div>
                        <%#Eval("FaTitle") %></div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button ID="btnConfirmDuplicate" OnClick="btnConfirmDuplicate_Click" Width="100"
                runat="server" Text="تایید " />
        </div>
    </asp:Panel>
    <div>
        <table class="cTblField">
            <tr>
                <td class="cCtrl">
                    <AKP:ExTextBox ID="txtEnTitle" jas="1" SkinID="English" MaxLength="500" Width="600"
                        runat="server" />
                </td>
                <td class="cLabel">
                    <asp:Label ID="lblTitle" runat="server" Text="عنوان انگلیسی:"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="cTblField">
            <tr>
                <td class="cCtrl">
                    <AKP:ExTextBox ID="txtFaTitle" jas="1" MaxLength="500" Width="600" runat="server" />
                </td>
                <td class="cLabel">
                    <asp:Label ID="Label40" runat="server" Text="عنوان فارسی:"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="cTblField">
            <tr>
                <td class="cCtrl">
                    <AKP:ExRadEditor dir="rtl" ID="txtDescription" DocumentManager-MaxUploadFileSize="11000000"
                        jas="1" Skin="Vista" ShowSubmitCancelButtons="false" runat="server" Width="600px"
                        Height="700px" SaveInFile="True" FileEncoding="65001" ToolsFile="~/RadControls/Editor/FullSetTools.xml"
                        CssClass="RadEContent">
                        <ImageManager ViewPaths="~/Files" UploadPaths="~/Files" DeletePaths="~/Files" />
                        <FlashManager ViewPaths="~/Files" UploadPaths="~/Files" DeletePaths="~/Files" />
                        <MediaManager SearchPatterns="*.avi,*.swf,*.flv,*.mp4" ViewPaths="~/Files" UploadPaths="~/Files"
                            DeletePaths="~/Files" />
                        <DocumentManager ViewPaths="~/Files" UploadPaths="~/Files" DeletePaths="~/Files" />
                        <CssFiles>
                            <telerik:EditorCssFile Value="~/RadControls/Editor/Editor1.css" />
                        </CssFiles>
                    </AKP:ExRadEditor>
                </td>
                <td class="cLabel">
                    <asp:Label ID="lblDescription" runat="server" Text="توضيحات:"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="UploaderContainer">
        <fieldset style="direction: rtl; margin-right: 120px;">
            <legend>&nbsp;<asp:Label runat="server" Text="عکس" ID="lblLargePicFile" />&nbsp;</legend>
            <table class="cTblOneRow">
                <tr>
                    <td class="cFieldLeft">
                        <div class="cPic">
                            <AKP:ExRadUpload ID="uplLargePicFile" jas="1" runat="server" AllowedFileExtensions=".gif,.jpg,.jpeg,.png,.bmp"
                                TargetFolder="~/Files/Products/Large/" Skin="Vista" MaxFileSize="900000" ControlObjectsVisibility="None" />
                            <br />
                            <asp:CheckBox ID="chkDeleteLargePicFile" runat="server" Text="حذف فایل" />
                        </div>
                    </td>
                    <td class="cFieldRight">
                        <asp:HyperLink rel="lightbox" EnableViewState="false" Target="_blank" runat="server"
                            ID="hplLargePicFile" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div>
        <table class="cTblOneRow">
            <tr>
                <td class="cFieldLeft">
                    <table class="cTblField">
                        <tr>
                            <td class="cCtrl">
                                <AKP:ExCheckBox ID="chkIsNew" jas="1" NumericType="IntType" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="Label45" runat="server" Text="جدید:"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="cFieldRight">
                    <table class="cTblField">
                        <tr>
                            <td class="cCtrl">
                                <AKP:ExCheckBox ID="chkIsDiscount" jas="1" NumericType="IntType" runat="server" />
                                
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="Label47" runat="server" Text="دارای تحفیف ویژه:"></asp:Label>
                                
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
                                <AKP:ExCheckBox ID="chkIsMostSold" jas="1" NumericType="IntType" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="Label48" runat="server" Text="پرفروش:"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="cFieldRight">
                    <table class="cTblField">
                        <tr>
                            <td class="cCtrl">
                                <AKP:ExCheckBox ID="chkSpecial" jas="1" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="Label46" runat="server" Text="پیشنهاد ویژه:"></asp:Label>
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
                                <AKP:NumericTextBox ID="txtMarketPrice" jas="1" NumericType="IntType" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblMarketPrice" runat="server" Text="قیمت بازار:"></asp:Label>
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
                                <AKP:LookupTree ID="treProductCatCode" BaseID="ProductCats" jas="1" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblProductCatCode" runat="server" Text="گروه:"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="cFieldRight">
                    <table class="cTblField">
                        <tr>
                            <td class="cCtrl">
                                <AKP:ExTextBox ID="txtInternalCode" jas="1" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblInternalCode" runat="server" Text="کد داخلی:"></asp:Label>
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
                                <AKP:FarsiDate ID="dteCreateDate" jas="1" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblCreateDate" runat="server" Text="تاریخ ایجاد:"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="cFieldRight">
                    <table class="cTblField">
                        <tr>
                            <td class="cCtrl">
                                <AKP:NumericTextBox NumericType="RealType" ID="txtStarCount" jas="1" MaxLength="9"
                                    runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblStarCount" runat="server" Text="امتیاز:"></asp:Label>
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
                                <AKP:NumericTextBox ID="txtDiscount" jas="1" NumericType="IntType" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblDiscount" runat="server" Text="تخفیف:"></asp:Label>
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
                </td>
                <td class="cFieldRight">
                    <table class="cTblField">
                        <tr>
                            <td class="cCtrl">
                                <AKP:NumericTextBox ID="txtShowOrder" jas="1" NumericType="IntType" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblShowOrder" runat="server" Text="ترتیب نمایش:"></asp:Label>
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
                                <AKP:ExCheckBox ID="chkActive" jas="1" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblActive" runat="server" Text="فعال:"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="cFieldRight">
                    <table class="cTblField">
                        <tr>
                            <td class="cCtrl">
                                <AKP:NumericTextBox ID="txtWeight" jas="1" NumericType="IntType" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblWeight" runat="server" Text="وزن:"></asp:Label>
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
                                <AKP:NumericTextBox ID="txtSendPishtazPrice" jas="1" NumericType="IntType" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="Label1" runat="server" Text="قیمت پست پیشنتاز:"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="cFieldRight">
                    <table class="cTblField">
                        <tr>
                            <td class="cCtrl">
                                <AKP:NumericTextBox ID="txtSendSefareshiPrice" jas="1" NumericType="IntType" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="Label2" runat="server" Text="قیمت پست سفارشی:"></asp:Label>
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
                                <AKP:Combo ID="cboHCProductAvailabilityCode" jas="1" runat="server" />
                            </td>
                            <td class="cLabel">
                                <asp:Label ID="lblHCProductAvailabilityCode" runat="server" Text="موجود بودن:"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="cFieldRight">
                   
                </td>
            </tr>
        </table>
    </div>
    
    <div class="clear">
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
    <div class="clear">
    </div>
</asp:Content>
