<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master" CodeBehind="ShowStudyInfoDetail.aspx.cs" Inherits="Ranjbaran.ShowStudyInfoDetail" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="CP1">
    <div class="panel panel-default MainArea">
        <div class="Padder5">
            <div class="panel panel-default Marginer20">
                <div class="panel-heading ListTitle">
                    <div class="NewsDate">
                        <asp:Literal ID="ltrDate" runat="server"></asp:Literal>
                    </div>
                    <h3 class="panel-title BulletList">
                        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                    </h3>
                </div>
                <div class="Padder5">
                    <div class="Justify Padder25 StudyInfoBody">
                        <asp:Literal ID="ltrDec" runat="server"></asp:Literal>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </div>
            <div id="divLinks">
            </div>
        </div>
    </div>
</asp:Content>