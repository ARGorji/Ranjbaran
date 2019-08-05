<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" Title="تالیفات" AutoEventWireup="true"
    CodeBehind="Publications.aspx.cs" Inherits="Ranjbaran.Publications" %>

<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<%@ Register Src="~/UserControls/UCNews.ascx" TagName="UCNews" TagPrefix="UCN" %>
<%@ Register Src="~/UserControls/Banner.ascx" TagName="Banner" TagPrefix="UCB" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <h1 class="PageTitle">
        <asp:Image ID="Image1" ImageUrl="~/images/lblPublications.png" ToolTip="تالیفات"
            runat="server" />
    </h1>
    <UCB:Banner ID="Banner4" runat="server" PositionCode="6" />
     <UCN:UCNews runat="server" SectionCode="5" />
    <asp:Repeater ID="rptPublications" runat="server">
        <ItemTemplate>
            <div class="PublicationItem row">
                
                <div class="col-lg-2">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl='<%#"~/ShowPublication.aspx?Code=" + Eval("Code") %>'
                        runat="server">
                        <asp:Image ID="Image1" ToolTip='<%#Eval("Title") %>' CssClass="PubImgSmall" ImageUrl='<%# "~/" + Eval("SmallPic") %>'
                            runat="server" />
                    </asp:HyperLink>
                </div>
                <div class="col-lg-10">
                    <div class="Title">
                        <asp:HyperLink ID="hplPub" NavigateUrl='<%#"~/ShowPublication.aspx?Code=" + Eval("Code") %>'
                            runat="server">
                            <%#Eval("Title") %>
                        </asp:HyperLink>
                    </div>
                    <div class="Justify MyFont">
                        <%#Tools.ShowBriefText( Tools.ChangeEnc( Eval("Description")), 400) %>
                    </div>
                    <div class="cMore">
                    <asp:HyperLink ID="HyperLink2" NavigateUrl='<%#"~/ShowPublication.aspx?Code=" + Eval("Code") %>'
                        runat="server">
                            «« ادامه 
                    </asp:HyperLink>
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
        <Tlb:pagertoolbar runat="server" id="pgrToolbar" />
    </asp:Panel>
</asp:Content>
