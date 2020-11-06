<%@ Page Title="OutilFiche" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OutilFiche.aspx.cs" Inherits="ManTools2020.OutilFiche" %>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
 
    <div class="row">
        <div class="large-12 medium-8 columns">
            <div class="panel">
                <div class="row">
                    <div class="large-12 medium-6 columns">

                        <h4>ENGIE Axima - Webtools</h4>

                        <!--================================start Ecran 1=========================-->

                   <fieldset>
                     <legend>Fiche Outil en fonction</legend>
                       <div class="large-12 medium-6 columns">
                            <div class="large-2 columns">
                                <label>Fam.</label>
                                <asp:TextBox runat="server" ID="NumFam" Enabled="false" />
                            </div>
                            <div class="large-4 columns">
                                <label>Fam. Descr. / Omschr.</label>
                                <asp:TextBox runat="server" ID="DescrFamFr" Enabled="false" />
                            </div>
                            <div class="large-2 columns">
                                <label>Owner</label>
                                <asp:DropDownList runat="server" ID="ddlPropr" Enabled="false"></asp:DropDownList>
                            </div>
                            <div class="large-2 columns">
                                <label>Tarif / Tarief</label>
                                <asp:TextBox runat="server" ID="Taux" Enabled="false" />
                            </div>
                            <div class="large-2 columns">
                                <label>Qté tot. / Tot. Aantal</label>
                                <asp:TextBox runat="server" ID="Total" Enabled="false" />
                                <asp:CheckBox runat="server" ID="CheckQuantifiable" Visible="false" Enabled="false" />
                            </div>
                            <!--<div class="large-2 columns">
                                <label>Etat / Staat Ctrl.</label>
                                <asp:TextBox runat="server" ID="EtatControl" Enabled="false" />
                            </div>-->
                        </div>
                            <!--2iemeligne-->
                       <div class="large-12 medium-6 columns">
                            <div class="large-2 columns">
                                <label>S-Fam</label>
                                <asp:TextBox runat="server" ID="SsFam" Enabled="false" />
                                <asp:DropDownList ID="DropDownSFamille" runat="server" visible="false" DataSourceID="SqlDataSource1" DataValueField="NumSousFamille" AutoPostBack="true" OnSelectedIndexChanged="DropDownSFamille_SelectedIndexChanged"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' ></asp:SqlDataSource>
                            </div>
                            <div class="large-4 columns">
                                <label>S-Fam. Descr. / Omschr.</label>
                                <asp:TextBox runat="server" ID="SsFamFR" Enabled="false" />
                                <asp:DropDownList runat="server" ID="DropDownSFamilleFR" visible="false" DataSourceID="SqlDataSource2" DataValueField="DescriptionSousFamilleFr" AutoPostBack="true" OnSelectedIndexChanged="DropDownSFamilleFR_SelectedIndexChanged"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings: conn %>' ></asp:SqlDataSource>
                                <asp:DropDownList runat="server" ID="DropDownSFamilleNL" visible="false" DataSourceID="SqlDataSource11" DataValueField="DescriptionSousFamilleNl" AutoPostBack="true" OnSelectedIndexChanged="DropDownSFamilleNL_SelectedIndexChanged"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource11" ConnectionString='<%$ ConnectionStrings: conn %>' ></asp:SqlDataSource>
                           </div>

                            <div class="large-2 columns">
                                <label>N outil / Gereedsch.</label>
                                <asp:TextBox runat="server" ID="NOutil" Enabled="false" />
                            </div>
                            <div class="large-4 columns">                            
                                <br />
                                <asp:Label ID="lbStatutCtrl" runat="server" font Font-Size ="Small" /><br /><br />
                                <asp:Label ID="lbStatutEtal" runat="server" font Font-Size ="Small" />
                            <!--<div class="large-2 columns">
                                <label>Etat étalonn. / Ijking</label>
                                <asp:TextBox runat="server" ID="EtatVerif" Enabled="false" />
                            </div>-->
                            </div>
                            <div class="large-1 columns">
                                <asp:TextBox runat="server" ID="IdOutil" visible="false" Enabled="false" />
                            </div>
                            <div class="large-1 columns">
                                <asp:TextBox runat="server" ID="refSousFamille" visible="false" Enabled="false" />
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                    </div>

                            <!--==================================================================End Ecran 1=============================================================-->

                               
                            <!--==================================================================Start Ecran 2=============================================================-->

                            <div class="large-12 columns">                                                                        
                                <dl class="tabs" data-tab>
                                    <dd><asp:Button ID="btnCarTechn" Text="Caract / Data Techn." runat="server" OnClick="btnCarTechn_Click"  class= "small button" ForeColor="Black" Font-Size="Medium"/></dd>
                                    <dd><asp:Button ID="btnCde" Text="Commande / Bestelling" runat="server" OnClick="btnCde_Click"  class= "small button" ForeColor="Black" Font-Size="Medium"/></dd>
                                    <dd><asp:Button ID="btnDivers" Text="Divers(e)" runat="server" OnClick="btnDivers_Click"  class= "small button" ForeColor="Black" Font-Size="Medium"/></dd>
                                </dl>                                                         
                                <div class="tabs-content">
                                    <!--================================================Onglet 1======================================-->
                                <asp:Panel Visible="true" ID="pnlCarTechn" runat="server">
                                        <div class="content active" id="panel2-1">
                                        <!--========== 1er ligne =======-->
                                        <div class="large-12 columns">
                                            <br />
                                            <div class="large-7 columns">
                                                <label>Marque / Merk</label>
                                                <asp:TextBox ID="txtMarque" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="DropDownMarque" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownMarque_SelectedIndexChanged" Enabled="false"></asp:DropDownList>
                                            </div>
                                            <div class="large-1 columns">
                                                <label>Choice</label>
                                                <asp:DropDownList runat="server" ID="DropDownMarqueChoix" AutoPostBack="true" OnSelectedIndexChanged="DropDownMarqueChoix_SelectedIndexChanged" Enabled="false" >
                                                </asp:DropDownList>
                                            </div>                                            
                                            <div class="large-3 columns">
                                                <label>Type</label>
                                                <asp:TextBox ID="txtType" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="DropDownType" runat="server" Enabled="false"></asp:DropDownList>
                                            </div>
                                            <div class="large-1 columns">
                                                <label>Choice</label>
                                                <asp:DropDownList runat="server" ID="DropDownTypeChoix" AutoPostBack="true" OnSelectedIndexChanged="DropDownTypeChoix_SelectedIndexChanged" Enabled="false" >
                                                </asp:DropDownList>
                                            </div>                                           
                                            <div class="large-4 columns">
                                                <label>N° de série / serienummer</label>
                                                <asp:TextBox ID="NSerie" runat="server" Enabled="false" ></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Gamme / Reeks</label>
                                                <asp:TextBox ID="txtGamme" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="DropDownGamme" runat="server" Enabled="false"></asp:DropDownList>
                                            </div>
                                            <div class="large-1 columns">
                                                <label>Choice</label>
                                                <asp:DropDownList runat="server" ID="DropDownGammeChoix" AutoPostBack="true" OnSelectedIndexChanged="DropDownGammeChoix_SelectedIndexChanged" Enabled="false" >
                                                </asp:DropDownList>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Proc. Techn.</label>
                                                <asp:TextBox ID="txtUnite2" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                                <asp:DropDownList ID="DropDownUnite2" runat="server" Enabled="false"></asp:DropDownList>
                                           </div>
                                             <div class="large-1 columns">
                                                <label>Choice</label>
                                                <asp:DropDownList runat="server" ID="DropDownProcTechnChoix" AutoPostBack="true" OnSelectedIndexChanged="DropDownProcTechnChoix_SelectedIndexChanged" Enabled="false" >
                                                </asp:DropDownList>
                                            </div>                                       
                                        </div> <!--Fermeture 2eime ligne-->
                                    </div> <!--Fermeture onglet1-->
                                    </asp:Panel>
                                    <asp:Panel Visible="false" ID="pnlCde" runat="server">
                                    <!--================================================Onglet 2========================================-->
                                    <div class="content" id="panel2-2">
                                        <!--========== 1er ligne =======-->
                                        <div class="large-12 columns">
                                            <br/>
                                            <div class="large-12 columns">
                                                <label>Fournisseur / Leverancier</label>
                                                <asp:TextBox ID="Fournisseur" runat="server" Enabled="false" ></asp:TextBox>
                                            </div>
                                            <div class="large-4 columns">
                                                <label>Date comm. / Best. datum</label>
                                                <asp:TextBox ID="DateCommande" runat="server" Enabled="false" ></asp:TextBox>
                                            </div>
                                            <div class="large-4 columns">
                                                <label>N° Comm. / Best. nummer</label>
                                                <asp:TextBox ID="NCommande" runat="server" Enabled="false" ></asp:TextBox>
                                            </div>
                                            <div class="large-4 columns">
                                                <label>Montant / Bedrag</label>
                                                <asp:TextBox ID="Montant" runat="server" Enabled="false" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    </asp:Panel>
                                    <asp:Panel Visible="false" ID="pnlDivers" runat="server">
                                    <!--====================================================Onglet 3======================================-->
                                    <div class="content" id="panel2-3">
                                            <div class="large-12 columns">
                                            <br/>
                                        <div class="large-2 columns">
                                            <label>Année / Jaar</label>
                                            <asp:TextBox ID="Annee" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                                            <asp:DropDownList ID="DropDownAnnee" runat="server" Enabled="false"></asp:DropDownList>
                                        </div>
                                        <div class="large-2 columns">
                                            <label>Choice</label>
                                            <asp:DropDownList runat="server" ID="DropDownAnneeChoix" AutoPostBack="true" OnSelectedIndexChanged="DropDownAnneeChoix_SelectedIndexChanged" Enabled="false" >
                                            </asp:DropDownList>
                                        </div> 
                                        <div class="large-8 columns">
                                            <label>Notes / Nota</label>
                                            <asp:TextBox ID="Notes" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="large-4 columns">
                                            <label>Int Etal / ijking</label>
                                            <asp:DropDownList ID="DropDownIntVerif" runat="server" Enabled="false"></asp:DropDownList>
                                       </div>
                                        <div class="large-8 columns">
                                            <label>Accessoires</label>
                                            <asp:TextBox ID="Accessoires" runat="server" Enabled="false" ></asp:TextBox>
                                        </div>
                                    </div>
                                    </div> <!--Fermeture onglet3-->
                                    </asp:Panel>
                                    <!--Fermeture du 1er Tabs Content-->
                                </div>
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

                    <!--==============================================================================End Ecran 2=================================================-->
                
                <dd class="accordion-navigation">
                <!--<a href="#panel2a">&nbsp;&nbsp;&nbsp;Listes complémentaires</a>-->
                </fieldset>
                <fieldset>
                <legend>Listes complémentaires / Andere lijsten</legend>
                <br/>
                    <div id="panel2a" class="content active">

                    <div class="row">
                        <div class="large-12 medium-6 columns">
                                    <!--=============================================================================Start Ecran 3===================================================-->
                                    <div class="large-12 columns">
                                        <dl class="tabs" data-tab>
                                            <dd><asp:Button ID="Position" Text="Positions / Posities" runat="server" OnClick="Position_Click"  class= "small button" ForeColor="Black" Font-Size="Medium"/></dd>
                                            <dd><asp:Button ID="Controle" Text="Contrôles / Keuringen" runat="server" class="small button" OnClick="Controle_Click" ForeColor="Black" Font-Size="Medium"/></dd>
                                            <dd><asp:Button ID="Entr" Text="Entr. / Répar." runat="server" class="small button" OnClick="Entr_Click" ForeColor="Black" Font-Size="Medium"/></dd>
                                            <dd><asp:Button ID="Etalonnage" Text="Etalonnages / Ijkingen" runat="server" class="small button" OnClick="Etalonnage_Click" ForeColor="Black" Font-Size="Medium"/></dd>
                                        </dl>
                                        
                                    
                                    <div class="tabs-content">
                                        <!--================================================================Onglet 1=========================-->
                                    <asp:Panel Visible="true" ID="panel25" runat="server">
                                        <div class="content active" id="panel2-5">
                                            <div class="large-12 columns">
                                                <br />
                                                <!--BOUTONS-->
                                                <div class="row">
                                                    <div class="large-12 columns">
                                                        &nbsp;&nbsp;<asp:Button ID="AjoutPos" Text="Add" runat="server" class="small round button" OnClick="AjoutPos_Click" />
                                                        <asp:Button ID="EnregistrerAjoutPos" Text="Save" runat="server" OnClick="EnregistrerAjoutPos_Click" class="small round button" />
                                                        <asp:Button ID="ModifierPos" Text="Update" runat="server" OnClick="ModifierPos_Click" class="small round button" />
                                                        <asp:Button ID="EnregistrerModifPos" Text="Save" runat="server" OnClick="EnregistrerModifPos_Click" class="small round button" />
                                                        <asp:Button ID="AnnulerPos" Text="Cancel" runat="server" OnClick="Annuler_Click" class="small round button" />
                                                        <asp:Button ID="SupprimerPos" Text="Delete" runat="server" OnClick="SupprimerPos_Click" class="small round button" />
                                                        <asp:Button ID="ConfirmerSuppressionPos" Text="Confirm Delete" runat="server" OnClick="ConfirmerSuppressionPos_Click" class="small round button" />
                                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessagePos" runat="server" Visible="False" ForeColor="#FF3300" />
                                                    </div>
                                                </div>
                                                <asp:GridView ID="gridviewPos" runat="server" Visible="True" CellPadding="2" ForeColor="#284E98" GridLines="None" HorizontalAlign="Left" Width="776px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#0099CC" BorderStyle="Solid" Font-Size="Smaller" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Size="Smaller" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbDatePos" Visible="false">Date/Datum.</asp:label>
                                                    <asp:Calendar ID="calDatePos" runat="server"  Visible="false"></asp:Calendar>
                                                </div>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbDocPos" Visible="false">Doc.</asp:label>
                                                    <asp:TextBox runat="server" ID="DocPos" Visible="false" />
                                                </div>
                                                <div class="large-2 columns">
                                                    <asp:label runat="server" ID="lbQtePos" Visible="false">Qte.</asp:label>
                                                    <asp:TextBox runat="server" ID="QtePos" Visible="false" Enabled="false" Text="1"/>
                                                </div>                                                
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbImputPosOrigine" Visible="false">Imput. From</asp:label>
                                                    <asp:TextBox runat="server" ID="txtImputPosOrigine" Visible="false" Enabled="false"/>
                                                    <asp:DropDownList runat="server" ID="ddlImputPosOrigine" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlImputPosOrigine_SelectedIndexChanged"></asp:DropDownList>
                                               </div>
                                                <div class="large-2 columns">
                                                    <asp:label runat="server" ID="lbSortPosOrigine" Visible="false">Sort</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlImputPosOrigineTri" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlImputPosOrigineTri_SelectedIndexChanged">
                                                    </asp:DropDownList>                                                   
                                                </div>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbImputPos" Visible="false">Imput. To</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlImputPos" Visible="false"></asp:DropDownList>
                                                </div>
                                                <div class="large-2 columns">
                                                    <asp:label runat="server" ID="lbSortPos" Visible="false">Sort</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlImputPosTri" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlImputPosTri_SelectedIndexChanged">
                                                    </asp:DropDownList>                                                   
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel Visible="false" ID="panel26" runat="server">
                                        <!--===========================================================Onglet 2=================================-->
                                        <div class="content " id="panel2-6">
                                            <div class="large-12 columns">
                                                <br />
                                                <!--BOUTONS-->
                                                <div class="row">
                                                    <div class="large-12 columns">
                                                        &nbsp;&nbsp;<asp:Button ID="AjoutCtrl" Text="Add" runat="server" class="small round button" OnClick="AjoutCtrl_Click" />
                                                        <asp:Button ID="EnregistrerAjoutCtrl" Text="Save" runat="server" OnClick="EnregistrerAjoutCtrl_Click" class="small round button" />
                                                        <asp:Button ID="ModifierCtrl" Text="Update" runat="server" OnClick="ModifierCtrl_Click" class="small round button" />
                                                        <asp:Button ID="EnregistrerModifCtrl" Text="Save" runat="server" OnClick="EnregistrerModifCtrl_Click" class="small round button" />
                                                        <asp:Button ID="AnnulerCtrl" Text="Cancel" runat="server" OnClick="Annuler_Click" class="small round button" />
                                                        <asp:Button ID="SupprimerCtrl" Text="Delete" runat="server" OnClick="SupprimerCtrl_Click" class="small round button" />
                                                        <asp:Button ID="ConfirmerSuppressionCtrl" Text="Confirm Delete" runat="server" OnClick="ConfirmerSuppressionCtrl_Click" class="small round button" />
                                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessageCtrl" runat="server" Visible="False" ForeColor="#FF3300" />
                                                    </div>
                                                </div>
                                                <asp:GridView ID="gridviewCtrl" runat="server" Visible="True" CellPadding="2" ForeColor="#284E98" GridLines="None" HorizontalAlign="Left" Width="776px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#0099CC" BorderStyle="Solid" Font-Size="Smaller" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Size="Smaller" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbDateCtrl" Visible="false">Date/Datum.</asp:label>
                                                    <asp:Calendar ID="calDateCtrl" runat="server"  Visible="false"></asp:Calendar>
                                                </div>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbDocCtrl" Visible="false">Doc.</asp:label>
                                                    <asp:TextBox runat="server" ID="DocCtrl" Visible="false" />
                                                </div>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbImputCtrl" Visible="false">Imput.</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlImputCtrl" Visible="false"></asp:DropDownList>
                                                </div>
                                                <div class="large-2 columns">
                                                    <asp:label runat="server" ID="lbSortCtrl" Visible="false">Sort</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlImputCtrlTri" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlImputCtrlTri_SelectedIndexChanged">
                                                    </asp:DropDownList>                                                   
                                                </div>
                                                <div class="large-7 columns">
                                                    <asp:label runat="server" ID="lbResCtrl" Visible="false">Res</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlResCtrl" Visible="false">
                                                    </asp:DropDownList>                                                   
                                                </div>
                                                <div class="large-7 columns">
                                                    <asp:label runat="server" ID="lbCommentCtrl" Visible="false">Comment</asp:label>
                                                    <asp:TextBox runat="server" ID="CommentCtrl" Visible="false" />                                                                                                       
                                                </div>                                                
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel Visible="false" ID="panel27" runat="server">
                                        <!--===========================================================Onglet 3=================================-->
                                        <div class="content " id="panel2-7">
                                            
                                            <!--========== 2ieme ligne =======-->
                                            <div class="large-12 columns">
                                                <br />
                                                <!--BOUTONS-->
                                                <div class="row">
                                                    <div class="large-12 columns">
                                                        &nbsp;&nbsp;<asp:Button ID="AjoutEntr" Text="Add" runat="server" class="small round button" OnClick="AjoutEntr_Click" />
                                                        <asp:Button ID="EnregistrerAjoutEntr" Text="Save" runat="server" OnClick="EnregistrerAjoutEntr_Click" class="small round button" />
                                                        <asp:Button ID="ModifierEntr" Text="Update" runat="server" OnClick="ModifierEntr_Click" class="small round button" />
                                                        <asp:Button ID="EnregistrerModifEntr" Text="Save" runat="server" OnClick="EnregistrerModifEntr_Click" class="small round button" />
                                                        <asp:Button ID="AnnulerEntr" Text="Cancel" runat="server" OnClick="Annuler_Click" class="small round button" />
                                                        <asp:Button ID="SupprimerEntr" Text="Delete" runat="server" OnClick="SupprimerEntr_Click" class="small round button" />
                                                        <asp:Button ID="ConfirmerSuppressionEntr" Text="Confirm Delete" runat="server" OnClick="ConfirmerSuppressionEntr_Click" class="small round button" />
                                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessageEntr" runat="server" Visible="False" ForeColor="#FF3300" />
                                                    </div>
                                                </div>
                                                <asp:GridView ID="gridviewEntr" runat="server" Visible="True" CellPadding="2" ForeColor="#284E98" GridLines="None" HorizontalAlign="Left" Width="776px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#0099CC" BorderStyle="Solid" Font-Size="Smaller" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Size="Smaller" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbDateEntr" Visible="false">Date/Datum.</asp:label>
                                                    <asp:Calendar ID="calDateEntr" runat="server"  Visible="false"></asp:Calendar>
                                                </div>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbDocEntr" Visible="false">Doc.</asp:label>
                                                    <asp:TextBox runat="server" ID="DocEntr" Visible="false" />
                                                </div>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbImputEntr" Visible="false">Imput.</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlImputEntr" Visible="false"></asp:DropDownList>
                                                </div>
                                                <div class="large-2 columns">
                                                    <asp:label runat="server" ID="lbSortEntr" Visible="false">Sort</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlImputEntrTri" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlImputEntrTri_SelectedIndexChanged">
                                                    </asp:DropDownList>                                                   
                                                </div>
                                                <div class="large-7 columns">
                                                    <asp:label runat="server" ID="lbCommentEntr" Visible="false">Comment.</asp:label>
                                                    <asp:TextBox runat="server" ID="CommentEntr" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel Visible="false" ID="panel28" runat="server">
                                        <!--===========================================================Onglet 4=================================-->
                                        <div class="content " id="panel2-8">                              
                                            <div class="large-12 columns">
                                                <br />
                                                <!--BOUTONS-->
                                                <div class="row">
                                                    <div class="large-12 columns">
                                                        &nbsp;&nbsp;<asp:Button ID="AjoutVerif" Text="Add" runat="server" class="small round button" OnClick="AjoutVerif_Click" />
                                                        <asp:Button ID="EnregistrerAjoutVerif" Text="Save" runat="server" OnClick="EnregistrerAjoutVerif_Click" class="small round button" />
                                                        <asp:Button ID="ModifierVerif" Text="Update" runat="server" OnClick="ModifierVerif_Click" class="small round button" />
                                                        <asp:Button ID="EnregistrerModifVerif" Text="Save" runat="server" OnClick="EnregistrerModifVerif_Click" class="small round button" />
                                                        <asp:Button ID="AnnulerVerif" Text="Cancel" runat="server" OnClick="Annuler_Click" class="small round button" />
                                                        <asp:Button ID="SupprimerVerif" Text="Delete" runat="server" OnClick="SupprimerVerif_Click" class="small round button" />
                                                        <asp:Button ID="ConfirmerSuppressionVerif" Text="Confirm Delete" runat="server" OnClick="ConfirmerSuppressionVerif_Click" class="small round button" />
                                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMessageVerif" runat="server" Visible="False" ForeColor="#FF3300" />
                                                    </div>
                                                </div>
                                                <asp:GridView ID="gridviewVerif" runat="server" Visible="True" CellPadding="2" ForeColor="#284E98" GridLines="None" HorizontalAlign="Left" Width="776px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <EditRowStyle BackColor="#0099CC" BorderStyle="Solid" Font-Size="Smaller" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Size="Smaller" HorizontalAlign="Left" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbDateVerif" Visible="false">Date/Datum.</asp:label>
                                                    <asp:Calendar ID="calDateVerif" runat="server"  Visible="false"></asp:Calendar>
                                                </div>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbDocVerif" Visible="false">Doc.</asp:label>
                                                    <asp:TextBox runat="server" ID="DocVerif" Visible="false" maxlength="12" />
                                                </div>
                                                <div class="large-5 columns">
                                                    <asp:label runat="server" ID="lbLaboVerif" Visible="false">Labo.</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlLaboVerif" Visible="false"></asp:DropDownList>
                                                </div>
                                                <div class="large-7 columns">
                                                    <asp:label runat="server" ID="lbResVerif" Visible="false">Res</asp:label>
                                                    <asp:DropDownList runat="server" ID="ddlResVerif" Visible="false">
                                                    </asp:DropDownList>                                                   
                                                </div>
                                                <div class="large-7 columns">
                                                    <asp:label runat="server" ID="lbCommentVerif" Visible="false">Comment</asp:label>
                                                    <asp:TextBox runat="server" ID="CommentVerif" Visible="false" />
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                   </div><!--Fermeture du 2iemes Tabs Content-->
                               
                               </div>
                                                   
                        </div>
                    </div> <!--END 3eime écran-->
                </div>
                </dd>
                </dl>
        </fieldset>
        </div>
        </div>
        </div>
        </div>
        </div>

      <!--================================================================POP UP=======================================================================================-->  

    <div id="myModal1" class="reveal-modal" data-reveal> <!--Ce qui va être écrit ds le pop up-->
		<fieldset  class="lead"> <!--========== 1er ligne =======-->
			 <legend class="title">Positions</legend>
		         <div class="large-2 columns">
                                                <label>Date Trans</label>
                                                <asp:TextBox ID="TextBox29" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-2 columns">
                                                <label>N°document</label>
                                                <asp:TextBox ID="TextBox30" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-2 columns">
                                                <label>Origine</label>
                                                <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-1 columns">
                                                <label>QTé</label>
                                                <asp:TextBox ID="TextBox32" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-2 columns">
                                                <label>Destination</label>
                                                <asp:TextBox ID="TextBox33" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Nom Chantier</label>
                                                <asp:TextBox ID="TextBox34" runat="server"></asp:TextBox>
                                            </div>			
		</fieldset>
        <fieldset  class="lead"> <!--========== 2ieme ligne =======-->
			 <legend class="title">Contrôles</legend>
		                                    <div class="large-3 columns">
                                                <label>Date Contrôle</label>
                                                <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>N°document</label>
                                                <asp:TextBox ID="TextBox17" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Imputation</label>
                                                <asp:TextBox ID="TextBox18" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Commentaire</label>
                                                <asp:TextBox ID="TextBox19" runat="server"></asp:TextBox>
                                            </div>
			
			
		</fieldset>
        <fieldset  class="lead"> <!--========== 3iem ligne =======-->
			 <legend class="title">Entretiens / Réparations</legend>
		
                                            <div class="large-3 columns">
                                                <label>Date E/R</label>
                                                <asp:TextBox ID="TextBox20" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>N°document</label>
                                                <asp:TextBox ID="TextBox21" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Imputation</label>
                                                <asp:TextBox ID="TextBox22" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Commentaire</label>
                                                <asp:TextBox ID="TextBox23" runat="server"></asp:TextBox>
                                            </div>
		</fieldset>
        <fieldset  class="lead"> <!--========== 4er ligne =======-->
			 <legend class="title">Vérifications</legend>
		                                <div class="large-2 columns">
                                                <label>Date Vérif</label>
                                                <asp:TextBox ID="TextBox24" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-2 columns">
                                                <label>N°document</label>
                                                <asp:TextBox ID="TextBox25" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-2 columns">
                                                <label>Labo</label>
                                                <asp:TextBox ID="TextBox26" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Degré de Vérif</label>
                                                <asp:TextBox ID="TextBox27" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Résultat</label>
                                                <asp:TextBox ID="TextBox28" runat="server"></asp:TextBox>
                                            </div>
			
			
		</fieldset>
                                            <div class="large-2 columns">
                                                <asp:Button ID="Button5" runat="server" Text="Ajouter" class="small round button" />
                                            </div>
                                            <div class="large-10 columns">
                                                <asp:Button ID="Button6" runat="server" Text="Mise à jour" class="small round button" />
                                            </div>
			<a class="close-reveal-modal">&#215;</a> <!--On ferme le pop up--> 
	</div>
 
    
  
</asp:Content>
