
// این قسمت مختصات اشاره گر موس را ذخیره میکند
// Detect if the browser is IE or not.
// If it is not IE, we assume that the browser is NS.
var IE = document.all ? true : false
// If NS -- that is, !IE -- then set up for mouse capture
if (!IE) document.captureEvents(Event.MOUSEMOVE)
// Set-up to use getMouseXY function onMouseMove
document.onmousemove = getMouseXY;
// Temporary variables to hold mouse x-y pos.s
var mouseX = 0
var mouseY = 0
// Main function to retrieve mouse x-y pos.s
function getMouseXY(e) {
    try {
        if (IE) { // grab the x-y pos.s if browser is IE
            mouseX = event.clientX + document.body.scrollLeft
            mouseY = event.clientY + document.body.scrollTop
        } else {  // grab the x-y pos.s if browser is NS
            mouseX = e.pageX
            mouseY = e.pageY
        }
        // catch possible negative values in NS4
        if (mouseX < 0) { mouseX = 0 }
        if (mouseY < 0) { mouseY = 0 }
    }
    catch (e) {mouseX = 300;mouseY = 300; }
    return true
}

var AppName = "Budget";

var http_request = null;
var DelObj = null;
function startRequest(url, Func, Method, Param, UpdateObj) { 
    DelObj = UpdateObj;
    if (window.XMLHttpRequest) { 
       http_request = new XMLHttpRequest(); 
    } 
    else if (window.ActiveXObject) { 
       http_request = new ActiveXObject('Microsoft.XMLHTTP'); 
    } 
    url = url + '&rnd=' + Math.random();
    http_request.onreadystatechange = Func;
    http_request.open(Method, url, true); 
    if( Method == 'GET')
       http_request.send(null); 
    else
       http_request.send(Param); 
} 
 
function isChild(s,d) {
	while(s) {
		if (s==d) 
			return true;
		s=s.parentNode;
	}
	return false;
}

function IsOnlyNumber()
{

var returnValue = false;
var keyCode = (window.event.which) ? window.event.which : window.event.keyCode;
if ( ((keyCode >= 48) && (keyCode <= 57)) ||
(keyCode == 8) ||
(keyCode == 9) ||
(keyCode == 46) || 
(keyCode == 46) || 
(keyCode == 13) ) 
returnValue = true;

if ( window.event.returnValue )
window.event.returnValue = returnValue;
return returnValue;
}
function IsOnlyNumberAndSlash()
{
var returnValue = false;
var keyCode = (window.event.which) ? window.event.which : window.event.keyCode;
if ( ((keyCode >= 47) && (keyCode <= 57)) ||
(keyCode == 8) || 
(keyCode == 9) || 
(keyCode == 13) ) 
returnValue = true;

if ( window.event.returnValue )
window.event.returnValue = returnValue;

return returnValue;
}

function GotoUrl(Url)
{
    window.location.href = Url
}

function getObj(objID)
{
    if (document.getElementById) {return document.getElementById(objID);}
    else if (document.all) {return document.all[objID];}
    else if (document.layers) {return document.layers[objID];}
}
function ShowNews(NewsCode)
{
    window.open("Forms/News/ShowNews.aspx?Code=" + NewsCode, 'News','width=700,height=600,top=10')
}
/****************************************** LookUpTree ************************************************/



function OpenTree(BaseID, ImgObj, parameters) {
    //        FormFieldCode = ImgObj.parentNode.parentNode.parentNode.parentNode.rows[0].cells[1].childNodes[0].name;
    //        FormFieldName = ImgObj.parentNode.parentNode.parentNode.parentNode.rows[0].cells[0].childNodes[1].name;

    //		whList = window.open('../GetTree.aspx?GetVal=1&BaseID=' + BaseID + '&FFC=' + FormFieldCode + '&FFN=' + FormFieldName ,'','width=450,height=450,menubar=no,status=no,titlebar=no,resizable=no,top=200,left=150');
    FormFieldCode = ImgObj.parentNode.parentNode.parentNode.parentNode.rows[0].cells[1].childNodes[0].name;
    FormFieldName = ImgObj.parentNode.parentNode.parentNode.parentNode.rows[0].cells[0].childNodes[0].name;
    if (parameters) {
        if (parameters.length > 0) parameters = "&" + parameters;
    }
    whList = window.open('../GetTree.aspx?GetVal=1&BaseID=' + BaseID + '&FFC=' + FormFieldCode + '&FFN=' + FormFieldName + parameters, '', 'width=450,height=450,menubar=no,status=no,titlebar=no,scrollbars,resizable,top=200,left=150');
}
/****************************************** LookUpTree ************************************************/

var http_request = null;
var DelObj = null;
function startRequest(url, Func, Method, Param, UpdateObj) { 
    DelObj = UpdateObj;
    if (window.XMLHttpRequest) { 
       http_request = new XMLHttpRequest(); 
    } 
    else if (window.ActiveXObject) { 
       http_request = new ActiveXObject('Microsoft.XMLHTTP'); 
    } 
    url = url + '&rnd=' + Math.random();
    http_request.onreadystatechange = Func;
    http_request.open(Method, url, true); 
    if( Method == 'GET')
       http_request.send(null); 
    else
       http_request.send(Param); 
} 


function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=1,status=0');
 WinPrint.document.write("<link href=\"../styles/main.css\" rel=\"stylesheet\" type=\"text/css\">" +  prtContent.innerHTML);
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.resizeTo(document.getElementById("ctl00_cphMain_tblList").offsetWidth + 30,document.getElementById("ctl00_cphMain_tblList").offsetHeight + 180 );
 WinPrint.print();
 WinPrint.close();
}
function OpenWin(Link, Width, Height)
{
  leftVal = (screen.width - Width) / 2;
  topVal = (screen.height - Height) / 2;
  window.open(Link,'','width=' + Width + ',height=' + Height + ',menubar=no,status=no,titlebar=no,scrollbars=yes,resizable=yes,top=' + topVal + ',left=' + leftVal );
}
function OnClicking(sender, eventArgs)
{ 
    var item = eventArgs.get_item();
    var itemValue = item.get_value();
    var GroupCode = document.getElementById('ctl00_hfGroupCode');

	if (itemValue != "") 
	{
	    switch(itemValue)
	    {
	    
	        case "UserGuide":
	            window.frames["fraMainBody"].location= 'Files/UserGuide.pdf';
	            break;

	        case "Panel":
	            window.location.href = '../Panel.aspx';
	            break;
	        case "Resources":
	            window.location.href = '../AccessLevel/ResourceTree.aspx';
	            break;
	        case "NewsPririty":
	            window.location.href = '../News/NewsPriorities.aspx';
	            break;
	        case "ChangePass":
	            window.location.href = '../Forms/ChangePass.aspx';
	            break;
	        case "EditSettings":
	            window.location.href = '../Settings/EditSettings.aspx';
	            break;
	            
	        default :
	        {
	               
	         if ((itemValue != undefined) && (itemValue != null))
	                window.location.href = '/Admin/Main/Default.aspx?BaseID='  + itemValue;// window.frames["fraMainBody"].location 
	                
	        }
	    } 
	    return false;
	}
} 

function CorrectText(str)
{
    return str.replace('ی', 'ی').replace('ی', 'ی').replace('ى', 'ی').replace('ك', 'ک')
            .replace('٠', '۰').replace('١', '۱').replace('٢', '۲').replace('٣', '۳').replace('٤', '۴')
            .replace('٥', '۵').replace('٦', '۶').replace('٧', '۷').replace('٨', '۸').replace('٩', '۹')
}

function PrintStr()
{
    CallPreviewPrint('PrintArea');
}

function CallPreviewPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','PrintPage','letf=0,top=0,width=950,height=800,toolbar=0,scrollbars=1,status=0,resizeable=true');
 
 StrBody = "<body style=\"background-color:white\" dir=rtl>";
 StrBody += "<link id=\"ctl00_Link1\" href=\"../styles/main.css\" rel=\"stylesheet\" type=\"text/css\" />";
 StrBody += "<script src=\"../js/main.js\" type=\"text/javascript\"></script>";
 StrBody += "<div id=\"PrintArea\">" + prtContent.innerHTML + "</div><br /><a href=\"#\" class=\"cPrint2\" onclick=\"CallPrint('PrintArea')\"><img src=\"../images/spacer.gif\" alt=\"Print\" class=\"cPrint2\" /></a></body>"
 WinPrint.document.write(StrBody);
 WinPrint.document.close();
 WinPrint.focus();

}
function PrintPage(strid)
{
     var prtContent = document.getElementById(strid);
     var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=1,status=0');
     WinPrint.document.write("<link href=\"../../styles/main.css\" rel=\"stylesheet\" type=\"text\/css\" /><body dir=rtl>" + prtContent.innerHTML + "</body>");
     WinPrint.print();
     WinPrint.document.close();
     WinPrint.focus();

}

function createCookie(name,value,days) {
	if (days) {
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
}

function readCookie(name) {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++) {
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

function eraseCookie(name) {
	createCookie(name,"",-1);
}

// ----------------------- AJAX --------------------------------
function GetXmlHttpObject()
{
var xmlHttp=null;
try
  {
  // Firefox, Opera 8.0+, Safari
  xmlHttp=new XMLHttpRequest();
  }
catch (e)
  {
  // Internet Explorer
  try
    {
    xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
    }
  catch (e)
    {
    xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
    }
  }
return xmlHttp;
}

function GetValue(root,field)
{
    var response = xmlHttp.responseXML.documentElement;
    if (response != null)
    {
        x=response.getElementsByTagName(root);
        if (x.length>0 && x[0].getElementsByTagName(field)[0]!=null && x[0].getElementsByTagName(field)[0].firstChild!=null)
            if (x[0].getElementsByTagName(field)[0].firstChild.nodeValue!='#ERR#')
                    return x[0].getElementsByTagName(field)[0].firstChild.nodeValue;
    }
    return '';
}
function SetValues(root,field,ctrl)
{
    var response = xmlHttp.responseXML.documentElement;
    if (response != null)
    {
    x=response.getElementsByTagName(root);
    if (x.length>0 && x[0].getElementsByTagName(field)[0]!=null && x[0].getElementsByTagName(field)[0].firstChild!=null)
            {
            if (x[0].getElementsByTagName(field)[0].firstChild.nodeValue!='#ERR#')
            {
                if (document.getElementById(ctrl)!=null && x[0].getElementsByTagName(field)[0].firstChild.nodeValue != '')
                    document.getElementById(ctrl).innerText=x[0].getElementsByTagName(field)[0].firstChild.nodeValue;
             }
             else 
             {
                 document.getElementById(ctrl).innerText='';
              }

            }
            else
               document.getElementById(ctrl).innerText='';
      }
}

function SetValuesCombo(root,fieldVal,Combo)
{
    ZoneRadCombo = document.getElementById(Combo);
    var response = xmlHttp.responseXML.documentElement;
    if (response != null)
    {
        x=response.getElementsByTagName(root);
        if (x.length>0 && x[0].getElementsByTagName(fieldVal)[0]!=null)
            if (x[0].getElementsByTagName(fieldVal)[0].firstChild!=null)
                if (x[0].getElementsByTagName(fieldVal)[0].firstChild.nodeValue!='#ERR#')
                    if (ZoneRadCombo!=null && x[0].getElementsByTagName(fieldVal)[0].firstChild.nodeValue != '' )
                        ZoneRadCombo.value=x[0].getElementsByTagName(fieldVal)[0].firstChild.nodeValue;
                 
//                 else 
//                     ZoneRadCombo.value='-1';
//            else  ZoneRadCombo.value='-1';
      }
}

function SetValuesTelerikCombo(root,fieldVal,fieldText,ZoneRadCombo)
{
    var response = xmlHttp.responseXML.documentElement;
    if (response != null)
    {
        x=response.getElementsByTagName(root);
        if (x.length>0 && x[0].getElementsByTagName(fieldVal)[0]!=null)
          {
                if (x[0].getElementsByTagName(fieldVal)[0].firstChild.nodeValue!='#ERR#')
                {
                    if (ZoneRadCombo!=null)
                        ZoneRadCombo.set_value(x[0].getElementsByTagName(fieldVal)[0].firstChild.nodeValue);
                 }
                 else 
                 {
                     ZoneRadCombo.set_value('');
                 }
                 
                if (x[0].getElementsByTagName(fieldText)[0].firstChild.nodeValue!='#ERR#')
                {
                    if (ZoneRadCombo!=null)
                        ZoneRadCombo.set_text(x[0].getElementsByTagName(fieldText)[0].firstChild.nodeValue);
                 }
                 else 
                 {
                     ZoneRadCombo.set_text('');
                 }
                 
           }
            else ZoneRadCombo.set_text('');
      }
}
/******************************/    
function ValidateDate(txtDate) {
    var EnteredDate = txtDate.value;
    var IsValid = true;
    if (EnteredDate != '') //Not empty
    {
        var dates_arr = EnteredDate.split("/");
        if (dates_arr.length != 3) {
            IsValid = false;
        }
        else {
            var year = parseInt(dates_arr[0], 10);
            var month = parseInt(dates_arr[1], 10);
            var day = parseInt(dates_arr[2], 10);
            if (isNaN(year) || isNaN(month) || isNaN(day) || year < 1287 || year > 1450 || month > 12 || month < 1 || day > 31 || day < 1 || (month > 6 && day > 30))
                IsValid = false;
            else
            {
                //سیستم کبیسه خورشیدی : باقیمانده تقسیم سال به 33 یکی از این عدد ها باشد : 1 و 5 و 9 و 13 و 17 و 22 و 26 و 30
                var kabise = year % 33;
                switch (kabise)
                {
                    case 1:
                    case 5:
                    case 9:
                    case 13:
                    case 17:
                    case 22:
                    case 26:
                    case 30:
                    break;
                    default: //kabise nist
                        if (month == 12 && day > 29) IsValid = false; 
                    break;
                }
            }
        }
        
    }
    if (!IsValid)
    {
        alert('تاریخ وارد شده صحیح نیست. لطفاً دوباره وارد کنید');
        txtDate.value='';
    }
    return IsValid;
}

/* END VALIDATION FUNCTIONS */
/****************************/
function ChangeEnc(str)
{
    var Result = "";
    for (var i = 0; i <= str.toString().length; i++)
    {
        var CurChar = str.toString().substring(i, 1);
        switch (CurChar)
        {
            case "0":
                Result += "۰";
                break;
            case "1":
                Result += "۱";
                break;
            case "2":
                Result += "۲";
                break;
            case "3":
                Result += "۳";
                break;
            case "4":
                Result += "۴";
                break;
            case "5":
                Result += "۵";
                break;
            case "6":
                Result += "۶";
                break;
            case "7":
                Result += "۷";
                break;
            case "8":
                Result += "۸";
                break;
            case "9":
                Result += "۹";
                break;
            default:
                Result += CurChar;
                break;
        }
    }
    return Result;
} 

function CustomValidateCombo(source, clientside_arguments) 
{
    var combo = document.getElementById(source.controltovalidate);
    clientside_arguments.IsValid = combo !=null && combo.value != '-1';       
}

function getInternetExplorerVersion()
// Returns the version of Internet Explorer or a -1
// (indicating the use of another browser).
{
    var rv = -1; // Return value assumes failure.
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}