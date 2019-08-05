<%@ Page Language="C#" AutoEventWireup="true" Title="خریدهای کاربر" MasterPageFile="~/MainMaster.master"
    CodeBehind="UserTrans.aspx.cs" Inherits="Ranjbaran.UsersFolder.UserTrans" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="panel panel-default Marginer20">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                خریدهای کاربر</h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
            </AKP:MsgBox>
            <asp:Repeater ID="rptUserTrans" OnItemCommand="HandleRepeaterCommand" runat="server">
                <HeaderTemplate>
                    <table class="tblCourseDays MyFont">
                        <tr>
                            <th class="NoWrap">
                                تاریخ
                            </th>
                            <th class="NoWrap RTL">
                                مبلغ (ریال)
                            </th>
                            <th>
                                نوع
                            </th>
                            <th>
                                عنوان
                            </th>
                            <th>
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="NoWrap">
                            <%#Eval("ChrgDate") %>
                        </td>
                        <td class="NoWrap">
                            <%#Tools.FormatCurrency( Eval("Amount")) %>
                        </td>
                        <td>
                            <%#GetItemName( Eval("ItemType") )%>
                        </td>
                        <td>
                            <%#GetItemTitle(Eval("ItemType"), Eval("ItemCode"))%>
                        </td>
                        <td>
                            <asp:Panel ID="Panel1" Visible='<%#IsVisible(Convert.ToInt32( Eval("Code"))) %>' runat="server">
                                <div>
                                    <asp:ImageButton ID="btnBuy" ToolTip="دریافت فایل" CommandArgument='<%#Eval("Code") %>'
                                        CommandName="StartDownload" ImageUrl="~/images/Download-32.png" runat="server" />
                                </div>
                                <div>
                                    دریافت فایل
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="clear">
            </div>
        </div>
    </div>
</asp:Content>
