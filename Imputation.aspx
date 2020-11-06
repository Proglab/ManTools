<%@ Page Title="Imputation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Imputation.aspx.cs" Inherits="ManTools2020.Imputation" %>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">

      <div class="row">
        <div class="large-12 medium-8 columns">
            <div class="panel">
                <div class="row">
                    <div class="large-12 medium-6 columns">
                        <h3 >ENGIE Axima - Webtools - <i>Imputation / Imputatie</i></h3>

                        <div class="row">
                            <div class="large-12 columns">
                                <fieldset><legend>Information Chantier / Werf Informatie</legend>
                                <div class="row">
                                    <div class="large-2 columns">
                                        <label>Imputation / Imputatie</label>
                                        <asp:TextBox ID="CodeImputation" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="large-5 columns">
                                        <label>Nom chantier / Werfnaam</label>
                                        <asp:TextBox ID="NomChantier" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    <div class="large-2 columns">
                                        <label>Service / Dienst</label>
                                        <asp:DropDownList runat="server" ID="service" Enabled="false"></asp:DropDownList>
                                    </div>
                                    <div class="large-3 columns">
                                        <label>Chargé d'affaire / zaakgelastigde</label>
                                        <asp:TextBox ID="ChargeAffaire" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                               
                                <div class="row">
                                    <div class="large-6 columns">
                                        <label>Adresse / Adress</label>
                                        <asp:TextBox ID="Adresse" runat="server" Enabled="false"/></asp:TextBox>
                                    </div>
                                    <div class="large-3 columns">
                                        <label>Code Postal / Post Code</label>
                                        <asp:TextBox ID="codepostal" runat="server" Enabled="false" /></asp:TextBox>
                                    </div>
                                    <div class="large-3 columns">
                                        <label>Localité / Plaats</label>
                                        <asp:TextBox ID="localite" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                  </div>
                                   
                                 <div class="row">  
                                    <div class="large-6 columns">
                                        <label>Tel Chantier / Tel Werf</label>
                                        <asp:TextBox ID="telchantier" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                     <div class="large-6 columns">
                                        <label>Fax Chantier / Fax Werf</label>
                                        <asp:TextBox ID="faxchantier" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                
                                  </fieldset>  
                                 </div>
                                <div class="row"> 
                                    <div class="large-12 columns">
                                    <fieldset><legend>Information Chef de Chantier / Informatie Werf Verantw.</legend>
                                    <div class="large-6 columns">
                                         <label>Chef de chantier / Werf Verantw.</label>
                                         <asp:TextBox ID="ChefChantier" runat="server" Enabled="false"></asp:TextBox>
                                     </div>
                                     <div class="large-6 columns">
                                        <label>Tel Chef de Chantier / Tel Werf Verantw.</label>
                                        <asp:TextBox ID="TelChefChantier" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                    </fieldset>  
                                  </div> </div> 
                            <div class="row">
                            <div class="row">
                                <div class="large-12 columns">
                                    <div class="row">
                            <asp:GridView ID="gridviewdata" runat="server" AutoGenerateColumns="true" Visible="true">
                            </asp:GridView>
                            </div>

                            <!--BOUTONS-->
                            <div class="row">
                                    <div class="large-12 columns">
                                    <asp:Button ID="Ajout" Text="Add" runat="server" class="small round button" OnClick="Ajout_Click" />
                                    <asp:Button ID="EnregistrerAjout" Text="Save" runat="server" OnClick="EnregistrerAjout_Click" class="small round button" />
                                    <asp:Button ID="Modifier" Text="Update" runat="server" OnClick="Modifier_Click" class="small round button" />
                                    <asp:Button ID="EnregistrerModif" Text="Save" runat="server" OnClick="EnregistrerModif_Click" class="small round button" />
                                    <asp:Button ID="Annuler" Text="Cancel" runat="server" OnClick="Annuler_Click" class="small round button" />
                                    <asp:Button ID="Supprimer" Text="Delete" runat="server" OnClick="Supprimer_Click" class="small round button" />
                                    <asp:Button ID="ConfirmerSuppression" Text="Confirm Delete" runat="server" OnClick="ConfirmerSuppression_Click" class="small round button" />
                                    <div>
                                      <asp:Label ID="lblMessage" runat="server" Visible="False" ForeColor="#FF3300" />
                                    </div>
                                </div>
                            </div>
                        </div>
                      </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
