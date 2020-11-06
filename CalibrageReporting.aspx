<%@ Page Title="CalibrageReporting" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CalibrageReporting.aspx.cs" Inherits="ManTools2020.CalibrageReporting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <div class="row">
        <div class="large-12 medium-8 columns">
            <div class="panel">
                <div class="row">
                    <div class="large-12 columns">
                        <h3 >ENGIE Axima S.A. / N.V. - Webtools </h3>
                            <div class="large-12 columns">
                                <!--Start formulaire de recherche -->
                                <asp:Panel ID="Form" Visible="true" runat="server">
                                    <fieldset>
                                        <legend>Les outils à calibrer / Gereedsch. te ijken</legend>
                                        <div class="row">
                                            <div class="large-4 columns">
                                                <!--<label>Calibration period</label>--> 
                                                <asp:DropDownList ID="periode" runat="server" Visible="true" width="200">

                                                </asp:DropDownList>          
                                            </div>

                                                <div class="large-8 columns">                                           
                                               <div class="large-4 columns">
                                                    <!-- 
                                                    <label>Trim. for calibration</label>
                                                    <asp:DropDownList ID="trimestre" runat="server" width="200" >
                                                        <asp:ListItem Text="1er" Value="1" />  
                                                        <asp:ListItem Text="2ième" Value="2" />
                                                        <asp:ListItem Text="3ième" Value="3" />
                                                        <asp:ListItem Text="4ième" Value="4" />
                                                    </asp:DropDownList>
                                                    -->  
                                                </div>
                                                <div class="large-4 columns">
                                                    <!-- 
                                                    <label>Year for calibration</label>
                                                    <asp:TextBox ID="annee" runat="server" width="200">
                                                    </asp:TextBox>
                                                   -->
                                                </div>
                                            </div>
                                        </div>                                                          
                                    </fieldset>
                                    <div class="row">
                                            <div class="large-2 columns">
                                                <asp:Button ID="btnDownloadPDF" Text="Donwload PDF" runat="server" class="small round button" OnClick="btnDownloadPDF_Click"/>
                                            </div>
                                            <div class="large-2 columns">
                                                <asp:Button ID="btnDownloadCSV" Text="Donwload XLS" runat="server" class="small round button" OnClick="btnDownloadCSV_Click"/>
                                            </div>
                                            <div class="large-6 columns">
                                                  <asp:DropDownList ID="langue" runat="server"  Width="200">
                                                      <asp:ListItem Text="FR" Value="FR" />  
                                                      <asp:ListItem Text="NL" Value="NL" />
                                                  </asp:DropDownList>  
                                            </div>
                                            
                                            <div class="large-2 columns">&nbsp;</div>
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
