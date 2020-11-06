<%@ Page Title="Transfert" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transfert.aspx.cs" Inherits="ManTools2020.Transfert" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <div class="row">
        <div class="large-12 medium-8 columns">
            <div class="panel">
                <div class="row">
                    <div class="large-12 medium-6 columns">
                        <h3 >ENGIE Axima - Webtools </h3>
                            <div class="large-12 columns">
                                <!--Start formulaire de recherche -->
                                <asp:Panel ID="Form" Visible="true" runat="server">
                                <fieldset>
                                        <legend>Transfert d'outils / Gereedschappen transfert</legend>
                                        <div class="row">
                                            <div class="large-6 columns">
                                                <div class="row">
                                                    <div class="large-8 columns">
                                                        <label>Chantier d’origine / Oorsprong werf<asp:RequiredFieldValidator Display="Dynamic" runat="server" ErrorMessage=" - <i>Mandatory field</i>" 
	                                                        ControlToValidate="DropDownSource" InitialValue="" ForeColor="Red"/></label>
                                                        <asp:DropDownList runat="server" ID="DropDownSource">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="large-4 columns">
                                                        <label>Tri / Soort</label>
                                                        <asp:DropDownList runat="server" ID="DropDownSourceTri" AutoPostBack="true" OnSelectedIndexChanged="TriChantierOrigine_SelectedIndexChanged">
                                                        </asp:DropDownList>                                                   
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="large-8 columns">
                                                        <label>Chantier de destination / Destinatie werf<asp:RequiredFieldValidator Display="Dynamic" runat="server" ErrorMessage=" - <i>Mandatory field</i>" 
	                                                        ControlToValidate="DropDownDestination" InitialValue="" ForeColor="Red"/></label>
                                                        <asp:DropDownList runat="server" ID="DropDownDestination">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="large-4 columns">
                                                        <label>Tri / Soort</label>
                                                        <asp:DropDownList runat="server" ID="DropDownDestinTri" AutoPostBack="true" OnSelectedIndexChanged="TriChantierDestination_SelectedIndexChanged">
                                                        </asp:DropDownList>                                                   
                                                    </div>                                                
                                                </div>
                                                <div class="row">
                                                    <div class="large-10 columns">
                                                        <label>Num du bon de transfert / Tansfert bonnummer<asp:RequiredFieldValidator Display="Dynamic" runat="server" ErrorMessage=" - <i>Mandatory field</i>" 
	                                                        ControlToValidate="BonTransfertNumber" ForeColor="Red"/></label>
                                                        <asp:TextBox ID="BonTransfertNumber" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="large-10 columns">
                                                        <label>Type de transfert / Transfert Type</label>
                                                        <asp:DropDownList runat="server" ID="type">
                                                            <asp:ListItem Text="Choix d’outils / Gereedsch. selectie" Value="0" />
                                                            <asp:ListItem Text="Transfert global / Globale transfert" Value="1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="large-1 columns"></div>
                                            <div class="large-5 columns">
                                                <label>Date de transfert / Transfert datum</label>
                                                <asp:Calendar ID="DateTransfert" runat="server"></asp:Calendar>
                                            </div>
                                        </div>
                                    </fieldset>
                                        <div class="row">
                                            <div class="large-6 columns">
                                                <asp:Button ID="Rechercher" Text="Rechercher les outils / Gereedsch. zoeken" class="small round button" runat="server" OnClick="search_Click" />
                                                <asp:Button ID="ReinitCrit" Text="Crit. --> 0" class="small round button" runat="server" OnClick="ReinitCrit_Click" />
                                            </div>
                                        </div>
                                </asp:Panel>
                                <!--end formulaire de recherche -->
                                <!--Start listing des outils -->
                                <asp:Panel ID="Datagrid" Visible="false" runat="server">
                                          <h5>Outils à transférer de / Gereedsch. te verplaatsen van <asp:Label runat="server" ID="origin"></asp:Label> à / naar <asp:Label runat="server" ID="destination"></asp:Label></h5>
                                          <div class="row">
                                                <div class="large-6 columns">
                                                    <asp:Label Text="Tous" ID="GridLabel" runat="server" Visible="false"></asp:Label>
                                                    <asp:DataGrid ID="Grid" runat="server" PageSize="5000" AllowPaging="false" DataKeyField="idOutil"
                                                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#284E98" GridLines="None" Width="955px">
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Sel.">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox id="caseChecked" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn HeaderText="Outil / G  ereedsch." DataField="idOutil" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Quantif. / Kwant." DataField="Quantifiable" Visible="false">
                                                            </asp:BoundColumn>                                                            
                                                            <asp:BoundColumn DataField="NumOutil" HeaderText="Num.">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Fam." DataField="Fam">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Position" HeaderText="Pos.">
                                                            </asp:BoundColumn>
                                                            <asp:TemplateColumn HeaderText="Nb/Aant">
                                                                <ItemTemplate>
                                                                    <asp:TextBox id="Nombre" Columns="5" Text="1" runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:BoundColumn HeaderText="Qte" DataField="Qte" Visible="false">
                                                            </asp:BoundColumn>
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Size="Smaller" HorizontalAlign="Left" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:DataGrid>
                                                </div>
                                            </div>

                                        <div class="row">
                                            <div class="large-12 columns">
                                                <asp:Button ID="transfertBouton" Text="Transfer" class="small round button" runat="server" OnClick="transfertBouton_Click" />
                                                &nbsp;
                                                <asp:Button ID="Annuler" Text="Cancel" class="small round button" runat="server" OnClick="Annuler_Click" />
                                            </div>                                            
                                        </div>
                                </asp:Panel>
                                <!--End listing des outils -->
                                <!--Start message ok -->
                                <asp:Panel ID="ok" Visible="false" runat="server">
                                    <div class="row">
                                        <div class="large-12 columns">
                                            <div data-alert class="alert-box success radius">
                                            Transfer synth. de / van <asp:Label runat="server" ID="source"></asp:Label> à / tot <asp:Label runat="server" ID="dest"></asp:Label> s'est bien effectué / werd uitgevoerd</br></br>
                                            Nombre total d'outils transférés / Totaal Gereedsch. getransfereerd : <b><asp:Label runat="server" ID="NbOutils"></asp:Label></b>
                                            </div>
                                        </div>
                                    </div>
                                    <h5>synthese du / van Transfert :</h5>
                                    <div class="row">
                                            <div class="large-12 columns">
                                                <asp:DataGrid ID="GridResume" runat="server" PageSize="5000" AllowPaging="false" AutoGenerateColumns="True" CellPadding="4" ForeColor="#284E98" GridLines="None" Width="955px">
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Size="Smaller" HorizontalAlign="Left" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:DataGrid>
                                            </div>                                            
                                        </div>
                                    <div class="row">
                                            <div class="large-12 columns">
                                                <asp:Button ID="PDF" Text="Download PDF" class="small round button" runat="server" OnClick="PDF_Click"/>
                                                <asp:Button ID="CSV" Text="Download CSV" class="small round button" runat="server" OnClick="CSV_Click"/>
                                                <asp:Button ID="Retour" Text="Retour / Terug" class="small round button" runat="server" OnClick="retour_Click" />
                                                <asp:DropDownList ID="langue" runat="server" Visible="true" Width="50">
                                                  <asp:ListItem Text="FR" Value="FR" />  
                                                  <asp:ListItem Text="NL" Value="NL" />
                                                </asp:DropDownList>  
                                            </div>                                            
                                    </div>
                                </asp:Panel>
                                <!--End message ok -->
                            </div>
                    </div>
                </div>
            </div>
        </div>   
</asp:Content>
