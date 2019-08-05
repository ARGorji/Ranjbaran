<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCourseDays.ascx.cs" Inherits="Ranjbaran.UserControls.UCCourseDays" %>
<div class="hide CourseDays" id="Course<%=strCourseCode %>">
    <asp:Repeater ID="rptCourseDays" runat="server">
        <HeaderTemplate>
            <table class="tblCourseDays MyFont">
                <tr>
                <th class="NoWrap">
                    روز
                </th>
                <th>
                    ساعت شروع
                </th>
                <th>
                    ساعت پایان
                </th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td class="NoWrap">
                    <%#Eval("DayName") %>
                </td>
                <td>
                    <%#Tools.ChangeEnc( Eval("StartTime") )%>
                </td>
                <td>
                    <%#Tools.ChangeEnc( Eval("EndTime")) %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Panel ID="pnlNoData" Visible="false" runat="server">
        <div>
            برنامه کلاسها تعیین نشده است
        </div>
    </asp:Panel>
</div>