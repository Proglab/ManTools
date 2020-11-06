<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ManTools2020._Default" %>
<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <div class="row">
        <div class="large-12 medium-8 columns">
            <div class="panel">
                
                <div class="row">
                    <div class="large-12 medium-6 columns">
                        <h3 data-magellan-destination="topbar">ENGIE Axima - Webtools - <i>Home Page</i></h3>
                        <br />
                        <br />
                        <div class="large-6 medium-6 columns">
                            <img src="Images/logo_cofely.png" alt="" />
                        </div>
                        <div class="large-6 medium-6 columns">
                            <p>
                                Webtools est une application WEB relative à la gestion de l'outillage de Cofely Axima.<br/>
                                Pour tout renseignement, veuillez vous adresser à Jean-Philippe Dewez, coordonnées ci-dessous.<br /><br />
                                Webtools is een WEB applicatie voor het beheer van de gereedschappen van Cofely Axima.<br/>
                                Voor inlichtingen, kan U contact nemen met Jean-Philippe Dewez, contactinformatie hieronder.<br /><br />
                                GSM : 0475 344 058<br/>
                                Mail : jde@iqs.be
									<br/>
                                <br/>
                            </p>
                        </div>
                        <panel ID="loginPanel" runat="server" visible="true">
                        <div class="large-6">
                                <div class="large-6 columns">
                                    <label>Login</label>
                                    <asp:TextBox ID="Login" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="Login"
                                        ErrorMessage="Veuillez entrer un Login / Vul een Login"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="row">
                                    <div class="large-6 columns">
                                        <label>Password</label>
                                        <asp:TextBox ID="Password" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="Password"
                                            ErrorMessage="Vul uw wachtwoord in"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            <br />
                                <div class="row">

                                        <asp:Button ID="Envoyer" Text="Login" runat="server" OnClick="Envoyer_Click" />
                                        <asp:Label ID="LblMessage2" runat="server" Visible="true" ForeColor="Red" Text="En attente du login / In afwachting van uw login" />

                                </div>
                        </div>
                        </panel>

                        <panel ID="logedPanel" runat="server" visible="false">

                        <div class="large-6">
                                
                                <div class="row">
                                        <asp:Label ID="LblMessage" runat="server" Visible="true" ForeColor="Red" Text="Vous êtes bien connecté" />
                                </div>
                        </div>
                    
                        </panel>
                    
                    
                    
                    
                    
                    </div>
                </div>
        
            </div>
        </div>
    </div>
    <script src="Scripts/vendor/jquery.js"></script>
    <script src="Scripts/foundation.min.js"></script>
    <div>
    </div>
</asp:Content>