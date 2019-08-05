<%@ Page Title="وبسایت شخصی هادی رنجبران" Language="C#" MasterPageFile="~/MainMaster.master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Ranjbaran._Default" %>

<%@ Register Src="~/UserControls/Banner.ascx" TagName="Banner" TagPrefix="UCB" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="row">
        <div class="float-left">
            <asp:Panel ID="pnlLargeSlider" runat="server">
                <div id="jssor_1" style="position: relative; margin-right: 15px auto; top: 0px; left: 0px; width: 840px; height: 274px; overflow: hidden; visibility: hidden;">
                    <!-- Loading Screen -->
                    <div data-u="loading" class="jssorl-009-spin" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; text-align: center; background-color: rgba(0,0,0,0.7);">
                        <img alt="" style="margin-top: -19px; position: relative; top: 50%; width: 38px; height: 38px;" src="images/spin.svg" />
                    </div>
                    <div data-u="slides" style="cursor: default; position: relative; top: 0px; left: 0px; width: 840px; height: 274px; overflow: hidden;">
                        <div data-p="225.00">
                            <asp:HyperLink ID="HyperLink9" NavigateUrl="~/Publications.aspx" runat="server">
                            <img data-u="image" alt="" src="images/SitePic4.jpg" />
                            </asp:HyperLink>
                        </div>
                        <div data-p="225.00">
                            <asp:HyperLink ID="HyperLink5" NavigateUrl="~/Publications.aspx" runat="server">
                            <img data-u="image" alt="" src="images/Sitepic1.jpg" />
                            </asp:HyperLink>
                        </div>
                        <div data-p="225.00">
                            <asp:HyperLink ID="HyperLink6" NavigateUrl="~/Publications.aspx" runat="server">
                            <img data-u="image" alt="" src="images/SitePic2.jpg" />
                            </asp:HyperLink>
                        </div>
                        <div data-p="225.00">
                            <asp:HyperLink ID="HyperLink7" NavigateUrl="~/Publications.aspx" runat="server">
                            <img data-u="image" alt="" src="images/SitePic3.jpg" />
                            </asp:HyperLink>
                        </div>

                    </div>
                    <!-- Bullet Navigator -->
                    <div data-u="navigator" class="jssorb032" style="position: absolute; bottom: 12px; right: 12px;" data-autocenter="1" data-scale="0.5" data-scale-bottom="0.75">
                        <div data-u="prototype" class="i" style="width: 16px; height: 16px;">
                            <svg viewbox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                                <circle class="b" cx="8000" cy="8000" r="5800"></circle>
                            </svg>
                        </div>
                    </div>
                    <!-- Arrow Navigator -->
                    <div data-u="arrowleft" class="jssora051" style="width: 65px; height: 65px; top: 0px; left: 25px;" data-autocenter="2" data-scale="0.75" data-scale-left="0.75">
                        <svg viewbox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                            <polyline class="a" points="11040,1920 4960,8000 11040,14080 "></polyline>
                        </svg>
                    </div>
                    <div data-u="arrowright" class="jssora051" style="width: 65px; height: 65px; top: 0px; right: 25px;" data-autocenter="2" data-scale="0.75" data-scale-right="0.75">
                        <svg viewbox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                            <polyline class="a" points="4960,1920 11040,8000 4960,14080 "></polyline>
                        </svg>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div class="float-right" style="width: 239px; overflow: hidden;">
            <div class="row">
                <div class="col-lg-6">
                    <asp:Image ID="Image2" ImageUrl="~/images/LeftBoxTel.jpg" runat="server" />
                </div>
                <div class="col-lg-6">
                    <img id="drftgwmdlbrhwmcssgui" style="cursor: pointer" onclick="window.open('https://trustseal.enamad.ir/Verify.aspx?id=19780&amp;p=nbpdjzpgqgwlaqgwdrfs', 'Popup','toolbar=no, location=no, statusbar=no, menubar=no, scrollbars=1, resizable=0, width=580, height=600, top=30')" alt="" src="https://trustseal.enamad.ir/logo.aspx?id=19780&amp;p=lznbzpfvpeukukaqgthv" />
                </div>
            </div>
            <asp:HyperLink ID="HyperLink8" NavigateUrl="http://telegram.me/HadiRanjbaran" runat="server">
                <asp:Image ID="Image1" Width="239" ImageUrl="~/images/LeftBoxBot.jpg" runat="server" />
            </asp:HyperLink>

        </div>
        <div class="Clear"></div>
    </div>

    <div class="row">
        <div class="col-lg-6 cl-md-6 col-sm-6">
            <div class="Box1">
                <div class="BoxHeader">
                    پیوندها
                </div>
                <div class="BoxBody">
                    <ul id="LinkBox">
                        <asp:Repeater ID="rptLinks" runat="server">
                            <ItemTemplate>
                                <li class="NewsItem">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("URL") %>'><%#Eval("Title") %></asp:HyperLink></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>

                </div>
            </div>
        </div>
        <div class="col-lg-6 cl-md-6 col-sm-6">
            <div class="Box1">
                <div class="BoxHeader">
                    تازه های سایت
                </div>
                <div class="BoxBody">
                    <ul id="NewsBox">
                        <asp:Repeater ID="rptNewsTicker" runat="server">
                            <ItemTemplate>
                                <li class="NewsItem">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/Shownews.aspx?Code=" + Eval("Code") %>'><%#Eval("Title") %></asp:HyperLink></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="float-left FullWidth">
                        <asp:HyperLink ID="HyperLink911" CssClass="btn btn-info" NavigateUrl="~/News.aspx" runat="server">بیشتر</asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div class="row">
        <div class="col-lg-3 col-md-3">
            <div class="Box2">
                <div class="Box2IconHolder">
                    <asp:HyperLink runat="server" NavigateUrl="~/Products">
                        <i class="fas fa-shopping-cart ExLarge"></i>
                    </asp:HyperLink>
                </div>
                <div class="Box2Header">
                    خرید آنلاین کتاب
                </div>
                <div class="Box2Body">
                    کاربر گرامی، شما می توانید از این طریق کتاب مورد نیاز خود را به صورت غیرحضوری در کمترین زمان ممکن تهیه نمایید. در این بخش می
توانید چندین کتاب را به سبد خرید خود اضافه کرده و به صورت یکجا اقدام به خرید نمایید
                   

                </div>

                <div class="text-center FullWidth">
                    <asp:HyperLink ID="hplFAQ" CssClass="btn btn-info" NavigateUrl="~/Products" runat="server">بیشتر</asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3">
            <div class="Box2">
                <div class="Box2IconHolder">
                    <asp:HyperLink runat="server" NavigateUrl="~/Courses.aspx">
                        <i class="fas fa-users ExLarge"></i>
                    </asp:HyperLink>
                </div>
                <div class="Box2Header">
                    کلاسهای ریاضی و آمار
                </div>
                <div class="Box2Body">
                    کاربر گرامی، شما میتوانید از این طریق در کلاسهای ریاضی و آمار جهت آمادگی در آزمون دکتری و کارشناسی ارشد اقدام کنید
                    

                </div>

                <div class="text-center FullWidth">
                    <asp:HyperLink ID="HyperLink2" CssClass="btn btn-info" NavigateUrl="~/Courses.aspx" runat="server">بیشتر</asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3">
            <div class="Box2">
                <div class="Box2IconHolder">
                    <asp:HyperLink runat="server" NavigateUrl="~/Booklets.aspx">
                        <i class="fas fa-book ExLarge"></i>
                    </asp:HyperLink>
                </div>
                <div class="Box2Header">
                    جزوات آموزشی
                </div>
                <div class="Box2Body">
                    مباحث «ریاضیات مقدماتی»، شامل: عبارت های جبری، اتحاد، تجزیه، عبارت های گویا، توان و رادیکال، حل انواع معادله، حل
انواع نامعادله، دستگاه مختصات دکارتی و معادلة خط، تصاعدها، لگاریتم، رسم نمودارهای
مهم، مثلثات و ... . حکمِ ابزار کار برای حل مساله و تست را دارد.
                </div>
                <div class="text-center FullWidth">
                    <asp:HyperLink ID="HyperLink3" CssClass="btn btn-info" NavigateUrl="~/Booklets.aspx" runat="server">بیشتر</asp:HyperLink>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3">
            <div class="Box2">
                <div class="Box2IconHolder">
                    <asp:HyperLink runat="server" NavigateUrl="~/Exams.aspx">
                        <i class="fas fa-file-alt ExLarge"></i>
                    </asp:HyperLink>
                </div>
                <div class="Box2Header">
                    آزمونهای آزمایشی
                </div>
                <div class="Box2Body">
                    با شرکت در آزمون های آزمایشی ریاضی و آمار، مطابق برنامه اعلام شده، موفقیت خود را در دروس ریاضی و آمار، تضمین کنید
                </div>
                <div class="text-center FullWidth">
                    <asp:HyperLink ID="HyperLink4" CssClass="btn btn-info" NavigateUrl="~/Exams.aspx" runat="server">بیشتر</asp:HyperLink>
                </div>
            </div>
        </div>

    </div>

    <div class="Clear">
    </div>
    <div class="center MarginTopBot">
        <UCB:Banner ID="Banner4" runat="server" PositionCode="4" />
    </div>

    <div class="Clear">
    </div>


    <div>
        <UCB:Banner ID="Banner1" runat="server" PositionCode="16" />
    </div>
    <div class="Clear">
    </div>
    <div class="center MarginTopBot">
        <UCB:Banner ID="Banner2" runat="server" PositionCode="1" />
    </div>
    <div class="center MarginTopBot">
        <UCB:Banner ID="Banner7" runat="server" PositionCode="12" />
    </div>
    <div class="center MarginTopBot">
        <UCB:Banner ID="Banner8" runat="server" PositionCode="13" />
    </div>
    <div class="center MarginTopBot">
        <UCB:Banner ID="Banner9" runat="server" PositionCode="14" />
    </div>

</asp:Content>

