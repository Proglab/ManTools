using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
using ListIte = System.Web.UI.WebControls.ListItem;
using System.Text;

namespace ManTools2020
{
    public partial class Imputation : System.Web.UI.Page
    {
        Connexion con = Connexion.Instance;
        protected DataTable dtImputation = null;
        protected DataTable dtServices = null;
        protected DataTable dtPos = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int i;

            if (!IsPostBack)
            {
                if (Session["Utilisateur"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                else
                {
                    //Préparation de la table pour l'alimentation du DDL Services
                    try
                    {
                        con.setQuery("SELECT codeService FROM dbo.tblsecServices ORDER BY codeService");
                        dtServices = con.getDataTable();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    //Alimentation des ddl
                    service.Items.Insert(0, new ListIte(string.Empty, string.Empty));
                    i = 1;
                    foreach (DataRow rowService in dtServices.Rows)
                    {
                        service.Items.Insert(i, new ListIte(rowService["codeService"].ToString(), rowService["codeService"].ToString()));
                        i++;
                    }
                    //Gestion des boutons de commande
                    if (((string)Session["Utilisateur"]) == "cons" || ((string)Session["Utilisateur"]) == "ggi")
                    {
                        Ajout.Visible = false;
                        EnregistrerAjout.Visible = false;
                        Modifier.Visible = false;
                        EnregistrerModif.Visible = false;
                        Supprimer.Visible = false;
                        ConfirmerSuppression.Visible = false;
                        Annuler.Visible = false;
                    }
                    {
                        Ajout.Visible = true;
                        EnregistrerAjout.Visible = false;
                        Modifier.Visible = true;
                        EnregistrerModif.Visible = false;
                        Supprimer.Visible = true;
                        ConfirmerSuppression.Visible = false;
                        Annuler.Visible = false;
                    }

                    DataTable dt = null;
                    try
                    {
                        //Remplit les données liées à l'outil
                        Connexion con = Connexion.Instance;
                        con.setQuery("SELECT * FROM tblpriImputations WHERE tblpriImputations.codeImputation = @CodeImputation");
                        //string x = Request.QueryString["id"];
                        Session["CodeImputation"] = Request.QueryString["id"];
                        con.setParam("@CodeImputation", Request.QueryString["id"]);
                        dt = con.getDataTable();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    DataRow row = dt.Rows[0];

                    //Alimentation de la fiche
                    CodeImputation.Text = row["codeImputation"].ToString();
                    NomChantier.Text = row["NomChantier"].ToString();
                    service.SelectedValue = row["refService"].ToString();
                    Adresse.Text = row["AdresseChantier"].ToString();
                    codepostal.Text = row["CodePostal"].ToString();
                    localite.Text = row["Localite"].ToString();
                    telchantier.Text = row["TelChantier"].ToString();
                    faxchantier.Text = row["FaxChantier"].ToString();
                    ChefChantier.Text = row["ChefChantier"].ToString();
                    TelChefChantier.Text = row["TelChefChantier"].ToString();

                }
            }
        }

        protected void Ajout_Click(object sender, EventArgs e)
        {
            ////Gestion des contrôles
            CodeImputation.Enabled = true;
            NomChantier.Enabled = true;
            service.Enabled = true;
            //ChargeAffaire.Enabled = true;
            Adresse.Enabled = true;
            codepostal.Enabled = true;
            localite.Enabled = true;
            telchantier.Enabled = true;
            faxchantier.Enabled = true;
            ChefChantier.Enabled = true;
            TelChefChantier.Enabled = true;

            //Gestion des boutons
            Ajout.Visible = false;
            EnregistrerAjout.Visible = true;
            Modifier.Visible = false;
            Supprimer.Visible = false;
            EnregistrerModif.Visible = false;
            Annuler.Visible = true;

            //Gestion du message
            lblMessage.Visible = true;
            lblMessage.Visible = true;
            if (Session["Langue"] == "FR")
            {
                lblMessage.Text = "Vous êtes occupé à ajouter une imputation !";
            }
            else
            {
                lblMessage.Text = "U bent bezig een nieuwe imputatie toe te voegen!";
            }
        }

        protected void EnregistrerAjout_Click(object sender, EventArgs e)
        {
            try
            {
                //Tests préalables sur le numéro d'outil

                //Taille du numéro d'outil (6 caractères numériques)
                if (CodeImputation.Text.Length != 6)
                {
                    lblMessage.Visible = true;
                    if (Session["Langue"] == "FR")
                    {
                        lblMessage.Text = "Impossible d'enregistrer l'ajout car le numéro d'imputation doit comporter 6 caractères !";
                    }
                    else
                    {
                        lblMessage.Text = "Onmogelijk actie ! De imputatie nummer moet 5 karakters zijn !";
                    }
                    return;
                }

                //Test caractères numériques
                int testImputationNumérique = 0;

                bool resulttestImputation = int.TryParse(CodeImputation.Text, out testImputationNumérique);

                if (resulttestImputation == false)
                {
                    lblMessage.Visible = true;
                    if (Session["Langue"] == "FR")
                    {
                        lblMessage.Text = "Impossible d'enregistrer l'ajout car les caractères du numéro d'imputation doivent être des chiffres !";
                    }
                    else
                    {
                        lblMessage.Text = "Onmogelijk actie ! alle karakters moeten cijfers zijn !";
                    }
                    return;
                }

                lblMessage.Visible = true;
                if (Session["Langue"] == "FR")
                {
                    lblMessage.Text = CodeImputation.Text + " est prêt à être enregistré ... ";
                }
                else
                {
                    lblMessage.Text = CodeImputation.Text + " is klaar voor recording ... ";
                }

                //Outil existe --> message d'erreur
                con.setQuery("SELECT dbo.tblpriImputations.codeImputation " +
                    "FROM  dbo.tblpriImputations " +
                    "WHERE (dbo.tblpriImputations.codeImputation = @codeImputation)");
                con.setParam("@codeImputation", CodeImputation.Text);
                dtImputation = con.getDataTable();
                if (dtImputation.Rows.Count != 0)
                {
                    //Gestion des contrôles
                    CodeImputation.Enabled = false;
                    NomChantier.Enabled = false;
                    service.Enabled = false;
                    //ChargeAffaire.Enabled = false;
                    Adresse.Enabled = false;
                    codepostal.Enabled = false;
                    localite.Enabled = false;
                    telchantier.Enabled = false;
                    faxchantier.Enabled = false;
                    ChefChantier.Enabled = false;
                    TelChefChantier.Enabled = false;

                    //Gestion des boutons
                    Ajout.Visible = true;
                    EnregistrerAjout.Visible = false;
                    Modifier.Visible = true;
                    Supprimer.Visible = true;
                    EnregistrerModif.Visible = false;
                    Annuler.Visible = false;

                    //Gestion du message
                    lblMessage.Visible = true;
                    if (Session["Langue"] == "FR")
                    {
                        lblMessage.Text = "Impossible d'enregistrer l'imputation car elle existe déjà";
                    }
                    else
                    {
                        lblMessage.Text = "Onmogelijk actie ! Deze imputatie bestaat al";
                    }
                    return;
                }
                else
                {
                    //Sinon on crée l'imputation

                    //Ajouter l'imputation
                    con = Connexion.Instance;
                    con.setQuery("INSERT INTO tblpriImputations (codeImputation, NomChantier, refService, AdresseChantier, CodePostal, Localite, TelChantier, FaxChantier, ChefChantier, TelChefChantier) " +
                                 "SELECT codeImputation = @codeImputation, NomChantier = @NomChantier, refService = @refService, AdresseChantier = @AdresseChantier, " +
                                    "CodePostal = @CodePostal, Localite = @Localite, TelChantier = @TelChantier, FaxChantier = @FaxChantier, ChefChantier = @ChefChantier, TelChefChantier = @TelChefChantier");
                    con.setParam("@codeImputation", CodeImputation.Text);
                    con.setParam("@NomChantier", NomChantier.Text);
                    con.setParam("@refService", service.SelectedValue.ToString());
                    con.setParam("@AdresseChantier", Adresse.Text);
                    con.setParam("@CodePostal", codepostal.Text);
                    con.setParam("@Localite", localite.Text);
                    con.setParam("@TelChantier", telchantier.Text);
                    con.setParam("@FaxChantier", faxchantier.Text);
                    con.setParam("@ChefChantier", ChefChantier.Text);
                    con.setParam("@TelChefChantier", TelChefChantier.Text);

                    con.getExecuteNonQuery();

                    //Gestion des contrôles
                    CodeImputation.Enabled = false;
                    NomChantier.Enabled = false;
                    service.Enabled = false;
                    //ChargeAffaire.Enabled = false;
                    Adresse.Enabled = false;
                    codepostal.Enabled = false;
                    localite.Enabled = false;
                    telchantier.Enabled = false;
                    faxchantier.Enabled = false;
                    ChefChantier.Enabled = false;
                    TelChefChantier.Enabled = false;

                    //Gestion des boutons
                    Ajout.Visible = true;
                    EnregistrerAjout.Visible = false;
                    Modifier.Visible = true;
                    Supprimer.Visible = true;
                    EnregistrerModif.Visible = false;
                    Annuler.Visible = false;

                    //Gestion du message
                    lblMessage.Visible = true;
                    if (Session["Langue"] == "FR")
                    {
                        lblMessage.Text = "Vous venez de créer cette imputation !";
                    }
                    else
                    {
                        lblMessage.Text = "Deze imputatie is gemaakt !";
                    }
                }

            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }

        }

        protected void Modifier_Click(object sender, EventArgs e)
        {
            //Gestion des contrôles
            NomChantier.Enabled = true;
            service.Enabled = true;
            //ChargeAffaire.Enabled = true;
            Adresse.Enabled = true;
            codepostal.Enabled = true;
            localite.Enabled = true;
            telchantier.Enabled = true;
            faxchantier.Enabled = true;
            ChefChantier.Enabled = true;
            TelChefChantier.Enabled = true;

            //Gestion des boutons
            Ajout.Visible = false;
            EnregistrerAjout.Visible = false;
            Modifier.Visible = false;
            Supprimer.Visible = false;
            EnregistrerModif.Visible = true;
            Annuler.Visible = true;

            //Gestion du message
            lblMessage.Visible = true;
            if (Session["Langue"] == "FR")
            {
                lblMessage.Text = "Vous êtes occupé à modifier une imputation !";
            }
            else
            {
                lblMessage.Text = "U bent bezig een imputatie te wijzigen !";
            }
        }

        protected void EnregistrerModif_Click(object sender, EventArgs e)
        {

            try
            {
                //Gérer l'enregistrement des modifications
                Connexion con = Connexion.Instance;
                con.setQuery("UPDATE tblpriImputations SET NomChantier = @NomChantier, refService = @refService, AdresseChantier = @AdresseChantier, " +
                    "CodePostal = @CodePostal, Localite = @Localite, TelChantier = @TelChantier, FaxChantier = @FaxChantier, ChefChantier = @ChefChantier, TelChefChantier = @TelChefChantier WHERE codeImputation = @codeImputation");
                con.setParam("@NomChantier", NomChantier.Text);
                con.setParam("@refService", service.SelectedValue.ToString());
                con.setParam("@AdresseChantier", Adresse.Text);
                con.setParam("@CodePostal", codepostal.Text);
                con.setParam("@Localite", localite.Text);
                con.setParam("@TelChantier", telchantier.Text);
                con.setParam("@FaxChantier", faxchantier.Text);
                con.setParam("@ChefChantier", ChefChantier.Text);
                con.setParam("@TelChefChantier", TelChefChantier.Text);
                con.setParam("@codeImputation", CodeImputation.Text);
                con.getExecuteNonQuery();

                //Gestion des contrôles
                NomChantier.Enabled = false;
                service.Enabled = false;
                //ChargeAffaire.Enabled = false;
                Adresse.Enabled = false;
                codepostal.Enabled = false;
                localite.Enabled = false;
                telchantier.Enabled = false;
                faxchantier.Enabled = false;
                ChefChantier.Enabled = false;
                TelChefChantier.Enabled = false;

                //Gestion des boutons
                Ajout.Visible = true;
                EnregistrerAjout.Visible = false;
                Modifier.Visible = true;
                Supprimer.Visible = true;
                EnregistrerModif.Visible = false;
                Annuler.Visible = false;

                //Gestion du message
                lblMessage.Visible = true;
                if (Session["Langue"] == "FR")
                {
                    lblMessage.Text = "Vous venez de modifier cette imputation !";
                }
                else
                {
                    lblMessage.Text = "U bent bezig deze imputatie te wijzigen !";
                }

                //Response.Redirect("famille.aspx");
                Response.Redirect("Imputation.aspx?id=" + Session["CodeImputation"]);
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        protected void Annuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Imputation.aspx?id=" + Session["CodeImputation"]);
        }

        protected void Supprimer_Click(object sender, EventArgs e)
        {
            // Response.Redirect("FamilleFiche.aspx?id=" + sf);
            //Gestion du message
            lblMessage.Visible = true;
            if (Session["Langue"] == "FR")
            {
                lblMessage.Text = "Etes vous certain de vouloir supprimer cette imputation ? Ce ne sera possible que si aucun outil n'a jamais été positionné sur cette imputation.";
            }
            else
            {
                lblMessage.Text = "Bent u zeker dat u deze imputatie willen verwijderen ? Dat zal alleen mogelijk zijn als er geen gereedschap is nooit geplaatst op de imputatie.";
            }

            //Gestion des boutons
            Ajout.Visible = false;
            EnregistrerAjout.Visible = false;
            Modifier.Visible = false;
            Supprimer.Visible = false;
            EnregistrerModif.Visible = false;
            Annuler.Visible = true;
            ConfirmerSuppression.Visible = true;
        }

        protected void ConfirmerSuppression_Click(object sender, EventArgs e)
        {
            //Test si on supprimer cette imputation 
            con.setQuery("SELECT dbo.tblpriPositionnements.idPositionnement " +
                "FROM  dbo.tblpriPositionnements " +
                "WHERE (dbo.tblpriPositionnements.Position = @Position) ");
            con.setParam("@Position", CodeImputation.Text);
            dtPos = con.getDataTable();
            if (dtPos.Rows.Count != 0)
            {
                //Gestion du message
                lblMessage.Visible = true;
                if (Session["Langue"] == "FR")
                {
                    lblMessage.Text = "Imposssible de supprimer cette imputation !";
                }
                else
                {
                    lblMessage.Text = "Onmogelijk deze imputatie te verwijderen !";
                }
                return;
            }

            //Supprimer l'imputation
            con.setQuery("DELETE dbo.tblpriImputations WHERE dbo.tblpriImputations.codeImputation = @codeImputation");
            con.setParam("@codeImputation", CodeImputation.Text);
            con.getExecuteNonQuery();

            //Gestion des boutons
            Ajout.Visible = true;
            EnregistrerAjout.Visible = false;
            Modifier.Visible = true;
            Supprimer.Visible = true;
            EnregistrerModif.Visible = false;
            Annuler.Visible = false;

            //Gestion du message
            lblMessage.Visible = true;
            if (Session["Langue"] == "FR")
            {
                lblMessage.Text = "Vous venez de supprimer cette imputation !";
            }
            else
            {
                lblMessage.Text = "Deze imputatie is verwijderd !";
            }
        }
    }
}