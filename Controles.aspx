<%@ Page Title="Controls" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Controles.aspx.cs" Inherits="ManTools2020.Controles" %>
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
                                        <legend>Ajout Controles d'outillage / Gereedschappen controles toevoeging</legend>
                                        <div class="row">
                                            <div class="large-6 columns">
                                                <div class="row">
                                                    <div class="large-8 columns">
                                                        <label>Imputat. Ctrl<asp:RequiredFieldValidator Display="Dynamic" runat="server" ErrorMessage=" - <i>Mandatory field</i>" 
	                                                        ControlToValidate="DropDownImputCtrl" InitialValue="" ForeColor="Red"/></label>
                                                        <asp:DropDownList runat="server" ID="DropDownImputCtrl">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="large-4 columns">
                                                        <label>Tri / Soort</label>
                                                        <asp:DropDownList runat="server" ID="DropDownImputCtrlTri" AutoPostBack="true" OnSelectedIndexChanged="DropDownImputCtrlTri_SelectedIndexChanged">
                                                        </asp:DropDownList>                                                   
                                                    </div>
                                                </div>                                              
                                                <div class="row">
                                                    <div class="large-12 columns">
                                                        <label>Num du bon de contrôle / Controle bonnummer<asp:RequiredFieldValidator Display="Dynamic" runat="server" ErrorMessage=" - <i>Mandatory field</i>" 
	                                                        ControlToValidate="BonCtrlNumber" ForeColor="Red"/></label>
                                                        <asp:TextBox ID="BonCtrlNumber" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="large-12 columns">
                                                        <label ID="lbResCtrl">Result.</label>
                                                        <asp:DropDownList runat="server" ID="ddlResCtrl">
                                                        </asp:DropDownList>                                                   
                                                    </div>
                                                </div>                
                                                <div class="row">
                                                    <div class="large-12 columns">
                                                        <label>Comment. Ctrl</label>
                                                        <asp:TextBox ID="CommentCtrl" runat="server" />
                                                    </div>
                                                </div>                                                
                                                <div class="row">
                                                    <div class="large-12 columns">
                                                        <label>Type Choix / Kies Type</label>
                                                        <asp:DropDownList runat="server" ID="type">
                                                            <asp:ListItem Text="Choix sur l'imputation / Op Imputatie Kiezen" Value="0" />
                                                            <asp:ListItem Text="Choix global / Globale Kiezen" Value="1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="large-1 columns"></div>
                                            <div class="large-5 columns">
                                                <label>Date de Contrôle / Controle datum</label>
                                                <asp:Calendar ID="DateCtrl" runat="server"></asp:Calendar>
                                            </div>
                                        </div>
                                    </fieldset>
                                        <div class="row">
                                            <div class="large-6 columns">
                                                <asp:Button ID="Rechercher" Text="Sélectionner les outils / Gereedsch. Selecteren" class="small round button" runat="server" OnClick="search_Click" />
                                                <asp:Button ID="ReinitCrit" Text="Crit. --> 0" class="small round button" runat="server" OnClick="ReinitCrit_Click" />
                                            </div>
                                        </div>
                                </asp:Panel>
                                <!--end formulaire de recherche -->
                                <!--Start listing des outils -->
                                <asp:Panel ID="Datagrid" Visible="false" runat="server">

                                            <h5>Sélection des outils contrôlés / Selectie van de gecontroleerde gereedschappen</h5>
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
                                                            <asp:BoundColumn HeaderText="Outil / Gereedsch." DataField="idOutil" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Quantif. / Kwant." DataField="Quantifiable" Visible="false">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="NumOutil" HeaderText="Num.">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn HeaderText="Fam." DataField="Fam">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Position" HeaderText="Pos.">
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
                                                <asp:Button ID="CtrlBouton" Text="Init. Ctrl." class="small round button" runat="server" OnClick="CtrlBouton_Click" />
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
                                            Les Contrôles ont été créés / De contrôles werden toegevoegd
                                            </div>
                                        </div>
                                    </div>
                                    <h5>synthese :</h5>
                                    <div class="row">
                                            <div class="large-12 columns">
                                                <asp:DataGrid ID="GridResume" runat="server" PageSize="5000" AllowPaging="false"
                                                        AutoGenerateColumns="true" CellPadding="4" ForeColor="#284E98" GridLines="None" Width="955px">
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Size="Smaller" HorizontalAlign="Left" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                </asp:DataGrid>
                                            </div>                                            
                                        </div>
                                    <div class="row">
                                            <div class="large-12 columns">
                                                <asp:Button ID="Retour" Text="Retour / Terug" class="small round button" runat="server" OnClick="retour_Click" />
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