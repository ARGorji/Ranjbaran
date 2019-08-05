<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_PagerToolbar" Codebehind="PagerToolbar.ascx.cs" %>
<div class="PagerContainer">
    <div class="MyPager">
        <ul>
            <li>
                <asp:HyperLink ID="hplFirstPage" runat="server" Text=" «« "></asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="hplPrePage" runat="server" Text=" « "></asp:HyperLink></li>
            <asp:Repeater EnableViewState="false" runat="server" ID="rptPaging" OnItemDataBound="PagingDataBound">
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="hplNum" runat="server"><%#Tools.ConvertToUnicode( DataBinder.Eval(Container.DataItem, "PageNo")) %></asp:HyperLink></li></ItemTemplate>
            </asp:Repeater>
            <li>
                <asp:HyperLink ID="hplNextPage" runat="server" Text=" » "></asp:HyperLink></li>
            <li>
                <asp:HyperLink ID="hplLastPage" runat="server" Text=" »» "></asp:HyperLink></li>
        </ul>
    </div>
    <br />
</div>
