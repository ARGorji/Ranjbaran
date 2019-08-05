// -_-_-_-_-_-_-_-_-_-_-_-_-  FARSI TYPE V1.2  -_-_-_-_-_-_-_-_-_-_-_-_-
//
//  to type farsi in web forms in Internet Explorer, Gecko family (Mozilla/FireFox) and now in Opera.
//  This script was created by k@\/Eh Ahmadi.
//  kaveh[at]ashoob[dot]net.
//
// Original idea is from:
// who really knows? tell us!
//
// Mozilla solving problem idea is from:
// http://forum.persiantools.com/showthread.php?p=667351
//
// auto Creation idea is from:
// http://ip.webkar.com/forums/index.php?act=ST&f=19&t=99
// with a look at behdad.org/editor and blogfa.com farsi solutions!
//
// ALL CODES HAVE BEEN EXPANDED BY ME!
// if you want to use this code PLEASE send me a note  and please keep this note intact
// (C) 2002-2006,
//
// -_-_-_-_-_-_-_-_-_-_-_-_-  ChangeLog  -_-_-_-_-_-_-_-_-_-_-_-_-
//
// V 1.1
//   - Some little Gecko fixes
// V 1.2
//   - opera full support added
//
// -_-_-_-_-_-_-_-_-_-_-_-_-  Usage Sample  -_-_-_-_-_-_-_-_-_-_-_-_-
//
// include the "farsitype.js" file to your <head> tag:
// <script language="javascript" src="farsitype.js" type="text/javascript">
//
// add "lang" attribute with value "fa" to any <input> or <textarea> tags that you want to have FarsiType!
// <input type="text" name="whatever" lang="fa" /> or <textarea cols="30" rows="7" name="whatever" lang="fa"></textarea>
// (only the lang="fa" is important for FarsiType!)
//
// you can also have an enable/disable checkbox. (it's not necessary)
// <input  type="checkbox" id="disableFarsiType" />
//
// you can use F8 keybord button to change language instead of the change language button.


// insertAdjacentHTML(), insertAdjacentText() and insertAdjacentElement() for Netscape 6/Mozilla by Thor Larholm me@jscript.dk
if(typeof HTMLElement!="undefined" && ! HTMLElement.prototype.insertAdjacentElement) {
	HTMLElement.prototype.insertAdjacentElement = function (where,parsedNode) {
		switch (where) {
			case 'beforeBegin':
				this.parentNode.insertBefore(parsedNode,this)
				break;
			case 'afterBegin':
				this.insertBefore(parsedNode,this.firstChild);
				break;
			case 'beforeEnd':
				this.appendChild(parsedNode);
				break;
			case 'afterEnd':
				if (this.nextSibling)
					this.parentNode.insertBefore(parsedNode,this.nextSibling);
				else
					this.parentNode.appendChild(parsedNode);
				break;
		}
	}

	HTMLElement.prototype.insertAdjacentHTML = function (where,htmlStr) {
		var r = this.ownerDocument.createRange();
		r.setStartBefore(this);
		var parsedHTML = r.createContextualFragment(htmlStr);
		this.insertAdjacentElement(where,parsedHTML)
	}

	HTMLElement.prototype.insertAdjacentText = function (where,txtStr) {
		var parsedText = document.createTextNode(txtStr)
		this.insertAdjacentElement(where,parsedText)
	}
}

var FarsiType =
{
	// Farsi keyboard map based on Iran Popular Keyboard Layout
	farsiKey : [
   0x0020, 0x0021, 0x061B, 0x066B, 0x00A4, 0x066A, 0x060C, 0x06AF,
   0x0029, 0x0028, 0x002A, 0x002B, 0x0648, 0x002D, 0x002E, 0x002F,
   0x0030, 0x0031, 0x0032, 0x0033, 0x0034, 0x0035, 0x0036, 0x0037,
   0x0038, 0x0039, 0x003A, 0x0643, 0x003E, 0x003D, 0x003C, 0x061F,
   0x066C, 0x0624, 0x200C, 0x0698, 0x0649, 0x064D, 0x0625, 0x0623,
   0x0622, 0x0651, 0x0629, 0x00BB, 0x00AB, 0x0621, 0x004E, 0x005D,
   0x005B, 0x0652, 0x064B, 0x0626, 0x064F, 0x064E, 0x0056, 0x064C,
   0x0058, 0x0650, 0x0643, 0x062C, 0x067E, 0x0686, 0x00D7, 0x0640,
   0x200D, 0x0634, 0x0630, 0x0632, 0x064A, 0x062B, 0x0628, 0x0644,
   0x0627, 0x0647, 0x062A, 0x0646, 0x0645, 0x067E, 0x062F, 0x062E,
   0x062D, 0x0636, 0x0642, 0x0633, 0x0641, 0x0639, 0x0631, 0x0635,
   0x0637, 0x063A, 0x0638, 0x007D, 0x007C, 0x007B, 0x007E ],
	Type : true,
	counter : 0
}

FarsiType.enable_disable = function(Dis) {
	var invis, obj;

	if (!Dis.checked)  {
		FarsiType.Type = true;
		invis = 'visible';
	}
	else {
		FarsiType.Type = false;
		invis = 'hidden';
	}

	for (var i=1; i<= FarsiType.counter; i++) {
		obj = document.getElementById('FarsiType_button_' + i);
		obj.style.visibility = invis;
	}
}

FarsiType.init = function () {
    var Inputs = document.getElementsByTagName('INPUT');
    for (var i = 0; i < Inputs.length; i++) {
        if (Inputs[i].type.toLowerCase() == 'text' && Inputs[i].lang.toLowerCase() == 'fa') {
            FarsiType.counter++;
            new FarsiType.KeyObject(Inputs[i], FarsiType.counter);
        }
    }

    var Areas = document.getElementsByTagName('TEXTAREA');
    for (var i = 0; i < Areas.length; i++) {
        if (Areas[i].lang.toLowerCase() == 'fa') {
            FarsiType.counter++;
            new FarsiType.KeyObject(Areas[i], FarsiType.counter);
        }
    }

    var Dis = document.getElementById('disableFarsiType')
    if (Dis != null) {
        FarsiType.enable_disable(Dis);
        Dis.onclick = new Function("FarsiType.enable_disable (this);")
    }
}

FarsiType.KeyObject = function (z, x) {

    z.insertAdjacentHTML("afterEnd", "<div style='display:none'><input type='button' id=FarsiType_button_" + x + " style='border: none; background-image:none;background-color:gray; font-size:10; color:white; font-family:tahoma; padding: 1px; margin: 1px; width: auto; height: auto;' value='FA' /></div>");
    z.bottelm = document.getElementById('FarsiType_button_' + x);

    z.farsi = true;
    z.style.textAlign = "right";
    z.style.direction = "rtl";
    z.bottelm.title = 'Change lang to english';

    setSelectionRange = function (input, selectionStart, selectionEnd) {
        input.focus()
        input.setSelectionRange(selectionStart, selectionEnd)
    }

    isChangeLang = function (e) {
        if (e == null) e = window.event;
        var key = e.keyCode ? e.keyCode : e.charCode;
        if (key == 119) ChangeLang();
    }

    ChangeLang = function () {
        if (z.farsi) {
            z.style.textAlign = "left";
            z.style.direction = "ltr";
            z.farsi = false;
            z.bottelm.value = "EN";
            z.bottelm.title = 'Change lang to persian'
        }
        else {
            z.style.textAlign = "right";
            z.style.direction = "rtl";
            z.farsi = true;
            z.bottelm.value = "FA";
            z.bottelm.title = 'Change lang to english'
        }
        z.focus();
    }

    Convert = function (e) {

        if (FarsiType.Type) {
            if (e == null) e = window.event;
            eElement = (e.srcElement) ? e.srcElement : e.originalTarget;

            var key = e.keyCode ? e.keyCode : e.charCode;
            if (navigator.userAgent.toLowerCase().indexOf('opera') > -1) key = e.which;

            if ((e.charCode != null) && (e.charCode != key)) return;
            if (e.ctrlKey || e.altKey || e.metaKey || key == 13 || key == 27 || key == 8) return;

            //check windows lang

            // if Farsi
            if (z.farsi && key > 31 && key < 128) {

                //check CpasLock
                if ((key >= 65 && key <= 90) && !e.shiftKey) {
                    alert("Caps Lock is On. To prevent entering farsi incorrectly, you should press Caps Lock to turn it off.");
                    return false;
                }
                else if ((key >= 97 && key <= 122) && e.shiftKey) {
                    alert("Caps Lock is On. To prevent entering farsi incorrectly, you should press Caps Lock to turn it off.");
                    return false;
                }

                // Shift-space -> ZWNJ
                if (key == 32 && e.shiftKey)
                    key = 8204;
                else
                    key = FarsiType.farsiKey[key - 32];

                // to farsi
                try {
                    // IE
                    e.keyCode = key
                }
                catch (error) {
                    try {
                        // Gecko before
                        e.initKeyEvent("keypress", true, true, document.defaultView, false, false, true, false, 0, key, eElement);
                    }
                    catch (error) {
                        try {
                            // Gecko & Opera now
                            var nScrollTop = eElement.scrollTop;
                            var nScrollLeft = eElement.scrollLeft;
                            var nScrollWidth = eElement.scrollWidth;

                            replaceString = String.fromCharCode(key);

                            var selectionStart = eElement.selectionStart;
                            var selectionEnd = eElement.selectionEnd;
                            eElement.value = eElement.value.substring(0, selectionStart) + replaceString + eElement.value.substring(selectionEnd);
                            setSelectionRange(eElement, selectionStart + replaceString.length, selectionStart + replaceString.length);

                            var nW = eElement.scrollWidth - nScrollWidth;
                            if (eElement.scrollTop == 0) { eElement.scrollTop = nScrollTop }

                            e.preventDefault()
                        }
                        catch (error) {
                            // else no farsi type!
                            alert('Sorry! no FarsiType support')
                            FarsiType.Type = false;

                            var Dis = document.getElementById('disableFarsiType')
                            if (Dis != null) {
                                Dis.disabled = true;
                                Dis.checked = true;
                            }

                            for (var i = 1; i <= FarsiType.counter; i++) {
                                document.getElementById('FarsiType_button_' + i).style.visibility = 'hidden';
                            }
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }

    z.bottelm.onmouseup = ChangeLang;
    z.onkeydown = isChangeLang;
    z.onkeypress = Convert;
}

if (window.attachEvent) {
	window.attachEvent('onload', FarsiType.init)
}
else if (window.addEventListener) {
	window.addEventListener('load', FarsiType.init, false)
}