<%@ Page Title="Facturation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Facturation.aspx.cs" Inherits="ManTools2020.Facturation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
 
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
                                        <legend>Facturation mensuelle / Maandelijkse Facturatie</legend>
                                        <div class="row">
                                            <div class="large-6 columns">
                                                <label>Date début facturation / Begin datum voor Facturatie</label>
                                                <asp:Calendar ID="BeginOrder" runat="server"></asp:Calendar>
                                            </div>
                                            <div class="large-1 columns"></div>
                                            <div class="large-5 columns">
                                                <label>Date fin facturation / Einddatum facturatie</label>
                                                <asp:Calendar ID="EndOrder" runat="server"></asp:Calendar>
                                            </div>
                                        </div>  
                                        <div class="row">
                                            <div class="large-5 columns">
                                                <label>Service / Dienst</label>
                                                <asp:DropDownList runat="server" ID="ServiceDropDown"  AutoPostBack="true"  OnSelectedIndexChanged="ServiceDropDown_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">                      
                                            <div class="large-5 columns">
                                                <label>Imputation / Imputatie</label>
                                                <asp:DropDownList runat="server" ID="ImputationDropDown"></asp:DropDownList>
                                            </div>    
                                            <div class="large-2 columns">
                                                <label runat="server" ID="lbSortImp">Sort</label>
                                                <asp:DropDownList runat="server" ID="ddlImputTri" AutoPostBack="true" OnSelectedIndexChanged="ddlImputTri_SelectedIndexChanged">
                                                </asp:DropDownList>                                                   
                                            </div>                  
                                            <div class="large-3 columns">
                                                <label runat="server" ID="lbLangue">Langue</label>
                                                <asp:DropDownList ID="langue" runat="server" Visible="true" Width="100">
                                                  <asp:ListItem Text="FR" Value="FR" />  
                                                  <asp:ListItem Text="NL" Value="NL" />
                                                </asp:DropDownList>          
                                            </div>
                                        </div>
                                        <div class="row">                      
                                            <div class="large-3 columns">
                                                <asp:Button ID="btnFactDet" Text="Facture détaillée / Gedetailleerde factuur" class="small round button" runat="server" OnClick="Facturer_Click"/>
                                            </div>
                                            <div class="large-3 columns">
                                                <asp:Button ID="btnFactSynth" Text="Synthèse / Recap" class="small round button" runat="server" OnClick="Synth_Click"/>
                                            </div>                                        
                                        </div>                                    
                                </fieldset>
                                        <div class="row">
                                            <div class="large-4 columns"></div>                                            
                                        </div>
                                        <div class="row">
                                            <div class="large-12 columns">
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
                                            </div>
                                         </div>
                                </asp:Panel>
                                <!--end formulaire de recherche -->
                                
                            </div>
                    </div>
                </div>
            </div>
        </div>   

    </div>
</asp:Content>
