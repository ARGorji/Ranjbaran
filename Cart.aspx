<%@ Page Language="C#" AutoEventWireup="True" Title="سفارش کالا" EnableEventValidation="false"
    MasterPageFile="~/MainMaster.Master" CodeBehind="Cart.aspx.cs" Inherits="Ranjbaran.Order" %>


<%@ Register Src="~/UserControls/NormalLogin.ascx" TagName="NormalLogin" TagPrefix="UCLogin" %>
<%@ Register Src="~/UserControls/UCRegister.ascx" TagName="UCRegister" TagPrefix="Reg" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <script src="scripts/bootstrap-combobox.js" type="text/javascript"></script>
    <script src="scripts/bootstrap-transition.js" type="text/javascript"></script>
    <div class="">
        <div class="Padder25">
            <div class="Hierarchy">
                <ul class="mnuHierarchy">
                    <li class="IcHome">
                        <asp:HyperLink ID="hplMainPage" NavigateUrl="~/" runat="server">صفحه اصلی</asp:HyperLink>
                    </li>
                    <li class="Sep">&nbsp; </li>
                    <li>
                        <asp:Label ID="Label1" runat="server" Text="سبد خرید"></asp:Label>
                    </li>
                </ul>
            </div>
            <div class="clear">
            </div>
            <div class="Marginer1">
                <AKP:MsgBox runat="server" ID="msgMessage">
                </AKP:MsgBox>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlBasket" CssClass="OrgaeWin">
                        <div class="Marginer1">
                            <div class="header">
                            </div>
                            <h2>
                                <asp:Literal ID="ltrBasketHeader" runat="server" Text=""></asp:Literal>
                            </h2>
                            <asp:Repeater ID="rptBasket" OnItemCommand="HandleRepeaterCommand" OnItemCreated="RepeaterItemCreated"
                                runat="server">
                                <HeaderTemplate>
                                    <table class="tblBasket" cellspacing="0">
                                        <tr>
                                            <th class="text-center">
                                                <asp:Label ID="Label1" runat="server" Text="حذف"></asp:Label>
                                            </th>
                                            <th class="text-center">
                                                <asp:Label ID="Label3" runat="server" Text="قیمت کل"></asp:Label>
                                            </th>
                                            <th class="text-center">
                                                <asp:Label ID="Label4" runat="server" Text="قیمت پایه"></asp:Label>
                                            </th>
                                            <th class="text-center">
                                                <asp:Label ID="Label5" runat="server" Text="تعداد"></asp:Label>
                                            </th>
                                            <%--<th>
                                                <asp:Label ID="Label40" runat="server" Text="وضعیت"></asp:Label>
                                            </th>--%>
                                            <th class="text-center">
                                                <asp:Label ID="Label6" runat="server" Text="نام محصول"></asp:Label>
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton runat="server" ProductCode='<%#DataBinder.Eval(Container.DataItem, "ProductCode") %>'
                                                OnClientClick=<%#"return confirm('آیا از حذف " +  DataBinder.Eval(Container.DataItem, "ProductName") + " مطمئن هستید؟ ')" %>
                                                ID="btnRemove" CommandName="RemoveFromBasket" ToolTip="حذف از سبد خرید" ImageUrl="~/images/Remove-icon.png" />
                                        </td>
                                        <td>
                                            <%#Tools.ChageEnc(Tools.FormatCurrency(DataBinder.Eval(Container.DataItem, "ProductTotalPrice").ToString()))%>
                                        </td>
                                        <td>
                                            <%#Tools.ChageEnc(Tools.FormatCurrency(DataBinder.Eval(Container.DataItem, "ProductPrice").ToString()))%>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlItemCount" SelectedValue='<%# Eval("ItemCount")%>' ProductCode='<%# Eval("ProductCode") %>'
                                                AutoPostBack="true" runat="server">
                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <%--<td>
                                            <%#GetStatus(Eval("ProductCode"))%>
                                        </td>--%>
                                        <td>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ProductName")%>'
                                                NavigateUrl='<%#"~/Products/ShowProduct.aspx?Code=" + DataBinder.Eval(Container.DataItem, "ProductCode")%>'></asp:HyperLink>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table></FooterTemplate>
                            </asp:Repeater>
                            <asp:Panel runat="server" ID="pnlTotal" CssClass="ContTotal">
                                <asp:Label ID="Label18" runat="server" Text="مجموع:"></asp:Label>&nbsp;
                                <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>&nbsp;ریال
                            </asp:Panel>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Panel runat="server" Visible="false" ID="pnlLoggedUser">
                <div >
                    <div >
                        <div class="NewsItemCont text-center">
                            <UCLogin:NormalLogin ID="NormalLogin1" AfterLoginUrl="~/Shipping.aspx" runat="server" />
                        </div>
                    </div>
                    <div >
                        <div class="NewsItemCont text-center">
                            <Reg:UCRegister runat="server" AfterRegUrl="~/Shipping.aspx" ID="UCRegister1" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            
            <div>
                <asp:Literal ID="ltrFactor" runat="server"></asp:Literal>
            </div>
            <div class="Spacer1">
            </div>
            <div>
                <asp:ImageButton ID="btnNext1" ImageUrl="~/images/spacer.gif" Width="220" CssClass="FinalizePurchase"
                    OnClientClick="Hide(this)" Text="خرید" runat="server" OnClick="btnFinalizePurchase_Click">
                </asp:ImageButton>
                <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/spacer.gif"  Width="220" CssClass="ContinuePurchase"
                    OnClientClick="Hide(this)" Text="ادامه خرید" runat="server" OnClick="btnContinuePurchase_Click">
                </asp:ImageButton>
            </div>
            <div class="Clear">
            </div>
            <br />
        </div>
        <div class="Clear">
        </div>
        <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
    </div>
</asp:Content>
