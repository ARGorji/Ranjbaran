document.write("<div class=\"cHiddenPanel\" id=\"ListPanel\" style=\"width:290px;position:absolute;\"><table border='0' cellpadding='0' cellspacing='0'><tr><td class='cWin6LeftTop'></td><td class='cWin6MiddleTop'></td><td class='cWin6RightTop'></td></tr><tr><td class='cWin6LeftMiddle'></td><td style='background-color:white' id='ListPanelTD'></td><td class='cWin6RightMiddle'></td></tr><tr><td class='cWin6LeftBot'></td><td class='cWin6MiddleBot'></td><td class='cWin6RightBot'></td></tr></table></div>");
document.write("<div class=\"cHiddenPanel\" id=\"FilterPanel\"></div>");
document.write("<div class=\"cHiddenPanel\" id=\"FieldPanel\"></div>");
document.write("<div class=\"cHiddenPanel\" id=\"ExportPanel\"></div>");

if (window.Node)
    Node.prototype.removeNode = function (removeChildren) {
        var self = this;
        if (Boolean(removeChildren)) {
            return this.parentNode.removeChild(self);
        }
        else {
            var range = document.createRange();
            range.selectNodeContents(self);
            return this.parentNode.replaceChild(range.extractContents(), self);
        }
    }
function XMLBrowse(BID, SMode) {
    this.http_request = null;
    this.BaseID = null;
    this.OrderIndex = 1;
    this.CurPage = 1;
    this.RowsPerPage = 10;
    this.Repeat = 1;
    this.Order = "Code";
    this.OldOrder = -1;
    this.CurrentRow = null;
    this.CellColor = "#ffffff";
    this.text = "";
    this.MainObj = null;
    this.TblObj = null;
    this.TblPaging = null;
    this.BoardObj = null;
    this.Keyword = "";
    this.ActiveCell = 0;
    this.TblDataPaging = null;
    this.AbsPath = '../';
    this.ShowMode = 'Browse';
    this.sUrl = "";
    this.LoadingObj = null;
    this.EditForm = null;
    this.ViewForm = null;
    this.CurrentForm = null;
    this.MasterCode = "";
    this.LabelName = "";
    this.ObjLable = null;
    this.ShowLableName = true;
    this.ClassInstanceName = "BrowseObj1";
    this.DisObj = null;
    this.AllDataCell = null;
    this.PagingCell = null;
    this.RecordCount = 0;
    this.SavedRepeat = null;
    this.TotalRowCount = null;
    this.CellNum = 0;
    this.DeleteMode = false;
    this.FilterObj = null;
    this.FilterTable = null;
    this.FilterIndex = '';
    this.FilterColumns = '';
    this.EditWidth = 500;
    this.EditHeight = 300;
    this.FieldObj = null;
    this.FieldTable = null;
    this.ReGenerateFields = false;
    this.ConditionCode = '';
    this.ViewEdit = 'Edit';
    this.SearchOperand = 'AND';
    this.CurReqUrl = '';
    this.ViewName = '';
    this.AccessVal = 0;
    this.MessageCell = null;
    this.LoadingCell = null;
    this.ShowFieldList = '';
    this.CurCookie = null;
    this.CheckFieldCount = 0;
    this.TotalWidth = 0;
    this.BrowseWidth = null;
    this.BrowseHeight = null;
    this.ForceViewPage = 0;
    this.DataContainer = null;
    this.ExportPanelObj = null;
    this.ExportTable = null;
    this.qStr = null;
    this.ShowGoToPage = false;
    this.ShowExportButton = true;
    this.NodeIndex = 0;

    var ClassObj = this;
    this.UpdateVal = function () {
        if (ClassObj.http_request != null) {
            if (ClassObj.http_request.readyState == 4) {
                if (ClassObj.http_request.status == 200) {
                    if (ClassObj.LoadingCell != null)
                        ClassObj.LoadingCell.className = 'cHidden'

                    result = ClassObj.http_request.responseText
                    ResultPrefix = result.substring(0, 8)
                    if (ResultPrefix == "Message:") {
                        alert(result)

                        if (ClassObj.LoadingObj != null) {
                            ClassObj.LoadingObj.style.display = 'none'
                            return;
                        }
                        else {
                            alert('بروز خطا')
                            return;
                        }
                    }
                    ClassObj.text = result;
                    if (ClassObj.LoadingObj != null)
                        ClassObj.LoadingObj.style.display = 'none'

                    ClassObj.GenerateTable(ClassObj)
                    ClassObj.GeneratePaging(ClassObj)
                    document.body.style.cursor = 'auto';
                }
            }
        }
    }

    if (SMode != undefined)
        this.ShowMode = SMode
    this.BaseID = BID;
    this.makeRequest = function (url, Func, Method, Param, Order) {
        if (window.XMLHttpRequest) {
            this.http_request = new XMLHttpRequest();
        }
        else if (window.ActiveXObject) {
            this.http_request = new ActiveXObject('Microsoft.XMLHTTP');
        }
        if (url.indexOf("?") == -1)
            url = url + '?rnd=' + Math.random();
        else
            url = url + '&rnd=' + Math.random();
        if (url.indexOf("&ViewName=") == -1)
            url = url + '&ViewName=' + ClassObj.ViewName

        if (!ClassObj.DeleteMode) {
            this.http_request.onreadystatechange = this.UpdateVal; //Func
            ClassObj.CurReqUrl = url;
        }
        else {

            this.http_request.onreadystatechange = this.DeleteDone;
            ClassObj.DeleteMode = false;
        }
        if (ClassObj.LoadingCell != null)
            ClassObj.LoadingCell.className = 'cGridLoading'

        this.http_request.open(Method, url, true);
        if (Method == 'GET')
            this.http_request.send(null);
        else
            this.http_request.send(Param);
    }

    this.Reload = function () {
        if (ClassObj.CurReqUrl.indexOf("CurPage=", 0) != -1) {
            ClassObj.CurReqUrl = ClassObj.CurReqUrl.replace("CurPage=", "OldPage="); //to remove curpage after refresh and go to firstpage
        }
        ClassObj.makeRequest(ClassObj.CurReqUrl, null, 'GET', null, null);
    }

    this.CancelFilter = function () {
        ClassObj.FilterColumns = ''
        ClassObj.Keyword = '';
        ClassObj.ConditionCode = '';

        if (ClassObj.CurReqUrl.indexOf("CurPage=", 0) != -1) {
            ClassObj.CurReqUrl = ClassObj.CurReqUrl.replace("CurPage=", "OldPage="); //to remove curpage after cancel filter and go to firstpage
        }
        if (ClassObj.CurReqUrl.indexOf("FilterClm=", 0) != -1) {
            ClassObj.CurReqUrl = ClassObj.CurReqUrl.replace("FilterClm=", "OldFilterClm="); //to remove filter after cancel filter
        }
        ClassObj.makeRequest(ClassObj.CurReqUrl, null, 'GET', null, null);
    }

    this.setStyle = function (obj, attrName, XMLAttrName, PassIndex) {
        AttrVal = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[PassIndex].getAttribute(XMLAttrName)
        if (AttrVal != null) {
            if (attrName == 'backgroundColor')
                obj.style.backgroundColor = AttrVal;
            if (attrName == 'direction')
                obj.setAttribute('dir', AttrVal);
            if (attrName == 'textAlign')
                obj.style.textAlign = AttrVal;
            if (attrName == 'width')
                obj.setAttribute('width', AttrVal);
        }
    }

    this.GetRealColName = function (OrderIndex) {
        if (this.CheckBrowser() == "FireFox")
            CurNode = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[2 * (OrderIndex - 1) + 1];
        else
            CurNode = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[OrderIndex - 1];

        if (CurNode.tagName != undefined)
            ColName = CurNode.getAttribute("name")
        else
            ColName = "";
        return ColName;
    }
    this.ShowFilterToolbar = function (e) {
        var evt = window.event || e
        if (ClassObj.TblObj.rows[1].className == 'cVisible') {
            ClassObj.TblObj.rows[1].className = 'cHidden';
            if (this.CheckBrowser() == "FireFox") {
                for (c = 0; c < ClassObj.TblObj.rows[1].cells.length; c++)
                    ClassObj.TblObj.rows[1].cells[c].className = 'cHidden';
            }
        }
        else {
            ClassObj.TblObj.rows[1].className = 'cVisible';
            for (c = 0; c < ClassObj.TblObj.rows[1].cells.length; c++)
                ClassObj.TblObj.rows[1].cells[c].className = 'cVisible';
        }
    }

    this.ShowFieldListToolbar = function (e) {
        ClassObj.FieldObj = document.getElementById("FieldPanel");
        var evt = window.event || e
        if (evt && evt.stopPropagation) //if stopPropagation method supported
            evt.stopPropagation()
        else
            evt.cancelBubble = true
        X = evt.clientX;
        Y = evt.clientY;
        OpenX = X - 32 + document.body.scrollLeft;
        if (true || this.CheckBrowser() == "FireFox")
            OpenX += "px";

        ClassObj.FieldObj.style.position = "absolute";
        ClassObj.FieldObj.style.left = OpenX;

        if (ClassObj.FieldObj != null) {
            if (ClassObj.FieldObj.childNodes.length > 0) {
                ChildLen = ClassObj.FieldObj.childNodes.length;
                for (rem = 0; rem < ChildLen; rem++) {
                    if (this.CheckBrowser() == "FireFox")
                        ClassObj.FieldObj.removeChild(ClassObj.FieldObj.childNodes[0]);
                    else
                        ClassObj.FieldObj.childNodes[0].removeNode(true);
                }
            }
        }
        ClassObj.FieldObj.appendChild(ClassObj.FieldTable);
        ClassObj.FieldObj.style.display = "block";
        OpenY = Y - 17 + document.body.scrollTop + document.documentElement.scrollTop - ClassObj.FieldTable.offsetHeight
        if (true || this.CheckBrowser() == "FireFox")
            OpenY += "px";
        ClassObj.FieldObj.style.top = OpenY;

    }

    this.HasAccess = function (Val) {
        return (Val & ClassObj.AccessVal)
    }

    this.IsInCurrentFilter = function (ColumnName) {
        FilterColArray = ClassObj.FilterColumns.split(';');
        for (i = 0; i < FilterColArray.length; i++) {
            if (FilterColArray[i] == ColumnName)
                return true;
        }
        return false;
    }

    this.RemoveColFromFilter = function (ColumnName) {
        NewFilterColumns = "";
        NewKeyword = "";
        NewConditionCode = "";

        FilterColArray = ClassObj.FilterColumns.split(';');
        FilterKeywordArray = ClassObj.Keyword.split(';');
        FilterConditionCodeArray = (ClassObj.ConditionCode + '').split(';');

        for (i = 0; i < FilterColArray.length; i++) {
            if (FilterColArray[i] != ColumnName) {
                NewFilterColumns = NewFilterColumns + ';' + FilterColArray[i];
                NewKeyword = NewKeyword + ';' + FilterKeywordArray[i];
                NewConditionCode = NewConditionCode + ';' + FilterConditionCodeArray[i];
            }
        }
        ClassObj.FilterColumns = NewFilterColumns
        ClassObj.Keyword = NewKeyword;
        ClassObj.ConditionCode = NewConditionCode;
    }

    this.DoFilter = function (ConditionCode, Keyword) {
        if (Keyword == undefined)
            CurrentKeyword = CorrectText(ClassObj.TblObj.rows[1].cells[ClassObj.FilterIndex].childNodes[0].rows[0].cells[0].childNodes[0].value)
        else
            CurrentKeyword = Keyword;

        if (this.CheckBrowser() == "FireFox")
            CorrectIndex = 2 * ClassObj.FilterIndex + 1;
        else
            CorrectIndex = ClassObj.FilterIndex;

        if (ConditionCode == -1) {

            CurColName = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[CorrectIndex].getAttribute("name");

            ClassObj.RemoveColFromFilter(CurColName);
            qStr = 'BaseID=' + this.BaseID + '&Order=' + this.Order + '&OldOrder=' + this.OldOrder + '&Repeat=' + this.Repeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + 1 + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&ShowMode=' + this.ShowMode + '&MasterCode=' + this.MasterCode;
            ClassObj.sUrl = this.AbsPath + 'jsGetBrowse.aspx?' + qStr;
            ClassObj.FilterObj.style.display = "none"
            this.makeRequest(ClassObj.sUrl, null, 'GET', null, this.CorrectOrderIndex)

        }
        else {
            if (CurrentKeyword != "") {
                CurColName = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[CorrectIndex].getAttribute("name");

                if (ClassObj.IsInCurrentFilter(CurColName))
                    ClassObj.RemoveColFromFilter(CurColName);

                NewFilterColumns = ClassObj.FilterColumns + ';' + ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[CorrectIndex].getAttribute("name");
                NewKeyword = ClassObj.Keyword + ';' + CurrentKeyword;
                NewConditionCode = ClassObj.ConditionCode + ';' + ConditionCode;
                //                }

                ClassObj.FilterColumns = NewFilterColumns
                ClassObj.Keyword = NewKeyword;
                ClassObj.ConditionCode = NewConditionCode;

                qStr = 'BaseID=' + this.BaseID + '&Order=' + this.Order + '&OldOrder=' + this.OldOrder + '&Repeat=' + this.Repeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + 1 + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&ShowMode=' + this.ShowMode + '&MasterCode=' + this.MasterCode;
                ClassObj.sUrl = this.AbsPath + 'jsGetBrowse.aspx?' + qStr;
                ClassObj.FilterObj.style.display = "none"
                this.makeRequest(ClassObj.sUrl, null, 'GET', null, this.CorrectOrderIndex)
            }
            else
                alert(' ! کلمه جستجو خالی است')
        }
    }
    this.RemoveCols = function (ColDataType) {
        for (cc = 0; cc < ClassObj.FilterTable.rows.length; cc++) {
            ClassObj.FilterTable.rows[cc].style.display = 'block'
        }

        for (cc = 0; cc < ClassObj.FilterTable.rows.length; cc++) {
            if (ColDataType == "System.String")
                if (cc == 6 || cc == 7 || cc == 8 || cc == 9 || cc == 10 || cc == 11)
                    ClassObj.FilterTable.rows[cc].style.display = 'none'

                if (ColDataType == "System.Int16" || ColDataType == "System.Int32" || ColDataType == "System.Int64" ||
                ColDataType == "System.Single" || ColDataType == "System.Decimal" || ColDataType == "System.Byte" ||
                ColDataType == "System.Double")
                    if (cc == 1 || cc == 2 || cc == 3 || cc == 4 || cc == 5)
                        ClassObj.FilterTable.rows[cc].style.display = 'none'
                }
            }
            this.CaptureKey = function (ColIndex, TextboxObj) {
                switch (event.keyCode) {
                    case 13: //Enter
                        ClassObj.FilterIndex = ColIndex;
                        ClassObj.DoFilter(0, TextboxObj.value);
                        break;
                }
            }

            this.ShowFilterTable = function (e, ColIndex) {
                if (this.CheckBrowser() == "FireFox")
                    ColDataType = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[2 * ColIndex + 1].getAttribute("msprop:DataType")
                else
                    ColDataType = ClassObj.MainObj.childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[ColIndex].getAttribute("msprop:DataType")
                ClassObj.FilterIndex = ColIndex
                var evt = window.event || e
                if (evt && evt.stopPropagation) //if stopPropagation method supported
                {
                    evt.stopPropagation()
                }
                else
                    evt.cancelBubble = true
                X = evt.clientX;
                Y = evt.clientY;
                ClassObj.FilterObj = document.getElementById("FilterPanel");
                ClassObj.FilterObj.style.position = "absolute";
                if (ClassObj.FilterObj.childNodes.length > 0) {
                    ChildLen = ClassObj.FilterObj.childNodes.length
                    for (rem = 0; rem < ChildLen; rem++) {
                        if (this.CheckBrowser() == "FireFox")
                            ClassObj.FilterObj.removeChild(ClassObj.FilterObj.childNodes[0]);
                        else
                            ClassObj.FilterObj.childNodes[0].removeNode(true);
                    }
                }
                OpenX = X - 32 + document.body.scrollLeft;
                OpenY = Y + 2 + document.body.scrollTop + document.documentElement.scrollTop;
                if (true || this.CheckBrowser() == "FireFox") {
                    OpenX += "px";
                    OpenY += "px";
                }

                ClassObj.RemoveCols(ColDataType)
                ClassObj.FilterObj.appendChild(ClassObj.FilterTable);
                ClassObj.FilterObj.style.left = OpenX;
                ClassObj.FilterObj.style.top = OpenY;
                ClassObj.FilterObj.style.display = "block"

            }
            this.ChangeDisplayField = function (ChangeField, Disp) {
                for (r = 0; r < ClassObj.TblObj.rows.length; r++) {
                    for (c = 0; c < ClassObj.CellNum; c++) {
                        if (this.CheckBrowser() == "FireFox") {
                            CorrectCellIndex = 2 * c + 1;
                            CorrectRowIndex = r;
                        }
                        else {
                            CorrectCellIndex = c;
                            CorrectRowIndex = r;
                        }
                        CurNode = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[CorrectCellIndex];
                        FieldName = CurNode.getAttribute("name")
                        if (FieldName == ChangeField) {
                            if (this.CheckBrowser() == "FireFox") {
                                CorrectCellIndex = c;
                                if (ClassObj.TblObj.rows.length - 1 == CorrectRowIndex)
                                    break;
                            }
                            ClassObj.TblObj.rows[CorrectRowIndex].cells[CorrectCellIndex].style.display = Disp;
                            break;
                        }
                    }
                }
            }
            this.IsInFieldList = function (FName) {
                FieldArray = ClassObj.ShowFieldList.split(',')
                for (i = 0; i < FieldArray.length; i++) {
                    if (FName == FieldArray[i])
                        return true;
                }
                return false;
            }
            this.RemoveFromShowFields = function (FName) {
                Result = ''
                FieldArray = ClassObj.ShowFieldList.split(',')
                for (i = 0; i < FieldArray.length; i++) {
                    if (FName != FieldArray[i]) {
                        if (Result == '')
                            Result = FieldArray[i];
                        else
                            Result = Result + ',' + FieldArray[i]
                    }
                }
                ClassObj.ShowFieldList = Result
            }

            this.ToggleAllFields = function (checkboxObj) {
                TotalRemovedCheckboxes = 0;
                if (!checkboxObj.checked) {
                    for (rem = 0; rem < ClassObj.FieldTable.rows.length; rem++) {
                        Curcheckbox = ClassObj.FieldTable.rows[rem].cells[1].childNodes[0];
                        if (TotalRemovedCheckboxes == 0) {
                            if (ClassObj.FieldTable.rows[rem].cells[1].childNodes[0].checked) {
                                ClassObj.FieldTable.rows[rem].cells[1].childNodes[0].checked = true;
                                TotalRemovedCheckboxes++;
                            }
                        }
                        else {
                            ClassObj.FieldTable.rows[rem].cells[1].childNodes[0].checked = false;
                        }
                        ClassObj.ToggleField(Curcheckbox.FieldName, ClassObj.FieldTable.rows[rem].cells[1].childNodes[0]);
                    }
                }
                else {
                    for (rem = 0; rem < ClassObj.FieldTable.rows.length; rem++) {
                        Curcheckbox = ClassObj.FieldTable.rows[rem].cells[1].childNodes[0];
                        ClassObj.FieldTable.rows[rem].cells[1].childNodes[0].checked = true;
                        ClassObj.ToggleField(Curcheckbox.FieldName, ClassObj.FieldTable.rows[rem].cells[1].childNodes[0]);
                    }
                }
            }

            this.ToggleField = function (FName, checkboxObj) {
                ClassObj.CheckFieldCount = 0
                for (i = 0; i < ClassObj.FieldTable.rows.length; i++) {
                    if (ClassObj.FieldTable.rows[i].cells[1].childNodes[0].checked)
                        ClassObj.CheckFieldCount++;
                }
                if (checkboxObj.checked) {
                    if (!ClassObj.IsInFieldList(FName)) {
                        if (ClassObj.ShowFieldList == '')
                            ClassObj.ShowFieldList = FName;
                        else
                            ClassObj.ShowFieldList = ClassObj.ShowFieldList + ',' + FName
                    }
                    ClassObj.ChangeDisplayField(FName, "table-cell")
                }
                else {
                    if (ClassObj.CheckFieldCount == 0) {
                        alert('حداقل یک ستون باید انتخاب شده باقی بماند')
                        checkboxObj.checked = true
                        return;
                    }
                    ClassObj.RemoveFromShowFields(FName);
                    ClassObj.ChangeDisplayField(FName, "none")
                }
                if (AppName != null)
                    createCookie(AppName + ClassObj.BaseID, ClassObj.ShowFieldList, 30);
                
                if(this.ObjLable != null)
                    this.ObjLable.style.width = ClassObj.TblDataPaging.offsetWidth + "px";

            }

            this.ModifyFieldList = function (CookieFiledList) //corrects checkboxes based on cookie
            {
                for (i = 0; i < ClassObj.FieldTable.rows.length; i++) {
                    if ((CookieFiledList + ",").indexOf(ClassObj.FieldTable.rows[i].cells[1].childNodes[0].getAttribute("FieldName")) >= 0)
                        ClassObj.FieldTable.rows[i].cells[1].childNodes[0].checked = true
                    else
                        ClassObj.FieldTable.rows[i].cells[1].childNodes[0].checked = false
                }
            }
            this.GetNewRow = function (obj) {
                if (obj.tagName == "TABLE") {
                    if (this.CheckBrowser() == "FireFox")
                        return obj.insertRow(-1);
                    else
                        return obj.insertRow();
                }
            }
            this.GetNewCell = function (obj) {
                if (obj.tagName == "TR") {
                    if (this.CheckBrowser() == "FireFox")
                        return obj.insertCell(-1);
                    else
                        return obj.insertCell();
                }
            }
            this.ShowExportPanel = function (e) {
                if (ClassObj.ExportPanelObj == null)
                    ClassObj.CreateExportPanel();

                if (ClassObj.ExportPanelObj.style.display == "none") {
                    ClassObj.ExportPanelObj.className = 'ExportPanelVisible';
                    var evt = window.event || e
                    X = evt.clientX - 100;
                    Y = evt.clientY - 110;
                    if (this.CheckBrowser() == "FireFox") {
                        X = X + "px";
                        Y = Y + "px";
                    }

                    ClassObj.ExportPanelObj.style.left = X + 'px';
                    ClassObj.ExportPanelObj.style.top = Y + 'px';
                    ClassObj.ExportPanelObj.style.display = "block";
                }
                else
                    ClassObj.ExportPanelObj.style.display = "none";
            }

            this.CreateExportPanel = function () {
                ClassObj.ExportPanelObj = document.getElementById("ExportPanel");
                ClassObj.ExportPanelObj.className = 'ExportPanelHidden';

                ClassObj.ExportTable = document.createElement("TABLE");
                ClassObj.ExportTable.className = 'cFieldListTable'

                RadioObj2 = document.createElement("INPUT");
                RadioObj2.setAttribute('type', 'Radio');
                RadioObj2.setAttribute('value', '2');
                RadioObj2.setAttribute('name', 'rdoExport');
                spanObj2 = document.createElement("SPAN");
                spanObj2.innerHTML = "صفحه جاری&nbsp";

                RadioObj = document.createElement("INPUT");
                RadioObj.setAttribute('value', '1');
                RadioObj.setAttribute('name', 'rdoExport');
                RadioObj.setAttribute('type', 'Radio');
                RadioObj.setAttribute('checked', '');

                spanObj = document.createElement("SPAN");
                spanObj.innerHTML = "تمام صفحات&nbsp;&nbsp;&nbsp;&nbsp;"

                newRow = this.GetNewRow(ClassObj.ExportTable);
                newCell4 = this.GetNewCell(newRow);
                newCell4.appendChild(spanObj2);
                newCell3 = this.GetNewCell(newRow);
                newCell3.appendChild(RadioObj2);

                newCell2 = this.GetNewCell(newRow);
                newCell2.appendChild(spanObj);
                newCell1 = this.GetNewCell(newRow);
                newCell1.appendChild(RadioObj);

                RadioObj = document.createElement("INPUT");
                RadioObj.setAttribute('type', 'Radio');
                RadioObj.setAttribute('value', 'excel');
                RadioObj.setAttribute('name', 'rdoTargetExport');
                RadioObj.onclick = function () { ClassObj.DoExport() }

                spanObj = document.createElement("SPAN");
                spanObj.innerHTML = "Excel";
                newRow = this.GetNewRow(ClassObj.ExportTable);
                newCell = this.GetNewCell(newRow);
                newCell.appendChild(RadioObj);

                spanObj.className = 'ExportSelection';
                newCell = this.GetNewCell(newRow);
                newCell.appendChild(spanObj);
                newCell = this.GetNewCell(newRow);
                newCell.appendChild(RadioObj);

                RadioObj = document.createElement("INPUT");
                RadioObj.setAttribute('type', 'Radio');
                RadioObj.setAttribute('value', 'text');
                RadioObj.setAttribute('name', 'rdoTargetExport');
                RadioObj.onclick = function () { ClassObj.DoExport() }
                spanObj = document.createElement("SPAN");
                spanObj.innerHTML = "XML";
                newRow = this.GetNewRow(ClassObj.ExportTable);
                newCell = this.GetNewCell(newRow);

                spanObj.className = 'ExportSelection';
                newCell = this.GetNewCell(newRow);
                newCell.appendChild(spanObj);
                newCell = this.GetNewCell(newRow);
                newCell.appendChild(RadioObj);

                RadioObj = document.createElement("INPUT");
                RadioObj.setAttribute('type', 'Radio');
                RadioObj.setAttribute('value', 'word');
                RadioObj.setAttribute('name', 'rdoTargetExport');
                RadioObj.onclick = function () { ClassObj.DoExport() }
                spanObj = document.createElement("SPAN");
                spanObj.innerHTML = "Word";
                newRow = this.GetNewRow(ClassObj.ExportTable);
                newCell = this.GetNewCell(newRow);

                spanObj.className = 'ExportSelection';
                newCell = this.GetNewCell(newRow);
                newCell.appendChild(spanObj);
                newCell = this.GetNewCell(newRow);
                newCell.appendChild(RadioObj);

                ClassObj.ExportPanelObj.appendChild(ClassObj.ExportTable);
            }

            this.CreateFieldList = function () {
                ClassObj.FieldTable = document.createElement("TABLE");
                ClassObj.FieldTable.className = 'cFieldListTable'

                //////////////start all column selection
                FieldName = '[ALLCOULMNS]';
                CellText = 'همه ستونها';
                if (this.CheckBrowser() == "FireFox") {
                    chkObj = document.createElement("INPUT");
                    chkObj.setAttribute('type', 'checkbox');
                    chkObj.setAttribute('checked', '');
                }
                else {
                    //chkObj = document.createElement("<INPUT checked type=checkbox>");
                    chkObj = document.createElement("INPUT");
                    chkObj.setAttribute("type", "checkbox");
                }
                chkObj.setAttribute('FieldName', FieldName);
                chkObj.onclick = (
        function (e, cobj) {
            var evt = window.event || e
            if (evt != null)
                evt.cancelBubble = true;
            return function () {
                ClassObj.ToggleAllFields(cobj)
            }
        }
        )('', chkObj);
                spanObj = document.createElement("SPAN");
                spanObj.innerHTML = CellText
                newRow = this.GetNewRow(ClassObj.FieldTable);
                newCell2 = this.GetNewCell(newRow);
                newCell2.appendChild(spanObj);
                newCell1 = this.GetNewCell(newRow);
                newCell1.appendChild(chkObj);
                //////////////end all column selection

                for (j = 0; j < ClassObj.CellNum; j++) {
                    if (this.CheckBrowser() == "FireFox")
                        CurNode = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[2 * j + 1];
                    else
                        CurNode = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[j];
                    var myObj = CurNode;
                    CellText = myObj.getAttribute("msprop:Caption")
                    FieldName = myObj.getAttribute("name")
                    DisplayMode = myObj.getAttribute("msprop:DisplayMode")

                    if (DisplayMode != "Hidden") {
                        if (DisplayMode == "Visible") {
                            if (this.CheckBrowser() == "FireFox") {
                                chkObj = document.createElement("INPUT");
                                chkObj.setAttribute('type', 'checkbox');
                                chkObj.setAttribute('checked', '');
                            }
                            else {
                                //chkObj = document.createElement("<INPUT checked type=checkbox>");
                                chkObj = document.createElement("INPUT");
                                chkObj.setAttribute('type', 'checkbox');
                                chkObj.setAttribute('checked', '');
                            }
                            if (ClassObj.ShowFieldList == '')
                                ClassObj.ShowFieldList = FieldName;
                            else
                                ClassObj.ShowFieldList = ClassObj.ShowFieldList + ',' + FieldName
                        }
                        else {
                            chkObj = document.createElement("INPUT");
                            chkObj.type = "checkbox";
                        }

                        chkObj.setAttribute("FieldName", FieldName);
                        chkObj.onclick = (
		        function (e, f, cobj) {
		            var evt = window.event || e
		            if (evt != null)
		                evt.cancelBubble = true;
		            return function () {
		                ClassObj.ToggleField(f, cobj)
		            }
		        }
                )('', FieldName, chkObj);
                        spanObj = document.createElement("SPAN");
                        spanObj.innerHTML = CellText

                        newRow = this.GetNewRow(ClassObj.FieldTable);
                        newCell2 = this.GetNewCell(newRow);
                        newCell2.appendChild(spanObj);
                        newCell1 = this.GetNewCell(newRow);
                        newCell1.appendChild(chkObj);
                    }
                }
                ClassObj.ReGenerateFields = false;
            }
            this.CheckedField = function (FName) {
                for (h = 0; h < ClassObj.FieldTable.rows.length; h++) {
                    if (ClassObj.FieldTable.rows[h].cells[1].childNodes[0].getAttribute('FieldName') == FName) {
                        if (ClassObj.FieldTable.rows[h].cells[1].childNodes[0].checked)
                            return true;
                        else
                            return false;
                    }
                }
                return false;
            }
            this.GenerateTable = function () {
                if (this.CheckBrowser() == "FireFox") {
                    if (ClassObj.AllDataCell.childNodes[0] != undefined) {
                        ParentToBeDeleteNode = ClassObj.AllDataCell.childNodes[0];
                        ParentToBeDeleteNode.removeChild(ParentToBeDeleteNode.childNodes[0]);
                        Parent2 = ClassObj.AllDataCell.childNodes[0].parentNode;
                        Parent2.removeChild(Parent2.childNodes[0]);
                    }
                }
                else {
                    if (ClassObj.AllDataCell.childNodes[0] != null)
                        ClassObj.AllDataCell.childNodes[0].removeNode(true);
                }
                ClassObj.TblObj = document.createElement("TABLE");
                ClassObj.TblObj.className = "VistaGrid"
                ClassObj.TblObj.setAttribute("width", "100%")

                ClassObj.FilterObj = document.createElement("DIV");
                ClassObj.FilterTable = document.createElement("TABLE");
                ClassObj.FilterTable.className = 'cFilterTable'
                ClassObj.CreateRow(ClassObj.FilterTable, -1, 'بدون فیلتر')
                ClassObj.CreateRow(ClassObj.FilterTable, 0, 'شامل')
                ClassObj.CreateRow(ClassObj.FilterTable, 1, 'برابر با')
                ClassObj.CreateRow(ClassObj.FilterTable, 2, 'مخالف')
                ClassObj.CreateRow(ClassObj.FilterTable, 3, 'شروع شود با')
                ClassObj.CreateRow(ClassObj.FilterTable, 4, 'متنهی شود به')

                ClassObj.CreateRow(ClassObj.FilterTable, 5, 'برابر با')
                ClassObj.CreateRow(ClassObj.FilterTable, 6, 'بزرگتر از')
                ClassObj.CreateRow(ClassObj.FilterTable, 7, 'بزرگتر یا مساوی')
                ClassObj.CreateRow(ClassObj.FilterTable, 8, 'کوچکتر از')
                ClassObj.CreateRow(ClassObj.FilterTable, 9, 'کوچکتر یا مساوی')
                ClassObj.CreateRow(ClassObj.FilterTable, 10, 'مخالف')

                //ClassObj.CreateRow(ClassObj.FilterTable, 8, 'ندارد')
                //ClassObj.CreateRow(ClassObj.FilterTable , 9 , 'بین')
                //ClassObj.FilterObj.appendChild(ClassObj.FilterTable);

                ConditionObj = document.getElementById("Condition");
                if (ConditionObj != null)
                    ConditionCode = ConditionObj.options[ConditionObj.selectedIndex].value;
                else
                    ConditionCode = '';

                ClassObj.CurrentRow = 2
                if (this.CheckBrowser() == "FireFox") {
                    var xmlDoc = document.implementation.createDocument("", "", null);
                    xmlDoc.async = "false"
                    var oParser = new DOMParser();

                    xmlDoc = oParser.parseFromString(this.text, "text/xml");
                    ClassObj.NodeIndex = 1;
                }
                else {
                    var xmlDoc = new ActiveXObject("Microsoft.XMLDOM")
                    xmlDoc.async = "false"
                    xmlDoc.loadXML(this.text)
                    ClassObj.NodeIndex = 0;
                }
                ClassObj.MainObj = xmlDoc.documentElement
                if (ClassObj.MainObj == null) {
                    alert('بروز خطا در گرفتن اطلاعات');
                    return;
                }
                var myObj = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex];
                ClassObj.RecordCount = myObj.getAttribute("msprop:RecCount")
                ClassObj.CurPage = myObj.getAttribute("msprop:CurPage")
                ClassObj.Order = myObj.getAttribute("msprop:Order")
                ClassObj.SavedRepeat = myObj.getAttribute("msprop:CurRepeat")
                ClassObj.RowsPerPage = myObj.getAttribute("msprop:RowsPerPage")
                ClassObj.CellNum = myObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes.length
                ClassObj.EditForm = myObj.getAttribute("msprop:EditForm")
                ClassObj.ViewForm = myObj.getAttribute("msprop:ViewForm")
                ClassObj.LabelName = myObj.getAttribute("msprop:LabelName")
                ClassObj.ViewName = myObj.getAttribute("msprop:ViewName")
                ClassObj.AccessVal = myObj.getAttribute("msprop:AccessVal")
                ClassObj.TotalRowCount = myObj.getAttribute("msprop:TotalRowCount")

                ClassObj.EditWidth = myObj.getAttribute("msprop:EditWidth")
                ClassObj.EditHeight = myObj.getAttribute("msprop:EditHeight")

                if (this.CheckBrowser() == "FireFox")
                    ClassObj.CellNum = (parseInt(ClassObj.CellNum, 10) - 1) / 2;

                if (ClassObj.CurrentForm == null) {
                    if (ClassObj.ViewEdit == 'Edit')
                        ClassObj.CurrentForm = ClassObj.EditForm
                    else
                        ClassObj.CurrentForm = ClassObj.ViewForm
                }
                if (ClassObj.CurrentForm == "")
                    ClassObj.CurrentForm = ClassObj.EditForm

                if (AppName != null)
                    ClassObj.CurCookie = readCookie(AppName + ClassObj.BaseID);
                if (ClassObj.FieldObj == null || ClassObj.ReGenerateFields)
                    ClassObj.CreateFieldList();
                if (ClassObj.CurCookie != null) {
                    ClassObj.ShowFieldList = ClassObj.CurCookie
                    ClassObj.ModifyFieldList(ClassObj.CurCookie)
                }
                if (ClassObj.ShowMode == "List" || ClassObj.MasterCode != "") {
                    if (this.ShowLableName)
                        this.ObjLable.innerHTML = this.LabelName
                    else
                        this.ObjLable.className = 'cHidden'
                }

                ClassObj.OldOrder = ClassObj.Order

                RowCount = ClassObj.TblObj.rows.length
                for (d = 0; d < RowCount; d++) {
                    ClassObj.TblObj.deleteRow(0);
                }

                newRow = this.GetNewRow(ClassObj.TblObj);
                for (j = 0; j < ClassObj.CellNum; j++) {
                    StyleStr = ""
                    CorrectOrderIndex = j + 1;

                    if (this.CheckBrowser() == "FireFox") {
                        CorrectCellIndex = 2 * j + 1;
                    }
                    else {
                        CorrectCellIndex = j;
                    }
                    CurNode1 = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[CorrectCellIndex];

                    CellText = CurNode1.getAttribute("msprop:Caption")
                    FieldName = CurNode1.getAttribute("name")
                    DisplayMode = CurNode1.getAttribute("msprop:DisplayMode")

                    newCell = this.GetNewCell(newRow);

                    newCell.className = 'cGridHeader'
                    if (ClassObj.OldOrder == this.GetRealColName(CorrectOrderIndex)) {
                        ClassObj.Repeat = 1 - ClassObj.Repeat
                        ClassObj.passRepeat = ClassObj.Repeat
                        ImgSrc = "arrowDown.gif"
                    }
                    else {
                        ClassObj.passRepeat = 1
                        ImgSrc = "arrowUp.gif"
                    }

                    if (FieldName == ClassObj.OldOrder) {
                        if (ClassObj.SavedRepeat == 0)
                            ImgSrc = "arrowDown.gif"
                        else
                            ImgSrc = "arrowUp.gif"

                        CellText = CellText + '&nbsp;<img src=../images/Browse/' + ImgSrc + '>'
                    }
                    this.setStyle(newCell, 'backgroundColor', 'msprop:HeaderBgColor', CorrectCellIndex)
                    //                    this.setStyle(newCell, 'direction', 'msprop:Direction', CorrectCellIndex)
                    //                    this.setStyle(newCell, 'width', 'msprop:Width', CorrectCellIndex)
                    this.setStyle(newCell, 'textAlign', 'msprop:Alignment', CorrectCellIndex)
                    CellText = CellText.replace("_x0020_", " ")
                    qStr = 'BaseID=' + ClassObj.BaseID + '&Order=' + ClassObj.GetRealColName(CorrectOrderIndex) + '&OldOrder=' + ClassObj.OldOrder + '&Repeat=' + ClassObj.passRepeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + ClassObj.CurPage + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&ShowMode=' + ClassObj.ShowMode + '&MasterCode=' + ClassObj.MasterCode;
                    sUrl = ClassObj.AbsPath + 'jsGetBrowse.aspx?' + qStr;
                    CurOrder = ClassObj.GetRealColName(CorrectOrderIndex)
                    newCell.onclick = (
	        function (e, u, o) {
	            var evt = window.event || e
	            if (evt != null)
	                evt.cancelBubble = true;
	            return function () {
	                ClassObj.Order = o;
	                ClassObj.makeRequest(u, null, 'GET', null)
	            }
	        }
            )('', sUrl, CurOrder);

                    newCell.innerHTML = "<nobr>" + CellText + "</nobr>";
                    ClassObj.Repeat = ClassObj.SavedRepeat

                    if (!ClassObj.CheckedField(FieldName)) {
                        if (DisplayMode == "Hidden" || (ClassObj.ShowFieldList + ",").indexOf(FieldName + ",") == -1) {
                            newCell.style.display = 'none'
                        }
                    }

                }
                //Filter Row
                newRow = this.GetNewRow(ClassObj.TblObj);
                if (ClassObj.Keyword == "")
                    newRow.className = "cHidden"
                for (j = 0; j < ClassObj.CellNum; j++) {
                    if (this.CheckBrowser() == "FireFox")
                        CurNode2 = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[2 * j + 1];
                    else
                        CurNode2 = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[j];
                    FieldName = CurNode2.getAttribute("name")
                    DisplayMode = CurNode2.getAttribute("msprop:DisplayMode")
                    InputObj = document.createElement("INPUT")
                    InputObj.className = 'cInput';
                    InputObj.size = '10'

                    if (ClassObj.Keyword != "") {
                        if (ClassObj.CurrentColIsFiltered(FieldName)) {
                            InputObj.value = ClassObj.GetColKeyword(FieldName);
                        }
                    }
                    FilterImageObj = document.createElement("DIV")
                    FilterImageObj.className = 'cFilter';
                    FilterImageObj.onclick = (
	        function (e, fieldIndex) {
	            return function (e) {
	                var evt = window.event || e
	                if (evt != null)
	                    evt.cancelBubble = true;
	                ClassObj.ShowFilterTable(e, fieldIndex)
	            }
	        }
            )('', j);


                    newCell = this.GetNewCell(newRow);
                    ObjTableFilter = document.createElement("TABLE")
                    ObjTableFilterRow = this.GetNewRow(ObjTableFilter);
                    ObjTableFilterCell1 = this.GetNewCell(ObjTableFilterRow);
                    ObjTableFilterCell2 = this.GetNewCell(ObjTableFilterRow);

                    ObjTableFilterCell1.appendChild(InputObj);
                    ObjTableFilterCell2.appendChild(FilterImageObj);
                    newCell.appendChild(ObjTableFilter);
                    if (DisplayMode != "Hidden" && ClassObj.CheckedField(FieldName)) {
                        newCell.className = 'cGridFilter'
                    }
                    else {

                        if (this.CheckBrowser() == "FireFox") {
                            newCell.style.display = 'none'
                            //newCell.className = 'cHidden';
                        }
                        else
                            newCell.style.display = 'none'
                    }

                }

                for (i = 1; i < ClassObj.MainObj.childNodes.length; i++) {
                    if (this.CheckBrowser() == "FireFox") {
                        if (i % 2 == 0)
                            newRow = this.GetNewRow(ClassObj.TblObj);
                    }
                    else
                        newRow = this.GetNewRow(ClassObj.TblObj);
                    for (j = 0; j < ClassObj.MainObj.childNodes[i].childNodes.length; j++) {
                        CurNode3 = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[j];
                        if (CurNode3.tagName == undefined)
                            continue;
                        newCell = this.GetNewCell(newRow);
                        FieldName = CurNode3.getAttribute("name")
                        FieldWidth = CurNode3.getAttribute("msprop:Width")
                        CurNode4 = ClassObj.MainObj.childNodes[i].childNodes[j];
                        if (CurNode4.tagName == undefined)
                            continue;
                        if (this.CheckBrowser() == "FireFox")
                            CellText = CurNode4.textContent;
                        else
                            CellText = CurNode4.text;

                        if (this.CheckBrowser() == "FireFox")
                            CellText = CurNode4.textContent;
                        else
                            CellText = CurNode4.text;
                        CellText = CellText.replace("_x0020_", " ")
                        DisplayMode = CurNode3.getAttribute("msprop:DisplayMode")
                        IsKey = CurNode3.getAttribute("msprop:IsKey")

                        if (FieldWidth == null)
                            FieldWidth = '50px';
                        ClassObj.TotalWidth = ClassObj.TotalWidth + parseInt(FieldWidth, 10)

                        if (!ClassObj.CheckedField(FieldName)) {
                            if (DisplayMode == "Hidden" || (ClassObj.ShowFieldList + ",").indexOf(FieldName + ",") == -1)
                                newCell.style.display = 'none'
                        }

                        if (CellText.indexOf("\n") > 0) {
                            intFieldWidth = FieldWidth.replace("px", "")
                            intFieldWidth = parseInt(intFieldWidth, 10)
                            LineBreakPos = CellText.indexOf("\n");
                            if (LineBreakPos < intFieldWidth)
                                CellText = CellText.substr(0, LineBreakPos) + "...";
                            else
                                CellText = CellText.substr(0, intFieldWidth) + "...";
                        }

                        ValidCharCount = FieldWidth.replace("px", "") / 4.5
                        ValidCharCount = parseInt(ValidCharCount, 10)
                        //if(CellText.length > ValidCharCount)
                        //    CellText = CellText.substring(0,ValidCharCount) + "..."

                        newCell.innerHTML = CellText
                        if (i == 1)
                            newCell.className = 'GridTDOver'
                        else
                            newCell.className = 'cGridDataCell'
                        newCell.onclick = function (e) {
                            var evt = window.event || e
                            evt.cancelBubble = true;
                            ClassObj.ChangeColor(this);
                        }
                        if (ClassObj.HasAccess(2) || ClassObj.ShowMode == "List")
                            newCell.ondblclick = (
	            function (e, InnerI) {
	                var evt = window.event || e
	                if (evt != null)
	                    evt.cancelBubble = true;
	                return function () {
	                    KeyCol = ClassObj.GetKeyCollection(this.parentNode.rowIndex)
	                    if (evt != null)
	                        evt.cancelBubble = true;
	                    ClassObj.DoUpdate(InnerI, KeyCol);
	                }
	            }
                )('', i);

                        //                        this.setStyle(newCell, 'direction', 'msprop:Direction', j);
                        this.setStyle(newCell, 'textAlign', 'msprop:Alignment', j);
                        //                        this.setStyle(newCell, 'width', 'msprop:Width', j);
                        //                        newCell.noWrap = "nowrap";

                    }
                }
                ClassObj.DataContainer = document.createElement("DIV");
                ClassObj.DataContainer.id = "DataContainer";
                ClassObj.DataContainer.className = 'cDataContainer';
                ClassObj.DataContainer.appendChild(ClassObj.TblObj);


                if (ClassObj.BrowseWidth != null && ClassObj.BrowseWidth != '')
                    ClassObj.DataContainer.style.width = ClassObj.BrowseWidth + "px";
                if (ClassObj.BrowseHeight != null && ClassObj.BrowseHeight != '')
                    ClassObj.DataContainer.style.height = ClassObj.BrowseHeight + "px";


                ClassObj.AllDataCell.appendChild(ClassObj.DataContainer);
                getObj('FieldPanel').style.display = 'none';
                //alert(ClassObj.DisObj.parentNode.parentNode.parentNode.parentNode.parentNode.innerHTML);


            }

            this.GetKeyCollection = function (RowIndex) {
                if (ClassObj.CheckBrowser() == "FireFox") {
                    CorrectRowIndex = 2 * RowIndex - 1
                }
                else
                    CorrectRowIndex = RowIndex - 1
                CurCodeList = ""
                for (r = 0; r < ClassObj.CellNum; r++) {
                    IsKey = ClassObj.MainObj.childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex].childNodes[ClassObj.NodeIndex + r].getAttribute("msprop:IsKey")
                    if (IsKey == "1") {
                        if (ClassObj.CheckBrowser() == "FireFox")
                            CellText = ClassObj.MainObj.childNodes[CorrectRowIndex].childNodes[1].textContent;
                        else
                            CellText = ClassObj.MainObj.childNodes[CorrectRowIndex].childNodes[ClassObj.NodeIndex + r].text
                        CellText = CellText.replace("_x0020_", " ")
                        if (CurCodeList == "")
                            CurCodeList = CellText
                        else
                            CurCodeList = CurCodeList + ";" + CellText
                        break;
                    }
                }
                return CurCodeList
            }

            this.GetNameListCollection = function (RowIndex) {
                NameList = ""
                for (r = 0; r < ClassObj.MainObj.childNodes[RowIndex].childNodes.length; r++) {
                    IsListTitle = ClassObj.MainObj.childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[0].childNodes[r].getAttribute("msprop:IsListTitle")
                    CellText = ClassObj.MainObj.childNodes[RowIndex].childNodes[r].text
                    CellText = CellText.replace("_x0020_", " ")
                    if (IsListTitle == "1") {
                        if (NameList == "")
                            NameList = CellText
                        else
                            NameList = NameList + " " + CellText
                    }
                }
                return NameList
            }

            this.CurrentColIsFiltered = function (ColName) {
                ColArray = ClassObj.FilterColumns.split(';');
                for (i = 0; i < ColArray.length; i++) {
                    if (ColName == ColArray[i]) {
                        return true;
                    }
                }
                return false;
            }
            this.GetColKeyword = function (ColName) {
                ColArray = ClassObj.FilterColumns.split(';');
                KeyArray = ClassObj.Keyword.split(';');
                for (i = 0; i < ColArray.length; i++) {
                    if (ColName == ColArray[i])
                        return KeyArray[i];
                }
                return "";
            }

            this.GetCorrectActiveCell = function (RowObj, ClickObj) //display none cells does not count in cellIndex
            {
                ResultVal = 0;
                for (c = 0; c < RowObj.cells.length; c++) {
                    if (RowObj.cells[c] == ClickObj)
                        break
                    ResultVal++;
                }
                return ResultVal;
            }

            this.ChangeColor = function (Obj, e) {
                if (ClassObj.CurrentRow != null)
                    for (i = 0; i < ClassObj.TblObj.rows[ClassObj.CurrentRow].cells.length; i++) {
                        if (ClassObj.CurrentRow != 1)
                            ClassObj.TblObj.rows[ClassObj.CurrentRow].cells[i].className = 'cGridDataCell';
                    }

                for (i = 0; i < Obj.parentNode.cells.length; i++)
                    Obj.parentNode.cells[i].className = 'GridTDOver';

                var evt = window.event || e
                if (evt != null)
                    if (event.srcElement != null)
                        this.ActiveCell = this.GetCorrectActiveCell(event.srcElement.parentNode, event.srcElement)
                ClassObj.CurrentRow = Obj.parentNode.rowIndex
                getObj('FilterPanel').style.display = 'none';
                getObj('FieldPanel').style.display = 'none';
            }
            this.NewRecord = function () {

                Url = ClassObj.AbsPath + ClassObj.CurrentForm + "?BaseID=" + ClassObj.BaseID + "&Code=&MasterCode=" + ClassObj.MasterCode + "&InstanceName=" + ClassObj.ClassInstanceName;
                if (this.ShowMode == "List")
                    OpenWin(Url, ClassObj.EditWidth, ClassObj.EditHeight)
                else {
                    if (this.MasterCode == "")
                        window.location.href = Url;
                    else
                        OpenWin(Url, ClassObj.EditWidth, ClassObj.EditHeight)
                }
            }

            this.DoUpdate = function (RowIndex, CodeList, ForceViewOrEditForm) {
                try {
                    
                    if (ClassObj.ForceViewPage == 1)
                        ClassObj.CurrentForm = ClassObj.ViewForm;

                    if (ClassObj.CurrentForm == "")
                        ClassObj.CurrentForm = ClassObj.EditForm;

                    if (ForceViewOrEditForm != undefined) {
                        if (ForceViewOrEditForm != "")
                            ClassObj.CurrentForm = ForceViewOrEditForm;
                    }
                    ClassObj.CurrentForm = ClassObj.EditForm;

                    if (ClassObj.CurrentForm != "") {
                        if (ClassObj.MasterCode != "")
                            Url = ClassObj.AbsPath + 'Detail.aspx?UC='
                        else //detail
                            Url = ClassObj.AbsPath + 'Default.aspx?UC='
                        Url = ClassObj.AbsPath + ClassObj.CurrentForm + "?BaseID=" + ClassObj.BaseID + "&Code=" + CodeList + '&MasterCode=' + ClassObj.MasterCode + '&InstanceName=' + ClassObj.ClassInstanceName + '&Keyword=' + ClassObj.Keyword + '&FilterColumns=' + ClassObj.FilterColumns + '&ConditionCode=' + ClassObj.ConditionCode
                        
                        if (ClassObj.ViewEdit == 'View')
                            Url = Url + '&ViewMode=1'

                        if (this.ShowMode != "List") {
                            if (this.MasterCode == "")
                                window.location.href = Url
                            else
                                OpenWin(Url, ClassObj.EditWidth, ClassObj.EditHeight)
                        }
                        else {

                            FormFieldCode.value = CodeList;
                            FormFieldName.value = this.GetNameListCollection(RowIndex);
                            document.getElementById('ListPanel').style.display = "none";
                            if (FormFieldCode != null)
                                FormFieldCode.fireEvent('onChange');
                            this.ShowMode = "Browse";
                        }
                    }
                }
                catch (e) {
                    alert(e.description)
                }
            }

            this.DoExport = function () {
                AllRecords = ClassObj.ExportTable.rows[0].cells[3].childNodes[0].checked

                if (ClassObj.ExportTable.rows[1].cells[2].childNodes[0].checked)
                    ExportTarget = 'Excel';
                else if (ClassObj.ExportTable.rows[2].cells[2].childNodes[0].checked)
                    ExportTarget = 'XML';
                else if (ClassObj.ExportTable.rows[3].cells[2].childNodes[0].checked)
                    ExportTarget = 'Word';

                Params = ClassObj.CurReqUrl.replace('../../jsGetBrowse.aspx?', '');

                Params = Params + '&ShowFieldList=' + this.ShowFieldList + '&AllRecords=' + AllRecords + '&TargetExport=' + ExportTarget;

                if (Params.indexOf("rnd=") == -1)
                    Params = Params + '&rnd=' + Math.random();

                if (Params.indexOf("&CurPage=") == -1)
                    Params = Params + '&CurPage=' + ClassObj.CurPage;

                window.open('../ExportGrid.aspx?' + Params, 'ExportToExcel', 'width=900,height=600');
                ClassObj.ExportPanelObj.style.display = "none";
            }

            this.DeleteDone = function () {
                if (ClassObj.http_request.readyState == 4) {
                    if (ClassObj.http_request.status == 200) {
                        result = ClassObj.http_request.responseText
                        if (result == "DELETED") {
                            ClassObj.TblObj.rows[ClassObj.CurrentRow].className = 'cHidden'
                            ClassObj.CurrentRow = 1;
                        }
                        else if (result.indexOf("FK_") > 0)
                            alert('این رکورد دارای اطلاعات مرتبط میباشد و قابل حذف نیست')
                        if (ClassObj.LoadingCell != null)
                            ClassObj.LoadingCell.className = 'cHidden'
                        document.body.style.cursor = 'auto';
                    }
                }
            }

            this.GetLowerPageLimit = function (Offset) {
                Result = ClassObj.CurPage - Offset
                if (Result <= 0)
                    Result = 1
                return Result
            }

            this.CreateRow = function (tblFil, Val, Text) {
                newRow = this.GetNewRow(tblFil);
                newCell = this.GetNewCell(newRow);
                newCell.innerHTML = Text
                newCell.value = Val
                newCell.onclick = (
		        function (td) {
		            return function () {
		                ClassObj.DoFilter(Val);
		            }
		        }
                )(this);
            }

            this.GoToPage = function (buttonobj) {
                PageNum = parseInt((ClassObj.RecordCount / ClassObj.RowsPerPage), 10)

                NewPageNum = buttonobj.parentNode.childNodes[1].value
                if (NewPageNum <= PageNum && PageNum > 0) {
                    qStr = 'BaseID=' + ClassObj.BaseID + '&Order=' + ClassObj.Order + '&OldOrder=' + ClassObj.OldOrder + '&Repeat=' + ClassObj.Repeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + NewPageNum + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&ShowMode=' + ClassObj.ShowMode + '&MasterCode=' + ClassObj.MasterCode;
                    sUrl = ClassObj.AbsPath + 'jsGetBrowse.aspx?' + qStr;
                    ClassObj.makeRequest(sUrl, null, 'GET', null)
                }
                else
                    alert('صفجه درخواستی معتبر نیست');
            }

            this.CheckBrowser = function () {
                if (navigator.userAgent.indexOf("Firefox") != -1)
                    return "FireFox";
                else
                    return "IE";
            }

            this.GeneratePaging = function () {
                if (ClassObj.PagingCell.childNodes[0] != null) {
                    if (ClassObj.CheckBrowser() == "FireFox") {
                        ClassObj.PagingCell.removeChild(ClassObj.PagingCell.firstChild);
                    }
                    else {
                        if (ClassObj.PagingCell.childNodes[ClassObj.NodeIndex] != null)
                            ClassObj.PagingCell.childNodes[ClassObj.NodeIndex].removeNode(true);
                    }
                }
                TblPaging = document.createElement("TABLE");
                ConditionObj = document.getElementById("Condition");
                if (ConditionObj != null)
                    ConditionCode = ConditionObj.options[ConditionObj.selectedIndex].value;
                else
                    ConditionObj = '';

                RowCount = TblPaging.rows.length
                if (TblPaging.rows.length == 1)
                    TblPaging.deleteRow(0);

                PageNum = parseInt((ClassObj.RecordCount / ClassObj.RowsPerPage), 10)
                if ((ClassObj.RecordCount % ClassObj.RowsPerPage) > 0)
                    PageNum = parseInt(PageNum, 10) + 1

                PageNumPlus = parseInt(PageNum, 10) + 1
                CurPageMinus = parseInt(ClassObj.CurPage, 10) - 1
                CurPagePlus = parseInt(ClassObj.CurPage, 10) + 1

                newRow = this.GetNewRow(TblPaging);
                newRow.className = 'pager'

                ClassObj.MessageCell = this.GetNewCell(newRow);
                ClassObj.MessageCell.className = 'cMessageCell'

                RecordCountCell = this.GetNewCell(newRow);
                RecordCountCell.className = 'cPersianContent'
                RecordCountCell.setAttribute("NOBR", "");
                RecordCountCell.innerHTML = "<nobr>&nbsp;تعداد رکوردها:&nbsp;</nobr>" + ClassObj.RecordCount;
                if (ClassObj.Keyword != '')
                    RecordCountCell.innerHTML = RecordCountCell.innerHTML + "<nobr>&nbsp;تعداد کل رکوردها:&nbsp;</nobr>" + ClassObj.TotalRowCount;


                if (ClassObj.ShowGoToPage) {
                    GoToPageCell = this.GetNewRow(newRow);
                    GoToPageCell.className = 'cPersianContent'
                    GoToPageCell.setAttribute("NOBR", "");
                    GoToPageCell.innerHTML = "<nobr>&nbsp;صفحه:&nbsp;</nobr>" + "<input onkeypress = 'return IsOnlyNumber()' type='text' name='NewPageNum' value='" + ClassObj.CurPage + "' size='2' >&nbsp;";
                    ButtonObj = document.createElement("INPUT");
                    ButtonObj.value = '  برو  '
                    ButtonObj.type = 'button';
                    ButtonObj.onclick = (
        function (cobj) {

            return function () {
                ClassObj.GoToPage(cobj);
            }
        }
        )(ButtonObj);


                    GoToPageCell.appendChild(ButtonObj);
                }

                if (ClassObj.CurPage > 1) {
                    GoToFirstCell = this.GetNewCell(newRow);
                    GoToFirstCell.className = 'cPersianContent'
                    GoToFirstCell.innerHTML = " »» "
                    qStr = 'BaseID=' + this.BaseID + '&Order=' + this.Order + '&OldOrder=' + this.OldOrder + '&Repeat=' + this.Repeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + 1 + '&Keyword=' + escape(this.Keyword) + '&FilterClm=' + this.ActiveCell + '&Condition=' + this.ConditionCode + '&ShowMode=' + this.ShowMode + '&MasterCode=' + this.MasterCode;
                    sUrl = this.AbsPath + 'jsGetBrowse.aspx?' + qStr;
                    GoToFirstCell.onclick = (
	        function (e, u) {
	            var evt = window.event || e
	            evt.cancelBubble = true;
	            if (evt != null)
	                evt.cancelBubble = true;
	            return function () {
	                ClassObj.makeRequest(u, null, 'GET', null)
	            }
	        }
            )('', sUrl);
                    GoToPreCell = this.GetNewCell(newRow);
                    GoToPreCell.className = 'cPersianContent'
                    GoToPreCell.innerHTML = " » "
                    qStr = 'BaseID=' + ClassObj.BaseID + '&Order=' + ClassObj.Order + '&OldOrder=' + ClassObj.OldOrder + '&Repeat=' + ClassObj.Repeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + CurPageMinus + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&ShowMode=' + ClassObj.ShowMode + '&MasterCode=' + ClassObj.MasterCode;
                    sUrl = ClassObj.AbsPath + 'jsGetBrowse.aspx?' + qStr;
                    GoToPreCell.onclick = (
		    function (e, u) {
		        var evt = window.event || e
		        evt.cancelBubble = true;
		        if (evt != null)
		            evt.cancelBubble = true;
		        return function () {
		            ClassObj.makeRequest(u, null, 'GET', null)
		        }
		    }
            )('', sUrl);

                }


                for (j = this.GetLowerPageLimit(4); j < this.GetLowerPageLimit(4) + 6; j++) {
                    if (j > PageNum)
                        break;
                    newCell = this.GetNewCell(newRow);
                    CellText = j

                    //newCell.innerHTML = '<a href=#>' + GetPersianNumber(CellText) + '</a>'
                    newCell.innerHTML = '<span>' + GetPersianNumber(CellText) + '</span>'
                    if (j == ClassObj.CurPage)
                        newCell.className = 'current'
                    else
                        newCell.className = 'cHandPointer'
                    strPage = j;
                    qStr = 'BaseID=' + ClassObj.BaseID + '&Order=' + ClassObj.Order + '&OldOrder=' + ClassObj.OldOrder + '&Repeat=' + ClassObj.Repeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + strPage + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&ShowMode=' + ClassObj.ShowMode + '&MasterCode=' + ClassObj.MasterCode;
                    sUrl = ClassObj.AbsPath + 'jsGetBrowse.aspx?' + qStr;
                    newCell.onclick = (
		    function (u) {
		        return function () {
		            ClassObj.makeRequest(u, null, 'GET', null)
		        }
		    }
            )(sUrl);
                }
                if (ClassObj.CurPage < PageNum) {
                    GoToNextCell = this.GetNewCell(newRow);
                    GoToNextCell.className = 'cPersianContent'
                    GoToNextCell.innerHTML = " « "
                    qStr = 'BaseID=' + ClassObj.BaseID + '&Order=' + ClassObj.Order + '&OldOrder=' + ClassObj.OldOrder + '&Repeat=' + ClassObj.Repeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + CurPagePlus + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&ShowMode=' + ClassObj.ShowMode + '&MasterCode=' + ClassObj.MasterCode;
                    sUrl = ClassObj.AbsPath + 'jsGetBrowse.aspx?' + qStr;
                    GoToNextCell.onclick = (
		    function (u) {
		        return function () {
		            ClassObj.makeRequest(u, null, 'GET', null)
		        }
		    }
            )(sUrl);

                    GoToLastCell = this.GetNewCell(newRow);
                    GoToLastCell.className = 'cPersianContent'
                    GoToLastCell.innerHTML = " «« "
                    qStr = 'BaseID=' + ClassObj.BaseID + '&Order=' + ClassObj.Order + '&OldOrder=' + ClassObj.OldOrder + '&Repeat=' + ClassObj.Repeat + '&RowsPerPage=' + ClassObj.RowsPerPage + '&CurPage=' + PageNum + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&ShowMode=' + ClassObj.ShowMode + '&MasterCode=' + ClassObj.MasterCode;
                    sUrl = ClassObj.AbsPath + 'jsGetBrowse.aspx?' + qStr;
                    GoToLastCell.onclick = (
		    function (u) {
		        return function () {
		            ClassObj.makeRequest(u, null, 'GET', null)
		        }
		    }
            )(sUrl);

                }
                if (ClassObj.ShowExportButton) {
                    ExportExcelCell = this.GetNewCell(newRow);
                    ExportExcelCell.className = 'cExportToExcel'
                    LinkObj = document.createElement("IMG");
                    LinkObj.setAttribute("src", "../images/spacer.gif");
                    LinkObj.setAttribute("alt", "انتقال اطلاعات به اکسل");
                    LinkObj.className = 'cExportToExcel'
                    ExportExcelCell.appendChild(LinkObj)
                    ExportExcelCell.onclick = function (e) { ClassObj.ShowExportPanel(e) }
                }

                RefreshCell = this.GetNewCell(newRow);
                LinkObj = document.createElement("IMG");
                LinkObj.setAttribute("src", "../images/spacer.gif");
                LinkObj.setAttribute("alt", "بازخوانی");
                LinkObj.className = 'cRefresh'
                RefreshCell.appendChild(LinkObj)
                RefreshCell.onclick = function () { ClassObj.Reload() }

                if (ClassObj.RecordCount > 0) {
                    FilterCell = this.GetNewCell(newRow);
                    LinkObj = document.createElement("IMG");
                    LinkObj.setAttribute("src", "../images/spacer.gif");
                    LinkObj.setAttribute("alt", "فیلتر");
                    LinkObj.className = 'cFilter'
                    FilterCell.appendChild(LinkObj)
                    FilterCell.onclick = function (e) { ClassObj.ShowFilterToolbar(e) }
                }

                UnFilterCell = this.GetNewCell(newRow);
                LinkObj = document.createElement("IMG");
                LinkObj.setAttribute("src", "../images/spacer.gif");
                LinkObj.setAttribute("alt", "لغو فیلتر");
                LinkObj.className = 'cFilterCancel'
                UnFilterCell.appendChild(LinkObj)
                UnFilterCell.onclick = function () { ClassObj.CancelFilter() }
                /*

                DO NOT REMOVE THIS SECION!

                HelpCell = newRow.insertCell()
                LinkObj = document.createElement("IMG");
                LinkObj.setAttribute("src", "../images/spacer.gif");
                LinkObj.setAttribute("alt", "راهنما");
                LinkObj.className = 'cHelp'
                HelpCell.appendChild(LinkObj)
                HelpCell.onclick = function() { window.open('../Help/Browse.htm', 'Browse Help', 'width=580,height=450,menubar=no,status=no,titlebar=no,resizable=no,top=200,left=150'); }

                DO NOT REMOVE THIS SECION!

                */

                AddFieldCell = this.GetNewCell(newRow);
                LinkObj = document.createElement("IMG");
                LinkObj.setAttribute("src", "../images/spacer.gif");
                LinkObj.setAttribute("alt", "تغییر ستونها");
                LinkObj.className = 'cAddField'
                AddFieldCell.appendChild(LinkObj)
                AddFieldCell.onclick = function (e) { ClassObj.ShowFieldListToolbar(e) }

                if ((ClassObj.HasAccess(2) || ClassObj.HasAccess(4)) && ClassObj.RecordCount > 0 && ClassObj.ViewEdit == 'Edit') {
                    DeleteCell = this.GetNewCell(newRow);
                    LinkObj = document.createElement("IMG");
                    LinkObj.setAttribute("src", "../images/spacer.gif");
                    LinkObj.setAttribute("alt", "حذف");
                    LinkObj.className = 'cBrowseDelete'
                    DeleteCell.appendChild(LinkObj)
                    DeleteCell.onclick = function () { ClassObj.DeleteRecord() }
                }

                if (ClassObj.ViewEdit == 'Edit' && ClassObj.ViewEdit != 'NewOnly' && ClassObj.HasAccess(2)) {
                    EditCell = this.GetNewCell(newRow);
                    LinkObj = document.createElement("IMG");
                    LinkObj.setAttribute("src", "../images/spacer.gif");
                    LinkObj.setAttribute("alt", "ویرایش");
                    LinkObj.className = 'cEdit'
                    EditCell.appendChild(LinkObj)

                    EditCell.onclick = (
	                function (e, InnerI, VEForm) {
	                    var evt = window.event || e
	                    evt.cancelBubble = true;
	                    if (evt != null)
	                        evt.cancelBubble = true;
	                    return function () {
	                        KeyCol = ClassObj.GetKeyCollection(ClassObj.CurrentRow)
	                        if (evt != null)
	                            evt.cancelBubble = true;
	                        ClassObj.DoUpdate(InnerI, KeyCol, VEForm);
	                    }
	                }
                    )('', ClassObj.CurrentRow, ClassObj.EditForm);
                }

	            if (ClassObj.HasAccess(1) && (ClassObj.ViewEdit == 'Edit' || ClassObj.ViewEdit == 'NewOnly')) {
                    NewRecordCell = this.GetNewCell(newRow);
                    LinkObj = document.createElement("IMG");
                    LinkObj.setAttribute("src", "../images/spacer.gif");
                    LinkObj.setAttribute("alt", "ایجاد");
                    LinkObj.className = 'cBrowseNewRec'
                    NewRecordCell.appendChild(LinkObj)
                    NewRecordCell.onclick = function () { ClassObj.NewRecord() }
                }

                if (ClassObj.Keyword != "" && ClassObj.RecordCount == 0) {
                    NoResultMessage = " هیچ نتیجه ای پیدا نشد ";
                    NoResultMessage = "<div class=\"cMsg\">" + NoResultMessage + "</div>";
                    ClassObj.MessageCell.innerHTML = NoResultMessage;
                }
                ClassObj.PagingCell.appendChild(TblPaging);
                if (ClassObj.DataContainer == null) {
                    alert('Invalid BaseID.');
                    return;
                }
                ClassObj.DataContainer.scrollLeft = 1000;

                if (ClassObj.ShowMode == "List")
                    this.ObjLable.style.width = ClassObj.TblDataPaging.offsetWidth + "px";
            }

            this.DeleteRecord = function () {
                AskDel = confirm("رکورد حذف شود؟")
                if (!AskDel)
                    return;
                KeyList = this.GetKeyCollection(ClassObj.CurrentRow)
                ClassObj.DeleteMode = true;
                var qStr = 'BaseID=' + this.BaseID + "&DelCode=" + KeyList + '&MasterCode=' + this.MasterCode;
                this.makeRequest(this.AbsPath + 'jsGetBrowse.aspx?' + qStr, new Function('ClassObj.DeleteDone()'), 'GET', new Function("ClassObj.UpdateVal()"), 1)
            }

            this.CreateBrowse = function (BID, SMode, FilterCols, Keyword, ConditionCode, ViewEdit, BrowseWidth, BrowseHeight, viewName) {
                this.AbsPath = '../';
                ClassObj.FilterIndex = '';
                ClassObj.FilterColumns = ''
                ClassObj.Keyword = '';
                ClassObj.ReGenerateFields = true;
                this.ShowMode = SMode;

                if ((ViewEdit == undefined) || (ViewEdit == ''))
                    ViewEdit = 'Edit';

                if (BrowseWidth != undefined)
                    ClassObj.BrowseWidth = BrowseWidth;
                if (BrowseHeight != undefined)
                    ClassObj.BrowseHeight = BrowseHeight;

                if (ViewEdit != undefined)
                    ClassObj.ViewEdit = ViewEdit

                if (FilterCols != undefined) {
                    ClassObj.FilterColumns = FilterCols
                    ClassObj.Keyword = Keyword
                    ClassObj.ConditionCode = ConditionCode
                }

                if (this.ShowMode == "Browse") {// && document.parentWindow.frameElement != null) {
                    ClassObj.BrowseHeight = 335; // document.parentWindow.frameElement.clientHeight - 87;
                    ClassObj.RowsPerPage = 25 //Math.floor(ClassObj.BrowseHeight / 29);
                    //                    ClassObj.RowsPerPage = ClassObj.RowsPerPage - 1;
                }

                if (viewName != null && viewName != '')
                    ClassObj.ViewName = viewName;
                if (SMode != undefined)
                    this.ShowMode = SMode;
                this.BaseID = BID

                EditAreaObj = document.getElementById('EditArea')
                ClassObj.TblDataPaging = document.createElement("TABLE");
                ClassObj.TblDataPaging.className = 'ctblDataPaging';
                if (this.ShowMode == "List") {
                    //                    X = document.parentWindow.frameElement != null ? Math.floor(document.parentWindow.frameElement.clientWidth / 2) : window.event.clientX + document.body.scrollLeft; // window.event.clientX + document.body.scrollLeft;
                    //                    Y = 100; // window.event.clientY + document.body.scrollTop + document.documentElement.scrollTop;
                    X = mouseX - 100;
                    Y = mouseY - 50;
                    ClassObj.DisObj = document.getElementById('ListPanelTD')
                    document.getElementById('ListPanel').style.left = X - 263 + document.body.scrollLeft + 'px';
                    document.getElementById('ListPanel').style.top = Y + 13 + document.body.scrollTop + 'px';
                    document.getElementById('ListPanel').style.display = "block";
                    document.getElementById('ListPanel').className = 'VisablePanel';
                }
                else {
                    ClassObj.TblDataPaging.setAttribute("width", "100%");

                    if (ClassObj.DisObj == null)
                        ClassObj.DisObj = document.getElementById('DisplayArea')
                    if (EditAreaObj) {
                        if (EditAreaObj.childNodes[0] != null)
                            EditAreaObj.childNodes[0].removeNode(true)
                    }
                }
                if (ClassObj.DisObj != null) {
                    if (ClassObj.DisObj.childNodes[0] != null) {
                        ChildLen = ClassObj.DisObj.childNodes.length
                        for (rem = 0; rem < ChildLen; rem++) {
                            ParentNode = ClassObj.DisObj.childNodes[0].parentNode;
                            ParentNode.removeChild(ParentNode.childNodes[0]);
                        }
                    }
                }
                Row1 = this.GetNewRow(ClassObj.TblDataPaging);
                ClassObj.AllDataCell = this.GetNewCell(Row1);
                ClassObj.AllDataCell.className = "RightCell";
                Row2 = this.GetNewRow(ClassObj.TblDataPaging);
                ClassObj.PagingCell = this.GetNewCell(Row2);
                ClassObj.PagingCell.className = 'cFooterPaging'
                Row3 = this.GetNewRow(ClassObj.TblDataPaging);
                ClassObj.LoadingCell = this.GetNewCell(Row3);
                ClassObj.LoadingCell.className = 'cHidden'
                if (ClassObj.ShowMode == "List") {
                    this.ObjLable = document.createElement("DIV");
                    this.ObjLable.className = 'cBrowseLabel'
                    ClassObj.DisObj.appendChild(this.ObjLable);
                }
                ClassObj.DisObj.appendChild(ClassObj.TblDataPaging);

                this.makeRequest(this.AbsPath + 'jsGetBrowse.aspx?BaseID=' + this.BaseID + '&ShowMode=' + this.ShowMode + '&MasterCode=' + this.MasterCode + '&Keyword=' + escape(ClassObj.Keyword) + '&FilterClm=' + ClassObj.FilterColumns + '&Condition=' + ClassObj.ConditionCode + '&SearchOperand=' + ClassObj.SearchOperand + '&NewsFlowCode=' + this.NewsFlowCode + '&IGroupCode=' + this.IGroupCode, new Function("ClassObj.UpdateVal()"), 'GET', new Function("ClassObj.UpdateVal()"), 1)
            }

            this.ShowDetail = function (BID, MCodes, SPL, InstanceName, ViewEdit) {
                ClassObj.FilterIndex = '';
                ClassObj.FilterColumns = ''
                ClassObj.Keyword = '';
                ClassObj.ReGenerateFields = true;
                if (ViewEdit != undefined)
                    ClassObj.ViewEdit = ViewEdit
                if (InstanceName != undefined)
                    ClassObj.ClassInstanceName = InstanceName
                if (SPL != undefined)
                    ClassObj.ShowLableName = SPL;
                if (MCodes != undefined)
                    ClassObj.MasterCode = MCodes
                ClassObj.TblDataPaging = document.createElement("TABLE");
                ClassObj.TblDataPaging.setAttribute("width", "100%");
                ClassObj.DisObj = document.getElementById(BID)
                if (ClassObj.DisObj.childNodes[0] != null) {
                    ChildLen = ClassObj.DisObj.childNodes.length
                    for (rem = 0; rem < ChildLen; rem++) {
                        ClassObj.DisObj.childNodes[0].removeNode(true);
                    }
                }

                ClassObj.className = 'cBrowseContainer'
                Row1 = this.GetNewRow(ClassObj.TblDataPaging);
                ClassObj.AllDataCell = this.GetNewCell(Row1);
                Row2 = this.GetNewRow(ClassObj.TblDataPaging);
                ClassObj.PagingCell = this.GetNewCell(Row2);
                ClassObj.PagingCell.className = 'cFooterPaging'
                ClassObj.LoadingObj = document.createElement("DIV");
                ClassObj.LoadingObj.className = 'cLoading'
                ClassObj.BoardObj = document.createElement("DIV");

                ClassObj.ObjLable = document.createElement("DIV");
                ClassObj.ObjLable.className = 'cBrowseLabel'
                ClassObj.DisObj.appendChild(ClassObj.ObjLable);

                ClassObj.DisObj.appendChild(ClassObj.TblDataPaging);
                ClassObj.DisObj.appendChild(ClassObj.LoadingObj);
                ClassObj.DisObj.appendChild(ClassObj.BoardObj);
                ClassObj.BaseID = BID
                qStr = this.AbsPath + 'Default.aspx?UC=' + ClassObj.CurrentForm + "&BaseID=" + ClassObj.BaseID + "&Code=" + '&MasterCode=' + ClassObj.MasterCode + '&InstanceName=' + ClassObj.ClassInstanceName;
                ClassObj.makeRequest(this.AbsPath + 'jsGetBrowse.aspx?' + qStr, new Function("ClassObj.UpdateVal()"), 'GET', null, 1)
            }
            GetPersianNumber = function (str) {
                str = str + ''
                var Result = "";
                for (num = 0; num < str.length; num++) {
                    Result = Result + String.fromCharCode(str.charCodeAt(num) + 1728);
                }
                return Result;
            }
        }

        function OpenList(BaseID, ImgObj, AbsPath) {
            var LookupObj = new XMLBrowse()
            if (AbsPath != undefined)
                LookupObj.AbsPath = AbsPath;
            if (getObj('LookupResults')) {
                getObj('LookupResults').style.display = 'none';
            }
            ShowMode = 'List';
            FormFieldCode = ImgObj.parentNode.parentNode.parentNode.parentNode.rows[0].cells[1].childNodes[0];
            if (ImgObj.parentNode.parentNode.parentNode.parentNode.rows[0].cells[0].childNodes.length > 1)
                FormFieldName = ImgObj.parentNode.parentNode.parentNode.parentNode.rows[0].cells[0].childNodes[1];
            else
                FormFieldName = ImgObj.parentNode.parentNode.parentNode.parentNode.rows[0].cells[0].childNodes[0];
            //FormFieldCode =document.getElementById(txtCode);
           // FormFieldName =  document.getElementById(txtTitle);
            LookupObj.ViewEdit = 'Edit'
            LookupObj.CreateBrowse(BaseID, ShowMode);
        }
        var BrowseObj1 = new XMLBrowse()
        var BrowseObj2 = new XMLBrowse()
        var BrowseObj3 = new XMLBrowse()
        var BrowseObj4 = new XMLBrowse()
        var BrowseObj5 = new XMLBrowse()

        var BrowseObjKeywords = new XMLBrowse()