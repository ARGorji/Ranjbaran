<%@ Page Language="C#" MasterPageFile="~/Admin/Main.master" AutoEventWireup="True"
    Inherits="HardCode_EditHardCode" Title="ویرایش اطلاعات پایه" CodeBehind="EditHardCode.aspx.cs" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <div class="EditHeader">
        <asp:Label runat="server" ID="lblSysName"></asp:Label></div>
    <div>
        <div>
            <table class="cTblOneRow">
                <tr>
                    <td class="cFieldLeft">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:ExTextBox ID="txtName" jas="1" MaxLength="50" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblName" runat="server" Text="نام:"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td class="cFieldRight">
                        <table class="cTblField">
                            <tr>
                                <td class="cCtrl">
                                    <AKP:ExTextBox ID="txtDescription" jas="1" CssClass="cMultiLine" TextMode="MultiLine"
                                        MaxLength="900" runat="server" />
                                </td>
                                <td class="cLabel">
                                    <asp:Label ID="lblDescription" runat="server" Text="توضیحات:"></asp:Label>
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
                        <AKP:ExRadEditor jas="1" ID="txtDescArshad" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd"
                            ShowHtmlMode="false" ShowPreviewMode="false" Skin="Vista" ImagesPaths="~/Files/News"
                            UploadImagesPaths="~/Files/News" DeleteImagesPaths="~/Files/News" ImageManager-ViewPaths="~/Files/News"
                            ImageManager-UploadPaths="~/Files/News" ImageManager-DeletePaths="~/Files/News"
                            ShowSubmitCancelButtons="false" runat="server" Width="680px" Height="400px" SaveInFile="True"
                            FileEncoding="65001" ToolsFile="~/RadControls/Editor/FullSetOfTools.xml" CssClass="RadEContent">
                            <Content>
                                                        
                            </Content>
                            <CssFiles>
                                <telerik:EditorCssFile Value="~/RadControls/Editor/Editor1.css" />
                            </CssFiles>
                        </AKP:ExRadEditor>
                    </td>
                    <td class="cLabel">
                        <asp:Label ID="lblNewsBody" runat="server" Text="توضیحات ارشد:"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table class="cTblField">
                <tr>
                    <td class="cCtrl">
                        <AKP:ExRadEditor jas="1" ID="txtDescDoc" DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd"
                            ShowHtmlMode="false" ShowPreviewMode="false" Skin="Vista" ImagesPaths="~/Files/News"
                            UploadImagesPaths="~/Files/News" DeleteImagesPaths="~/Files/News" ImageManager-ViewPaths="~/Files/News"
                            ImageManager-UploadPaths="~/Files/News" ImageManager-DeletePaths="~/Files/News"
                            ShowSubmitCancelButtons="false" runat="server" Width="680px" Height="400px" SaveInFile="True"
                            FileEncoding="65001" ToolsFile="~/RadControls/Editor/FullSetOfTools.xml" CssClass="RadEContent">
                            <Content>
                                                        
                            </Content>
                            <CssFiles>
                                <telerik:EditorCssFile Value="~/RadControls/Editor/Editor1.css" />
                            </CssFiles>
                        </AKP:ExRadEditor>
                    </td>
                    <td class="cLabel">
                        <asp:Label ID="Label1" runat="server" Text="توضیحات دکترا:"></asp:Label>
                    </td>
                </tr>
            </table>
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
