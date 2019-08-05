<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCProductList.ascx.cs"
    Inherits="Ranjbaran.UserControls.UCProductList" %>
<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<asp:Panel runat="server" ID="pnlSerachTools" Visible="false" CssClass="RightAlign">
    <div>
        <div class="sortoption">
            <span class="boldtext">حالت نمایش</span> <a href="javascript:void(0);" class="listbtn">
            </a><a href="javascript:void(0);" class="gridbtn selected"></a>
        </div>
    </div>
    <div class="clear">
    </div>
    <table class="tblProTools">
        <tr>
            <td>
                <span>مرتب سازی بر اساس </span>
            </td>
            <td>
                <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                    <asp:ListItem Text="پربازدیدترین" Value="1"></asp:ListItem>
                    <asp:ListItem Text="جدیدترین" Value="2"></asp:ListItem>
                    <asp:ListItem Text="محبوب ترین" Value="3"></asp:ListItem>
                    <asp:ListItem Text="پیشنهاد ویژه" Value="4"></asp:ListItem>
                    <asp:ListItem Text="قیمت" Value="5"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlAscDesc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAscDesc_SelectedIndexChanged">
                    <asp:ListItem Text="نزولی" Value="1"></asp:ListItem>
                    <asp:ListItem Text="صعودی" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <span>تعداد نمایش </span>
            </td>
            <td>
                <asp:DropDownList ID="ddlItemPerPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlItemPerPage_SelectedIndexChanged">
                    <asp:ListItem Text="24" Value="24"></asp:ListItem>
                    <asp:ListItem Text="36" Value="36"></asp:ListItem>
                    <asp:ListItem Text="48" Value="48"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</asp:Panel>
<div class="clear">
</div>
<div class="ProductListCont">
    <div class="Caption">
        <asp:Literal ID="ltrHeader" Text="با خرید آنلاین از تخقیق ویژه بهرمند شوید" runat="server"></asp:Literal>
    </div>
    <ul class="SelectedPros">
        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <li class="ProListItem GridView  <%#ShowClass(Eval("Code")) %> ">
                    <%#ISpecial( Eval("Special")) %>
                    <asp:HyperLink CssClass="zoom" runat="server" NavigateUrl='<%#"~/Products/ShowProduct.aspx?Code=" + Eval("Code")  %>'>
                        <div class="ListViewRight">
                            <div class="ProPicCont">
                                <div class="block">
                                    <asp:Image AlternateText='<%#Eval("FaTitle") %>' ToolTip='<%#Eval("FaTitle") %>'
                                        ImageUrl='<%#"~/" + Eval("SmallPicFile") %>' runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="ListViewLeft">
                            <div class="ProTitle">
                                <%#Eval("FaTitle") %>
                            </div>
                            <div class="ProDesc NoDisp">
                                <%#Eval("Description") %>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="AddToCartMinCont">
                                        <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# "~/Cart.aspx?ProductCode=" + Eval("Code") %>'
                                            runat="server">
                                    <i class="fa fa-cart-plus fa-2x Maroon"></i>
                                        </asp:HyperLink>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div class="ProPriceAdd">
                                        <asp:Panel ID="Panel1" runat="server" Visible='<%#ShowMarketPrice(Eval("Price"), Eval("MarketPrice")) %>'
                                            class="MarketPrice">
                                            <%#Tools.FormatCurrency2( Convert.ToInt32( Eval("MarketPrice")) / 10) %>
                                            تومان
                                        </asp:Panel>
                                        <div class="LargePrice">
                                            <%#Tools.FormatCurrency2( Convert.ToInt32( Eval("Price")) / 10) %>
                                            تومان
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </asp:HyperLink>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <AKP:MsgBox runat="server" ID="msg" />
    <div class="Clear">
    </div>
</div>
<div>
    <Tlb:PagerToolbar runat="server" ID="pgrToolbar" />
</div>
<script type="text/javascript">


    $(document).ready(function () {


        $(".listbtn").click(function () {
            $(".gridbtn").removeClass("selected");
            $(".listbtn").addClass("selected");

            $(".ProListItem").removeClass("GridView");
            $(".ProListItem").addClass("ListView");
            $(".ProTitle").addClass("ProTitleList");

            $(".ProDesc").removeClass("NoDisp");


            createCookie("ViewType", "List", 7);


        });

        $(".gridbtn").click(function () {
            $(".listbtn").removeClass("selected");
            $(".gridbtn").addClass("selected");

            $(".ProListItem").removeClass("ListView");
            $(".ProListItem").addClass("GridView");
            $(".ProDesc").addClass("NoDisp");
            $(".ProTitle").removeClass("ProTitleList");

            createCookie("ViewType", "Grid", 7);

        });

        var CurrentViewType = readCookie("ViewType");
        if (CurrentViewType == "List") {
            $(".listbtn").click();
        }

        $(".product-bestsales").removeClass('hide');
    });
    $(function () {
        try {

            $('.ProListItem').hoverZoom({
                overlay: false, // false to turn off (default true)
                overlayOpacity: 0.7, // overlay opacity
                zoom: 25, // amount to zoom (px)
                speed: 300 // speed of the hover
            });
        }
        catch (err)
        {

        }

    }); 


</script>
