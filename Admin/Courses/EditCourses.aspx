<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master"
    AutoEventWireup="True" Inherits="Courses_EditCourses" Title="Courses" CodeBehind="EditCourses.aspx.cs" %>

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
                                                    <AKP:NumericTextBox ID="txtDurationLen" jas="1" NumericType="IntType" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblDurationLen" runat="server" Text="مدت دوره (ساعت):"></asp:Label>
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
                                                    <AKP:NumericTextBox ID="txtFee" jas="1" MaxLength="4" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblFee" runat="server" Text="شهریه:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExTextBox ID="txtPlace" jas="1" MaxLength="50" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblPlace" runat="server" Text="محل برگزاری:"></asp:Label>
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
                                                    <AKP:Combo ID="cboHCGradeCode" AllowNull="false" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblHCGradeCode" runat="server" Text="مقطع تحصیلی:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                       <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:Combo ID="cboHCMainFieldCode" AllowNull="false" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="Label1" runat="server" Text="نوع:"></asp:Label>
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
                                                    <AKP:FarsiDate ID="dteStartTime" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblStartTime" runat="server" Text="شروع دوره:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:FarsiDate ID="dteEndTime" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="lblEndTime" runat="server" Text="پایان دوره:"></asp:Label>
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
                                                    <AKP:ExTextBox ID="txtID" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="Label2" runat="server" Text="کد:"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="cFieldRight">
                                        <table class="cTblField">
                                            <tr>
                                                <td class="cCtrl">
                                                    <AKP:ExTextBox ID="txtDayTime" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="Label3" runat="server" Text="روز ساعت:"></asp:Label>
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
                                                    <AKP:ExTextBox ID="txtLocation" jas="1" runat="server" />
                                                </td>
                                                <td class="cLabel">
                                                    <asp:Label ID="Label4" runat="server" Text="محل تشکیل کلاس:"></asp:Label>
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
            <telerik:RadTabStrip Style="margin-right: 8px;" dir="rtl" ID="tsCourses" runat="server"
                MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Vista" SkinsPath="~/RadControls/TabStrip/Skins">
                <Tabs>
                    <telerik:RadTab ID="Tab1" runat="server" Text="روزهای کلاس">
                    </telerik:RadTab>
                    
                </Tabs>
                <Tabs>
                    <telerik:RadTab ID="Tab2" runat="server" Text="کاربران ثبت نام شده">
                    </telerik:RadTab>
                    
                </Tabs>
            </telerik:RadTabStrip>
            <div class="cTabWrapper">
                <telerik:RadMultiPage ID="RadMultiPage1" SelectedIndex="0" runat="server">
                    <telerik:RadPageView ID="PageView1" runat="server">
                        <div class="cBrowseArea" id="CourseDays">
                        </div>
                        <div class="cDivSep">
                        </div>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <div class="cBrowseArea" id="CourseUsers">
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
