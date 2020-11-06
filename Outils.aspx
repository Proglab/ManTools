<%@ Page Title="Outils" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Outils.aspx.cs" Inherits="ManTools2020.Outils" %>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
    <div class="row">
		<div class="large-12 medium-8 columns">
			<div class="panel"> 
				<div class="row">
					<div class="large-12 medium-6 columns">	
                                <h3>ENGIE Axima - Webtools </h3>
                        <div class="row">
                            <div class="large-12 columns">        
                                <fieldset>
                                    <legend>Moteur de recherche sur les Outils / Zoek op gereedchappen</legend>
                                    <div class="row">
                                        <div class="large-1 columns">
                                            <label>Fam</label>
                                            <asp:DropDownList runat="server" ID="DropDownFamille" DataSourceID="SqlDataSource7" DataTextField="codeFamille" DataValueField="codeFamille" AutoPostBack="true" OnSelectedIndexChanged="DropDownFamille_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource7" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand="SELECT '' AS [codeFamille] UNION SELECT [codeFamille] FROM [tblpriFamilles] ORDER BY [codeFamille]"></asp:SqlDataSource>
                                        </div>
                                        <div class="large-4 columns">
                                            <!--<asp:label runat="server" ID="FamDescrFR" css="label">Famille Description FR</asp:label>-->
                                            <label>Fam. Descr. / Omschr. FR</label>
                                            <asp:DropDownList runat="server" ID="DropDownFamilleFR" DataSourceID="SqlDataSource8" DataTextField="DescriptionFamilleFr" DataValueField="DescriptionFamilleFr" AutoPostBack="true" OnSelectedIndexChanged="DropDownFamilleFR_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource8" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand="SELECT '' AS [DescriptionFamilleFr] UNION SELECT [DescriptionFamilleFr] FROM [tblpriFamilles] ORDER BY [DescriptionFamilleFr]"></asp:SqlDataSource>
                                        </div>
                                        <div class="large-4 columns">
                                            <label>Fam. Descr. / Omschr. NL</label>
                                            <asp:DropDownList runat="server" ID="DropDownFamilleNL" DataSourceID="SqlDataSource10" DataTextField="DescriptionFamilleNl" DataValueField="DescriptionFamilleNl" AutoPostBack="true" OnSelectedIndexChanged="DropDownFamilleNL_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource10" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand="SELECT '' AS [DescriptionFamilleNl] UNION SELECT [DescriptionFamilleNl] FROM [tblpriFamilles] ORDER BY [DescriptionFamilleNl]"></asp:SqlDataSource>
                                        </div>

                                        <div class="large-1 columns">
                                            <label>Owner</label>
                                            <asp:DropDownList runat="server" ID="Propr"></asp:DropDownList>
                                        </div>                                        

                                        <div class="large-2 columns">
                                            <label>Bon transf</label>
                                            <asp:TextBox runat="server" ID="BonTransf"></asp:TextBox>
                                        </div>

                                        <!--<div class="large-2 columns">
                                            <label>Etat vérif</label>
                                            <asp:DropDownList ID="DropDownVerif" runat="server" DataSourceID="SqlDataSource9" DataTextField="DegresVerifOutil" DataValueField="DegresVerifOutil"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource9" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand=" SELECT '' AS [DegresVerifOutil] UNION SELECT  DISTINCT [DegresVerifOutil] FROM [tblpriOutil] ORDER BY [DegresVerifOutil]"></asp:SqlDataSource>
                                        </div>-->
                                    </div>

                                    <!--2ieme ligne-->
                                    <br />
                                    <div class="row">
                                        <div class="large-1 columns">
                                            <label>S-Fam</label>
                                            <asp:DropDownList ID="DropDownSFamille" runat="server" DataSourceID="SqlDataSource1" DataTextField="NumSousFamille" DataValueField="NumSousFamille" AutoPostBack="true" OnSelectedIndexChanged="DropDownSFamille_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand="SELECT '' AS [NumSousFamille]"></asp:SqlDataSource>
                                        </div>
                                        <div class="large-4 columns">
                                            <label>S-Fam. Descr. / Omschr. FR</label>
                                            <asp:DropDownList runat="server" ID="DropDownSFamilleFR" DataSourceID="SqlDataSource2" DataTextField="DescriptionSousFamilleFr" DataValueField="DescriptionSousFamilleFr" AutoPostBack="true" OnSelectedIndexChanged="DropDownSFamilleFR_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings: conn %>' SelectCommand="SELECT '' AS [DescriptionSousFamilleFr]"></asp:SqlDataSource>
                                        </div>
                                       <div class="large-4 columns">
                                            <label>S-Fam. Descr. / Omschr. NL</label>
                                            <asp:DropDownList runat="server" ID="DropDownSFamilleNL" DataSourceID="SqlDataSource11" DataTextField="DescriptionSousFamilleNl" DataValueField="DescriptionSousFamilleNl" AutoPostBack="true" OnSelectedIndexChanged="DropDownSFamilleNL_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource11" ConnectionString='<%$ ConnectionStrings: conn %>' SelectCommand="SELECT '' AS [DescriptionSousFamilleNl]"></asp:SqlDataSource>
                                        </div>
                                        <div class="large-3 columns">
                                            <label>Num Outil / Gereedsch.</label>
                                            <asp:TextBox runat="server" ID="NumOutil"></asp:TextBox>
                                        </div>                                         

                                        <!--<div class="large-2 columns">
                                            <label>Chantier</label>
                                            <asp:TextBox runat="server" ID="Chantier"></asp:TextBox>
                                        </div>-->

                                    </div>

                                    <!--3ieme ligne-->
                                    <div class="row">
                                        <div class="large-3 columns">
                                            <label>Marque / Merk</label>
                                            <asp:DropDownList ID="DropDownMarque" runat="server" DataSourceID="SqlDataSource3" DataTextField="MarqueOutil" DataValueField="MarqueOutil" AutoPostBack="true" OnSelectedIndexChanged="DropDownMarque_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource3" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand="SELECT DISTINCT [MarqueOutil] FROM [tblpriOutil] ORDER BY [MarqueOutil]"></asp:SqlDataSource>
                                        </div>

                                        <div class="large-3 columns">
                                            <label>Type</label>
                                            <asp:DropDownList ID="DropDownType" runat="server" DataSourceID="SqlDataSource4" DataTextField="TypeOutil" DataValueField="TypeOutil"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand="SELECT DISTINCT [TypeOutil] FROM [tblpriOutil] ORDER BY [TypeOutil]"></asp:SqlDataSource>
                                        </div>

                                        <div class="large-3 columns">
                                            <label>Année / Jaar</label>
                                            <asp:DropDownList ID="DropDownUtilisation" runat="server" DataSourceID="SqlDataSource6" DataTextField="Utilisation" DataValueField="Utilisation"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource6" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand="SELECT DISTINCT [Utilisation] FROM [tblpriOutil] ORDER BY [Utilisation]"></asp:SqlDataSource>
                                        </div>

                                        <div class="large-3 columns">
                                            <label>N° de série / Serienummer</label>
                                            <asp:TextBox ID="NumSerie" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <!--4ieme ligne-->
                                    <div class="row">                                    
                                        <div class="large-4 columns">
                                            <label>Pos. Choice in existing</label>
                                            <asp:DropDownList runat="server" ID="DropDownPosition" DataSourceID="SqlDataSource5" DataTextField="Imputation" DataValueField="codeImputation"></asp:DropDownList>
                                            <asp:SqlDataSource runat="server" ID="SqlDataSource5" ConnectionString='<%$ ConnectionStrings:WebToolsConnectionString %>' SelectCommand="SELECT '' AS [codeImputation], '' AS [Imputation] UNION SELECT codeImputation, CONCAT(codeImputation, ' - ',NomChantier) AS Imputation FROM  dbo.tblpriImputations ORDER BY codeImputation"></asp:SqlDataSource>
                                        </div> 
                                        <div class="large-2 columns">
                                            <label>Tri / Soort</label>
                                            <asp:DropDownList runat="server" ID="DropDownPosTri" AutoPostBack="true" OnSelectedIndexChanged="TriPos_SelectedIndexChanged">
                                            </asp:DropDownList>                                                   
                                        </div>
                                        <div class="large-2 columns">
                                            <label>Pos. Open choice</label>
                                            <asp:TextBox ID="PosOpenChoice" runat="server"></asp:TextBox>
                                        </div>                                        
                                        <div class="large-4 columns">
                                            <label>Notes / Nota</label>
                                            <asp:TextBox ID="Notes" runat="server"></asp:TextBox>
                                        </div>                                  
                                    </div>

                                </fieldset>	
                                <div class="row">
                                            <div class="large-7 columns">
                                                <asp:Button ID="Button1" Text="Rechercher / Zoeken" class="small round button" runat="server" OnClick="Rechercher_Click" />
                                                <asp:Button ID="SupprimerCritères" Text="Crit --> 0" class="small round button" runat="server" OnClick="SupprimerCritères_Click" />                       
                                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="AjouterPremierOutil" Text="1st tool in this Fam." class="small round button" runat="server" visible="false" OnClick="AjouterPremierOutil_Click" />                       
                                           </div>
                                            <div class="large-3 columns">
                                                <asp:Label ID="Lblresults" runat="server" Visible="false" Text="" />
                                            </div>
                                 </div>                                
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

                                  <asp:Button ID="btnDownloadPDF" Text="Donwload PDF" runat="server" OnClick="Download_Pdf" Visible="false" class="small round button" />
                                  <asp:Button ID="btnDownloadCSV" Text="Donwload XLS" runat="server" OnClick="Download_Csv" Visible="false" class="small round button" />
                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                       
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                         
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                     
                                  <asp:DropDownList ID="RapportType" runat="server" Visible="false" Width="400">
                                      <asp:ListItem Text="Format technique / Technische formaat" Value="1" />  
                                      <asp:ListItem Text="Format commande / Bestelling formaat" Value="2" /> 
                                      <asp:ListItem Text="Format liste par famille / Lijst per familie formaat" Value="3" /> 
                                      <asp:ListItem Text="Format Liste par imputation / Lijst per imputatie formaat" Value="4" /> 
                                      <asp:ListItem Text="Format Total par famille / Totaal per familie formaat" Value="5" />
                                      <asp:ListItem Text="Format Vérification / Ijking formaat" Value="6" />
                                      <asp:ListItem Text="Format Entretiens / Onderhoud formaat" Value="7" />
                                      <asp:ListItem Text="Format Contrôles / Controle formaat" Value="8" />
                                  </asp:DropDownList>
                                  <asp:DropDownList ID="langue" runat="server" Visible="false" Width="50">
                                      <asp:ListItem Text="FR" Value="FR" />  
                                      <asp:ListItem Text="NL" Value="NL" />
                                  </asp:DropDownList>  
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
  </div> 		
</asp:Content>

