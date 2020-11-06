<%@ Page Title="Famille" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FamilleFiche.aspx.cs" Inherits="ManTools2020.FamilleFiche" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <div class="row">
        <div class="large-12 medium-8 columns">
            <div class="panel">
                <div class="row">
                    <div class="large-12 medium-6 columns">
                        <h3 >ENGIE Axima - Webtools </h3>

                  <fieldset><legend>Fiche Famille / Familie </legend>
                          <div class="row">
                              <div class="large-2 columns">
                                  <label>Fam.</label>
                                  <asp:TextBox runat="server" ID="NumFam" Enabled="false" />
                              </div>
                              <div class="large-5 columns">
                                  <label>Fam. Descr. / Omschr. FR</label>
                                  <asp:TextBox runat="server" ID="DescrFamFr" Enabled="false" />
                              </div>
                              <div class="large-5 columns">
                                  <label>Fam. Descr. / Omschr. NL</label>
                                  <asp:TextBox runat="server" ID="DescrFamNl" Enabled="false" />
                              </div>
                          </div>
                          <div class="row">
                              <div class="large-2 columns">
                                  <label>S-Fam</label>
                                  <asp:TextBox runat="server" ID="SsFam" Enabled="false" />
                              </div>
                              <div class="large-5 columns">
                                  <label>S-Fam. Descr. / Omschr. FR</label>
                                  <asp:TextBox runat="server" ID="SsFamFR" Enabled="false" />
                              </div>
                              <div class="large-5 columns">
                                  <label>S-Fam. Descr. / Omschr. NL</label>
                                  <asp:TextBox runat="server" ID="DescrSsFamNl" Enabled="false" />
                              </div>
                          </div>
                          <div class="row">
                              <div class="large-2 columns">
                                  <label>Tarif / Tarief</label>
                                  <asp:TextBox runat="server" ID="Taux" Enabled="false" />
                              </div>
                              <div class="large-2 columns">
                                  <label>Pr. Ctrl.</label>
                                  <asp:DropDownList runat="server" ID="ddlCtrl" Enabled="false"></asp:DropDownList>
                              </div>
                              <div class="large-2 columns">
                                  <label>Int. Etalonn. / Ijkingen</label>
                                  <asp:DropDownList runat="server" ID="ddlIntEtal" Enabled="false"></asp:DropDownList>
                              </div>
                               <div class="large-3 columns">
                                  <asp:TextBox runat="server" ID="IdSousFam" visible="false" Enabled="false" />
                              </div>
                              <div class="large-5 columns">
                                  <label for="checkbox1">Quantif. / kwant. ?</label>
                                  <asp:CheckBox runat="server" ID="CheckQuantifiable" Enabled="false" />
                              </div>
                              <div class="large-5 columns">
                                  <label for="checkbox1">Ctrl ?</label>
                                  <asp:CheckBox runat="server" ID="Ctrl" Enabled="false" />
                              </div>
                          </div>                         
                       </fieldset>
                          <div class="row">
                              <div class="large-12 columns">
                                  <asp:Button ID="Ajout" Text="Add" runat="server" class="small round button" OnClick="Ajout_Click"/>
                                  <asp:Button ID="EnregistrerAjout" Text="Save" runat="server" OnClick="EnregistrerAjout_Click" class="small round button" />
                                  <asp:Button ID="Modifier" Text="Update" runat="server" OnClick="Modifier_Click" class="small round button" />
                                  <asp:Button ID="EnregistrerModif" Text="Save" runat="server" OnClick="EnregistrerModif_Click" class="small round button" />
                                  <asp:Button ID="Annuler" Text="Cancel" runat="server" OnClick="Annuler_Click" class="small round button" />
                                  <asp:Button ID="Supprimer" Text="Delete" runat="server" OnClick="Supprimer_Click" class="small round button" />
                                  <asp:Button ID="ConfirmerSuppression" Text="Confirm Delete" runat="server" OnClick="ConfirmerSuppression_Click" class="small round button" />
                                  <asp:Button ID="ViderFiche" Text="All fields --> Empty" runat="server" Visible="False" OnClick="ViderFiche_Click" class="small round button" />
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
</asp:Content>