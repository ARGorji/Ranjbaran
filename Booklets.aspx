<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master"
    Title="وبسایت هادی رنجبران | جزوات" CodeBehind="Booklets.aspx.cs" Inherits="Ranjbaran.Booklets" %>

<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<%@ Register Src="~/UserControls/UCNews.ascx" TagName="UCNews" TagPrefix="UCN" %>
<%@ Register Src="~/UserControls/Banner.ascx" TagName="Banner" TagPrefix="UCB" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <h1 class="PageTitle">
        <asp:Image ID="Image1" ImageUrl="~/images/lblBooklet.png" ToolTip="جزوات" runat="server" />
    </h1>
    <AKP:MsgBox runat="server" ID="msg">
    </AKP:MsgBox>
    <div class="center MarginTopBot">
        <UCB:Banner ID="Banner4" runat="server" PositionCode="8" />
    </div>
    
    
 
     <UCN:UCNews runat="server" SectionCode="2" />
    <asp:Repeater ID="rptBooklets" OnItemCommand="HandleRepeaterCommand" runat="server">
        <ItemTemplate>
            <div class="BookletItem">
                <div class="Left">
                    <asp:Panel ID="Panel1" Visible='<%#!IsFree(Convert.ToInt32( Eval("Code"))) %>' runat="server">
                        <div>
                            <asp:ImageButton ID="btnBuy" ToolTip="خرید" ImageUrl="~/images/Buy-32.png" CommandArgument='<%#Eval("Code") %>' CommandName="StartPay" runat="server" />
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
                    <div>
                    <ul class="BookletItemList">
                        <li class="Val">
                            <div class="Title">
                                <%#Eval("Title") %>
                            </div>
                        </li>
                        <li>
                            <asp:Label ID="Label1" runat="server" Text="تاریخ:"></asp:Label>
                        </li>
                        <li class="Val">
                            <div class="Title">
                                <%#Eval("CDate") %>
                            </div>
                        </li>
                        <li>
                            <asp:Label ID="Label2" runat="server" Text="قیمت:"></asp:Label>
                        </li>
                        <li class="Val">
                            <div class="Title">
                                <table>
                                        <tr>
                                            
                                            <td><%#Tools.ChangeEnc( Eval("Price")) %></td>
                                            <td>&nbsp;ریال&nbsp;</td>
                                        </tr>
                                </table>
                            </div>
                        </li>
                    </ul>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="Farsi">
                        <%#Eval("Description") %>
                    </div>
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
