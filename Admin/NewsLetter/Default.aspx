<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Main.master" CodeBehind="Default.aspx.cs" Inherits="Ranjbaran.Admin.NewsLetter.Default" %>


<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
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
                                                    <AKP:ExTextBox ID="txtSubject" Width="600" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblTitle" runat="server" Text="عنوان:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>
                            </table>
                        </div>
                        
                        <div>
                            <table class="cTblField">
                                <tr>
                                    <td class="cCtrl">
                                        <AKP:ExRadEditor dir="rtl" ID="txtMailBody" DocumentManager-MaxUploadFileSize="11000000"
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
                                        <asp:Label ID="lblNewsBody" runat="server" Text="متن ایمیل:"></asp:Label>
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
                        <asp:Button ID="btnSend" Text=" ارسال " OnClick="btnSend_Click"
                            runat="server" />
                    </td>
                   
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hfPassword" runat="server" />
</asp:Content>