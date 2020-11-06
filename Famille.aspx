<%@ Page Title="Famille" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Famille.aspx.cs" Inherits="ManTools2020.Famille" %>
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
                                        <legend>Moteur de recherche sur les Familles / Zoek op families</legend>
                                        <div class="row">
                                            <div class="large-2 columns">
                                                <label>Fam. Num.</label>
                                                <asp:DropDownList runat="server" ID="DropDownFamille" AutoPostBack="true" OnSelectedIndexChanged="DropDownFamille_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="large-5 columns">
                                                <label>Fam. Descr. / Omschr. FR</label>
                                                <asp:DropDownList runat="server" ID="DropDownFamilleFR" AutoPostBack="true" OnSelectedIndexChanged="DropDownFamilleFR_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="large-5 columns">
                                                <label>Fam. Descr. / Omschr. NL</label>
                                                <asp:DropDownList runat="server" ID="DropDownFamilleNL" AutoPostBack="true" OnSelectedIndexChanged="DropDownFamilleNL_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="large-2 columns">
                                                <label>Tarif / Tarief</label>
                                                <asp:TextBox ID="IdTaux" runat="server" />
                                            </div>
                                            <div class="large-3 columns">
                                                <label>Per. Ctrl</label>
                                                <asp:TextBox ID="PrCtrl" runat="server" />
                                            </div>
                                            <div class="large-4 columns">
                                                <label>Quantifiable / kwantificeerbaar</label>
                                                <asp:CheckBox runat="server" ID="Quantifiable" />
                                            </div>
                                            <div class="large-2 columns"></div>
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
