<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCourses.ascx.cs" Inherits="Ranjbaran.UserControls.UCCourses" %>

<div class="CourseItem">
    <div>
        <div>
            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
        </div>

        <table class="table table-striped tblCourses">
            <tr>
                <th>
                    کد
                </th>
                <th>
                    مدت دوره (ساعت)
                </th>
                <th>
                    روز/ساعت
                </th>
                <th>
                    تاریخ شروع
                </th>
                <th>
                    تاریخ پایان
                </th>
                <th>
                    شهریه (ریال)
                </th>
                <th>
                    محل تشکیل کلاس
                </th>
                <th>
                    پرداخت
                </th>
            </tr>
            <asp:Repeater ID="rptCourses" runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("ID")%>
                        </td>
                        <td>
                            <%#Tools.ChangeEnc(Eval("DurationLen"))%>
                        </td>
                        <td>
                            <%#Eval("DayTime")%>
                        </td>
                        <td class="tblval">
                            <%#Tools.ChangeEnc( Eval("STime"))%>
                        </td>
                        <td class="tblval">
                            <%#Tools.ChangeEnc( Eval("ETime"))%>
                        </td>
                        <td><%#Tools.ChangeEnc( Eval("Fee"))%></td>
                        <td class="tblval">
                            <%#Eval("Location")%>&nbsp;
                        </td>
                        
                        <td colspan="2">
                            <div style="text-align: center;">
                                <asp:HyperLink ID="btnBuy" ToolTip="پرداخت شهریه" NavigateUrl='<%# "~/PayStep1.aspx?ItemType=Course&Code=" + Eval("Code")%>'
                                    ImageUrl="~/images/Buy-32.png" runat="server" /><br />
                                <asp:HyperLink ID="HyperLink1" ToolTip="پرداخت شهریه" NavigateUrl='<%# "~/PayStep1.aspx?ItemType=Course&Code=" + Eval("Code")%>'
                                    runat="server" Text="پرداخت شهریه" />
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>


    </div>
    <div class="Clear">
    </div>
</div>



