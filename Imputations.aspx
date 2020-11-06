<%@ Page Title="Imputations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Imputations.aspx.cs" Inherits="ManTools2020.Imputations" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">

    <div class="row">
        <div class="large-12 medium-8 columns">
            <div class="panel">
                <div class="row">
                    <div class="large-12 medium-6 columns">
                        <h3 >ENGIE Axima - Webtools </h3>
                            <div class="large-12 columns">
                                <!--Start moteur de recherche-->
                                <fieldset>
                                        <legend>Moteur de recherche sur les Imputations / Zoek op Imputaties</legend>
                                        <div class="row">
                                            <div class="large-2 columns">
                                                <label>Code</label>
                                                <asp:TextBox ID="CodeImputation" runat="server" />
                                            </div>
                                           <div class="large-4 columns">
                                                <label>Nom chantier / Werf naam</label>
                                                <asp:TextBox ID="NomChantier" runat="server" />
                                            </div>
                                           <div class="large-2 columns">
                                                <label>CodePost. / Postcode</label>
                                                <asp:TextBox ID="CodePostal" runat="server" />
                                            </div>
                                           <div class="large-4 columns">
                                                <label>Localité / Plaats</label>
                                                <asp:TextBox ID="Localite" runat="server" />
                                            </div>
                                            <div class="large-2 columns">
                                                <label>Service / Dienst</label>
                                                <asp:DropDownList runat="server" ID="DropDownServices">
                                                </asp:DropDownList>
                                            </div>                                          
                                            <div class="large-4 columns">
                                                <label>Nom chef Chantier / Werf Verantw. Naam</label>
                                                <asp:TextBox ID="ChefChantier" runat="server" />
                                            </div>                                       
                                            <div class="large-4 columns">
                                                <label>Chargé d'affaire / zaakgelastigde</label>
                                                <asp:TextBox ID="ChargeAffaire" runat="server" />
                                            </div>                                       
                                        </div>
                                    </fieldset>
                                        <div class="row">
                                            <div class="large-4 columns">
                                                <asp:Button ID="Rechercher" Text="Rechercher / Zoeken" class="small round button" runat="server" OnClick="Rechercher_Click" />
                                                <asp:Button ID="SupprimerCritères" Text="Crit --> 0" class="small round button" runat="server" OnClick="SupprimerCritères_Click" />                       
                                            </div>
                                            <div class="large-2 columns"></div>
                                            <div class="large-4 columns">
                                                <asp:Label ID="Lblresults" runat="server" Visible="false" Text="" />
                                            </div>
                                        </div>
                                
                                <!--END moteur de recherche-->
                                <!--Start resultat recherche-->
                                <asp:GridView ID="gridviewdata" runat="server" Visible="False" CellPadding="2" ForeColor="#284E98" GridLines="None" HorizontalAlign="Left" Width="955px">
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
                                <!--END resultat recherche-->
                                <!--butom pdf csv-->
                                <asp:Button ID="btnDownloadPDF" Text="Donwload PDF" runat="server" OnClick="Download_Pdf" Visible="false" class="small round button" />
                                <asp:Button ID="btnDownloadCSV" Text="Donwload XLS" runat="server" OnClick="Download_Csv" Visible="false" class="small round button" />
                            </div>
                    </div>
                </div>
            </div>
        </div>
  </div>   
</asp:Content>
