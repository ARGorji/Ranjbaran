var langFarsi = true;
var langFarsi2 = true;
var langFarsi3 = true;
var farsikey = [

   0x0020, // " "

   0x0021, // "!"

   0x061B, // ";"

   0x066B, // ","

   0x00A4, // " "

   0x066A, // "%"

   0x060C, // "،"

   0x06AF, // "گ"

   0x0029, // "("

   0x0028, // ")"

   0x002A, // "*"

   0x002B, // "+"

   0x0648, // "و"

   0x002D, // "-"

   0x002E, // "."

   0x002F, // "/"

   0x06F0, // "۰"

   0x06F1, // "۱"

   0x06F2, // "۲"

   0x06F3, // "۳"

   0x06F4, // "۴"

   0x06F5, // "۵"

   0x06F6, // "۶"

   0x06F7, // "۷"

   0x06F8, // "۸"

   0x06F9, // "۹"

   0x003A, // ":"

//0x0643, // "ك"

   0x06A9, // "ک"

   0x003E, // "<"

   0x003D, // "="

   0x003C, // ">"

   0x061F, // "?"

   0x066C, // "،"

   0x0624, // "ؤ"

   0x200C, // " "

   0x0698, // "ژ"

   0x0649, // "ی"

   0x064D, // " "

   0x0625, // " "

   0x0623, // "ًٌٍإ"

   0x0622, // " "

   0x0651, // " "

   0x0629, // "ًٌة"

   0x00BB, // "«"

   0x00AB, // "»"

   0x0621, // "ء"

   0x004E, // "N"

   0x005D, // "["

   0x005B, // "]"

   0x0652, // " "

   0x064B, // " "

   0x0626, // "ئ"

   0x064F, // " "

   0x064E, // " "

   0x0056, // "V"

   0x064C, // " "

   0x0058, // "X"

   0x0650, // " "

   0x0643, // "ك"

   0x062C, // "ج"

   0x005C, // "\"

   0x0686, // "چ"

   0x00D7, // "x"

   0x0640, // "-"

   0x200D, // " "

   0x0634, // "ش"

   0x0630, // "ذ"

   0x0632, // "ز"

   0x06CC, // "ی"

// 0x064A = ي

   0x062B, // "ث"

   0x0628, // "ب"

   0x0644, // "ل"

   0x0627, // "ا"

   0x0647, // "ه"

   0x062A, // "ت"

   0x0646, // "ن"

   0x0645, // "م"

//0x067E, // "پ"

   0x0626, // "ئ"

   0x062F, // "د"

   0x062E, // "خ"

   0x062D, // "ح"

   0x0636, // "ض"

   0x0642, // "ق"

   0x0633, // "س"

   0x0641, // "ف"

   0x0639, // "ع"

   0x0631, // "ر"

   0x0635, // "ص"

   0x0637, // "ط"

   0x063A, // "غ"

   0x0638, // "ظ"

   0x007D, // "{"

   0x007C, // "|"

   0x007B, // "}"

   0x007E  // "~"

   ];
farsibtnObj =new Image()
englishbtnObj =new Image()

farsibtnObj.src="../images/farsibtn.gif" 
englishbtnObj.src="../images/englishbtn.gif" 

function ChangeLang(){
	langFarsi=!langFarsi;
	document.images.langimg.src=langFarsi?farsibtnObj.src : englishbtnObj.src;
	return ;
}

function ChangeLang2(){
	langFarsi2=!langFarsi2;
	document.images.langimg2.src=langFarsi2?"../images/farsibtn.gif" : "images/englishbtn.gif";
	return ;
}

function ChangeLang3(){
	langFarsi3=!langFarsi3;
	document.images.langimg3.src=langFarsi3?"../images/farsibtn.gif" : "images/englishbtn.gif";
	return ;
}


function FaKeyPress() {

    var key = window.event.keyCode;



    if (key == 13) { window.event.keyCode = 13; return true; }

    if (langFarsi == 1) { // If Farsi

        if (key == 0x0020 && window.event.shiftKey) // Shift-space -> ZWNJ

            window.event.keyCode = 0x200C;

        else

            window.event.keyCode = farsikey[key - 0x0020];

        if (farsikey[key - 0x0020] == 92) {

            window.event.keyCode = 0x0698;

        }

        if (farsikey[key - 0x0020] == 8205) {

            window.event.keyCode = 0x067E;

        }

    }

    return true;

}

function FaKeyPressWithEnglishNumbers() {
   var key = window.event.keyCode;
   if ((key == 13 || key == 27 || key == 8) || ((key >= 48) && (key <= 57)))
      return;

   if (key < 0x0020 || key >= 0x00FF){
      alert("لطفا صفحه کلید خود را در حالت لاتین قرار دهید.");
      window.event.keyCode ="";
      return;
   }
   if (langFarsi) {
      var el = event.srcElement;
      var objRegExp = new RegExp("[A-Za-z\x27\x2C\x3B\x5B\x5C\x5D\x7C]");
      var validate_key = objRegExp.test(String.fromCharCode(key));
      if ((validate_key || (key==92)) && (key != 0x200C) && (el.value.lastIndexOf(String.fromCharCode(1740)) == el.value.length - 1) && el.value.length > 0) {
         el.value = el.value.slice(0, -1);
         el.value += String.fromCharCode(1610);
      }
      if (key == 0x0020 && window.event.shiftKey)
         window.event.keyCode = 0x200C;
      else
         window.event.keyCode = farsikey[key - 0x0020];
   }
   return true;
}

function FaKeyPress2() {
   var key = window.event.keyCode;
   if (key == 13 || key == 27 || key == 8)
      return;

   if (key < 0x0020 || key >= 0x00FF){
      alert("Please change your windows language to English");
      window.event.keyCode ="";
      return;
   }
   if (langFarsi2) {
      var el = event.srcElement;
      var objRegExp = new RegExp("[A-Za-z\x27\x2C\x3B\x5B\x5C\x5D\x7C]");
      var validate_key = objRegExp.test(String.fromCharCode(key));
      if ((validate_key || (key==92)) && (key != 0x200C) && (el.value.lastIndexOf(String.fromCharCode(1740)) == el.value.length - 1) && el.value.length > 0) {
         el.value = el.value.slice(0, -1);
         el.value += String.fromCharCode(1610);
      }
      if (key == 0x0020 && window.event.shiftKey)
         window.event.keyCode = 0x200C;
      else
         window.event.keyCode = farsikey[key - 0x0020];
   }
   return true;
}

function FaKeyPress3() {
   var key = window.event.keyCode;
   if (key == 13 || key == 27 || key == 8)
      return;

   if (key < 0x0020 || key >= 0x00FF){
      alert("Please change your windows language to English");
      window.event.keyCode ="";
      return;
   }
   if (langFarsi3) {
      var el = event.srcElement;
      var objRegExp = new RegExp("[A-Za-z\x27\x2C\x3B\x5B\x5C\x5D\x7C]");
      var validate_key = objRegExp.test(String.fromCharCode(key));
      if ((validate_key || (key==92)) && (key != 0x200C) && (el.value.lastIndexOf(String.fromCharCode(1740)) == el.value.length - 1) && el.value.length > 0) {
         el.value = el.value.slice(0, -1);
         el.value += String.fromCharCode(1610);
      }
      if (key == 0x0020 && window.event.shiftKey)
         window.event.keyCode = 0x200C;
      else
         window.event.keyCode = farsikey[key - 0x0020];
   }
   return true;
}
