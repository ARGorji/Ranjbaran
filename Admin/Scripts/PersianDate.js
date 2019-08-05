/*********************************************
Persian Calendar 
*********************************************/
var Lang = 1;
var CurrentLang = 1//1 for farsi, 0 for english. farsi default
var dtLDate = 0
var sFDate = 0

var MyYear = 0;
var MyMonth = 0;
var MyDay = 0;

var CurrentDay
var CurrentMonth
var CurrentYear
var FFN = null;
var InitDate = null;
var DayNumBGColor = "#86BEF9";
var CellBGColor = "#E0ECF9";
var HeadBGColor = "#01346D";
var FooterBGColor = "#01346D";
var InputDateID = ''

function ChangeLanguage()
{

	CurrentLang = 1 - CurrentLang	
    Init(CurrentLang);

}

function GetMonthName(MonthNum)
{
	MonthNum = parseInt(MonthNum, 10)

	if ( CurrentLang == 0)
	{
		switch(MonthNum)
		{
			case 1:
				return "January"
				break;
			case 2:
				return "February"
				break;
			case 3:
				return "March"
				break;
			case 4:
				return "April"
				break;
			case 5:
				return "May"
				break;
			case 6:
				return "June"
				break;
			case 7:
				return "July"
				break;
			case 8:
				return "August"
				break;
			case 9:
				return "September"
				break;
			case 10:
				return "October"
				break;
			case 11:
				return "November"
				break;
			case 12:
				return "December"
				break;
		}

	}

	switch(MonthNum)
	{
		case 1:
			return "فروردین"
			break;
		case 2:
			return "اردیبهشت"
			break;
		case 3:
			return "خرداد"
			break;
		case 4:
			return "تیر"
			break;
		case 5:
			return "مرداد"
			break;
		case 6:
			return "شهریور"
			break;
		case 7:
			return "مهر"
			break;
		case 8:
			return "آبان"
			break;
		case 9:
			return "آذر"
			break;
		case 10:
			return "دی"
			break;
		case 11:
			return "بهمن"
			break;
		case 12:
			return "اسفند"
			break;

	}
}

function CurFDate()
{
	CurrentLDate = new Date()
    if (dtLDate == CurrentLDate) 
    	return dtLDate

    dtLDate = CurrentLDate

	nLDay = dtLDate.getDate()
    nLMonth = dtLDate.getMonth() + 1
    nLYear = dtLDate.getFullYear()

    nDays = 0
    for (N = 1999 ; N <= nLYear - 1; N++)
    {
            nDays = nDays + LYearDays(N);
    }

    for ( N = 1 ; N <= nLMonth - 1; N++)
    {
        nDays = nDays + LMonthDays(nLYear, N);
    }

    nDays = nDays + nLDay + 285;

    sFDate = FDateAddDay("13770101", nDays)
	return sFDate

}
function LYearDays(niYear) 
{

    if ( IsLLeapYear(niYear) )
        return 366
    else
        return 365
}

function IsLLeapYear(niYear)
{
    if ( (niYear % 4) == 0 && (niYear % 100) != 0 || (niYear % 400) == 0 )
        return true
    else
        return false
}

function LMonthDays(niYear , niMonth)
{
	switch(niMonth)
	{
	    case 1:
	        return 31
	        break;
	    case 3:
	        return 31
	        break;
	    case 5:
	        return 31
	        break;
	    case 7:
	        return 31
	        break;
	    case 8:
	        return 31
	        break;
	    case 10:
	        return 31
	        break;
	    case 12 :
	        return 31
	        break;
	    case 4:
	        return 30
	        break;
	    case 6:
	        return 30
	        break;
	    case 9:
	        return 30
	        break;
	    case 11 :
	        return 30
	        break;
    	case 2 :
	    	{
		        if ( IsLLeapYear(niYear) )
		            return 29
		        else
		            return 28
		        break;
	        }
	    default :
	    {
	        return 0;
	        }
	}
}


function FDateAddDay(siDate , niDays ) 
{
    if ( !IsValidFDate(siDate) )
    	return false;

    if ( niDays < 0 )
    	return false;
   
    dt = FDateSplit(siDate)


    if (dt[0] > 1 )
    {
        niDays = niDays + dt[0] - 1
        dt[0] = 1
    }

    while ( niDays >= FYearDays(dt[2]) )	
	{
        niDays = niDays - FYearDays(dt[2]);
        dt[2] = dt[2] + 1;
    }


    while ( niDays >= FMonthDays(dt[2], dt[1]) )
	{
        niDays = niDays - FMonthDays(dt[2], dt[1]);
        dt[1] = dt[1] + 1;
        if ( dt[1] > 12 )
        {
            dt[1] = 1
            dt[2] = dt[2] + 1
         }
    }



    dt[0] = niDays + dt[0]

    Result = FDateMerge(dt)
    return Result
}

function FDateSplit(siDate)
{
	var DateArray = new Array()
	DateArray[0] = parseInt(siDate.substr(6, 2), 10)
	DateArray[1] = parseInt(siDate.substr(4, 2), 10)
	DateArray[2] = parseInt(siDate.substr(0, 4), 10)

	return DateArray
}

function FDateMerge(dtiDate ) 
{
	if ( dtiDate[1] < 10 )
		RealMonth = '0' + dtiDate[1]
	else
		RealMonth = dtiDate[1]
	
	if ( dtiDate[0] < 10 )
		RealDay = '0' + dtiDate[0]
	else
		RealDay = dtiDate[0]

	RealYear = dtiDate[2]

    return (RealYear + '' + RealMonth + '' + RealDay)
}

function FMonthDays(niYear, niMonth) 
{
	if ( CurrentLang == 0 )
		return LMonthDays(niYear, niMonth)

    switch( niMonth)
    {
	    case 1:
	        return 31;
	        break;
	    case 2:
	        return 31;
	        break;
	    case 3:
	        return 31;
	        break;
	    case 4:
	        return 31;
	        break;
	    case 5:
	        return 31;
	        break;
	    case 6:
	        return 31;
	        break;
	    case 7:
	        return 30
	        break;
	    case 8:
	        return 30
	        break;
	    case 9:
	        return 30
	        break;
	    case 10:
	        return 30
	        break;
	    case 11:
	        return 30
	        break;
    	case 12 :
    		if( IsFLeapYear(niYear) )
    			return 30
    		else
    			return 29
	        break;
    }
}


function IsValidFDate(siDate ) 
{
    if ( siDate.length  != 8 )
    {
    	return false
	}
    if ( isNaN(siDate) )
    {
    	return false
	}
    dt = FDateSplit(siDate)

    if ( dt[2] <= 0 )
    {
    	return false
	}
    if ( dt[1] <= 0 || dt[1] > 12 )
    {
    	return false
	}
    if ( dt[0] <= 0 || dt[0] > FMonthDays(dt[2], dt[1]) )
    {
    	return false
   	}
    	

   	return true

}


function FYearDays(niYear) 
{
	if ( CurrentLang == 0 )
		return LYearDays(niYear) 
		
    if ( IsFLeapYear(niYear) )
        return 366
    else
        return 365
}

function IsFLeapYear(niYear )
{
	if ( CurrentLang == 0 )
		return IsLLeapYear(niYear)		 
    niYear = (niYear - 22) % 33

    if ( niYear == 32 )
        return false
    else if ( (niYear % 4) == 0 )
        return true
    else
        return false
}

function DayOfWeek(siDate )
{
    if ( !IsValidFDate(siDate) )
    {
        return -1
        return false
	}        

    lDayOfWeek = 0 // 13770101 was a saturday
    nYear = parseInt(siDate.substr(0, 4), 10)
    nMonth = parseInt(siDate.substr(4, 2), 10)
    nDay = parseInt(siDate.substr(6, 2), 10)

	if ( CurrentLang == 0 )
	{
		TempDate = new Date(nYear, nMonth - 1, 1)
		return (TempDate.getDay() + 1)%7
	}


    while ( nYear != 1377 )
    {
        if ( nYear < 1377 )
        {
           lDayOfWeek = lDayOfWeek - FYearDays(nYear)
           nYear = nYear + 1
        }
        else
        {
           lDayOfWeek = lDayOfWeek + FYearDays(nYear - 1)
           nYear = nYear - 1
        }
	}
	
    while ( nMonth > 1 )
    {
       lDayOfWeek = lDayOfWeek + FMonthDays(1377, nMonth - 1)
       nMonth = nMonth - 1
    }

    lDayOfWeek = lDayOfWeek + nDay - 1;

    lDayOfWeek = lDayOfWeek % 7

    if ( lDayOfWeek < 0 )
        lDayOfWeek = 7 + lDayOfWeek

    return lDayOfWeek
}

var SelectedCellObj = null
var CurrentDateCellObj = null

function FillCells(YearIndex, MonthIndex, DayIndex)
{
	RealDay = 1

	for (j=0; j < 42; j++)
	{
		document.getElementById("Cell" + j).style.backgroundColor = ''
		CellID  = document.getElementById("Cell" + j).innerHTML = "&nbsp;"
		document.getElementById("Cell" + j).style.cursor = ''
	}
    
	for (i=DayIndex; i < 42; i++)
	{
		if ( RealDay < 10 )
			TempDate = '0' + RealDay
		else
			TempDate = RealDay + ''

//		if ( MonthIndex < 10 )
//			TempDate = '0' + MonthIndex + TempDate
//		else
		TempDate = MonthIndex + '' + TempDate

		TempDate = YearIndex + '' + TempDate
			
		if( IsValidFDate(TempDate))
		{
			try{
				document.getElementById("Cell" + i).EngDate = RealDay;
				if(CurrentLang == 1)
				    document.getElementById("Cell" + i).innerText = GetPersianNumber(RealDay + '');
				else
				    document.getElementById("Cell" + i).innerText = RealDay ;
				document.getElementById("Cell" + i).style.cursor = 'hand'
				if( RealDay == MyDay )
				{
					document.getElementById("Cell" + i).style.backgroundColor = 'CCCCCC'
					CurrentDateCellObj = document.getElementById("Cell" + i)
					SelectedCellObj = CurrentDateCellObj
				}
			}
			catch(e)
			{
				alert(e.description)
			}
		}
		RealDay++;
	} 
	HeaderHtml = '<font face=Tahoma color=white size=2>' + GetMonthName(MyMonth) + '  ';
	if(CurrentLang == 1)
	    HeaderHtml = HeaderHtml + GetPersianNumber(MyYear + '') + '</font>'
	else
	    HeaderHtml = HeaderHtml + MyYear  + '</font>'
    CellID = document.getElementById("TitleCell").innerHTML = HeaderHtml
}

function Init(CurrentLang)
{
	CurrentFarsiDate = CurFDate()
	var dt = new Array()

	dt = FDateSplit(CurrentFarsiDate)
	CurrentDay = dt[0]
	CurrentMonth = dt[1]
	CurrentYear = dt[2]

    if(InitDate != null)
    {
        InitYear = InitDate.substring(0, 4);
        InitMonth = InitDate.substring(5, 2);
        InitDay = InitDate.substring(InitDate.Length - 2, 2);
        StringDate = InitYear + InitMonth + InitDay;
        
        if(IsValidFDate(StringDate))
        {
                dt[2] = InitYear;
                dt[1] = InitMonth;
                dt[0] = InitDay;
        }
    }
    
	if ( dt[1] < 10 ) 
		StringMonth = '0' + parseInt(dt[1], 10)
	else
		StringMonth = dt[1]

	FirstMonthDay = dt[2] + '' + StringMonth + '01'
	DayIndex = DayOfWeek(FirstMonthDay)
	MyYear = dt[2];
	MyMonth = dt[1];
	MyDay = dt[0];


	if( CurrentLang == 1)
	{

	    document.getElementById("ImageLang").className = 'cDateFa'

		document.getElementById("Friday").innerText = 'ج'
		document.getElementById("Thursday").innerText = 'پ'
		document.getElementById("Wednesday").innerText = 'چ'
		document.getElementById("Tuesday").innerText = 'س'
		document.getElementById("Monday").innerText = 'د'
		document.getElementById("Sunday").innerText = 'ی'
		document.getElementById("Saterday").innerText = 'ش'
	
//		Init();
	}
	else
	{
		document.getElementById("ImageLang").className = 'cDateEn'

		document.getElementById("Friday").innerText = 'F'
		document.getElementById("Thursday").innerText = 'T'
		document.getElementById("Wednesday").innerText = 'W'
		document.getElementById("Thursday").innerText = 'T'
		document.getElementById("Tuesday").innerText = 'T'
		document.getElementById("Monday").innerText = 'M'
		document.getElementById("Sunday").innerText = 'S'
		document.getElementById("Saterday").innerText = 'S'

		CurrentLDate = new Date()
		MyDay = CurrentLDate.getDate()
		MyMonth = CurrentLDate.getMonth() + 1
		MyYear = CurrentLDate.getYear()

		CurrentDay = MyDay
		CurrentMonth = MyMonth
		CurrentYear = MyYear
	}

	FillCells(dt[2], StringMonth, DayIndex)

}

function AddOneMonth()
{
	MyMonth++;
	if( MyMonth > 12 ) 
	{
		MyMonth = 1
		MyYear++;
	}

	if ( MyMonth < 10 ) 
		StringMonth = '0' + MyMonth
	else
		StringMonth = MyMonth

	FirstMonthDay = MyYear + '' + StringMonth + '01'
	DayIndex = DayOfWeek(FirstMonthDay)

	FillCells(MyYear, StringMonth, DayIndex)

}

function SubOneMonth()
{
	MyMonth--;
	if( MyMonth <= 0 ) 
	{
		MyMonth = 12
		MyYear--;
	}

	if ( MyMonth < 10 ) 
		StringMonth = '0' + MyMonth
	else
		StringMonth = MyMonth

	FirstMonthDay = MyYear + '' + StringMonth + '01'
	DayIndex = DayOfWeek(FirstMonthDay)

	FillCells(MyYear, StringMonth, DayIndex)

}

function SubOneYear()
{
	MyYear--;

	if ( parseInt(MyMonth, 10) < 10 ) 
		StringMonth = '0' + parseInt(MyMonth, 10)
	else
		StringMonth = MyMonth

	FirstMonthDay = MyYear + '' + StringMonth + '01'
	DayIndex = DayOfWeek(FirstMonthDay)

	FillCells(MyYear, StringMonth, DayIndex)

}


function AddOneYear()
{
	MyYear++;

	if ( parseInt(MyMonth, 10) < 10 ) 
		StringMonth = '0' + parseInt(MyMonth, 10)
	else
		StringMonth = MyMonth

	FirstMonthDay = MyYear + '' + StringMonth + '01'
	DayIndex = DayOfWeek(FirstMonthDay)

	FillCells(MyYear, StringMonth, DayIndex)

}

function HighLight(CellID)
{
	if (document.getElementById(CellID).innerHTML != '&nbsp;' && document.getElementById(CellID).style.backgroundColor != '#cccccc')
	{
		SelectedCellObj.style.backgroundColor = ''
		
		document.getElementById(CellID).style.backgroundColor = 'BED6F1'
		SelectedCellObj = document.getElementById(CellID)
	}
}

function UnHighLight(CellID)
{
	if ( document.getElementById(CellID).style.backgroundColor != 'CCCCCC' && document.getElementById(CellID).style.backgroundColor != '#cccccc')
	{
		document.getElementById(CellID).style.backgroundColor = ''
	}
}

function SelectDate(CellID)
{
	
	var SelectedMonth = 0
	var SelectedDay = 0
	
	SelectedDay = document.getElementById(CellID).EngDate	
	
	if(SelectedDay != '&nbsp;')
	{
		if( parseInt(SelectedDay, 10) < 10 )
			SelectedDay = '0' + SelectedDay

		if ( parseInt(MyMonth, 10) < 10)
			SelectedMonth = '0' + parseInt(MyMonth, 10)
		else
			SelectedMonth = '' + parseInt(MyMonth, 10)
		
		if(FFN != null)
		{
		    //eval("document.aspnetForm." + FFN + ".value = MyYear + '/' + SelectedMonth + '/' + SelectedDay;")
		    eval("document.getElementById(InputDateID).value = MyYear + '/' + SelectedMonth + '/' + SelectedDay;")
		    
		    DatePan.style.display = "none"
		    document.getElementsByName(FFN)[0].fireEvent('onChange');
		}
	}
	
}

function SelectCurrentDay()
{
	MyDay = CurrentDay;
	if (MyDay < 10)
		MyDay = '0' + MyDay

	MyMonth = CurrentMonth;
	if (MyMonth < 10)
		MyMonth = '0' + MyMonth

	MyYear = CurrentYear;

	YearMonthDay = MyYear + '' + MyMonth + '' + '01'
	DayIndex = DayOfWeek(YearMonthDay)

	FillCells(MyYear, StringMonth, DayIndex)
}

function isChild(s,d) {
	while(s) {
		if (s==d) 
			return true;
		s=s.parentNode;
	}
	return false;
}

function getObj(objID)
{
    if (document.getElementById) {
//        alert(objID + '   ' + document.getElementById(objID));
        return document.getElementById(objID);
    }
    else if (document.all)
        return document.all[objID];
    else if (document.layers)
        return document.layers[objID];
}

function Left(obj)
{
	var curleft = 0;
	if (obj.offsetParent)
	{
		while (obj.offsetParent)
		{
			curleft += obj.offsetLeft
			obj = obj.offsetParent;
		}
	}
	else if (obj.x)
		curleft += obj.x;
	return curleft;
}

function Top(obj)
{
	var curtop = 0;
	if (obj.offsetParent)
	{
		while (obj.offsetParent)
		{
			curtop += obj.offsetTop
			obj = obj.offsetParent;
		}
	}
	else if (obj.y)
		curtop += obj.y;
	return curtop;
}

function checkClick(e) {
	e?evt=e:evt=event;
	CSE = evt.target ? evt.target : evt.srcElement;
	if (getObj('ListPanel'))
	{
	    if (!isChild(CSE, getObj('ListPanel'))) {
	        //alert(evt.srcElement.tagName);
	        return;
	        getObj('ListPanel').style.display = 'none';
        }	
	}
	
	if (getObj('DatePanel'))
		if (!isChild(CSE,getObj('DatePanel')))
		{
		    try
		    {
			    getObj('DatePanel').style.display='none';
			    if(!isChild(CSE,getObj('ListPanel')))
			    {
			        if(!isChild(CSE,getObj('FilterPanel')) && !isChild(CSE,getObj('FieldPanel')) )
    			        getObj('ListPanel').style.display='none';
			        getObj('FilterPanel').style.display='none';
			    }
			    if(!isChild(CSE,getObj('FieldPanel')))
			    {
    			    getObj('FieldPanel').style.display='none';
			    }
			    if(!isChild(CSE,getObj('AccessPanel')))
			    {
    			    getObj('AccessPanel').style.display='none';
			    }
			    
			    
			}
			catch(e)
			{
			    //alert(e.description);
			}
    	}
}

GetPersianNumber = function(str) {
    str = str + ''
    var Result = "";
    for (num = 0; num < str.length; num++) {
        Result = Result + String.fromCharCode(str.charCodeAt(num) + 1728);
    }
    return Result;
}


document.write("<div class=\"cHiddenDate\" id=\"DatePanel\">" +
"<table id=\"table4\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tr><td><img src=\"../images/Date/wing_01.gif\" width=\"21\" height=\"88\" alt=\"\"></td><td height=\"88\" style=\" background-repeat: repeat-x; background-position: center bottom\" valign=\"top\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" id=\"table5\"><tr><td width=1% background=\"../images/Date/wingtop1.gif\" style=\"background-image: url('../images/Date/wingtop1.gif'); background-repeat: repeat-x; background-position-y: bottom\">&nbsp;</td><td width=\"89\"><img border=\"0\" style=\"z-index:-1\" id=imgArrow src=\"../images/Date/wing_03.gif\" width=\"89\" height=\"88\"></td><td background=\"../images/Date/wingtop1.gif\" style=\"background-image: url('../images/Date/wingtop1.gif'); background-repeat: repeat-x; background-position-y: bottom\">&nbsp;</td></tr></table></td><td><img src=\"../images/Date/wing_05.gif\" width=\"22\" height=\"88\" alt=\"\"></td></tr><tr><td style=\"background-image: url('../images/Date/wingleft.gif'); background-repeat: repeat-y; background-position: right center\" height=\"98\">&nbsp;</td><td bgcolor=white >" +
"<table border=\"0\" id=\"tblDateCells\" class=\"PersTD\" width=\"200\" style=\"border-collapse: collapse\" bordercolor=\"#111111\">        <tr>            <td colspan=\"7\" id=\"tdHead\" width=\"100%\" align=\"center\">                <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">                    <tr>                        <td width=\"20\" align=\"center\">                            <a class=\"cCalBut\" style=\"background: url(../images/Date/Calendar.gif) no-repeat 98% -63px;\" onclick=\"SubOneYear()\" title=\"سال قبل\"><img width=\"14\" height=\"14\" src=\"../images/Date/spacer.gif\"></a></td>                        <td width=\"30\" align=\"center\">                            <a class=\"cCalBut\" style=\"background: url(../images/Date/Calendar.gif) no-repeat 98% -20px;\" onclick=\"SubOneMonth()\" title=\"ماه قبل\"><img width=\"14\" height=\"14\" src=\"../images/Date/spacer.gif\"></a></td>                        <td align=\"center\" id=\"TitleCell\">                            <font face=\"Tahoma\" color=\"white\" size=\"2\">F</font></td>                        <td width=\"30\" align=\"center\">                            <a class=\"cCalBut\" style=\"background: url(../images/Date/Calendar.gif) no-repeat 98% 0px;\" onclick=\"AddOneMonth()\" title=\"ماه بعد\"><img width=\"14\" height=\"14\" src=\"../images/Date/spacer.gif\"></a></td>                        <td width=\"20\" height=\"25\" align=\"center\">                            <a class=\"cCalBut\" style=\"background: url(../images/Date/Calendar.gif) no-repeat 98% -42px;\" onclick=\"AddOneYear()\" title=\"سال بعد\"><img width=\"14\" height=\"14\" src=\"../images/Date/spacer.gif\"></a></td>                    </tr>                </table>            </td>        </tr>        <tr id=\"trHeader\" class=\"cPersian2\">            <td height=\"25\"  width=\"14%\" align=\"center\">                <font id=\"Friday\">ج</font></td>            <td  width=\"14%\" align=\"center\">                <font id=\"Thursday\">پ</font></td>            <td  width=\"14%\" align=\"center\">                <font id=\"Wednesday\">چ</font></td>            <td  width=\"14%\" align=\"center\">                <font id=\"Tuesday\">س</font></td>            <td  width=\"14%\" align=\"center\">                <font id=\"Monday\">د</font></td>            <td  width=\"14%\" align=\"center\">                <font id=\"Sunday\">ی</font></td>            <td  width=\"14%\" align=\"center\">                <font id=\"Saterday\">ش</font></td>        </tr>");
Result = "";
for (I = 1; I <= 6; I++)
{
    Result += "<tr>\n";
    for ( J = 6; J >= 0; J--)
        Result += "<td height=15 bgcolor=\"" + CellBGColor + "\"  class=\"cPershand\" onclick=\"SelectDate('Cell" + ((I - 1) * 7 + J) + "');\" onmouseover=\"HighLight('Cell" + ((I - 1) * 7 + J) + "')\" onmouseout=\"UnHighLight('Cell" + ((I - 1) * 7 + J) + "')\" id=Cell" + ((I - 1) * 7 + J) + " align=center width=14% >&nbsp;</td>";
    Result += "</tr>";
}
document.write(Result)
document.write("<tr>            <td colspan=\"7\" id=\"tdFooter\" width=\"100%\" align=\"center\">                <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">                    <tr>                        <td align=\"left\"><img src=\"../images/Date/today.gif\" class=\"cImage\" style=\"float:right\" alt=\"امروز\" border=\"0\" onclick=\"SelectCurrentDay()\"><img id=ImageLang onclick=\"ChangeLanguage()\" class=\"cDateFa\" ></td>                        <td width=\"20\" align=\"center\">                            </td>                    </tr>                </table>            </td>        </tr>    </table>" + 
"</td><td width=\"22\" background=\"../images/Date/wingright.gif\">&nbsp;</td></tr><tr><td><img src=\"../images/Date/wing_09.gif\" width=\"21\" height=\"22\" alt=\"\"></td><td background=\"../images/Date/wingbottom.gif\" height=\"22\">&nbsp;</td><td><img src=\"../images/Date/wing_11.gif\" width=\"22\" height=\"22\" alt=\"\"></td></tr></table>" +
"</div>")

if(CurrentLang == 0)
    document.getElementById("tblDateCells").style.direction = 'rtl'
Init(Lang)
//alert( document.getElementById("bod").innerHTML)
for(i = 0; i < document.getElementById("trHeader").cells.length; i++)
    document.getElementById("trHeader").cells[i].style.backgroundColor = DayNumBGColor
document.getElementById("tdHead").style.backgroundColor = HeadBGColor
document.getElementById("tdFooter").style.backgroundColor = FooterBGColor

var DatePan = document.getElementById("DatePanel")
document.all?document.attachEvent('onclick',checkClick):document.addEventListener('click',checkClick,false);
document.all ? document.body.attachEvent('onresize', checkClick) : document.addEventListener('resize', checkClick, false);

/************************************************ Mask Date ****************************************************/
function DoMask(MaskFormat, str, textbox, loc, delim) {

    DoNothing = 0
    CurrentPos = textbox.value.length;
    var locs = loc.split(',');

    KeyCode = event.keyCode
    if (!window.event.shiftKey && !window.event.ctrlKey && !window.event.altKey) {
        if ((KeyCode > 47 && KeyCode < 58) || (KeyCode > 95 && KeyCode < 106)) {
            MaxL = MaskFormat.length + locs.length
            if (textbox.value.length >= MaxL) {
                event.returnValue = false
                return false;
            }
            if (KeyCode > 95) KeyCode -= (95 - 47);
        } else if (KeyCode != 8 && KeyCode != 46) {
            event.returnValue = false
            return false;
        }
    }

    CurrentChar = String.fromCharCode(KeyCode)

    for (var i = 0; i <= locs.length; i++) {
        for (var k = 0; k <= str.length; k++) {
            if (k == locs[i]) {
                if (str.substring(k, k + 1) != delim) {
                    str = str.substring(0, k) + delim + str.substring(k, str.length)
                    textbox.value = str
                }
            }
        }
    }

    if (CurrentPos < MaskFormat.length + locs.length) {
        if (MaskFormat.charAt(CurrentPos) == 'N') {
            if (!isNaN(CurrentChar)) {
                DoNothing = 0
            }
            else {
                DoNothing = 1
            }
        }
        else if (MaskFormat.charAt(CurrentPos) == '/') {
            if (CurrentChar == '/') {
                DoNothing = 0
            }
            else {
                DoNothing = 1
            }
        }
        else if (MaskFormat.charAt(CurrentPos) == 'L') {
            if (!isNaN(CurrentChar)) {
                DoNothing = 1
            }
            else {
                DoNothing = 0
            }
        }

    }
    else
        DoNothing = 1


    if (DoNothing == 1)
        event.keyCode = 0
    return true;
}
 
/************************************************ Mask Date ****************************************************/

/************************************************ Open Date ****************************************************/
//function OpenDate(DivObj) {

//    InitVal = DivObj.parentNode.parentNode.childNodes[1].childNodes[0].value;
//    FFN = DivObj.parentNode.parentNode.childNodes[1].childNodes[0].name;
//    InitVal = InitVal.replace("/", "");
//    InitVal = InitVal.replace("/", "");
//    X = window.event.clientX
//    Y = window.event.clientY
//    if (InitVal.length != 8)
//        InitVal = '';

//    DatePan.style.left = X - 32 + document.body.scrollLeft;
//    DatePan.style.top = Y + 2 + document.body.scrollTop + document.documentElement.scrollTop;
//    DatePan.style.display = "block";
//    Init(1)
//}

function OpenDate(evt, DivObj, ShowTime) {
    //alert(DivObj.parentNode.parentNode.childNodes[2].childNodes[1].tagName);

    var ver = getInternetExplorerVersion();
    IEIndex = 0;
    if (ver == 9)
        IEIndex = 1;

    InitVal = DivObj.parentNode.parentNode.childNodes[1 + IEIndex].childNodes[0 + IEIndex].value;
    FFN = DivObj.parentNode.parentNode.childNodes[1 + IEIndex].childNodes[0 + IEIndex].name;
    InputDateID = DivObj.parentNode.parentNode.childNodes[1 + IEIndex].childNodes[0 + IEIndex].id;
    
    InitVal = InitVal.replace("/", "");
    InitVal = InitVal.replace("/", "");
    X = window.event.clientX
    Y = window.event.clientY
    if (InitVal.length != 8)
        InitVal = '';

    DatePan.style.left = X - 32 + document.body.scrollLeft + 'px';
    DatePan.style.top = Y + 2 + document.body.scrollTop + document.documentElement.scrollTop  + 'px';
    DatePan.style.display = "block"
    objTimeRow = document.getElementById('TimeRow');
    if (objTimeRow != null) {
        if (ShowTime == 'False')
            objTimeRow.style.display = 'none';
    }

    Init(1)
}

/************************************************ Open Date ****************************************************/
