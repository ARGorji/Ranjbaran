<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master"
    CodeBehind="ShowProduct.aspx.cs" Inherits="Ranjbaran.ProductsFolder.ShowProduct" %>

<%@ Register Src="~/UserControls/NormalLogin.ascx" TagName="NormalLogin" TagPrefix="UCLogin" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <script type="text/javascript" src="../Scripts/jquery.jcarousel.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.elevatezoom.js"></script>
    <script type="text/javascript">
        function CheckTick(objCheckBox) {
            if (!$(objCheckBox).is(':checked')) {
                $("#txtComment").prop("disabled", true);
                $("#txtComment").addClass('DisabledArea');
            }
            else {
                $("#txtComment").prop("disabled", false);
                $("#txtComment").removeClass('DisabledArea');
            }
        }

        $(document).ready(function () {
            $("#btnRecommend").button();
            $("#btnRecommend").click(function () { ShowRecommendForm(); return false; });

            //            $("#btnSendComment").button();
            //            $("#btnSendComment").click(function () { ShowLogin(); return false; });


        });

        function ShowRecommendForm() {
            $("#divRecommend").modal('show');
        }

        function ShowLogin() {
            $("#divLogin").modal('show');
        }


    </script>
    
    
    <div class="">
        <div class="Hierarchy">
            <ul class="mnuHierarchy">
                <li class="IcHome">
                    <asp:HyperLink ID="hplMainPage" NavigateUrl="~/" runat="server">صفحه اصلی</asp:HyperLink>
                </li>
                <asp:Literal ID="ltrHirarchy" runat="server"></asp:Literal>
            </ul>
        </div>
        <div class="clear">
        </div>
        <div class="">
            
            <div id="productdetails" class="MainProDetail row">
                
                <div class="ProOptions col-md-8">
                    
                    <div class="cProTitle pull-right">
                        <h3>
                            <asp:Literal ID="lblEnTitle" runat="server"></asp:Literal></h3>
                    </div>
                    <div class="cProTitle  pull-left">
                        <h3>
                            <asp:Literal ID="lblFaTitle" runat="server"></asp:Literal></h3>
                    </div>
                    <div>
                        <table class="tblProInfo1">
                            
                            
                            
                            <asp:Panel ID="pnlMarketPrice" Visible="false" runat="server">
                                <tr>
                                    <td class="TDVal">
                                        <div>
                                            <asp:Label ID="lblMarketPrice" runat="server" Target="_blank" CssClass="MarketPrice"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="TDlbl">
                                        قیمت بازار:
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td class="TDVal">
                                    <div>
                                        <asp:Label ID="lblPrice" runat="server" Target="_blank" CssClass="Price"></asp:Label>
                                    </div>
                                </td>
                                <td class="TDlbl">
                                    قیمت:
                                </td>
                            </tr>
                            <asp:Panel ID="pnlCoupon" Visible="false" CssClass="Marginer10 RTL pull-right" runat="server">
                                <tr>
                                    <td colspan="2" class="TDVal">
                                        <asp:Label ID="lblCouponMessage" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <div class="clear">
                            </div>
                        </table>
                    </div>
                    <div class="clear">
                    </div>
                    <div style="text-align: left; float: right;">
                        <table class="tblBasketCount">
                            <tr>
                                <td>
                                    <asp:HyperLink ID="hplBuy" ImageUrl="~/images/addtocart.gif" ToolTip="اضافه کردن محصول به سبد خرید"
                                        runat="server"></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <AKP:MsgBox runat="server" ID="msgMessage">
                                    </AKP:MsgBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="clear">
                    </div>
                    <div class="cNormText">
                        <div class="ProDesc">
                            <asp:Label ID="lblDescription" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
                
            </div>
            <div class="Clear">
            </div>
            
            <div class="clear">
            </div>
            <div id="technicalspecs" class="clear">
            </div>
            
            
            <br />
        </div>
        <div class="Clear">
        </div>
    </div>
    <div class="Clear">
    </div>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $("area[rel^='prettyPhoto']").prettyPhoto();

            $(".gallery:first a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'normal', theme: 'light_square', slideshow: 9000, autoplay_slideshow: true });


            $("#btnproductdetails").click(function () {
                $('html, body').animate({
                    scrollTop: $("#productdetails").offset().top
                }, 2000);
            });
            $("#btnrelatedproducts").click(function () {
                $('html, body').animate({
                    scrollTop: $("#RelatedProducts").offset().top
                }, 2000);
            });
            $("#btnusercomments").click(function () {
                $('html, body').animate({
                    scrollTop: $("#usercomments").offset().top
                }, 2000);
            });
            $("#btnTechnicalspecs").click(function () {
                $('html, body').animate({
                    scrollTop: $("#technicalspecs").offset().top
                }, 2000);
            });
        });





        (function ($) {
            $(function () {
                $('.jcarousel').jcarousel();

                $('.jcarousel-control-prev')
            .on('jcarouselcontrol:active', function () {
                $(this).removeClass('inactive');
            })
            .on('jcarouselcontrol:inactive', function () {
                $(this).addClass('inactive');
            })
            .jcarouselControl({
                target: '-=1'
            });

                $('.jcarousel-control-next')
            .on('jcarouselcontrol:active', function () {
                $(this).removeClass('inactive');
            })
            .on('jcarouselcontrol:inactive', function () {
                $(this).addClass('inactive');
            })
            .jcarouselControl({
                target: '+=1'
            });

                $('.jcarousel-pagination')
            .on('jcarouselpagination:active', 'a', function () {
                $(this).addClass('active');
            })
            .on('jcarouselpagination:inactive', 'a', function () {
                $(this).removeClass('active');
            })
            .jcarouselPagination();
            });
        })(jQuery);


        //$(".ShowProPic").elevateZoom({zoomEnabled:!1,mobile:!1,cursor:"crosshair",galleryActiveClass:"active",imageCrossfade:!0,scrollZoom:!1,tint:!1,zoomWindowWidth:320,zoomWindowHeight:400,tintColour:"#000",tintOpacity:.5,borderSize:1,loadingIcon:"../Images/Loading.GIF",zoomWindowPosition:9,easing:!0,easingDuration:1e3,zoomType:""});


        $(".ShowProPic").elevateZoom({
            easing: true,
            scrollZoom: true,
            zoomWindowPosition: 10,
            borderSize: 1
        });

    </script>
</asp:Content>
