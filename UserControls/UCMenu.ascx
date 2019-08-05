<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCMenu.ascx.cs" Inherits="Ranjbaran.UserControls.UCMenu" %>
<div class="VerMenu d-none d-md-block">
    <div class="MenuContent ">
        <div class="SubMenuCont Home">
            <div class="VMenuItem">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Default.aspx" runat="server">صفحه اصلی</asp:HyperLink>
            </div>
        </div>
        <div class="SubMenuCont">
            <div class="VMenuItem">
                <asp:HyperLink ID="HyperLink12" NavigateUrl="~/Esbaat.aspx" runat="server">نشر اثبات</asp:HyperLink>
            </div>
        </div>
        <div class="SubMenuCont Books">
            <div class="VMenuItem">
                <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Publications.aspx" runat="server">تالیفات</asp:HyperLink>
            </div>
        </div>
        <div class="SubMenuCont Education">
            <div class="VMenuItem">
                <a>کارشناسی ارشد
                </a>
            </div>
            <div id="MasterChilds" class="SubMenu hide">
                <div class="row">
                    <asp:Repeater ID="rptMasterStudyFields" runat="server">
                        <ItemTemplate>
                            <div class="col-3 SubMenuBox ">
                                <i class="far fa-dot-circle"></i>
                                <asp:HyperLink NavigateUrl='<%#"~/ShowStudyInfo.aspx?HCGradeCode=4&HCStudyFieldCode=" + Eval("HCStudyFieldCode")%>'
                                    ToolTip='<%#Eval("HCStudyField")%>' runat="server"><%#Tools.ShowBriefText(Eval("HCStudyField"), 45)%></asp:HyperLink>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

        <div class="SubMenuCont Education">
            <div class="VMenuItem"><a>دکتری</a></div>
            <div id="PHDChilds" class="SubMenu hide">

                <div class="row">
                    <asp:Repeater ID="rptPHDStudyFields" runat="server">
                        <ItemTemplate>
                            <div class="col-3 SubMenuBox ">
                                <i class="far fa-dot-circle"></i>
                                <asp:HyperLink NavigateUrl='<%#"~/ShowStudyInfo.aspx?HCGradeCode=5&HCStudyFieldCode=" + Eval("HCStudyFieldCode")%>'
                                    ToolTip='<%#Eval("HCStudyField")%>' runat="server"><%#Tools.ShowBriefText(Eval("HCStudyField"), 45)%></asp:HyperLink>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>

        <div class="SubMenuCont Calendar">
            <div class="VMenuItem">
                <asp:HyperLink ID="HyperLink3" NavigateUrl="~/Courses.aspx" runat="server">کلاس‌ها</asp:HyperLink>
            </div>
        </div>
        <div class="SubMenuCont Booklet">
            <div class="VMenuItem">
                <asp:HyperLink ID="HyperLink4" NavigateUrl="~/Booklets.aspx" runat="server">جزوات</asp:HyperLink>
            </div>
        </div>
        <div class="SubMenuCont Exam">
            <div class="VMenuItem">
                <asp:HyperLink ID="HyperLink10" NavigateUrl="~/Exams.aspx" runat="server">آزمون‌ها</asp:HyperLink>
            </div>
        </div>
        
        <div class="SubMenuCont FAQ">
            <div class="VMenuItem">
                <asp:HyperLink ID="HyperLink6" NavigateUrl="~/Questions.aspx" runat="server">پرسش و پاسخ</asp:HyperLink>
            </div>
        </div>
        
    </div>
</div>
