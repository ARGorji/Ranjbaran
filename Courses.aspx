<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master"
    Title="کلاس‌های آمادگی آزمون کارشناسی ارشد و دکتری" CodeBehind="Courses.aspx.cs"
    Inherits="Ranjbaran.Courses" %>

<%@ Register Src="~/UserControls/UCCourses.ascx" TagName="UCCourses" TagPrefix="UCCD" %>
<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<%@ Register Src="~/UserControls/UCNews.ascx" TagName="UCNews" TagPrefix="UCN" %>
<%@ Register Src="~/UserControls/Banner.ascx" TagName="Banner" TagPrefix="UCB" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    
        <div class="row">
            <div class="col"><asp:Image ID="Image1" ImageUrl="~/images/lblCourse.png" ToolTip="کلاس‌های آمادگی آزمون کارشناسی ارشد و دکتری"
            runat="server" /></div>
            <div class="col">
                <div><br />
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Files/Reg.pdf" Target="_blank" runat="server">تعهد نامه ثبت نام</asp:HyperLink>
                </div>

            </div>
        </div>
        
    
    <div class="center MarginTopBot">
        <UCB:Banner ID="Banner4" runat="server" PositionCode="18" />
    </div>
    <UCN:UCNews runat="server" SectionCode="1" />
    
    <div class="Clear">
    </div>
    
    <UCCD:UCCourses ID="UCCourses1" Title="مدیریت (کلیه گرایشها)، حسابداری، اقتصاد، مدیریت جهانگردی، برنامه ریزی شهری، سنجش از دور، محیط زیست" HCGradeCode="4" HCMainFieldCode="1" runat="server"  />

    <UCCD:UCCourses ID="UCCourses2" Title="مدیریت (کلیه گرایشها)، حسابداری، اقتصاد، مدیریت جهانگردی، برنامه ریزی شهری، سنجش از دور و .." HCGradeCode="4" HCMainFieldCode="2" runat="server"  />

    <UCCD:UCCourses ID="UCCourses3" Title="مدیریت (کلیه گرایشها)، حسابداری، اقتصاد، مجموعه مالی، کارآفرینی، آینده پژوهی، جهانگردی، برنامه ریزی شهری، سنجش از دور" HCGradeCode="5" HCMainFieldCode="1" runat="server"  />

    <UCCD:UCCourses ID="UCCourses4" Title="حسابداری، مجموعه مالی، اقتصاد، جهانگردی، برنامه ریزی شهری، سنجش از دور و .." HCGradeCode="5" HCMainFieldCode="2" runat="server"  />

    <div class="Clear">
    </div>
    <asp:Panel runat="server" ID="pnlPaging">
        <Tlb:PagerToolbar runat="server" ID="pgrToolbar" />
    </asp:Panel>
</asp:Content>
