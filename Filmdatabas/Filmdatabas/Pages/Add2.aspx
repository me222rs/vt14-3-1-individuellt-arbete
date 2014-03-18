<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Add2.aspx.cs" Inherits="Filmdatabas.Pages.Add2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:LinkButton ID="BackButton" runat="server" OnClick="BackButton_Click" CausesValidation="False">Bakåt</asp:LinkButton>
            <p>
        <asp:Label ID="ShowMessage" runat="server" Text="" Visible="false"></asp:Label>
        </p>
           <div id="addForm">
        <asp:FormView ID="ContactListView" runat="server"
                ItemType="Filmdatabas.Model.Title"
                DefaultMode="Insert"
                RenderOuterTable="false"
                InsertMethod="ContactListView_InsertItem"
                DataKeyBind="TitelID">
                <InsertItemTemplate>
                    <%-- Mall för rad i tabellen för att lägga till nya kunduppgifter. Visas bara om InsertItemPosition 
                     har värdet FirstItemPosition eller LasItemPosition.--%>
                    <div id="formContent">
                    <tr>
                        <div class="InsertForm">
                            <p>Titel</p>
                            <asp:TextBox ID="Titel" runat="server" Text='<%# BindItem.Titel %>' MaxLength="40" />
                            <asp:RequiredFieldValidator ID="TitelRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange ett namn på filmen" ControlToValidate="Titel" Text="*"></asp:RequiredFieldValidator>
                        </div>
<%--                        <div class="InsertForm">
                            <p>Betyg</p>
                            <asp:TextBox ID="Betyg" runat="server" Text='<%# BindItem.Betyg %>' />
                        </div>--%>
                        <div class="InsertForm">
                            <p>Land</p>
                            <asp:TextBox ID="Land" runat="server" Text='<%# BindItem.Land %>' MaxLength="30" />
                            <asp:RequiredFieldValidator ID="LandRequiredFieldValidator" runat="server" ControlToValidate="Land" ErrorMessage="Du måste ange ett land" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="InsertForm">
                            <p>Produktionsår</p>
                            <asp:TextBox ID="Produktionsar" runat="server" Text='<%# BindItem.Produktionsar %>' />
                            <asp:RequiredFieldValidator ID="ProduktionsarRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange ett år" ControlToValidate="Produktionsar" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Året du angett kanske var innan filmens tid eller alltför långt fram i framtiden" Type="Integer" MaximumValue="2100" MinimumValue="1896" Text="*" ControlToValidate="Produktionsar"></asp:RangeValidator>
                        </div>
                        <div class="InsertForm">
                            <p>Filmbolag</p>
                            <asp:TextBox ID="Filmbolag" runat="server" Text='<%# BindItem.Filmbolag %>' MaxLength="40" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Du måste ange ett filmbolag" ControlToValidate="Filmbolag" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="InsertForm">
                            <p>Beskrivning</p>
                            <asp:TextBox ID="Beskrivning" runat="server" Text='<%# BindItem.Beskrivning %>' MaxLength="500" TextMode="MultiLine" />
                            <asp:RequiredFieldValidator ID="BeskrivningRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange en beskrivning av filmen." ControlToValidate="Beskrivning" Text="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="InsertForm">
                            <p>Hyllplats</p>
                            <asp:TextBox ID="Hyllplats" runat="server" Text='<%# BindItem.Hyllplats %>' MaxLength="10" />
                            <asp:RequiredFieldValidator ID="HyllplatsRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange en hyllplats" ControlToValidate="Hyllplats" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="InsertForm">
                            <p>Speltid</p>
                            <asp:TextBox ID="Speltid" runat="server" Text='<%# BindItem.Speltid %>' />
                            <asp:RequiredFieldValidator ID="SpeltidRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange hur lång filmen är" ControlToValidate="Speltid" Display="Dynamic" Text="*"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="SpeltidRegularExpressionValidator" runat="server" ErrorMessage="Du måste ange längden i minuter" ControlToValidate="Speltid" ValidationExpression="^\d+$" Display="Dynamic" Text="*"></asp:RegularExpressionValidator>
                        </div>
                        <div class="InsertForm">
                            <p>Filmformat</p>
                            <asp:CheckBoxList 
                                ID="FormatCheckBoxList" 
                                runat="server" 
                                ItemType="Filmdatabas.Model.Format" 
                                SelectMethod="FormatCheckboxList_GetData" 
                                DataTextField="Formattyp" 
                                DataValueField="FormatID"
                                OnDataBinding="FormatCheckBoxList_DataBinding">
                            
                            </asp:CheckBoxList>
                            <asp:CustomValidator ID="CheckCustomValidator" runat="server" ErrorMessage="Du måste välja något!" OnServerValidate="CheckCustomValidator_ServerValidate"></asp:CustomValidator>
                           
                        </div>
                        <div id="AddButtons">
                            <%-- "Kommandknappar" för att lägga till en ny kunduppgift och rensa texfälten. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Lägg till" />
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                        </div>
                    </tr>
                        </div>
                </InsertItemTemplate>
        </asp:FormView>
               </div>
</asp:Content>
