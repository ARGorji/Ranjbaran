<%@ Page Language="C#" StylesheetTheme="Edit" MasterPageFile="~/Admin/Main.master" AutoEventWireup="True" Inherits="AccessGroups_EditAccessGroups"
    Title="AccessGroups" Codebehind="~/Admin/AccessLevel/EditAccessGroup.aspx.cs" %>

<asp:Content runat="server" ID="content1" ContentPlaceHolderID="cphMain">
    <script language="javascript" type="text/javascript">
        document.write("<div class=\"cHiddenDate\" id=\"AccessPanel\"></div>");
        var AccessTable;
        var AccessObj = document.getElementById("AccessPanel");
        var ClickedX;
        var ClickedY;

        function CreateAccessTable(CellText, AccCode, AccType) {
            AccType = parseInt(AccType, 10)
            HasAccess = ((AccType & AccCode) == AccCode)
            if (HasAccess) {
                chkObj = document.createElement("input");
                chkObj.setAttribute("type", "checkbox");
                chkObj.setAttribute("checked", true);
            }
            else {
                chkObj = document.createElement("input");
                chkObj.setAttribute("type", "checkbox");
            }
            chkObj.setAttribute("AccessCode", AccCode);
            spanObj = document.createElement("SPAN");
            spanObj.innerHTML = CellText

            newRow = AccessTable.insertRow()
            newCell2 = newRow.insertCell()
            newCell2.appendChild(spanObj);
            newCell1 = newRow.insertCell()
            newCell1.appendChild(chkObj);

        }

        function UpdateResources() {
            if (http_request != null) {
                if (http_request.readyState == 4) {
                    if (http_request.status == 200) {
                        result = http_request.responseText
                        if (result == "Success") {
                            document.getElementById('msgBox').innerHTML = 'عملیات با موفقیت انجام شد'
                            getObj('AccessPanel').style.display = 'none';
                        }
                        else
                            alert(result);
                        document.body.style.cursor = 'auto';
                    }
                }
            }

        }

        function AccessTypeIsReady() {
            if (http_request != null) {
                if (http_request.readyState == 4) {
                    if (http_request.status == 200) {
                        result = http_request.responseText
                        resultArray = result.split(';')
                        NodeCode = resultArray[0];
                        AccessType = resultArray[1];
                        if (result != "") {
                            ShowEditForm(NodeCode, AccessType);
                        }
                        else
                            alert(result);
                        document.body.style.cursor = 'auto';
                    }
                }
            }

        }

        function ReqSave(NodeCode) {
            SumAccess = 0
            if (AccessTable.rows[0].cells[1].childNodes[0].checked)
                ApplyToChildNodes = 1;
            else
                ApplyToChildNodes = 0
            for (acRow = 0; acRow < AccessTable.rows.length - 1; acRow++) {
                if (AccessTable.rows[acRow].cells[1].childNodes[0].checked)
                    SumAccess = SumAccess + parseInt(AccessTable.rows[acRow].cells[1].childNodes[0].AccessCode, 10);
            }
            SaveUrl = 'ajxGroupResources.aspx?GroupCode=<%=Code%>&ResourceCode=' + NodeCode + '&AccessType=' + SumAccess + '&ApplyToChildNodes=' + ApplyToChildNodes;
            startRequest(SaveUrl, UpdateResources, 'GET', null, null)
        }

        function GetAccessType(sender, eventArgs) {
            if (event != null)
                event.cancelBubble = true;
            ClickedX = window.event.clientX
            ClickedY = window.event.clientY
            var node = eventArgs.get_node();


            NodeCode = node.get_value();
            SaveUrl = 'ajxGroupResources.aspx?GroupCode=<%=Code%>&ResourceCode=' + NodeCode + '&AccessType=';
            //alert(SaveUrl)
            startRequest(SaveUrl, AccessTypeIsReady, 'GET', null, null)
        }

        function ShowEditForm(NodeCode) {
            if (AccessObj != null) {
                if (AccessObj.childNodes.length > 0) {
                    ChildLen = AccessObj.childNodes.length
                    for (rem = 0; rem < ChildLen; rem++) {
                        AccessObj.childNodes[0].removeNode(true);
                    }
                }
            }
            AccessTable = document.createElement("TABLE");
            AccessTable.className = 'cFieldListTable'
            CreateAccessTable("همه زیر مجموعه ها", 0, AccessType)
            CreateAccessTable("ایجاد", 1, AccessType)
            CreateAccessTable("ویرایش", 2, AccessType)
            CreateAccessTable("حذف", 4, AccessType)
            CreateAccessTable("نمایش", 8, AccessType)
            CreateAccessTable("چاپ", 16, AccessType)

            saveObj = document.createElement("input");
            saveObj.setAttribute("type", "button");
            saveObj.setAttribute("value", "ذخیره");
            saveObj.onclick = (
		        function (f) {
		            if (event != null)
		                event.cancelBubble = true;
		            return function () {
		                ReqSave(f)
		            }
		        }
                )(NodeCode);

            newRow = AccessTable.insertRow()
            newCell1 = newRow.insertCell()
            newCell1.align = 'center';
            newCell1.setAttribute('colspan', '2');
            newCell1.appendChild(saveObj);
            newCell2 = newRow.insertCell()

            AccessObj.appendChild(AccessTable);


            AccessObj.style.left = ClickedX - 132 + document.body.scrollLeft;
            AccessObj.style.top = ClickedY + 2 + document.body.scrollTop + document.documentElement.scrollTop;
            AccessObj.style.display = "block"

        } 

    </script>
    <div class="cTblEdit">
        <div style="width: 700px; float: right; text-align: right; color: White;">
            <asp:Label runat="server" ID="lblSysName"></asp:Label>
        </div>
        <div class="cTDEdit">
            <div class="cEditRight">
                <div class="cEditMain">
                    <div class="cEditMainData">
                        <div>
                            <AKP:MsgBox ID="msgBox" ClientIDMode="Static" runat="server" CssClass="cError" />
                        </div>
                        <div>
                            <div>
                                <table class="cTblOneRow">
                                    <tr>
                                        <td class="cFieldLeft">
                                            <table class="cTblField">
                                                <tr>
                                                    <td class="cCtrl">
                                                        <AKP:ExTextBox jas="1" ID="txtName" MaxLength="200" runat="server" />
                                                    </td>
                                                    <td class="cLabel">
                                                        <asp:Label ID="lblName" runat="server" Text="نام گروه:"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="cFieldRight">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                            </div>
                        </div>
                        <div style="text-align: right">
                            <telerik:RadTreeView ID="RadTree1" EnableViewState="false" runat="server" Skin="WebBlue"
                                ImagesBaseDir="~/Images/TreeView/Outlook" OnClientNodeClicking="GetAccessType"
                                ShowLineImages="true" expanddelay="3" dir="rtl" Width="600px" OnNodeExpand="RadTree1_NodeExpand">
                            </telerik:RadTreeView>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="cHorSep">
        </div>
        <div class="clear">
        </div>
        <div style="text-align: right">
            <table class="tblEditButtons" cellpadding="2" cellspacing="4">
                <tr>
                    
                    <td>
                        <asp:ImageButton ID="imgBtnBack" Text=" بازگشت " SkinID="BackButton" OnClick="GoToList"
                            runat="server" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgBtnSave" Text=" ذخیره " SkinID="SaveButton" OnClick="DoSave"
                            runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

