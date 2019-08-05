
//-----------------------------------------------------------
String.prototype.replaceAll = function (strTarget, strSubString) {
    var strText = this;
    var intIndexOfMatch = strText.indexOf(strTarget);
    while (intIndexOfMatch != -1) {
        strText = strText.replace(strTarget, strSubString)
        intIndexOfMatch = strText.indexOf(strTarget);
    }
    return (strText);
}
//-----------------------------------------------------------------------------
function disableEnterKey(e) {
    var key;
    if (window.event)
        key = window.event.keyCode; //IE
    else
        key = e.which; //firefox     
    return (key != 13);
}
//----------------------------------
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
//----------------------------------
function getQuerystring(key, default_) {
    if (default_ == null) default_ = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null)
        return default_;
    else
        return qs[1];
}
//----------------------------------


$(document).ready(function () {
    GetNotication();
    GetNoticationNews();
    //    GetVisitShop();
    //    GetVisitCity();
    //    $("#btnUpdate").button();
    //    $("#btnUpdate").click(function () { GetVisitShop(); return false; });
    //    $("#btnCity").button();
    //    $("#btnCity").click(function () { GetVisitCity(); return false; });
    $("#objStarsCont").mouseover(function (e) {
        $("#StarInfoCont").show();
    });
    $("#objStarsCont").mouseout(function (e) {
        if ($("#StarInfoCont").css('display') == 'block')
            $("#StarInfoCont").hide();
    });

    $("#NewsLink").mouseover(function (e) {
        $("#PreNews").show();
    });

    $("#NewsLink").mouseout(function (e) {
        if ($("#PreNews").css('display') == 'block')
            $("#PreNews").hide();
    });


    var active = true;
    try{
    $('#accordion').on('show.bs.collapse', function () {
        if (active) $('#accordion .in').collapse('hide');
    });
    }
    catch (err){
    }

});
//---------------------------------------------------------------
function GetMainReport() {
    $("#divMostVisit").html("<img src='images/bigloading.gif' />");
    $("#divVisitToday").html("<img src='images/bigloading.gif' />");
    $("#divVisityesterday").html("<img src='images/bigloading.gif' />");
    $("#divSaleYesterday").html("<img src='images/bigloading.gif' />");

    $.ajax({
        type: "POST",
        async: true,
        cache: false,
        dataType: "json",
        data: { i: 1 },
        url: "postback/pltdGetReportMaster.aspx",
        success: function (data) {

            $("#TodayDate").html(data.TodayDate);
            $("#TodayDate").slideDown("slow");

            $("#divVisitToday").html(data.numVisitToday);
            $("#divVisitToday").slideDown("slow");

            $("#divSaleToday").html(data.numSaleToday);
            $("#divSaleToday").slideDown("slow");

            $("#divVisityesterday").html(data.numVisitYesterday);
            $("#divVisityesterday").slideDown("slow");

            $("#divSaleYesterday").html(data.numSaleYesterday);
            $("#divSaleYesterday").slideDown("slow");
        }
    });
}
//---------------------------------------------------------------
function GetNotication() {
    $.ajax({
        type: "POST",
        async: true,
        cache: false,
        dataType: "json",
        data: { i: 2 },
        url: "postback/pltdGetReportMaster.aspx",
        success: function (data) {
            $("#notif").html(data.result);
        }
    });
}
//---------------------------------------------------------------
function GetNoticationNews() {
    $.ajax({
        type: "POST",
        async: true,
        cache: false,
        dataType: "json",
        data: { i: 3 },
        url: "postback/pltdGetReportMaster.aspx",
        success: function (data) {
            $("#notifNews").html(data.result);
        }
    });
}
//---------------------------------------------------------------
function GetHelp(id) {
    $(".BackLoading").fadeIn();
    var strHelp, result;
    switch (id) {
        case '1':
            //Default
            strHelp = 'ستاره بندی :<br />جهت اختصاص امکانات ویژه به وبمسترهای فعال و تشویق وب مسترهایی که به طور مداوم در حال پیشرفت هستند ، درجه بندی همکاران و اختصاص ستاره بر اساس میزان فروش هر یک از اعضا  با شرایط زیر صورت می پذیرد :<br /> بی ستاره &gt;&gt; در ماه اخیر هیچ سفارش سالمی نداشته است . <br />1 ستاره &gt;&gt; در ماه اخیر بین 1 تا 14 سفارش سالم داشته است .<br /> 2 ستاره &gt;&gt; در ماه اخیر بین 15 تا 29 سفارش سالم داشته است .<br /> 3 ستاره &gt;&gt; در ماه اخیر بین 30 تا 49 سفارش سالم داشته است .<br /> 4 ستاره &gt;&gt; در ماه اخیر بین 50 تا 119 سفارش سالم داشته است.<br /> 5 ستاره &gt;&gt; در ماه اخیر بین 120 تا 299 سفارش سالم داشته است .<br /> 6 ستاره &gt;&gt; در ماه اخیر بین 300 تا 599 سفارش سالم داشته است . <br /> 7ستاره &gt;&gt; در ماه اخیر بین 600 تا 1999 سفارش سالم داشته است . <br /> نقره ای &gt;&gt; در ماه اخیر بین 2000 تا 3999سفارش سالم داشته است . <br /> طلایی &gt;&gt; در ماه اخیر بالاتر از 4000 سفارش سالم داشته است . <br /> سفارش سالم به سفارشاتی گفته می شود که دارای یکی از حالتهای زیر باشد : <br />آماده به ارسال ، آماده به بسته بندی ، در حال ارسال ، در حال ارسال پیکی ،توزیع  گردید و توزیع گردید پیکی <br />';
            break;
        case '2':
            //Default
            strHelp = 'محصولاتی که در روز جاری بیشترین فروش را در کل سیستم و توسط تمام اعضا داشته اند. به این ترتیب که فروش کلی سیستم در روز جاری بررسی شده و محصولات به ترتیب میزان فروش مرتب سازی شده اند.';
            break;
        case '3':
            //Default
            strHelp = 'در این نمودار میزان بازدید با کد اختصاصی شما از تمام صفحات فروشگاههای پایگان اعم از صفحه نخست، صفحه توضیحات محصول، لینک خرید و غیره به همراه میزان فروش مربوط به همان روز نمایش داده می شود. با کمک این نمودار می توانید روند افزایشی یا کاهشی بازدید و فروش خود را مشاهده نموده و بازدهی فروش خود را بالاتر ببرید. لازم به ذکر است که متوسط میزان نسبت بازدید به فروش محصولات بین مینیمم هر 70 بازدید 1 خرید و ماکزیمم هر 150 بازدید 1 خرید می باشد.';
            break;
        case '4':
            //Default
            strHelp = 'در این بخش گزارش درصدی کل سفارشات ثبتی شما از آغاز فعالیت تا روز جاری نمایش داده می شود. این اطلاعات کمک زیادی به افزایش بازدهی تبلیغات شما خواهد کرد';
            break;
        case '5':
            //Product
            strHelp = 'این بخش شامل کلیه محصولات موجود در فروشگاهها بوده که امکان جستجوی یک محصول خاص بر اساس نام یا کد آن، در این قسمت وجود دارد. همچنین امکان نمایش محصولات در یک دسته خاص و یا با ترتیب نمایش خاص از جمله پر فروشترین ، جدیدترین و ... در این بخش وجود دارد. در ستون درصد پورسانت میزان درصدی که از مبلغ فروش هر محصول به عنوان پورسانت به شما تعلق می گیرد درج شده است. ضریب افزایشی برای وبمسترهای 5 ستاره به بالا در درصد پورسانت هر محصول ضرب می شود که این افزایش درآمد در قسمت \"درآمد شما\" قابل ملاحظه می باشد. همانطور که مطلع هستید، محصولات بر اساس دسته بندی خود در دو دسته 40 و 25 درصدی قرار دارند و پورسانت محصولاتی که در دسته سی و دی وی دی بوده 34 درصد می باشد.امکان دیگری که وجود دارد معرفی بنرهای پیشنهادی شما برای هر محصول است. شما می توانید با کلیک بر روی ستون آخر این بخش به قسمت ثبت بنر پیشنهادی وارد شده و URL بنر پیشنهادی خود را در این بخش ثبت نمایید . در تعداد بنرهای پیشنهادی هیچ محدودیتی وجود ندارد .';
            break;
        case '6':
            //News
            strHelp = 'در این بخش اخبار و اتفاقات مهم سیستم به اطلاع همکاران گرامی می رسد. شما میتوانید در مورد هر یک از خبرهای مندرج در این بخش نظرات خود را اعلام بفرمایید. نظرات شما بعد از تایید توسط کارشناسان ما به نمایش در خواهد آمد. لطفا در بخش نظرات از بیان مشکلات موجود در سیستم یا اختلالات پیش آمده و یا درخواست کمک در جهت افزایش فروش خودداری بفرمایید و این موضوعات را از طریق ایمیل پشتیبانی و یا تلفنهای بخش پشتیبانی با کارشناسان ما در میان بگذارید .';
            break;
        case '7':
            //GoodSuggest
            strHelp = 'لیست محصولات پیشنهادی برای هر یک از وبمسترها منحصر به فرد بوده و صرفا بر اساس عملکرد و میزان بازدهی تبلیغات شما طراحی شده است . در اینجا نسبت میزان بازدید از هر محصول با کد اختصاصی شما به میزان فروش آن محصول مورد توجه قرار گرفته و نسبتی برای آن بدست آمده است که این نسبت در ستون آخر به نام درصد فروش درج شده است . هر چقدر نسبت تعداد فروش به تعداد بازدید یک محصول بالاتر باشد به این معناست که آن محصول اولویت بالاتری جهت تبلیغ داشته و بهتر است که تبلیغات بیشتری را بر روی این محصول متمرکز نمایید . مراجعه به این بخش می تواند کمک شایانی در افزایش بهره وری تبلیغات و فروش شما داشته باشد .';
            break;
        case '8':
            //OrderGood
            strHelp = 'در این بخش محصولات فروخته شده توسط شما در بازه های زمانی مورد نظرتان به همراه مبلغ و پورسانت آنها نمایش داده می شود . توجه داشته باشید که وضعیت محصولات فروخته شده در حالت پیش فرض بر روی همه وضعیتها  قرار دارد و شما می توانید محصولات فروخته شده در پنل خود را در وضعیتهای گوناگون و در بازه زمانی دلخواه خود مشاهده نمایید. همچنین امکان جستجوی یک کالای خاص بر اساس نام محصول نیز در این قسمت وجود دارد.';
            break;
        case '9':
            //SubSellerGood
            strHelp = 'در این بخش محصولات فروخته شده توسط زیر مجموعه های شما در بازه های زمانی مورد نظرتان به همراه مبلغ و پورسانت آن محصول نمایش داده می شود . توجه داشته باشید که وضعیت محصولات فروخته شده در حالت پیش فرض بر روی همه وضعیتها  قرار دارد و شما می توانید محصولات فروخته شده زیر مجموعه های خود را در وضعیتهای گوناگون و در بازه زمانی دلخواه مشاهده نمایید . همچنین امکان جستجوی یک کالای خاص بر اساس نام محصول نیز در این قسمت وجود دارد. همچنین امکان مشاهده محصولات تک تک زیر مجموعه های به صورت جداگانه نیز مهیا می باشد.';
            break;
        case '10':
            //GoodSaleOff
            strHelp = 'محصولات مختلفی در این بخش به مدت محدود و تا پایان یافتن موجودی آنها در انبار ، به حراج گذاشته می شوند . توجه داشته باشید که تعداد این محصولات محدود بوده و همکارانی از سود قابل توجه این محصولات بهره مند می گردند که زودتر به فروش آنها اقدام نمایند . حراج محصولات در سه حالت مختلف صورت می گیرد که عبارتند از : افزایش درصد پورسانت : در این قسمت محصولاتی قرار دارند که درصد پورسانت آنها بالاتر از 40 درصد می باشد و ممکن است محصولاتی با 70 یا حتی 80 درصد پورسانت نیز در این بخش وجود داشته باشد . کاهش قیمت محصول : قیمت محصولات موجود در این قسمت از حراجی به میزان قابل توجهی کاهش یافته که این امر باعث فروش بیشتر این محصولات خواهد شد . افزایش درصد – کاهش قیمت : محصولات موجود در این بخش از حراجی ، هم پورسانت بالاتری دارند و هم قیمت آنها کاهش یافته است . طبیعتا محصولات این قسمت طرفداران بسیاری داشته و برای بهره مندی از سود آنها، پیشنهاد می شود که هر روز به این قسمت مراجعه نمایید.';
            break;
        case '11':
            //incom
            strHelp = 'در این قسمت میزان درآمد شما تا این لحظه به تفکیک حالتهای مختلف نمایش داده می شود . این گزارش بر اساس تعداد سفارشات ثبت شده است و همانطور که مطلع هستید، 80 درصد سفارشات در حال ارسال و 100 درصد سفارشات  توزیع گردید و در حال ارسال پیکی و  توزیع گردید پیکی  در محاسبه درآمد شما منظور می گردد . در صورتی که سفارشات در حال ارسال به حالت توزیع گردید درآیند 20 درصد باقی مانده نیزدر محاسبات درآمد شما منظور خواهد شد و در صورتی که این سفارشات برگشت بخورند مبلغی که به عنوان پورسانت در درآمد شما لحاظ شده بود ، کسر خواهد شد . به دلیل همین برگشت خوردن سفارشات است که گاها میزان درآمد شما در این جدول کاهش می یابد . در این قسمت درآمد شما از ابتدای ثبت نام تا به امروز محاسبه شده و جمع مبالغ واریزی به حساب شما از آن کسر می شود تا میزان طلب شما تا امروز بدست آید . درآمد ناشی از فعالیت زیر مجموعه های شما نیز در این قسمت درج شده و به درآمد کلی شما افزوده می شود . واریز درآمدها دو مرتبه در ماه و برای تمام همکارانی که میزان درآمد آنها بیشتر از 10000 تومان بوده و حداقل یک سفارش توزیع شده در ماه اخیر داشته باشند ، صورت می گیرد.';
            break;
        case '12':
            //TargetVisitSite
            strHelp = 'جزئیات بازدید از هریک از صفحات فروشگاه های سیستم با کد اختصاصی شما در این بخش قابل مشاهده می باشد . به این ترتیب شما قادر خواهید بود تا میزان بازدید از هریک از محصولات را به راحتی مدیریت کنید و با بررسی دقیق بازدهی عملکرد خود را افزایش دهید.';
            break;
        case '13':
            //SubReseller
            strHelp = 'گزارش سفارشات ثبت شده توسط زیر مجموعه های شما در بازه زمانی دلخواه و وضعیتهای مختلف در این قسمت قابل مشاهده است . همچنین امکان مشاهده سفارشات ثبتی تک تک زیر مجموعه ها به صورت جداگانه نیز در اینجا وجود دارد .';
            break;
        case '14':
            //AmarBazdid
            strHelp = ' صفحه نخست : منظور بازدید از صفحه اصلی یا Home page  شاپلاگ و فروشگاه شما می باشد.</br>  صفحه توضیحات : بازدید صفحه مربوط به توضیحات و جزئیات هر محصول از شاپلاگ یا فروشگاه شما منظور شده است.</br>صفحه خرید : آمار بازدید از صفحه مربوط به انجام خرید می باشد این آمار بدین معنی است که چند بار بازدید کننده وارد صفحه خرید محصولات شاپلاگ یا فروشگاه شما شده است در واقع میزان تمایل به خرید بازدید کننده شما را نمایش می دهد.</br> صفحه پاپ آپ:این آمار ویژه همکارانی است که از تبلیغات پاپ آپ استفاده می کنند و این آمار بانگر بازدید از صفحات پاپ آپ و ورودی این صفحات می باشد.</br><b> * قابل ذکر است که آمار نمایش داده شده براساس Hit  ورودی میباشد.</b><br/> منظور از هر hit در واقع هر یک مرتبه بازدید می باشد یعنی ممکن است با یک ip چندین hit ثبت شود.';
            break;
        case '15':
            //SearchOrder
            strHelp = 'در این قسمت امکان جستجوی سفارش بر اساس وضعیت ، نام خریدار، شناسه سفارش در بازه زمانی مورد نظر برای شما ایجاد شده است.';
            break;
        case '16':
            //ReSendOrder
            strHelp = 'در قسمت سفارش های لغو شده امکانی به نام ارسال مجدد وجود دارد که با استفاده از آن می توانید سفارشات لغوی که به اشتباه لغو شده اند را به حالت ارسال مجدد در بیاورید تا بررسی دوباره بر روی آنها صورت گیرد. در این قسمت گزارش دقیق از سفارشات ارسال مجدد در اختیار شما قرار می گیرد.';
            break;
        case '17':
            //city
            strHelp = 'با قرار گرفتن بر روی هر قسمت از دایره ی آماری، آمار هر شهر را مشاهده می کنید.آماری که مشاهده می نمایید آمار 10 شهر پر بازدید فروشگاه ها با کد شما می باشد.</br><b> * قابل ذکر است که آمار نمایش داده شده براساس Hit ورودی میباشد.</b></br>منظور از هر hit در واقع هر یک مرتبه بازدید می باشد یعنی ممکن است با یک ip چندین hit ثبت شود.';
            break;
        case '18':
            //ReferVisitSite
            strHelp = 'در این قسمت لینک تمام صفحاتی که از آنها به یکی از صفحات فروشگاه های سیستم با کد اختصاصی شما لینک داده اند مشخص شده است . با استفاده از این امکان جدید می توانید میزان ورودی هر یک از مکانهایی که در آنها تبلیغ کرده اید را مشاهد کرده و راندمان تبلیغ در مکانهای مختلف را با یکدیگر مقایسه کنید .';
            break;
        case '19':
            //ReasonCancelOrder
            strHelp = 'در این بخش تمام سفارشاتی که به دلایل مختلف لغو شده اند نمایش داده می شود.همانطور که می دانید سفارشات در ابتدا به حالت &quot;تحت بررسی&quot; بوده و سپس جهت بررسی دقیقتر و جداسازی سفارشات سالم و ناسالم از یکدیگر، در حالت &quot; بررسی نهایی &quot; قرار می گیرند.</br/>در این حالت سفارشات نا سالم به دلایل مختلفی لغو می شوند که این دلایل عبارتند از:<br/>&nbsp;تلفنی: برخی مواقع در هنگام بررسی سفارشات، مواردی مشاهده می شود که با بررسی ظاهری آنها نمیتوان به سالم و نا سالم بودن آنها پی برد و نیاز به تماس تلفنی با خریدار جهت مشخص شدن این موضوع دارد. طبعا در طی این تماسهای تلفنی تعدادی از این سفارشات لغو می شوند .<br/>ادغامی:در صورتی که یک خریدار جهت خرید دو محصول متفاوت دو سفارش مجزا ثبت کرده باشد،این دو محصول در یک سفارش ادغام شده تا خریدار مجبور به پرداخت دو هزینه ارسال بابت سفارشات خود نباشد.در این حالت یکی از سفارشات باید لغو شوندکه دلیل این نوع از لغو سفارش را ادغامی می نامیم.<br/>&nbsp;پیغامی:در قسمت پیغام بعضی از سفارشات مطالبی نوشته شده است که منجر به لغو آن سفارش می شود. برای مثال در بخش پیغام سفارش محصولی مانند تی شرت نوشته می شود که &quot; در صورت داشتن سایز خیلی بزرگ مایل به خرید تیشرت هستم &quot;<br/>&nbsp;آدرس اشتباه:سفارشات زیادی وجود دارند که در آنها نام،تلفن و یا آدرس خریدار نادرست می باشد.این دسته از سفارشات نا سالم با دقت به ظاهر آنها به سادگی قابل تشخیص هستند.<br/>&nbsp;تکراری:گاهی ممکن است که دو سفارش با مشخصات کاملا مشابه دو مرتبه در سیستم ثبت شده باشد که در این حالت یکی از آنها لغو خواهد شد<br/> تستی:گاهی کارشناسان پایگان و یا وبمسترهای عضو جهت تست بخشهای مختلف سیستم اقدام به ثبت سفارشات تستی می نمایند که ناسالم بودن این سفارشات نیز از ظاهر آنها به سادگی قابل تشخیص می باشد .<br/>&nbsp;سایر موارد : ...';
            break;
        case '20':
            //PrevisionOrder
            strHelp = 'بر اساس میزان فروش شما در بازه های زمانی 10 روزه ، یک ماهه و سه ماهه ، فروش شما در آینده پیش بینی شده و نمودار آن برای شما ترسیم می شود . امکانی جدید و بی نظیر که کمک بسیار زیادی به افزایش راندمان شما خواهد کرد .';
            break;
        case '22':
            //Order
            strHelp = 'گزارش سفارشات ثبت شده توسط شما و با کد اختصاصی شما در بازه های زمانی دلخواه و در وضعیت های مختلف در این بخش قابل مشاهده می باشد. دقت داشته باشید که این گزارش شامل تعداد سفارشات ثبت شده است و نه تعداد محصولات فروخته شده و طبیعتا یک سفارش می تواند شامل بیش از یک محصول باشد که بر این اساس ممکن است تعداد محصولات فروخته شده از تعداد سفارشات ثبت شده بیشتر باشد .';
            break;
        case '23':
            //DeportOrder
            strHelp = 'گزارشی از سفارشاتی که توزیع شده و به دست مشتری رسیده است ولی به دلایل مختلف از طرف مشتری برگشت داده می شود و مبلغ آن به حساب مشتری واریز می گردد. طبیعتاً پورسانت این سفارش توزیع شده قبلا به حساب وب مستر واریز شده است که حالا با برگشت سفارش این مبلغ پورسانت از درآمد حال حاضر وبمستر کسر خواهد شد.';
            break;
        case '24':
            //CustomCode
            strHelp = 'در این بخش کد های تبلیغاتی آماده در 4 حالت مختلف برای 15 محصول پرفروش در کل سیستم، قرار دارد. با قرار دادن این کدهای تبلیغاتی در وب سایت یا وبلاگ خود و با هر بار رفرش شدن سایت یکی از 15 بنر تبلیغاتی به صورت تصادفی و با کد اختصاصی شما نمایش داده می شود.';
            break;
        case '25':
            //attraction
            strHelp = 'جهت جذب زیرمجموعه فقط کافیست کد لینک مورد نظر را کپی کرده و در مکان دلخواه از سایت قرار دهید.';
            break;
    }
    $("#helpText").html(strHelp);
    $("#help").dialog({
        autoOpen: false,
        resizable: false,
        title: "<img src='Images/info_button.png' alt='راهنما'/>",
        width: 700,
        dialogClass: 'RightToLeftText',
        buttons: {
            "انصراف": function () {
                $(this).dialog("close");
                $(".BackLoading").fadeOut();
            }
        }
    });
    $("#help").dialog("open");
}
//---------------------------------------------------------------
function GetVisitShop() {
    $('#AmarVisitShop').html("<img src='images/bigloading.gif' />");
    var Data = [];
    var Category = [];
    var i = 0;
    $.ajax({
        type: "POST",
        async: true,
        cache: false,
        dataType: "json",
        data: { i: 4 },
        url: "postback/pltdGetReportMaster.aspx",
        success: function (data) {
            $.each(data, function (index) {
                Category.push(this['strPageTypeTitle']);
                Data.push({ y: this['cnt'] * 1, color: Highcharts.getOptions().colors[i] });
                i = i + 1;
            });
            var colors = Highcharts.getOptions().colors, categories = Category, name = ' ', data = Data;
            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'AmarVisitShop',
                    defaultSeriesType: 'bar'
                },
                title: {
                    text: ''
                },
                xAxis: {
                    categories: categories
                },
                legend: {
                    enabled: false
                },
                yAxis: {
                    labels: {
                        enabled: false
                    },
                    gridLineColor: 'white',
                    title: {
                        text: ' '
                    }
                },
                plotOptions: {
                    bar: {
                        dataLabels: {
                            enabled: true
                        }
                    }
                },
                tooltip: {
                    enabled: false
                },
                series: [{
                    name: name,
                    data: Data,
                    color: 'gray'
                }]
            });
        }
    });
}
//---------------------------------------------------------------
function GetVisitCity() {
    $('#DivCity').html("<img src='images/bigloading.gif' />");
    var Data = [];
    var Category = [];
    $.ajax({
        type: "POST",
        async: true,
        cache: false,
        dataType: "json",
        data: { i: 5 },
        url: "postback/pltdGetReportMaster.aspx",
        success: function (data) {
            $.each(data, function (index) {
                Data.push({ name: this['city'], y: this['cnt'] * 1 });
            });

            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'DivCity',
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false
                },
                legend: {
                    enabled: false
                },
                title: {
                    text: ''
                },
                tooltip:
                {
                    formatter: function () {

                        return this.point.name + ': ' + this.y;
                    },
                    pointFormat: '{series.name}: <b>{point.percentage}%</b>',
                    percentageDecimals: 1
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: false
                        },
                        showInLegend: true
                    }
                },
                series: [{
                    type: 'pie',
                    name: ' ',
                    data: Data
                }]
            });
        }
    });
}
//---------------------------------------------------------------

function SelectText(objTextArea) {
    objTextArea.focus();
    objTextArea.select();
}

function GetPager(AllRecordCount, ItemPerPage,  CurrentPage) {

    var PageCount;
    if (AllRecordCount % ItemPerPage == 0)
        PageCount = AllRecordCount / ItemPerPage;
    else
        PageCount = parseInt(AllRecordCount / ItemPerPage) + 1;
    var PreWinSize = 4;
    var NextWinSize = 4;
    var StartIndex = 1;
    var EndIndex = 1;
    var PrevPage = CurrentPage - 1;
    var NextPage = CurrentPage + 1;

    if (CurrentPage > PreWinSize)
        StartIndex = CurrentPage - PreWinSize;
    if ((CurrentPage + NextWinSize) <= PageCount)
        EndIndex = CurrentPage + NextWinSize;
    else
        EndIndex = PageCount;

    var footerPager = "<div ><ul class=\"pagination\">";
    if (CurrentPage > 2)
        footerPager += "<li><span onclick=\"GoPage(1 {ExParams})\" >&raquo;&raquo;</span></li>";
    if (CurrentPage > 1)
        footerPager += "<li><span onclick=\"GoPage(" + PrevPage + "{ExParams})\" >&raquo;</span></li>";
    for (var i = StartIndex; i <= EndIndex; i++) {
        if (CurrentPage == i)
            footerPager += "<li class=\"active\">";
        else
            footerPager += "<li>";
        footerPager += "<span onclick=\"GoPage(" + i + " {ExParams})\" >" + i + "</span>";
        footerPager += "</li>";
    }
    if (CurrentPage < PageCount)
        footerPager += "<li><span onclick=\"GoPage(" + NextPage + "{ExParams})\" >&laquo;</span></li>";
    if (CurrentPage < PageCount)
        footerPager += "<li><span onclick=\"GoPage(" + PageCount + " {ExParams})\" >&laquo;&laquo;</span></li>";
    footerPager += "</ul></div>";
    return footerPager;

}

function ArrayContains(arr, item) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == item)
            return true;
    }
    return false;
}

function loadBack2Up() {
    var pxShow = 300;
    var fadeInTime = 1000;
    var fadeOutTime = 1000;
    var scrollSpeed = 1000;
    $(window).scroll(function () {
        if ($(window).scrollTop() >= pxShow) {
            $("#backtotop").fadeIn(fadeInTime);
        } else {
            $("#backtotop").fadeOut(fadeOutTime);
        }
    });

    $('#backtotop a').click(function () {
        $('html, body').animate({ scrollTop: 0 }, scrollSpeed);
        return false;
    });
}

$(function () {
    $(document).ready(function () {
        loadBack2Up();
    });
});

var active = true;

function LoadBox(BoxID) {
    $("#DynamicBox").html($("#" + BoxID).html());
    $("#DynamicBoxCont").slideDown("slow");
    $("#DynamicBoxCont").slideDown("slow");
    
}

