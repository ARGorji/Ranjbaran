<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" Title="وبسایت هادی رنجبران | آزمون‌های آزمایشی"
    AutoEventWireup="true" CodeBehind="Exams.aspx.cs" Inherits="Ranjbaran.Exams" %>

<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<%@ Register Src="~/UserControls/UCNews.ascx" TagName="UCNews" TagPrefix="UCN" %>
<%@ Register Src="~/UserControls/Banner.ascx" TagName="Banner" TagPrefix="UCB" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">

    <h1 class="PageTitle">
        <asp:Image ID="Image1" ImageUrl="~/images/lblExams.png" ToolTip="آزمون‌های آزمایشی"
            runat="server" />
    </h1>
    <AKP:MsgBox runat="server" ID="msg">
    </AKP:MsgBox>

    <div class="center MarginTopBot">
        <UCB:Banner ID="Banner4" runat="server" PositionCode="9" />
    </div>

    <UCN:UCNews runat="server" SectionCode="4" />
    <%--<div>
        <table style="width: 700px; direction: ltr !important;" align="center" cellpadding="0"
            cellspacing="0">
            <tr>
                <td class="WinThem1Corner1">
                    &nbsp;
                </td>
                <td class="WinThem1Mid">
                    <div class="NewsDate">
                    </div>
                    لطفا مقطع تحصیلی و رشته خود را انتخاب کنید
                </td>
                <td class="WinThem1Corner2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="WinThem1Left">
                    &nbsp;
                </td>
                <td style="background-color: White; direction: rtl; padding: 6px;">
                    <div>
                        <table class="tblCourseTools">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlHCLessonCode" AutoPostBack="true" DataTextField="Lesson"
                                        DataValueField="HCLessonCode" runat="server" OnSelectedIndexChanged="ddlHCLessonCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="درس"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHCStudyFieldCode" AutoPostBack="true" DataTextField="HCStudyField"
                                        DataValueField="HCStudyFieldCode" runat="server" OnSelectedIndexChanged="ddlHCStudyFieldCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="رشته"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHCGradeCode" AutoPostBack="true" DataTextField="Name" DataValueField="Code"
                                        runat="server" OnSelectedIndexChanged="ddlHCGradeCode_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="مقطع"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td class="WinThem1Right">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="WinThem1Corner3">
                    &nbsp;
                </td>
                <td class="WinThem1Bot">
                    &nbsp;
                </td>
                <td class="WinThem1Corner4">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>--%>
    <div class="ExamTitles">
        <div class="text-right RTL RanjBox1">
            <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Exams.aspx?HCGradeCode=4&Lesson=ریاضی" runat="server">آزمون های آزمایشی ریاضی/آمادگی کارشناسی ارشد</asp:HyperLink>

        </div>
        <div class="text-right RTL RanjBox1">
            <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Exams.aspx?HCGradeCode=4&Lesson=آمار" runat="server">آزمون های آزمایشی آمار/آمادگی کارشناسی ارشد</asp:HyperLink>

        </div>
        <div class="text-right RTL RanjBox1">
            <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Exams.aspx?HCGradeCode=5&Lesson=ریاضی" runat="server">آزمون های آزمایشی ریاضی/آمادگی دکتری</asp:HyperLink>

        </div>
        <div class="text-right RTL RanjBox1">
            <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Exams.aspx?HCGradeCode=5&Lesson=آمار" runat="server">آزمون های آزمایشی آمار/آمادگی دکتری</asp:HyperLink>

        </div>

    </div>
    <asp:Repeater ID="rptExams" OnItemCommand="HandleRepeaterCommand" runat="server">
        <ItemTemplate>
            <div class="ExamItem">
                <div class="Left">
                    <asp:Panel ID="Panel1" Visible='<%#!IsFree(Convert.ToInt32( Eval("Code"))) %>' runat="server">
                        <div>
                            <asp:ImageButton ID="btnBuy" ToolTip="خرید" CommandArgument='<%#Eval("Code") %>'
                                CommandName="StartPay" ImageUrl="~/images/Buy-32.png" runat="server" />
                        </div>
                        <div>
                            خرید
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel2" Visible='<%#IsFree(Convert.ToInt32( Eval("Code"))) %>' runat="server">
                        <div>
                            <asp:ImageButton ID="btnDownload" CommandArgument='<%#Eval("Code") %>' CommandName="StartDownload"
                                ToolTip="دانلود" ImageUrl="~/images/Download-32.png" runat="server" />
                        </div>
                        <div>
                            دانلود
                        </div>
                    </asp:Panel>
                </div>
                <div class="Right">
                    <ul class="ExamItemList">
                        <li class="Val">
                            <div class="Title">
                                <%#Eval("Title") %>
                            </div>
                        </li>
                        <li>
                            <asp:Label ID="Label1" runat="server" Text="مقطع:"></asp:Label>
                        </li>
                        <li class="Val">
                            <div class="Title">
                                <%#Eval("GradeName") %>
                            </div>
                        </li>
                        <li>
                            <asp:Label ID="Label3" runat="server" Text="رشته تحصیلی:"></asp:Label>
                        </li>
                        <li class="Val">
                            <div class="Title">
                                <%#Eval("StudyName") %>
                            </div>
                        </li>
                        <li>
                            <asp:Label ID="Label2" runat="server" Text="قیمت:"></asp:Label>
                        </li>
                        <li class="Val">
                            <div class="Title">
                                <%#Eval("Price") %>
                                &nbsp;
                                ریال
                            </div>
                        </li>
                        <li>
                            <asp:Label ID="Label5" runat="server" Text="درس:"></asp:Label>
                        </li>
                        <li class="Val">
                            <div class="Title">
                                <%#Eval("Lesson") %>
                            </div>
                        </li>
                    </ul>
                    <div class="Clear">
                    </div>
                </div>
                <div class="Clear">
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="Clear">
    </div>
    <asp:Panel runat="server" ID="pnlPaging">
        <Tlb:PagerToolbar runat="server" ID="pgrToolbar" />
    </asp:Panel>
</asp:Content>
