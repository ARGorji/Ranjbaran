<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master"
    Title="وبسایت هادی رنجبران | سخن روز" CodeBehind="Speeches.aspx.cs" Inherits="Ranjbaran.Speeches" %>

<%@ Register Src="~/UserControls/PagerToolbar.ascx" TagName="PagerToolbar" TagPrefix="Tlb" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <h1 class="PageTitle">
        <asp:Image ID="Image1" ImageUrl="~/images/lblSpeechs.png" ToolTip="سخن روز" runat="server" />
    </h1>
    
        <asp:Repeater ID="rptSpeeches" runat="server">
            <ItemTemplate>
                <div class="SpeechItemCont">
                    <div class="Speech">
                        <%#Eval("Title")%>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    
    <div class="Clear">
    </div>
    <asp:Panel runat="server" ID="pnlPaging">
        <Tlb:PagerToolbar runat="server" ID="pgrToolbar" />
    </asp:Panel>
</asp:Content>
