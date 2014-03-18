<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Update2.aspx.cs" Inherits="Filmdatabas.Pages.Update2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <asp:Label ID="ShowMessage" runat="server" Text="" Visible="false"></asp:Label>
        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" OnClick="AddRedirectButton_Click">Bakåt</asp:LinkButton>
    <div id="addForm">
    <asp:FormView ID="ContactListView" runat="server"
                ItemType="Filmdatabas.Model.Title"
                DefaultMode="Edit"
                RenderOuterTable="false"
                UpdateMethod="ContactListView_UpdateItem"
                DataKeyNames="TitelID"
                SelectMethod="ContactListView_GetData">

            <EditItemTemplate>
                    <%-- Mall för rad i tabellen för att redigera kunduppgifter. --%>
                    <tr>
                        <td>
                            <p>Titel</p>
                            <asp:TextBox ID="Titel" runat="server" Text='<%# BindItem.Titel %>' MaxLength="40"/>
                            <asp:RequiredFieldValidator ID="TitelRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange ett namn på filmen" ControlToValidate="Titel" Display="Dynamic"></asp:RequiredFieldValidator>
                       </td>
<%--                        <td>
                            <asp:TextBox ID="Betyg" runat="server" Text='<%# BindItem.Betyg %>' />
                       </td>--%>
                        <td><p>Land</p>
                            <asp:TextBox ID="Land" runat="server" Text='<%# BindItem.Land %>' MaxLength="30"/>
                            <asp:RequiredFieldValidator ID="LandRequiredFieldValidator" runat="server" ControlToValidate="Land" ErrorMessage="Du måste ange ett land" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <p>Produktionsår</p>
                            <asp:TextBox ID="Produktionsar" runat="server" Text='<%# BindItem.Produktionsar %>'/>
                            <asp:RequiredFieldValidator ID="ProduktionsarRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange ett år" ControlToValidate="Produktionsar" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Året du angett kanske var innan filmens tid eller alltför långt fram i framtiden" Type="Integer" MaximumValue="2100" MinimumValue="1896" Text="*" ControlToValidate="Produktionsar"></asp:RangeValidator>
                        </td>
                        <td>
                            <p>Filmbolag</p>
                            <asp:TextBox ID="Filmbolag" runat="server" Text='<%# BindItem.Filmbolag %>' MaxLength="30"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Du måste ange ett filmbolag" ControlToValidate="Filmbolag" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <p>Beskrivning</p>
                            <asp:TextBox ID="Beskrivning" runat="server" Text='<%# BindItem.Beskrivning %>' MaxLength="500"/>
                            <asp:RequiredFieldValidator ID="HyllplatsRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange en hyllplats" ControlToValidate="Hyllplats" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <p>Hyllplats</p>
                            <asp:TextBox ID="Hyllplats" runat="server" Text='<%# BindItem.Hyllplats %>' MaxLength="10"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Du måste ange en hyllplats" ControlToValidate="Hyllplats" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <p>Speltid</p>
                            <asp:TextBox ID="Speltid" runat="server" Text='<%# BindItem.Speltid %>'/>
                            <asp:RequiredFieldValidator ID="SpeltidRequiredFieldValidator" runat="server" ErrorMessage="Du måste ange hur lång filmen är" ControlToValidate="Speltid" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="SpeltidRegularExpressionValidator" runat="server" ErrorMessage="Du måste ange längden i minuter" ControlToValidate="Speltid" ValidationExpression="^\d+$" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
<%--                        <td>
                            <asp:TextBox ID="Filmformat" runat="server" Text='<%# BindItem.FormatID %>'/>
                        </td>--%>
                            <asp:CheckBoxList 
                                ID="FormatCheckBoxList" 
                                runat="server" 
                                ItemType="Filmdatabas.Model.Format" 
                                SelectMethod="FormatCheckboxList_GetData" 
                                DataTextField="Formattyp" 
                                DataValueField="FormatID"
                                OnDataBinding="FormatCheckBoxList_DataBinding"
                                OnDataBound="FormatCheckBoxList_DataBound">
                            
                            </asp:CheckBoxList>
                         <asp:CustomValidator ID="CheckCustomValidator" runat="server" ErrorMessage="Du måste välja något!" OnServerValidate="CheckCustomValidator_ServerValidate"></asp:CustomValidator>
                        <td>
                            <%-- "Kommandknappar" för att uppdatera en kunduppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="SaveButton" runat="server" CommandName="Update" Text="Spara" CausesValidation="true"/>
                            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:FormView>
        </div>
</asp:Content>
