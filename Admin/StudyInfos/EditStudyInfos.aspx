<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" Inherits="StudyInfos_EditStudyInfos" Title="StudyInfos"
    CodeBehind="EditStudyInfos.aspx.cs" %>

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
                            <table class="cTblField">
                                <tr>
                                    <td class="cCtrl">
                                        <AKP:ExTextBox ID="txtTitle" jas="1" Width="600" runat="server" />
                                    </td>
                                    <td class="cLabel">
                                        <asp:Label ID="lblTitle" runat="server" Text="عنوان:"></asp:Label>
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
                                                    <AKP:Combo ID="cboHCGradeCode" AllowNull="false" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblHCGradeCode" runat="server" Text="مقطع:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:Combo ID="cboHCStudyFieldCode" AllowNull="false" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblHCStudyFieldCode" runat="server" Text="رشته:"></asp:Label>
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
                                                    <AKP:ExRadEditor dir="rtl" ID="txtDescription" DocumentManager-MaxUploadFileSize="11000000"
                                                        jas="1" Skin="Vista" ShowSubmitCancelButtons="false" runat="server" Width="480px"
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
                                                    <asp:Label ID="lblDescription" runat="server" Text="توضیحات:"></asp:Label>
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
