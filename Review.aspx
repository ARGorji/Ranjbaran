<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.Master"
    CodeBehind="Review.aspx.cs" Inherits="Ranjbaran.Review" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="">
        <div class="Padder25">
            <div class="Hierarchy">
                <ul class="mnuHierarchy">
                    <li class="IcHome">
                        <asp:HyperLink ID="hplMainPage" NavigateUrl="~/" runat="server">صفحه اصلی</asp:HyperLink>
                    </li>
                    <li class="Sep">&nbsp; </li>
                    <li>
                        <asp:Label ID="Label1" runat="server" Text="بازبینی سفارش"></asp:Label>
                    </li>
                </ul>
            </div>
            <div class="clear"></div>
            <div class="Marginer1">
                <AKP:MsgBox runat="server" ID="msgMessage">
                </AKP:MsgBox>
            </div>
            <div class="header">
                <h2>
                    <asp:Label ID="lblBasketHeader" runat="server" Text="سبد خرید خالی است"></asp:Label>
                </h2>
            </div>
            <div class="clearfix"></div>
            <asp:Panel runat="server" ID="pnlBasket" CssClass="OrgaeWin">
                <div class="Marginer1">
                    <asp:Repeater ID="rptBasket" runat="server">
                        <HeaderTemplate>
                            <table class="tblBasket" cellspacing="0">
                                <tr>
                                    <th class="text-center">
                                        <asp:Label ID="Label3" runat="server" Text="قیمت کل"></asp:Label>
                                    </th>
                                    <th class="text-center">
                                        <asp:Label ID="Label4" runat="server" Text="قیمت پایه"></asp:Label>
                                    </th>
                                    <th class="text-center">
                                        <asp:Label ID="Label5" runat="server" Text="تعداد"></asp:Label>
                                    </th>
                                    <th class="text-center">
                                        <asp:Label ID="Label6" runat="server" Text="نام محصول"></asp:Label>
                                    </th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Tools.ChageEnc(Tools.FormatCurrency(DataBinder.Eval(Container.DataItem, "ProductTotalPrice").ToString()))%>
                                </td>
                                <td>
                                    <%#Tools.ChageEnc(Tools.FormatCurrency(DataBinder.Eval(Container.DataItem, "ProductPrice").ToString()))%>
                                </td>
                                <td>
                                    <%#Tools.ChangeEnc( DataBinder.Eval(Container.DataItem, "ItemCount").ToString())%>
                                </td>
                                <td>
                                    <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductName")%>'
                                        NavigateUrl='<%#"~/ShowProduct.aspx?Code=" + DataBinder.Eval(Container.DataItem, "ProductCode")%>'></asp:HyperLink>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table></FooterTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
            <div class="header">
                <h2>
                    صورتحساب شما
                </h2>
            </div>
            <div class="clearfix"></div>
            <div>
                <table class="tblReview">
                    <tr>
                        <td class="TDText NoWrap">
                            جمع کل خرید شما:
                        </td>
                        <td class="TDCurrencyVal">
                            <asp:Label ID="lblTotalAmount" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="CurrencyLabel">
                            تومان
                        </td>
                    </tr>
                    <tr>
                        <td class="TDText NoWrap">
                            هزینه ارسال، بیمه و بسته بندی سفارش:
                        </td>
                        <td class="TDCurrencyVal">
                            <asp:Label ID="lblOtherCosts" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="CurrencyLabel">
                            تومان
                        </td>
                    </tr>
                    <tr>
                        <td class="TDText NoWrap">
                            جمع کل تخفیف:
                        </td>
                        <td class="TDCurrencyVal">
                            <asp:Label ID="lblDiscount" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="CurrencyLabel">
                            تومان
                        </td>
                    </tr>
                    <tr>
                        <td class="TDText NoWrap">
                            جمع کل قابل پرداخت:
                        </td>
                        <td class="TDCurrencyVal">
                            <asp:Label ID="lblTotalOrderPrice" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="CurrencyLabel">
                            تومان
                        </td>
                    </tr>
                </table>
            </div>
            <div class="header">
                <h2>
                    اطلاعات ارسال سفارش
                </h2>
            </div>
            <div class="clearfix"></div>
            <div>
                <table class="tblReview">
                    <tr>
                        <td class="TDText">
                        </td>
                        <td class="TDText">
                            این سفارش به
                            <asp:Label ID="lblFullName" CssClass="lblGreen" runat="server" Text=""></asp:Label>
                            به آدرس
                            <asp:Label ID="lblAddress" CssClass="lblGreen" runat="server" Text=""></asp:Label>
                            و شماره تماس
                            <asp:Label ID="lblContactNumber" CssClass="lblGreen" runat="server" Text=""></asp:Label>
                            تحویل می‌گردد
                        </td>
                    </tr>
                    <tr>
                        <td class="TDText">
                        </td>
                        <td class="TDText">
                            این سفارش از طریق تحويل
                            <asp:Label ID="lblSendType" CssClass="lblGreen" runat="server" Text=""></asp:Label>
                            با هزینه
                            <asp:Label ID="lblSendCost" CssClass="lblGreen" runat="server" Text=""></asp:Label>
                            تومان به شما تحویل داده خواهد شد.
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Panel runat="server" ID="pnlPayTools">
                    <div class="Marginer1">
                        <div class="OrderForm" style="text-align: right;">
                            <div style="text-align: left; padding-left: 15px;">
                                <asp:ImageButton ID="btnConfirm" Width="230" ImageUrl="~/images/spacer.gif" OnClientClick="Hide(this)"
                                    CssClass="SaveOrder" Text="تایید اطلاعات و ثبت سفارش" runat="server" OnClick="btnConfirm_Click">
                                </asp:ImageButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="Clear">
            </div>
            <br />
            <br />
        </div>
        <div class="Clear">
        </div>
    </div>
    <div class="Clear">
    </div>
</asp:Content>
