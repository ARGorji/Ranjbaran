<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Panel.aspx.cs" Inherits="BudgetWebApp.Panel"
    MasterPageFile="~/Admin/Main.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphMain">
    <div class="homeBox">
        <div class="homeIconTable">
            <div onmouseout="this.className='homeIconBox'" onmouseover="this.className='homeIconBox homeIconBoxHover'"
                class="homeIconBox">
                <a href="News/EditNews.aspx">
                    <asp:Image runat="server" ImageUrl="~/Admin/images/Site/addNews.png" />
                    <h4>
                        ثبت خبر جدید
                    </h4>
                </a>
            </div>
            <div onmouseout="this.className='homeIconBox'" onmouseover="this.className='homeIconBox homeIconBoxHover'"
                class="homeIconBox">
                <a href="Main/Default.aspx?BaseID=News">
                    <asp:Image runat="server" ImageUrl="~/Admin/images/Site/newsList.png" />
                    <h4>
                        اخبار ثبت شده</h4>
                </a>
            </div>
            <div onmouseout="this.className='homeIconBox'" onmouseover="this.className='homeIconBox homeIconBoxHover'"
                class="homeIconBox">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/Main/Default.aspx?BaseID=Banners">
                    <asp:Image runat="server" ImageUrl="~/Admin/images/Site/addPage.png" />
                    <h4>
                        بنرها
                    </h4>
                </asp:HyperLink>
            </div>
            <div onmouseout="this.className='homeIconBox'" onmouseover="this.className='homeIconBox homeIconBoxHover'"
                class="homeIconBox">
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Admin/Main/Default.aspx?BaseID=Links">
                    <asp:Image runat="server" ImageUrl="~/Admin/images/Site/addPage.png" />
                    <h4>
                        پیوندهای مرتبط
                    </h4>
                </asp:HyperLink>
            </div>
            
            <div onmouseout="this.className='homeIconBox'" onmouseover="this.className='homeIconBox homeIconBoxHover'"
                class="homeIconBox">
                <a href="HardCode/HardCodes.aspx?ResourceName=HardCodes">
                    <asp:Image runat="server" ImageUrl="~/Admin/images/Site/config.png" />
                    <h4>
                        تنظیمات سایت</h4>
                </a>
            </div>
            <div onmouseout="this.className='homeIconBox'" onmouseover="this.className='homeIconBox homeIconBoxHover'"
                class="homeIconBox">
                <a href="Logout.aspx">
                    <asp:Image runat="server" ImageUrl="~/Admin/images/Site/SignOut.png" />
                    <h4>
                        خروج</h4>
                </a>
            </div>
            <br class="clearfloat" />
        </div>
    </div>
</asp:Content>
