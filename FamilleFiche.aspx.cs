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
//using iTextSharp;
//using iTextSharp.text.api;
using ListIte = System.Web.UI.WebControls.ListItem;
using System.Text;

namespace ManTools2020
{
    public partial class FamilleFiche : System.Web.UI.Page
    {
        Connexion con = Connexion.Instance;
        protected DataTable dtFamSousFam = null;
        protected DataTable dtFam = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Utilisateur"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                else
                {
                    if (((string)Session["Utilisateur"]) == "cons" || ((string)Session["Utilisateur"]) == "ggi")
                    {
                        /*NumFam.Enabled = false;
                        DescrFamFr.Enabled = false;
                        DescrFamNl.Enabled = false;
                        SsFam.Enabled = false;
                        SsFamFR.Enabled = false;
                        DescrSsFamNl.Enabled = false;
                        Taux.Enabled = false;
                        //Ctrl.Enabled = false;
                        CheckQuantifiable.Enabled = false;*/
                        Ajout.Visible = false;
                        EnregistrerAjout.Visible = false;
                        Modifier.Visible = false;
                        EnregistrerModif.Visible = false;
                        Supprimer.Visible = false;
                        ConfirmerSuppression.Visible = false;
                        Annuler.Visible = false;
                    }
                    else
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
                        //Chercher la famille - sous-famille concernée
                        Connexion con = Connexion.Instance;
                        con.setQuery("SELECT codefamille AS fam, idSousFamille AS IdSousFam, NumSousFamille AS SubFam, DescriptionFamilleFR AS Descr_F_FR, DescriptionSousFamilleFr AS Descr_Ss_F_FR , " +
            "DescriptionFamilleNL AS Descr_F_NL, DescriptionSousFamilleNL AS Descr_Ss_F_NL, TauxSousFamille AS Taux , PeriodeControle AS Ctr " +
            "FROM tblpriFamilles LEFT JOIN tblpriSousFamilles ON (tblpriFamilles.codeFamille = tblpriSousFamilles.refFamille) WHERE  tblpriSousFamilles.idSousFamille = @Sous_famille");
                        //string x = Request.QueryString["id"];
                        Session["sf"] = Request.QueryString["id"];
                        con.setParam("@Sous_famille", Request.QueryString["id"]);
                        dt = con.getDataTable();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    DataRow row = dt.Rows[0];

                    NumFam.Text = row["fam"].ToString();
                    DescrFamFr.Text = row["Descr_F_FR"].ToString();
                    DescrFamNl.Text = row["Descr_F_NL"].ToString();
                    IdSousFam.Text = row["IdSousFam"].ToString();
                    SsFam.Text = row["SubFam"].ToString();
                    SsFamFR.Text = row["Descr_Ss_F_FR"].ToString();
                    DescrSsFamNl.Text = row["Descr_Ss_F_NL"].ToString();
                    Taux.Text = row["Taux"].ToString();
                    //Ctrl.Text = row["Ctr"].ToString();
                    //CheckQuantifiable.Checked = (bool)row["Quantifiable"];

                    //Gestion des boutons de commande
                    EnregistrerModif.Visible = false;
                    EnregistrerAjout.Visible = false;
                    Annuler.Visible = false;
                    ConfirmerSuppression.Visible = false;
                }
            }
        }

        protected void Ajout_Click(object sender, EventArgs e)
        {
            NumFam.Enabled = true;
            DescrFamFr.Enabled = true;
            DescrFamNl.Enabled = true;
            SsFam.Enabled = true;
            SsFamFR.Enabled = true;
            DescrSsFamNl.Enabled = true;
            Taux.Enabled = true;
            //Ctrl.Enabled = true;
            CheckQuantifiable.Enabled = true;

            //Gestion des boutons
            Ajout.Visible = false;
            EnregistrerAjout.Visible = true;
            Modifier.Visible = false;
            Supprimer.Visible = false;
            EnregistrerModif.Visible = false;
            Annuler.Visible = true;

            //Gestion du message
            lblMessage.Visible = true;
            if (Session["Langue"] == "FR")
            {
                lblMessage.Text = "Vous êtes occupé à ajouter une famille !";
            }
            else
            {
                lblMessage.Text = "U bent bezig een familie te toevoegen!";
            }
        }

        protected void EnregistrerAjout_Click(object sender, EventArgs e)
        {
            try
            {
                //Tests préalables

                //Famille et Sous-famille non vide ou mauvais nombre de caractères
                if ((NumFam.Text.Length != 3) || (SsFam.Text.Length != 2))
                {
                    lblMessage.Visible = true;
                    if (Session["Langue"] == "FR")
                    {
                        lblMessage.Text = "Impossible d'enregistrer l'ajout car la famille doit comporter exactement 3 caractère et la sous-famille 2 caractères !";
                    }
                    else
                    {
                        lblMessage.Text = "Onmogelijk actie ! Een familie moet 3 digitale karakters hebben en een sub-familie moet 2 digitale karakters hebben";
                    }
                    return;
                }

                //Famille ou Sous-famille non numérique

                //Famille sous-famille existe ? Message d'erreur
                con.setQuery("SELECT dbo.tblpriFamilles.codeFamille, dbo.tblpriSousFamilles.NumSousFamille FROM dbo.tblpriFamilles INNER JOIN dbo.tblpriSousFamilles ON dbo.tblpriFamilles.codeFamille = dbo.tblpriSousFamilles.refFamille WHERE (dbo.tblpriSousFamilles.refFamille = @NumFam AND dbo.tblpriSousFamilles.NumSousFamille = @NumSousFam)");
                con.setParam("@NumFam", NumFam.Text);
                con.setParam("@NumSousFam", SsFam.Text);
                dtFamSousFam = con.getDataTable();
                if (dtFamSousFam.Rows.Count != 0)
                {
                    NumFam.Enabled = false;
                    DescrFamFr.Enabled = false;
                    DescrFamNl.Enabled = false;
                    SsFam.Enabled = false;
                    SsFamFR.Enabled = false;
                    DescrSsFamNl.Enabled = false;
                    Taux.Enabled = false;
                    //Ctrl.Enabled = false;
                    CheckQuantifiable.Enabled = false;

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
                        lblMessage.Text = "Impossible d'enregistrer l'ajout car cette famille existe déjà !";
                    }
                    else
                    {
                        lblMessage.Text = "Onmogelijk actie ! Deze familie is al in de database !";
                    }
                    return;
                }

                //Famille existe --> créer sous-famille
                con.setQuery("SELECT dbo.tblpriFamilles.codeFamille FROM dbo.tblpriFamilles WHERE (dbo.tblpriFamilles.codeFamille = @NumFam)");
                con.setParam("@NumFam", NumFam.Text);
                dtFam = con.getDataTable();
                if (dtFam.Rows.Count != 0)
                {
                    //Retrouver le nouvel id de la sous-famille
                    con.setQuery("SELECT MAX(idSousFamille) AS MaxId FROM dbo.tblpriSousFamilles");
                    DataTable dt = null;
                    dt = con.getDataTable();
                    DataRow row = dt.Rows[0];
                    int intNewIdSousFam;
                    intNewIdSousFam = (int)row["MaxId"] + 1;

                    //con.setQuery("INSERT INTO dbo.tblpriSousFamilles (refFamille, NumSousFamille, DescriptionSousFamilleFr, DescriptionSousFamilleNl, TauxSousFamille, Quantifiable) SELECT  @Fam, @SousFam, @DescrSousFamFr, @DescrSousFamNl, @Taux, @Quantifiable");
                    con.setQuery("INSERT INTO dbo.tblpriSousFamilles (idSousFamille, refFamille, NumSousFamille, DescriptionSousFamilleFr, DescriptionSousFamilleNl, TauxSousFamille) SELECT  @idSsFam, @Fam, @SousFam, @DescrSousFamFr, @DescrSousFamNl, @Taux");
                    con.setParam("@idSsFam", intNewIdSousFam.ToString());
                    con.setParam("@Fam", NumFam.Text);
                    con.setParam("@SousFam", SsFam.Text);
                    con.setParam("@DescrSousFamFr", SsFamFR.Text);
                    con.setParam("@DescrSousFamNl", DescrSsFamNl.Text);
                    con.setParam("@Taux", Taux.Text);
                    //con.setParam("@Quantifiable", CheckQuantifiable.Text);
                    con.getExecuteNonQuery();
                    IdSousFam.Text = intNewIdSousFam.ToString();

                    NumFam.Enabled = false;
                    DescrFamFr.Enabled = false;
                    DescrFamNl.Enabled = false;
                    SsFam.Enabled = false;
                    SsFamFR.Enabled = false;
                    DescrSsFamNl.Enabled = false;
                    Taux.Enabled = false;
                    //Ctrl.Enabled = false;
                    CheckQuantifiable.Enabled = false;

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
                        lblMessage.Text = "Cette nouvelle famille vient d'être créée !";
                    }
                    else
                    {
                        lblMessage.Text = "Deze nieuwe familie is net gemaakt !";
                    }
                    return;
                }

                //Famille / Sous-famille n'existe pas --> on crée la totale
                //Création de la famille
                //con.setQuery("INSERT INTO dbo.tblpriFamilles SELECT  @codeFam, @DescrFamFr, @DescrFamNl, @PerCtrl");
                con.setQuery("INSERT INTO dbo.tblpriFamilles SELECT  @codeFam, @DescrFamFr, @DescrFamNl");
                con.setParam("@codeFam", NumFam.Text);
                con.setParam("@DescrFamFr", DescrFamFr.Text);
                con.setParam("@DescrFamNl", DescrFamNl.Text);
                //con.setParam("@PerCtrl", Ctrl.Text);
                con.getExecuteNonQuery();

                //Création de la sous-famille
                //Retrouver le nouvel id de la sous-famille
                con.setQuery("SELECT MAX(idSousFamille) AS MaxId FROM dbo.tblpriSousFamilles");
                DataTable dt2 = null;
                dt2 = con.getDataTable();
                DataRow row2 = dt2.Rows[0];
                int intNewIdSousFam2;
                intNewIdSousFam2 = (int)row2["MaxId"] + 1;

                //con.setQuery("INSERT INTO dbo.tblpriSousFamilles (refFamille, NumSousFamille, DescriptionSousFamilleFr, DescriptionSousFamilleNl, TauxSousFamille, Quantifiable) SELECT  @Fam, @SousFam, @DescrSousFamFr, @DescrSousFamNl, @Taux, @Quantifiable");
                con.setQuery("INSERT INTO dbo.tblpriSousFamilles (idSousFamille, refFamille, NumSousFamille, DescriptionSousFamilleFr, DescriptionSousFamilleNl, TauxSousFamille) SELECT  @idSsFam, @Fam, @SousFam, @DescrSousFamFr, @DescrSousFamNl, @Taux");
                con.setParam("@idSsFam", intNewIdSousFam2.ToString());
                con.setParam("@Fam", NumFam.Text);
                con.setParam("@SousFam", SsFam.Text);
                con.setParam("@DescrSousFamFr", SsFamFR.Text);
                con.setParam("@DescrSousFamNl", DescrSsFamNl.Text);
                con.setParam("@Taux", Taux.Text);
                //con.setParam("@Quantifiable", CheckQuantifiable.Text);
                con.getExecuteNonQuery();
                IdSousFam.Text = intNewIdSousFam2.ToString();

                NumFam.Enabled = false;
                DescrFamFr.Enabled = false;
                DescrFamNl.Enabled = false;
                SsFam.Enabled = false;
                SsFamFR.Enabled = false;
                DescrSsFamNl.Enabled = false;
                Taux.Enabled = false;
                //Ctrl.Enabled = false;
                CheckQuantifiable.Enabled = false;

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
                    lblMessage.Text = "Cette nouvelle famille vient d'être créée !";
                }
                else
                {
                    lblMessage.Text = "Deze nieuwe familie is net gemaakt !";
                }
                return;

            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        protected void Modifier_Click(object sender, EventArgs e)
        {
            //Gestion des contrôles
            NumFam.Enabled = false;
            DescrFamFr.Enabled = true;
            DescrFamNl.Enabled = true;
            SsFam.Enabled = false;
            SsFamFR.Enabled = true;
            DescrSsFamNl.Enabled = true;
            Taux.Enabled = true;
            //Ctrl.Enabled = true;
            CheckQuantifiable.Enabled = false;

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
                lblMessage.Text = "Vous êtes occupé à modifier une famille !";
            }
            else
            {
                lblMessage.Text = "U bent bezig een familie te wijzigen !";
            }
        }

        protected void EnregistrerModif_Click(object sender, EventArgs e)
        {
            try
            {
                //Gérer l'enregistrement des modifications
                //Modification au niveau familles
                Connexion con = Connexion.Instance;
                //con.setQuery("UPDATE tblpriFamilles SET DescriptionFamilleFr = @DESCRIPTION_FR, DescriptionFamilleNl = @DESCRIPTION_NL, PeriodeControle = @PER_CTRL WHERE codeFamille = @famille");
                con.setQuery("UPDATE tblpriFamilles SET DescriptionFamilleFr = @DESCRIPTION_FR, DescriptionFamilleNl = @DESCRIPTION_NL WHERE codeFamille = @famille");
                //con.setQuery("UPDATE Famille SET DESCRIPTION_NL = @DESCRIPTION_NL, PER_CTRL= @PER_CTRL, DESCRIPTION_FR = @DESCRIPTION_FR FROM Famille WHERE famille = @famille");
                //con.setParam("@DESCRIPTION_NL", DescrFamNl.Text);
                con.setParam("@DESCRIPTION_FR", DescrFamFr.Text);
                con.setParam("@DESCRIPTION_NL", DescrFamNl.Text);
                //con.setParam("@PER_CTRL", Ctrl.Text);
                con.setParam("@famille", NumFam.Text);
                con.getExecuteNonQuery();

                //Modification au niveau sous-familles
                //con.setQuery("UPDATE tblpriSousFamilles SET DescriptionSousFamilleFr = @SOUS_FAMILLE_DESCRIPTION_FR, DescriptionSousFamilleNl = @SOUS_FAMILLE_DESCRIPTION_NL, TauxSousFamille = @TAUX_SOUS_FAMILLE, DegresVerifSousFamille = @DEGRE_VERIF_SOUS_FAMILLE, IntervalleVerifSousFamille = @INTERV_VERIF_SOUS_FAMILLE WHERE idSousFamille = @ID_SOUS_FAM");
                con.setQuery("UPDATE tblpriSousFamilles SET DescriptionSousFamilleFr = @SOUS_FAMILLE_DESCRIPTION_FR, DescriptionSousFamilleNl = @SOUS_FAMILLE_DESCRIPTION_NL, TauxSousFamille = @TAUX_SOUS_FAMILLE WHERE idSousFamille = @ID_SOUS_FAM");
                //con.setQuery("UPDATE Famille SET DESCRIPTION_NL = @DESCRIPTION_NL, PER_CTRL= @PER_CTRL, DESCRIPTION_FR = @DESCRIPTION_FR FROM Famille WHERE famille = @famille");
                //con.setParam("@DESCRIPTION_NL", DescrFamNl.Text);
                con.setParam("@SOUS_FAMILLE_DESCRIPTION_FR", SsFamFR.Text);
                con.setParam("@SOUS_FAMILLE_DESCRIPTION_NL", DescrSsFamNl.Text);
                con.setParam("@TAUX_SOUS_FAMILLE", Taux.Text);
                //con.setParam("@DEGRE_VERIF_SOUS_FAMILLE", NumFam.Text);
                //con.setParam("@INTERV_VERIF_SOUS_FAMILLE", NumFam.Text);
                con.setParam("@ID_SOUS_FAM", IdSousFam.Text);
                con.getExecuteNonQuery();

                NumFam.Enabled = false;
                DescrFamFr.Enabled = false;
                DescrFamNl.Enabled = false;
                SsFam.Enabled = false;
                SsFamFR.Enabled = false;
                DescrSsFamNl.Enabled = false;
                Taux.Enabled = false;
                //Ctrl.Enabled = false;
                CheckQuantifiable.Enabled = false;

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
                    lblMessage.Text = "Vous venez de modifier cette famille !";
                }
                else
                {
                    lblMessage.Text = "U veranderde deze familie!";
                }

                /*
                 con.setQuery("UPDATE Sous_familles SET Desription_FR = @Desription_FR, Description_NL = @Description_NL, Taux = @Taux, QTBLE = 1 FROM Famille where sous_famille = @sous_famille");
                 con.setParam("@Desription_FR", SsFamFR.Text);
                 con.setParam("@Description_NL", DescrSsFamNl.Text);
                 con.setParam("@Taux", Taux.Text);
                 con.setParam("@sous_famille", Request.QueryString["id"]);
                 con.getExecuteNonQuery();
                     */

                //Response.Redirect("famille.aspx");
                Response.Redirect("FamilleFiche.aspx?id=" + Session["sf"]);
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        protected void Annuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("FamilleFiche.aspx?id=" + Session["sf"]);

        }

        protected void ViderFiche_Click(object sender, EventArgs e)
        {
            
        }

        protected void Supprimer_Click(object sender, EventArgs e)
        {
            // Response.Redirect("FamilleFiche.aspx?id=" + sf);
            //Gestion du message
            lblMessage.Visible = true;
            if (Session["Langue"] == "FR")
            {
                lblMessage.Text = "Etes vous certain de vouloir supprimer cette famille ? Ce ne sera possible que si aucun outil n'y est associé. Si la famille ne dispose que d'une seule sous-famille, les deux seront supprimées. Prière de confirmer votre demande.";
            }
            else
            {
                lblMessage.Text = "Bent u zeker dat u wilt deze familie verwijderen ? Dit zal alleen mogelijk zijn als er geen gereedschappen zijn verbonden. Als de familie slechts één subfamilyhebt, zullen beide worden verwijderd. Bevestig uw verzoek.";
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
            //Test du nombre d'outils dans la sous-famille
            con.setQuery("SELECT COUNT(idOutil) AS CountOutils FROM dbo.tblpriOutil INNER JOIN dbo.tblpriSousFamilles ON dbo.tblpriOutil.refSousFamille = dbo.tblpriSousFamilles.idSousFamille WHERE dbo.tblpriSousFamilles.idSousFamille = @IdSousFam");
            con.setParam("@IdSousFam", IdSousFam.Text);
            con.getExecuteNonQuery();
            DataTable dt = null;
            dt = con.getDataTable();
            DataRow row = dt.Rows[0];
            int intOutilsSousFam = (int)row["CountOutils"];

            if (intOutilsSousFam == 0)
            {
                //Suppression de la sous-famille
                con.setQuery("DELETE dbo.tblpriSousFamilles WHERE dbo.tblpriSousFamilles.idSousFamille = @IdSousFam");
                con.setParam("@IdSousFam", IdSousFam.Text);
                con.getExecuteNonQuery();

                //Suppression de la famille si elle n'a plus de famille associée
                //Nombre de sous famille de la famille associée
                con.setQuery("SELECT COUNT(idSousFamille) AS CountSsFam FROM dbo.tblpriSousFamilles WHERE dbo.tblpriSousFamilles.refFamille = @IdFam");
                con.setParam("@IdFam", NumFam.Text);
                con.getExecuteNonQuery();
                DataTable dt2 = null;
                dt2 = con.getDataTable();
                DataRow row2 = dt2.Rows[0];
                int intCountSousFam = (int)row2["CountSsFam"];

                if (intCountSousFam == 0)
                {
                    con.setQuery("DELETE dbo.tblpriFamilles WHERE dbo.tblpriFamilles.codeFamille = @CodeFam");
                    con.setParam("@CodeFam", NumFam.Text);
                    con.getExecuteNonQuery();
                }

                //Gestion du message
                lblMessage.Visible = true;
                if (Session["Langue"] == "FR")
                {
                    lblMessage.Text = "Cette famille vient d'être supprimée !";
                }
                else
                {
                    lblMessage.Text = "Deze familie is verwijderd!";
                }

                //Gestion des boutons
                Ajout.Visible = false;
                EnregistrerAjout.Visible = false;
                Modifier.Visible = false;
                Supprimer.Visible = false;
                EnregistrerModif.Visible = false;
                Annuler.Visible = false;
                ConfirmerSuppression.Visible = false;
            }
            else
            {
                //Gestion du message
                lblMessage.Visible = true;
                if (Session["Langue"] == "FR")
                {
                    lblMessage.Text = "Impossible de supprimer cette famille car il y a encore des outils associés !";
                }
                else
                {
                    lblMessage.Text = "Onmogelijk aktie ! Er zijn nog gereedschappen in deze familie.";
                }

                //Gestion des boutons
                Ajout.Visible = true;
                EnregistrerAjout.Visible = false;
                Modifier.Visible = true;
                Supprimer.Visible = true;
                EnregistrerModif.Visible = false;
                Annuler.Visible = false;
                ConfirmerSuppression.Visible = false;
            }
        }

    }
}