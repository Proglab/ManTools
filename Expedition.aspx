<%@ Page Title="Expedition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Expedition.aspx.cs" Inherits="ManTools2020.Expedition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <div class="row">
        <div class="large-12 medium-8 columns">
            <div class="panel">
                <div class="row">
                    <div class="large-12 medium-6 columns">
                        <h4>ENGIE Axima - Webtools </h4>

                        <div class="row">

                            <div class="large-12 columns">

                                <div class="row">
                                <fieldset><legend> Expédition des outils</legend>
                                    <div class="large-2 columns">
                                        <label>Imputation d'origine</label>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="large-4 columns">
                                        <label>Chantier</label>
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="large-3 columns">
                                        <label>Date de transfère</label>
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="large-3 columns">
                                        <label>N° de transfère</label>
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </div>
                                    </fieldset>

                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="41px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
             
</asp:Content>
