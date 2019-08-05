<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCLinks.ascx.cs" Inherits="Ranjbaran.UserControls.UCLinks" %>
<div>
    <asp:Repeater ID="rptLinks" runat="server">
        <ItemTemplate>
            <div class="LinkItem">
                <div>
                    <asp:HyperLink Target="_blank" ID="HyperLink1" NavigateUrl='<%#Eval("URL") %>' runat="server">
                        <asp:Image ID="Image1" ImageUrl='<%#"~/" + Eval("PicFile") %>' runat="server" />
                    </asp:HyperLink>
                </div>
                <div class="LinkTitle">
                    <asp:HyperLink Target="_blank" ID="hplLink" NavigateUrl='<%#Eval("URL") %>' runat="server"><%#Eval("Title") %></asp:HyperLink>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
