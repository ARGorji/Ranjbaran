<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeBehind="ShowPublication.aspx.cs" Inherits="Ranjbaran.ShowPublication" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="ShowPubCont row">
        <div class="col-lg-4">
            <asp:Image CssClass="PubImgLarge" ID="imgPubLargePic" runat="server" />
        </div>
        <div class="col-lg-8">
            <div class="PubTitle">
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
            </div>
            <div class="Justify">
                <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
            </div>
            <div class="PubInfo">
                <table>
                    <tr>
                        <td style="text-align: left; float: left;">
                            <table class="tblPrice">
                                <tr>
                                    <td class="PubItemInfoVal">ریال
                                         &nbsp;
                                    </td>
                                    <td class="PubItemInfoVal">
                                        <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="tbllbl">قیمت:
                        </td>
                    </tr>
                    <tr>
                        <td class="PubItemInfoVal">
                            <asp:Label ID="lblPublicationTurn" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="tbllbl">نوبت چاپ:
                        </td>
                    </tr>
                    <tr>
                        <td class="PubItemInfoVal">
                            <asp:Label ID="lblEntesharat" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td class="tbllbl">انتشارات:
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
    </div>
</asp:Content>
