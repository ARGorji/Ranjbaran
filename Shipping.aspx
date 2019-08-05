<%@ Page Language="C#" AutoEventWireup="true" Title="سفارش کالا" EnableEventValidation="false"
    MasterPageFile="~/MainMaster.Master" CodeBehind="Shipping.aspx.cs" Inherits="Ranjbaran.Shipping" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <script type="text/javascript">
        function ldMenu(Indx, CityCodeID) {
            with (document.getElementById(CityCodeID)) {
                options.length = 0;
                if (Indx == 0) {
                    options[0] = new Option("لطفا استان خود را انتخاب کنيد", "");
                }
                if (Indx == 1) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("تبريز", "410001");
                    options[2] = new Option("كندوان", "410002");
                    options[3] = new Option("بندر شرفخانه", "410003");
                    options[4] = new Option("مراغه", "410004");
                    options[5] = new Option("ميانه", "410005");
                    options[6] = new Option("شبستر", "410006");
                    options[7] = new Option("مرند", "410007");
                    options[8] = new Option("جلفا", "410008");
                    options[9] = new Option("سراب", "410009");
                    options[10] = new Option("هاديشهر", "410010");
                    options[11] = new Option("بناب", "410011");
                    options[12] = new Option("كليبر", "410012");
                    options[13] = new Option("تسوج", "410013");
                    options[14] = new Option("اهر", "410014");
                    options[15] = new Option("هريس", "410015");
                    options[16] = new Option("عجبشير", "410016");
                    options[17] = new Option("هشترود", "410017");
                    options[18] = new Option("ملكان", "410018");
                    options[19] = new Option("بستان آباد", "410019");
                    options[20] = new Option("ورزقان", "410020");
                    options[21] = new Option("اسكو", "410021");
                    options[22] = new Option("آذر شهر", "410022");
                    options[23] = new Option("قره آغاج", "410023");
                    options[24] = new Option("ممقان", "410024");
                }

                if (Indx == 2) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("اروميه", "440001");
                    options[2] = new Option("نقده", "440002");
                    options[3] = new Option("ماكو", "440003");
                    options[4] = new Option("تكاب", "440004");
                    options[5] = new Option("خوي", "440005");
                    options[6] = new Option("مهاباد", "440006");
                    options[7] = new Option("سر دشت", "440007");
                    options[8] = new Option("چالدران", "440008");
                    options[9] = new Option("بوكان", "440009");
                    options[10] = new Option("مياندوآب", "440010");
                    options[11] = new Option("سلماس", "440011");
                    options[12] = new Option("شاهين دژ", "440012");
                    options[13] = new Option("پيرانشهر", "440013");
                    options[14] = new Option("سيه چشمه", "440014");
                    options[15] = new Option("اشنويه", "440015");
                    options[16] = new Option("چایپاره", "440016");
                    options[17] = new Option("پلدشت", "440017");
                    options[18] = new Option("شوط", "440018");
                }
                if (Indx == 3) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("اردبيل", "450001");
                    options[2] = new Option("سرعين", "450002");
                    options[3] = new Option("بيله سوار", "450003");
                    options[4] = new Option("پارس آباد", "450004");
                    options[5] = new Option("خلخال", "450005");
                    options[6] = new Option("مشگين شهر", "450006");
                    options[7] = new Option("مغان", "450007");
                    options[8] = new Option("نمين", "450008");
                    options[9] = new Option("نير", "450009");
                    options[10] = new Option("كوثر", "450010");
                    options[11] = new Option("كيوي", "450011");
                    options[12] = new Option("گرمي", "450012");

                }
                if (Indx == 4) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("اصفهان", "310001");
                    options[2] = new Option("فريدن", "3100012");
                    options[3] = new Option("فريدون شهر", "3100013");
                    options[4] = new Option("فلاورجان", "3100014");
                    options[5] = new Option("گلپايگان", "3100015");
                    options[6] = new Option("دهاقان", "3100016");
                    options[7] = new Option("نطنز", "3100017");
                    options[8] = new Option("نايين", "3100018");
                    options[9] = new Option("تيران", "3100019");
                    options[10] = new Option("كاشان", "310002");
                    options[11] = new Option("فولاد شهر", "3100020");
                    options[12] = new Option("اردستان", "3100021");
                    options[13] = new Option("سميرم", "3100022");
                    options[14] = new Option("درچه", "3100023");
                    options[15] = new Option("کوهپایه", "3100024");
                    options[16] = new Option("مباركه", "310003");
                    options[17] = new Option("شهرضا", "310004");
                    options[18] = new Option("خميني شهر", "310005");
                    options[19] = new Option("شاهين شهر", "310006");
                    options[20] = new Option("نجف آباد", "310007");
                    options[21] = new Option("دولت آباد", "310008");
                    options[22] = new Option("زرين شهر", "310009");
                    options[23] = new Option("آران و بيدگل", "310010");
                    options[24] = new Option("باغ بهادران", "310011");
                    options[25] = new Option("خوانسار", "310013");
                    options[26] = new Option("مهردشت", "310014");
                    options[27] = new Option("علويجه", "310015");
                    options[28] = new Option("عسگران", "310016");
                    options[29] = new Option("نهضت آباد", "310017");
                    options[30] = new Option("حاجي آباد", "310018");
                    options[31] = new Option("تودشک", "310019");
                    options[32] = new Option("ورزنه", "310020");
                }
                if (Indx == 5) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("ايلام", "840001");
                    options[2] = new Option("مهران", "840002");
                    options[3] = new Option("دهلران", "840003");
                    options[4] = new Option("آبدانان", "840004");
                    options[5] = new Option("شيروان چرداول", "840005");
                    options[6] = new Option("دره شهر", "840006");
                    options[7] = new Option("ايوان", "840007");
                    options[8] = new Option("سرابله", "840008");
                }
                if (Indx == 6) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("بوشهر", "770001");
                    options[2] = new Option("تنگستان", "770002");
                    options[3] = new Option("دشتستان", "770003");
                    options[4] = new Option("دير", "770004");
                    options[5] = new Option("ديلم", "770005");
                    options[6] = new Option("كنگان", "770006");
                    options[7] = new Option("گناوه", "770007");
                    options[8] = new Option("ريشهر", "770008");
                    options[9] = new Option("دشتي", "770009");
                    options[10] = new Option("خورموج", "770010");
                    options[11] = new Option("اهرم", "770011");
                    options[12] = new Option("برازجان", "770012");
                    options[13] = new Option("خارك", "770013");
                    options[14] = new Option("جم", "770014");
                    options[15] = new Option("کاکی", "770015");
                    options[16] = new Option("عسلویه", "770016");
                    options[17] = new Option("بردخون", "770017");
                }
                if (Indx == 7) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("تهران", "210001");
                    options[2] = new Option("ورامين", "210005");
                    options[3] = new Option("فيروزكوه", "210007");
                    options[4] = new Option("ري", "210008");
                    options[5] = new Option("دماوند", "210009");
                    options[6] = new Option("اسلامشهر", "210010");
                    options[7] = new Option("رودهن", "210012");
                    options[8] = new Option("لواسان", "210013");
                    options[9] = new Option("بومهن", "210014");
                    options[10] = new Option("تجريش", "210015");
                    options[11] = new Option("فشم", "210016");
                    options[12] = new Option("كهريزك", "210017");
                    options[13] = new Option("پاكدشت", "210018");
                    options[14] = new Option("چهاردانگه", "210025");
                    options[15] = new Option("شريف آباد", "210027");
                    options[16] = new Option("قرچك", "210028");
                    options[17] = new Option("باقرشهر", "210029");
                }
                if (Indx == 8) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("شهركرد", "380001");
                    options[2] = new Option("فارسان", "380002");
                    options[3] = new Option("بروجن", "380003");
                    options[4] = new Option("چلگرد", "380004");
                    options[5] = new Option("اردل", "380005");
                    options[6] = new Option("لردگان", "380006");
                    options[7] = new Option("سامان", "380007");
                }
                if (Indx == 9) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("قائن", "510007");
                    options[2] = new Option("فردوس", "510009");
                    options[3] = new Option("بيرجند", "560001");
                    options[4] = new Option("نهبندان", "560002");
                    options[5] = new Option("سربيشه", "560003");
                    options[6] = new Option("طبس مسینا", "560004");
                    options[7] = new Option("قهستان", "560005");
                    options[8] = new Option("درمیان", "560006");
                }
                if (Indx == 10) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("مشهد", "510001");
                    options[2] = new Option("نيشابور", "510002");
                    options[3] = new Option("سبزوار", "510003");
                    options[4] = new Option("كاشمر", "510005");
                    options[5] = new Option("گناباد", "510006");
                    options[6] = new Option("طبس", "510010");
                    options[7] = new Option("تربت حيدريه", "510011");
                    options[8] = new Option("خواف", "510012");
                    options[9] = new Option("تربت جام", "510013");
                    options[10] = new Option("تايباد", "510014");
                    options[11] = new Option("قوچان", "510015");
                    options[12] = new Option("سرخس", "510018");
                    options[13] = new Option("بردسكن", "510021");
                    options[14] = new Option("فريمان", "510022");
                    options[15] = new Option("چناران", "510023");
                    options[16] = new Option("درگز", "510025");
                    options[17] = new Option("كلات", "510027");
                    options[18] = new Option("طرقبه", "510028");
                    options[19] = new Option("سر ولایت", "510029");
                }
                if (Indx == 11) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("بجنورد", "580001");
                    options[2] = new Option("اسفراين", "580002");
                    options[3] = new Option("جاجرم", "580003");
                    options[4] = new Option("شيروان", "580004");
                    options[5] = new Option("آشخانه", "580005");
                    options[6] = new Option("گرمه", "580006");
                    options[7] = new Option("ساروج", "580007");
                }
                if (Indx == 12) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("اهواز", "610001");
                    options[2] = new Option("ايرانشهر", "610002");
                    options[3] = new Option("شوش", "610003");
                    options[4] = new Option("آبادان", "610004");
                    options[5] = new Option("خرمشهر", "610005");
                    options[6] = new Option("مسجد سليمان", "610006");
                    options[7] = new Option("ايذه", "610007");
                    options[8] = new Option("شوشتر", "610008");
                    options[9] = new Option("انديمشك", "610009");
                    options[10] = new Option("سوسنگرد", "610010");
                    options[11] = new Option("هويزه", "610011");
                    options[12] = new Option("دزفول", "610012");
                    options[13] = new Option("شادگان", "610013");
                    options[14] = new Option("بندر ماهشهر", "610014");
                    options[15] = new Option("بندر امام خميني", "610015");
                    options[16] = new Option("اميديه", "610016");
                    options[17] = new Option("بهبهان", "610017");
                    options[18] = new Option("رامهرمز", "610018");
                    options[19] = new Option("باغ ملك", "610019");
                    options[20] = new Option("هنديجان", "610020");
                    options[21] = new Option("لالي", "610021");
                    options[22] = new Option("رامشیر", "610022");
                    options[23] = new Option("حمیدیه", "610023");
                    options[24] = new Option("دغاغله", "610024");
                    options[25] = new Option("ملاثانی", "610025");
                    options[26] = new Option("شادگان", "610026");
                    options[27] = new Option("ویسی", "610027");
                }
                if (Indx == 13) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("زنجان", "240001");
                    options[2] = new Option("ابهر", "240002");
                    options[3] = new Option("خدابنده", "240003");
                    options[4] = new Option("كارم", "240004");
                    options[5] = new Option("ماهنشان", "240005");
                    options[6] = new Option("خرمدره", "240006");
                    options[7] = new Option("ايجرود", "240007");
                    options[8] = new Option("زرين آباد", "240008");
                    options[9] = new Option("آب بر", "240009");
                    options[10] = new Option("قيدار", "240010");
                }
                if (Indx == 14) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("سمنان", "230001");
                    options[2] = new Option("شاهرود", "230002");
                    options[3] = new Option("گرمسار", "230003");
                    options[4] = new Option("ايوانكي", "230004");
                    options[5] = new Option("دامغان", "230005");
                    options[6] = new Option("بسطام", "230006");
                }
                if (Indx == 15) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("زاهدان", "540001");
                    options[2] = new Option("چابهار", "540002");
                    options[3] = new Option("خاش", "540003");
                    options[4] = new Option("سراوان", "540004");
                    options[5] = new Option("زابل", "540005");
                    options[6] = new Option("سرباز", "540006");
                    options[7] = new Option("نيكشهر", "540007");
                    options[8] = new Option("ايرانشهر", "540008");
                    options[9] = new Option("راسك", "540009");
                    options[10] = new Option("ميرجاوه", "540010");
                }
                if (Indx == 16) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("شيراز", "710001");
                    options[2] = new Option("اقليد", "710002");
                    options[3] = new Option("داراب", "710003");
                    options[4] = new Option("فسا", "710004");
                    options[5] = new Option("مرودشت", "710005");
                    options[6] = new Option("خرم بيد", "710006");
                    options[7] = new Option("آباده", "710007");
                    options[8] = new Option("كازرون", "710008");
                    options[9] = new Option("ممسني", "710009");
                    options[10] = new Option("سپيدان", "710010");
                    options[11] = new Option("لار", "710011");
                    options[12] = new Option("فيروز آباد", "710012");
                    options[13] = new Option("جهرم", "710013");
                    options[14] = new Option("ني ريز", "710014");
                    options[15] = new Option("استهبان", "710015");
                    options[16] = new Option("لامرد", "710016");
                    options[17] = new Option("مهر", "710017");
                    options[18] = new Option("حاجي آباد", "710018");
                    options[19] = new Option("نورآباد", "710019");
                    options[20] = new Option("اردكان", "710020");
                    options[21] = new Option("صفاشهر", "710021");
                    options[22] = new Option("ارسنجان", "710022");
                    options[23] = new Option("قيروكارزين", "710023");
                    options[24] = new Option("سوريان", "710024");
                    options[25] = new Option("فراشبند", "710025");
                    options[26] = new Option("سروستان", "710026");
                    options[27] = new Option("ارژن", "710027");
                    options[28] = new Option("گويم", "710028");
                    options[29] = new Option("داريون", "710029");
                    options[30] = new Option("زرقان", "710030");
                    options[31] = new Option("خان زنیان", "710031");
                    options[32] = new Option("کوار", "710032");
                    options[33] = new Option("ده بید", "710033");
                    options[34] = new Option("باب انار/خفر", "710034");
                    options[35] = new Option("بوانات", "710035");
                    options[36] = new Option("خرامه", "710036");
                    options[37] = new Option("خنج", "710037");
                    options[38] = new Option("سیاخ دارنگون", "710038");
                }
                if (Indx == 17) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("قزوين", "280001");
                    options[2] = new Option("تاكستان", "280002");
                    options[3] = new Option("آبيك", "280003");
                    options[4] = new Option("بوئين زهرا", "280004");
                }
                if (Indx == 18) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("قم", "250001");
                }
                if (Indx == 19) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("طالقان", "210002");
                    options[2] = new Option("شهريار", "210004");
                    options[3] = new Option("قدس", "210006");
                    options[4] = new Option("نظرآباد", "210011");
                    options[5] = new Option("رباط كريم", "210019");
                    options[6] = new Option("اشتهارد", "210020");
                    options[7] = new Option("هشتگرد", "210021");
                    options[8] = new Option("كن", "210022");
                    options[9] = new Option("ملارد", "210023");
                    options[10] = new Option("آسارا", "210024");
                    options[11] = new Option("شهرک گلستان", "210026");
                    options[12] = new Option("اندیشه", "210030");
                    options[13] = new Option("كرج", "260001");
                    options[14] = new Option("نظر آباد", "260002");
                    options[15] = new Option("گوهردشت", "260003");
                    options[16] = new Option("ماهدشت", "260004");
                    options[17] = new Option("مشکین دشت", "260005");
                }

                if (Indx == 20) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("سنندج", "870001");
                    options[2] = new Option("ديواندره", "870002");
                    options[3] = new Option("بانه", "870003");
                    options[4] = new Option("بيجار", "870004");
                    options[5] = new Option("سقز", "870005");
                    options[6] = new Option("كامياران", "870006");
                    options[7] = new Option("قروه", "870007");
                    options[8] = new Option("مريوان", "870008");
                    options[9] = new Option("صلوات آباد", "870009");
                    options[10] = new Option("حسن آباد", "870010");
                }
                if (Indx == 21) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("كرمان", "340001");
                    options[2] = new Option("راور", "3400010");
                    options[3] = new Option("بابك", "3400011");
                    options[4] = new Option("انار", "3400012");
                    options[5] = new Option("کوهبنان", "3400013");
                    options[6] = new Option("رفسنجان", "340002");
                    options[7] = new Option("بافت", "340003");
                    options[8] = new Option("سيرجان", "340004");
                    options[9] = new Option("كهنوج", "340005");
                    options[10] = new Option("زرند", "340006");
                    options[11] = new Option("بم", "340007");
                    options[12] = new Option("جيرفت", "340008");
                    options[13] = new Option("بردسير", "340009");
                }
                if (Indx == 22) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("كرمانشاه", "830001");
                    options[2] = new Option("اسلام آباد غرب", "830002");
                    options[3] = new Option("سر پل ذهاب", "830003");
                    options[4] = new Option("كنگاور", "830004");
                    options[5] = new Option("سنقر", "830005");
                    options[6] = new Option("قصر شيرين", "830006");
                    options[7] = new Option("گيلان غرب", "830007");
                    options[8] = new Option("هرسين", "830008");
                    options[9] = new Option("صحنه", "830009");
                    options[10] = new Option("پاوه", "830010");
                    options[11] = new Option("جوانرود", "830011");
                    options[12] = new Option("شاهو", "830012");
                }
                if (Indx == 23) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("ياسوج", "740001");
                    options[2] = new Option("گچساران", "740002");
                    options[3] = new Option("دنا", "740003");
                    options[4] = new Option("دوگنبدان", "740004");
                    options[5] = new Option("سي سخت", "740005");
                    options[6] = new Option("دهدشت", "740006");
                    options[7] = new Option("ليكك", "740007");
                }
                if (Indx == 24) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("گرگان", "170001");
                    options[2] = new Option("آق قلا", "170002");
                    options[3] = new Option("گنبد كاووس", "170003");
                    options[4] = new Option("علي آباد كتول", "170004");
                    options[5] = new Option("مينو دشت", "170005");
                    options[6] = new Option("تركمن", "170006");
                    options[7] = new Option("كردكوي", "170007");
                    options[8] = new Option("بندر گز", "170008");
                    options[9] = new Option("كلاله", "170009");
                    options[10] = new Option("آزاد شهر", "170010");
                    options[11] = new Option("راميان", "170011");
                }
                if (Indx == 25) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("رشت", "130001");
                    options[2] = new Option("منجيل", "130003");
                    options[3] = new Option("لنگرود", "130004");
                    options[4] = new Option("رود سر", "130005");
                    options[5] = new Option("تالش", "130006");
                    options[6] = new Option("آستارا", "130007");
                    options[7] = new Option("ماسوله", "130008");
                    options[8] = new Option("آستانه اشرفيه", "130009");
                    options[9] = new Option("رودبار", "130010");
                    options[10] = new Option("فومن", "130011");
                    options[11] = new Option("صومعه سرا", "130012");
                    options[12] = new Option("بندرانزلي", "130013");
                    options[13] = new Option("كلاچاي", "130014");
                    options[14] = new Option("هشتپر", "130015");
                    options[15] = new Option("رضوان شهر", "130016");
                    options[16] = new Option("ماسال", "130017");
                    options[17] = new Option("شفت", "130018");
                    options[18] = new Option("سياهكل", "130019");
                    options[19] = new Option("املش", "130020");
                    options[20] = new Option("لاهیجان", "130021");
                    options[21] = new Option("خشک بيجار", "130022");
                    options[22] = new Option("خمام", "130023");
                    options[23] = new Option("لشت نشا", "130024");
                    options[24] = new Option("بندر کياشهر", "130025");
                }
                if (Indx == 26) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("خرم آباد", "660001");
                    options[2] = new Option("ماهشهر", "6600010");
                    options[3] = new Option("دزفول", "6600011");
                    options[4] = new Option("بروجرد", "660002");
                    options[5] = new Option("دورود", "660003");
                    options[6] = new Option("اليگودرز", "660004");
                    options[7] = new Option("ازنا", "660005");
                    options[8] = new Option("نور آباد", "660006");
                    options[9] = new Option("كوهدشت", "660007");
                    options[10] = new Option("الشتر", "660008");
                    options[11] = new Option("پلدختر", "660009");
                }
                if (Indx == 27) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("ساري", "150001");
                    options[2] = new Option("آمل", "150002");
                    options[3] = new Option("بابل", "150003");
                    options[4] = new Option("بابلسر", "150004");
                    options[5] = new Option("بهشهر", "150005");
                    options[6] = new Option("تنكابن", "150006");
                    options[7] = new Option("جويبار", "150007");
                    options[8] = new Option("چالوس", "150008");
                    options[9] = new Option("رامسر", "150009");
                    options[10] = new Option("سواد كوه", "150010");
                    options[11] = new Option("قائم شهر", "150011");
                    options[12] = new Option("نكا", "150012");
                    options[13] = new Option("نور", "150013");
                    options[14] = new Option("بلده", "150014");
                    options[15] = new Option("نوشهر", "150015");
                    options[16] = new Option("پل سفيد", "150016");
                    options[17] = new Option("محمود آباد", "150017");
                    options[18] = new Option("فريدون كنار", "150018");
                }
                if (Indx == 28) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("اراك", "860001");
                    options[2] = new Option("آشتيان", "860002");
                    options[3] = new Option("تفرش", "860003");
                    options[4] = new Option("خمين", "860004");
                    options[5] = new Option("دليجان", "860005");
                    options[6] = new Option("ساوه", "860006");
                    options[7] = new Option("سربند", "860007");
                    options[8] = new Option("محلات", "860008");
                    options[9] = new Option("شازند", "860009");
                }
                if (Indx == 29) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("بندرعباس", "760001");
                    options[2] = new Option("قشم", "760002");
                    options[3] = new Option("كيش", "760003");
                    options[4] = new Option("بندر لنگه", "760004");
                    options[5] = new Option("بستك", "760005");
                    options[6] = new Option("حاجي آباد", "760006");
                    options[7] = new Option("دهبارز", "760007");
                    options[8] = new Option("انگهران", "760008");
                    options[9] = new Option("ميناب", "760009");
                    options[10] = new Option("ابوموسي", "760010");
                    options[11] = new Option("بندر جاسك", "760011");
                    options[12] = new Option("تنب بزرگ", "760012");
                    options[13] = new Option("بندر خمیر", "760013");
                    options[14] = new Option("پارسیان", "760014");
                    options[15] = new Option("قشم", "760015");
                }
                if (Indx == 30) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("همدان", "810001");
                    options[2] = new Option("ملاير", "810002");
                    options[3] = new Option("تويسركان", "810003");
                    options[4] = new Option("نهاوند", "810004");
                    options[5] = new Option("كبودر اهنگ", "810005");
                    options[6] = new Option("رزن", "810006");
                    options[7] = new Option("اسدآباد", "810007");
                    options[8] = new Option("بهار", "810008");
                }
                if (Indx == 31) {
                    options[0] = new Option("لطفا شهر را انتخاب کنید", "");
                    options[1] = new Option("يزد", "350001");
                    options[2] = new Option("تفت", "350002");
                    options[3] = new Option("اردكان", "350003");
                    options[4] = new Option("ابركوه", "350004");
                    options[5] = new Option("ميبد", "350005");
                    options[6] = new Option("طبس", "350006");
                    options[7] = new Option("بافق", "350007");
                    options[8] = new Option("مهريز", "350008");
                    options[9] = new Option("اشكذر", "350009");
                    options[10] = new Option("هرات", "350010");
                    options[11] = new Option("خضرآباد", "350011");
                    options[12] = new Option("شاهديه", "350012");
                    options[13] = new Option("حمیدیه شهر", "350013");
                    options[14] = new Option("سید میرزا", "350014");
                    options[15] = new Option("زارچ", "350015");
                }
                document.getElementById(CityCodeID).options[0].selected = true;
            }

        }

 
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnShowAddressForm").button();
            $("#btnShowAddressForm").click(function () { ShowAddressForm(); return false; });

        });

        function ShowAddressForm() {
            $("#divNewAddress").modal('show');
            $("#msgNewAddress").addClass("hide");
            $('#btnSubmitNewAddress').unbind("click");
            $("#btnSubmitNewAddress").click(function () { SubmitNewAddress(); return false; });

        }

        function RemoveAddress(Code) {
            Result = confirm('آیا از حذف این آدرس مطمئن هستید؟ ');
            if (Result) {
                $.ajax({
                    type: "POST",
                    async: true,
                    cache: false,
                    dataType: "json",
                    data: { i: 2, Code: Code },
                    url: "Postback/UserAddresses.aspx",
                    success: function (data) {
                        if (data.success == "1") {
                            $("#divNewAddress").modal('hide');
                            window.location.href = 'Shipping.aspx';
                        }
                        else {
                            $("#msgNewAddress").html(data.result);
                            $("#msgNewAddress").fadeIn("slow");
                            $("#msgNewAddress").slideDown("slow");

                        }
                    },
                    error: function () {
                        alert("اطلاعات به درستی وارد نشده است.");

                    }
                });
            }
        }

        function SubmitNewAddress() {
            NewAddressFullName = $("#txtNewAddressFullName").val();
            NewAddressCellPhone = $("#txtNewAddressCellPhone").val();
            NewAddressTel = $("#txtNewAddressTel").val();
            NewAddressAddress = $("#txtNewAddressAddress").val();
            NewAddressPostalCode = $("#txtNewAddressPostalCode").val();

            NewAddressProvinceCode = $("#ddlNewAddressProvinceCode").val();
            NewAddressCityCode = $("#ddlNewAddressCityCode").val();

            var ErrorMessage = '';
            if (NewAddressFullName == '')
                ErrorMessage = 'لطفا نام گیرنده را وارد کنید'
            else if (NewAddressCellPhone == '' && NewAddressTel == '')
                ErrorMessage = 'لطفا حداقل یک شماره تماس وارد کنید'
            else if (NewAddressAddress == '')
                ErrorMessage = 'لطفا آدرس را وارد کنید'
            else if (NewAddressProvinceCode == '0')
                ErrorMessage = 'لطفا نام استان را وارد کنید'
            else if (NewAddressCityCode == '0')
                ErrorMessage = 'لطفا نام شهر را وارد کنید'
            if (NewAddressCellPhone.length != 11)
                ErrorMessage = 'شماره موبایل معتبر نیست'

            if (ErrorMessage != '') {
                $("#msgNewAddress").html(ErrorMessage);
                $("#msgNewAddress").removeClass("hide");
                $("#msgNewAddress").fadeIn("slow");
                $("#msgNewAddress").slideDown("slow");
                return;
            }

            $.ajax({
                type: "POST",
                async: true,
                cache: false,
                dataType: "json",
                data: { i: 1, NewAddressFullName: NewAddressFullName, NewAddressCellPhone: NewAddressCellPhone, NewAddressTel: NewAddressTel, NewAddressAddress: NewAddressAddress,
                    NewAddressPostalCode: NewAddressPostalCode, NewAddressProvinceCode: NewAddressProvinceCode, NewAddressCityCode: NewAddressCityCode
                },
                url: "Postback/UserAddresses.aspx",
                success: function (data) {
                    if (data.success == "1") {
                        $("#divNewAddress").modal('hide');
                        window.location.href = 'Shipping.aspx';
                    }
                    else {
                        $("#msgNewAddress").html(data.result);
                        $("#msgNewAddress").fadeIn("slow");
                        $("#msgNewAddress").slideDown("slow");

                    }
                },
                error: function () {
                    alert("اطلاعات به درستی وارد نشده است.");

                }
            });
        }

    </script>
    <div id="divNewAddress" class="modal fade" style="display: none">
        <div class="modal-dialog" >
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">
                        افزودن آدرس جدید</h4>
                </div>
                <div class="modal-body">
                    <table align="center" cellpadding="3" cellspacing="0" dir="rtl" class="tbWide">
                        <tr>
                            <td class="form-lbl">
                                نام و نام‌خانوادگی تحویل گیرنده:
                            </td>
                            <td align="right">
                                <input id="txtNewAddressFullName" value="<%=strDefaultFullName %>" class="form-control"
                                    style="width: 180px; font-family: tahoma;" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td class="form-lbl">
                                شماره موبایل ضروری:
                            </td>
                            <td align="right" >
                                <div >
                                    <input id="txtNewAddressCellPhone" class="form-control" 
                                        type="text" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-lbl">
                                شماره تلفن ثابت:
                            </td>
                            <td align="right" >
                                <div >
                                    <input id="txtNewAddressTel" class="form-control" 
                                        type="text" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-lbl">
                                استان/شهر:
                            </td>
                            <td align="right">
                                <table>
                                    <tr>
                                        <td>
                                            <select style="font-family: Tahoma; font-size: 8pt; width: 200px; font-size: 12px;"
                                                onchange="ldMenu(this.selectedIndex, 'ddlNewAddressCityCode');" class="form-control"
                                                id="ddlNewAddressProvinceCode" name="ddlProvinceCode">
                                                <option value="0">لطفا استان خود را انتخاب کنید</option>
                                                <option value="O9">آذربايجان شرقي</option>
                                                <option value="O10">آذربايجان غربي</option>
                                                <option value="O13">اردبيل</option>
                                                <option value="O14">اصفهان</option>
                                                <option value="O11">ايلام</option>
                                                <option value="O15">بوشهر</option>
                                                <option value="O30">تهران</option>
                                                <option value="O3">چهارمحال بختياري</option>
                                                <option value="O19">خراسان جنوبي</option>
                                                <option value="O17">خراسان رضوي</option>
                                                <option value="O18">خراسان شمالي</option>
                                                <option value="O16">خوزستان</option>
                                                <option value="O20">زنجان</option>
                                                <option value="O21">سمنان</option>
                                                <option value="O23">سيستان و بلوچستان</option>
                                                <option value="O1">فارس</option>
                                                <option value="O22">قزوين</option>
                                                <option value="O12">قم</option>
                                                <option value="O31">کرج</option>
                                                <option value="O27">كردستان</option>
                                                <option value="O25">كرمان</option>
                                                <option value="O26">كرمانشاه</option>
                                                <option value="O24">كهكيلويه و بويراحمد</option>
                                                <option value="O8">گلستان</option>
                                                <option value="O7">گيلان</option>
                                                <option value="O28">لرستان</option>
                                                <option value="O29">مازندران</option>
                                                <option value="O2">مركزي</option>
                                                <option value="O5">هرمزگان</option>
                                                <option value="O4">همدان</option>
                                                <option value="O6">يزد</option>
                                            </select>
                                            <select style="font-family: Tahoma; font-size: 8pt; width: 150px; font-size: 12px;
                                                width: 200px;" class="form-control" id="ddlNewAddressCityCode" name="ddlNewAddressCityCode">
                                                <option value="0">لطفا شهر خود را انتخاب کنید</option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-lbl">
                                آدرس پستی:
                            </td>
                            <td align="right">
                                <textarea id="txtNewAddressAddress" rows="3" cols="60" class="form-control" style="width: 180px;
                                    font-family: tahoma;"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td class="form-lbl">
                                کد پستی:
                            </td>
                            <td align="right">
                                <input id="txtNewAddressPostalCode" class="form-control" style="width: 180px; font-family: tahoma;"
                                    type="text" />
                            </td>
                        </tr>
                    </table>
                    <div id="msgNewAddress" class="cErrorMessage hide">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">
                        انصراف</button>
                    <button type="button" id="btnSubmitNewAddress" class="btn btn-primary">
                        ثبت تغییرات</button>
                </div>
            </div>
        </div>
    </div>
    <div class="">
        <div class="">
            <div class="Hierarchy">
                <ul class="mnuHierarchy">
                    <li class="IcHome">
                        <asp:HyperLink ID="hplMainPage" NavigateUrl="~/" runat="server">صفحه اصلی</asp:HyperLink>
                    </li>
                    <li class="Sep">&nbsp; </li>
                    <li>
                        <asp:Label ID="Label1" runat="server" Text="اطلاعات ارسال"></asp:Label>
                    </li>
                </ul>
            </div>
            <div class="Clear">
            </div>
            <div class="Marginer1">
                <AKP:MsgBox runat="server" ID="msgMessage">
                </AKP:MsgBox>
            </div>
            <asp:Panel runat="server" ID="pnlBuyInfo">
                <div class="BoxYel">
                    <div id="cphMain_pnlBuyInfo" class="BoxYelMid">
                        <div style="padding: 10px; direction: rtl;">
                            <ul class="OrderMsg">
                                <li>لطفا" اطلاعات خواسته شده در فرم سفارش را با دقت تکمیل نمائید . ثبت اطلاعات ناقص
                                    و نادرست باعث بروز اختلال در ارسال کالا توسط اداره پست میگردد و درصورت عودت محصول
                                    به هردلیل ، هزینه پستی مجدد آن به عهده خریدار می باشد . کلیه سفارشات ثبت شده در
                                    سیستم ، طی 2 الی 4 روز کاری و در ساعات اداری ( بین 8 صبح الی 4 بعدازظهر ) توسط اداره
                                    پست به آدرس ثبت شده در فرم سفارش ارسال میگردد. لذا خواهشمند است آدرس پستی دقیق به
                                    همراه پلاک ثبتی و کد پستی صحیح خود را در فرم ثبت نموده و در صورتیکه صلاح میدانید
                                    ، به دیگر همکاران خود در محل کار یا دیگر افراد خانواده دررابطه با سفارش خود اطلاع
                                    رسانی نموده تا در صورت مراجعه مامور پست به جهت عدم حضورشما ، سفارش عودت داده نشود
                                    و درصورت امکان آدرس محلی را ثبت نمائید ، که بیشتر اوقات خود را در آن محل میگذرانید.
                                    درصورت مراجعه مامور پست و عدم تحویل کالا به دلیل عدم حضور ، قبض عدم حضور صادر گشته
                                    و به درب منزل یا محل کار الصاق میگردد و مرسوله به واحد پستی منطقه ارجاع میگردد .
                                    مامور پست مجددا" روز بعد به محل مراجعه میکند و درصورت عدم حضور ، قبض دوم عدم حضور
                                    صادر میگردد و درصورتیکه ظرف مدت 5 روز جهت دریافت مرسوله خود به واحد پستی منطقه مراجعه
                                    ننمائید ، مرسوله به واحد پستی مبداء مرجوع میگردد .
                                    <br />
                                    چنانچه توضیح خاصی دارید و نکته ای که مامور پست باید به آن توجه داشته باشد ، لطفا"
                                    درقسمت توضیحات ( انتهای فرم سفارش ) قید نمائید . کلیه سفارشات از طریق پست پیشتاز
                                    ارسال میگردد . </li>
                            </ul>
                        </div>
                    </div>
                    <div class="BoxYelBot">
                    </div>
                </div>
            </asp:Panel>
            <div class="header">
                <h2>
                    انتخاب آدرس
                </h2>
            </div>
            <div class="Clear">
            </div>
            <asp:Panel ID="pnlNoAddress" Visible="false" CssClass="cWarning" runat="server">
                <asp:Label ID="Label2" runat="server" Text="لطفا آدرس پستی خود را وارد کنید"></asp:Label>
            </asp:Panel>
            <asp:Repeater ID="rptUserAddresses" runat="server">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="AddressCont">
                        <table class="tblUserAddress">
                            <tr>
                                <td class="TDDel">
                                    <img onclick="RemoveAddress('<%#Eval("Code") %>')" src="images/Remove-icon.png" alt="حذف آدرس" />
                                </td>
                                <td>
                                    <div class="ShippingName">
                                        <%#Eval("FullName") %>&nbsp;
                                    </div>
                                    <div class="row MarginerRL">
                                        <div class="col-md-4 col-sm-4 col-xs-12 ShippingData text-left" >
                                            استان&nbsp;<%#Eval("Province") %>&nbsp;شهر&nbsp;<%#Eval("City") %>
                                        </div>
                                        <div class="col-md-4 col-sm-4 col-xs-12 ShippingData text-left">
                                            <%#Eval("Address") %>&nbsp;
                                        </div>
                                        <div class="col-md-2 col-sm-2 col-xs-12 ShippingData">
                                            <%#Eval("PostalCode") %>&nbsp;
                                        </div>
                                        <div class="col-md-2 col-sm-2 col-xs-12 ShippingData">
                                            <%#Eval("Tel") %>&nbsp;
                                        </div>
                                    </div>
                                </td>
                                <td class="TDRadio text-center">
                                    <input type="radio" <%#IsAddressChecked(Eval("Code")) %> name="rbAddress" value="<%#Eval("Code") %>" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <button type="button" id="btnShowAddressForm" class="AddNewAddress"  data-toggle="modal" data-target="#ModalNewAddress">
            </button>
            <div>
                <asp:Literal ID="ltrFactor" runat="server"></asp:Literal>
            </div>
            <div class="Spacer1">
            </div>
            <div class="header">
                <h2>
                    شیوه ارسال
                </h2>
            </div>
            <div class="clearfix">
            </div>
            <div>
                <div class="AddressCont">
                    <table class="tblDeliverTypes">
                        <tr>
                            <td class="TDRadio text-center">
                                <asp:RadioButton ID="rbPostPishtaz" Checked="true" GroupName="DeliverType" runat="server" />
                            </td>
                            <td class="TDText">
                                پست پیشتاز
                            </td>
                            <td class="TDPrice">
                                <asp:Label ID="lblPost" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--<div class="AddressCont">
                    <table class="tblDeliverTypes">
                        <tr>
                            <td class="TDRadio text-center">
                                <asp:RadioButton ID="rbPishtaz" GroupName="DeliverType" runat="server" />
                            </td>
                            <td class="TDText">
                                پست پیشتاز (سراسر کشور)
                            </td>
                            <td class="TDPrice">
                                <asp:Label ID="lblPishtaz" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>--%>
                <div class="AddressCont">
                    <table class="tblDeliverTypes">
                        <tr>
                            <td class="TDRadio text-center">
                                <asp:RadioButton ID="rbSefareshi" GroupName="DeliverType" runat="server" />
                            </td>
                            <td class="TDText">
                                پست سفارشی
                            </td>
                            <td class="TDPrice">
                                <asp:Label ID="lblPeik" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Panel runat="server" ID="pnlPayTools">
                    <div class="Marginer1">
                        <div class="OrderForm" style="text-align: right;">
                            <div style="text-align: left; padding-left: 15px;">
                                <asp:ImageButton ID="btnSaveContinue" Width="230" ImageUrl="~/images/spacer.gif" OnClientClick="Hide(this)"
                                    CssClass="SaveContinue" Text="ثبت اطلاعات و ادامه خرید" runat="server" OnClick="btnSaveContinue_Click">
                                </asp:ImageButton>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="Clear">
                </div>
            </div>
            <br />
        </div>
    </div>
    <div class="Clear">
    </div>
    <asp:Literal ID="ltrScript" runat="server"></asp:Literal>
</asp:Content>
