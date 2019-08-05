<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default.aspx.cs" MasterPageFile="~/MainMaster.Master" Inherits="Ranjbaran.ProductsFolder.Default" %>

<%@ Register Src="~/UserControls/UCProductList.ascx" TagName="UCProductList" TagPrefix="UC" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="">
        <div class="Hierarchy">
            <ul class="mnuHierarchy">
                <li class="IcHome">
                    <asp:HyperLink ID="hplMainPage" NavigateUrl="~/" runat="server">صفحه اصلی</asp:HyperLink>
                </li>
                <asp:Literal ID="ltrHirarchy" runat="server"></asp:Literal>
            </ul>
        </div>

        <div class="InnerPage">
            <UC:UCProductList runat="server" id="UCProductList1" />
        </div>
        <div class="Clear">
        </div>
        
        <div class="Clear">
        </div>
    </div>
    <div class="Clear">
    </div>
    
</asp:Content>

