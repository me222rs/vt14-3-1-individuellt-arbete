<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Home2.aspx.cs" Inherits="Filmdatabas.Pages.Home2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <asp:LinkButton ID="AddRedirectButton" runat="server" OnClick="AddRedirectButton_Click" CausesValidation="False">Lägg till ny film</asp:LinkButton>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <asp:Label ID="ShowMessage" runat="server" Text="" Visible="false"></asp:Label>


        <asp:ListView ID="MovieListView" runat="server"
                ItemType="Filmdatabas.Model.Title"
                SelectMethod="MovieListView_GetData"
                DeleteMethod="MovieListView_DeleteItem"
                UpdateMethod="MovieListView_UpdateItem"
                DataKeyNames="TitelID" 
                OnItemDataBound="MovieListView_ItemDataBound">
                <LayoutTemplate>
                    <div class="movieTable">
                    <table class="grid">
                        <tr><td>
                                TitelID
                            </td>
                            <td>
                                Titel
                            </td>
<%--                            <td>
                                Betyg
                            </td>--%>
                            <td>
                                Land
                            </td>
                            <td>
                                Produktionsår
                            </td>
                            <td>
                                Filmbolag
                            </td>
                            <td>
                                Beskrivning
                            </td>
                            <td>
                                Hyllplats
                            </td>
                            <td>
                                Speltid
                            </td>
                            <td>
                                Format
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- Mall för nya rader. --%>
                    <tr>
                        <td>
                            <%#: Item.TitelID %>
                        </td>
                        <td>
                            <%#: Item.Titel %>
                        </td>
<%--                        <td>
                            <%#: Item.Betyg %>
                        </td>--%>
                        <td>
                            <%#: Item.Land %>
                        </td>
                        <td>
                            <%#: Item.Produktionsar %>
                        </td>
                        <td>
                            <%#: Item.Filmbolag %>
                        </td>
                        <td>
                            <%#: Item.Beskrivning %>
                        </td>
                        <td>
                            <%#: Item.Hyllplats %>
                        </td>
                        <td>
                            <%#: Item.Speltid %>
                        </td>
                        <td>
                            <asp:Literal ID="FormatLiteral" runat="server"></asp:Literal>
                            <%--<%#: Item.FormatID %>--%>
                        </td>
                        <td>
                            <asp:LinkButton ID="DeleteButton" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" OnClientClick="return confirm('Vill du ta bort filmen?')"/>
                            <asp:HyperLink ID="HyperLink1" runat="server" Text="Redigera"  NavigateUrl='<%# "~/Pages/Update2.aspx?titleID=" + Item.TitelID %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <%-- Detta visas då filmer saknas i databasen. --%>
                    <table class="grid">
                        <tr>
                            <td>
                                Filmer saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
</asp:Content>
