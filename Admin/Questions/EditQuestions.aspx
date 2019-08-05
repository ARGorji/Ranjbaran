<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="true" Inherits="Questions_EditQuestions" Title="Questions" CodeBehind="EditQuestions.aspx.cs" %>

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
                                                    <AKP:ExTextBox ID="txtQues" jas="1" CssClass="cMultiLine" TextMode="MultiLine"
                                                        MaxLength="1000" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblQuestion" runat="server" Text="پرسش:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExTextBox ID="txtAnswer" jas="1" CssClass="cMultiLine" TextMode="MultiLine"
                                                        MaxLength="4000" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblAnswer" runat="server" Text="پاسخ:"></asp:Label>
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
                                                    <AKP:Lookup ID="lkpUserCode" BaseID="Users" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblUserCode" runat="server" Text="کاربر:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:FarsiDate ID="dteSendDate" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblSendDate" runat="server" Text="تاریخ ارسال:"></asp:Label>
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
                                                    <AKP:Combo ID="cboHCQuestionStatusCode" AllowNull="false" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblHCGradeCode" runat="server" Text="وضعیت:"></asp:Label>
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
