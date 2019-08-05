<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCNews.ascx.cs" Inherits="Ranjbaran.UserControls.UCNews" %>
<asp:Repeater ID="rptNews" runat="server">
            <ItemTemplate>
                <div class="NewsItem">
                    <div>
                        <ul class="NewsItemList">
                            <li>
                                <div class="NewsTitle" onclick="ToggleNews('<%#Eval("Code")%>', '<%#Eval("Link")%>')">
                                    <%#Eval("Title") %>
                                </div>
                            </li>
                            <li>
                                <div class="ViewNews">
                                    <%#Tools.ChangeEnc( Eval("NDate")) %>
                                </div>
                            </li>
                            
                        </ul>
                    </div>
                    <div class="Clear">
                    </div>
                    
                    <div id="News<%#Eval("Code") %>" class="NewsBody hide">
                        <%#Tools.FormatString( Eval("NewsBody") )%>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>