<%@ Page Language="C#" AutoEventWireup="true" Title="گالری تصاویر" MasterPageFile="~/MainMaster.master"
    CodeBehind="Galleries.aspx.cs" Inherits="Ranjbaran.Galleries" %>

<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <link rel="stylesheet" href="Styles/prettyPhoto.css" type="text/css" media="screen"
        title="prettyPhoto main stylesheet" charset="utf-8" />
    <script src="Scripts/jquery.prettyPhoto.js" type="text/javascript" charset="utf-8"></script>


    <div class="panel panel-default Marginer20">
        <div class="panel-heading ListTitle">
            <h3 class="panel-title BulletList">
                گالری تصاویر</h3>
        </div>
        <div class="Padder5">
            <AKP:MsgBox runat="server" ID="msgMessage">
            </AKP:MsgBox>
            <asp:Repeater ID="rptGalleries" EnableViewState="false" runat="server">
                <HeaderTemplate>
                    <ul class="gallery clearfix">
                </HeaderTemplate>
                <ItemTemplate>
                    <li><a href="http://static.parset.com/Files/HGalleries/<%#Eval("PicFile") %>" rel="prettyPhoto[gallery2]" title="">
                        <img src="http://static.parset.com/Files/HGalleries/<%#Eval("SmallPicFile") %>" height="80" alt="" /></a></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            <script type="text/javascript" charset="utf-8">
                $(document).ready(function () {
                    $("area[rel^='prettyPhoto']").prettyPhoto();

                    $(".gallery:first a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'normal', theme: 'light_square', slideshow: 9000, autoplay_slideshow: true });

                });
            </script>
            <div class="clear">
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlPaging">
            <Tlb:PagerToolbar runat="server" ID="pgrToolbar" />
        </asp:Panel>
    </div>
</asp:Content>
