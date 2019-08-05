var xmlHttp

function GetXmlHttpObject() {
    var xmlHttp = null;
    try {
        // Firefox, Opera 8.0+, Safari
        xmlHttp = new XMLHttpRequest();
    }
    catch (e) {
        // Internet Explorer
        try {
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        }
    }
    return xmlHttp;
}

function RemoveUserFromApp() {
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }

    var url = "frmAjax.aspx?qParam=RemoveCurrentUser";

    xmlHttp.onreadystatechange = stateChangedUnload;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function stateChangedUnload() {
}

function showMessage() {
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }

    var url = "frmAjax.aspx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function stateChanged() {
    if (xmlHttp.readyState == 4) {
        var strRecievedMessage = xmlHttp.responseText;

        if (strRecievedMessage != "") {
            var url = "Forms/Message/ShowNewMessage.aspx";
            url = url + "?qParam=" + strRecievedMessage;

            window.open(url, "", 'width=600, height=450, toolbar=0, scrollbars=1, resizable =0, status=0');
        }
    }
}

function RejectUser() {
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp == null) {
        alert("Your browser does not support AJAX!");
        return;
    }

    var url = "frmAjax.aspx?qParam=RejectUsers";

    xmlHttp.onreadystatechange = RejectStateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
}

function RejectStateChanged() {
    if (xmlHttp.readyState == 4) {
        var strUCode = xmlHttp.responseText;

        if (strUCode != "") {
            window.location = "Logout.aspx";
        }
    }
}
