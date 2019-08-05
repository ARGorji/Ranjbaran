<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master"
    Title="وبسایت هادی رنجبران | اخبار" CodeBehind="News.aspx.cs" Inherits="Ranjbaran.NewsPage" %>

<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <h1 class="PageTitle">
        <asp:Image ID="Image1" ImageUrl="~/images/lblNews.png" ToolTip="اخبار" runat="server" />
    </h1>
    <div class="NewsItemCont">
        <asp:Repeater ID="rptNews" runat="server">
            <ItemTemplate>
                <div class="NewsItem">
                    <div>
                        <ul class="NewsItemList">
                            <li>
                                <div class="NewsTitle" onclick="ToggleNews('<%#Eval("Code")%>', '<%#Eval("Link")%>')">
                                    <%#Eval("Title") %>
                                </div>
                            </li>
                            <li>
                                <div class="ViewNews">
                                    <%#Tools.ChangeEnc( Eval("NDate")) %>
                                </div>
                            </li>
                            
                        </ul>
                    </div>
                    <div class="Clear">
                    </div>
                    
                    <div id="News<%#Eval("Code") %>" class="NewsBody hide">
                        <%#Tools.FormatString( Eval("NewsBody") )%>
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
    </div>
</asp:Content>
