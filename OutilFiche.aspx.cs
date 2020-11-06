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
    public partial class OutilFiche : System.Web.UI.Page
    {
        Connexion con = Connexion.Instance;
        protected DataTable dtOutil = null;
        protected DataTable dtOutilPos = null;
        protected DataTable dtOutilCtrl = null;
        protected DataTable dtOutilEntr = null;
        protected DataTable dtOutilVerif = null;
        protected DataTable dtMarques = null;
        protected DataTable dtTypes = null;
        protected DataTable dtGammes = null;

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
                    //Préparation des datatables pour l'alimentation des dropdownlists
                    try
                    {
                        con.setQuery("SELECT DISTINCT MarqueOutil FROM  tblpriOutil ORDER BY MarqueOutil");
                        dtMarques = con.getDataTable();
                        con.setQuery("SELECT DISTINCT TypeOutil FROM  tblpriOutil ORDER BY TypeOutil");
                        dtTypes = con.getDataTable();
                        con.setQuery("SELECT DISTINCT GammeOutil FROM  tblpriOutil ORDER BY GammeOutil");
                        dtGammes = con.getDataTable();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    //Alimentation des ddl
                    DropDownMarque.Items.Insert(0, new ListIte(string.Empty, string.Empty));
                    DropDownType.Items.Insert(0, new ListIte(string.Empty, string.Empty));
                    DropDownGamme.Items.Insert(0, new ListIte(string.Empty, string.Empty));

                    i = 1;
                    foreach (DataRow rowMarque in dtMarques.Rows)
                    {
                        DropDownMarque.Items.Insert(i, new ListIte(rowMarque["MarqueOutil"].ToString(), rowMarque["MarqueOutil"].ToString()));
                        i++;
                    }
                    i = 1;
                    foreach (DataRow rowType in dtTypes.Rows)
                    {
                        DropDownType.Items.Insert(i, new ListIte(rowType["TypeOutil"].ToString(), rowType["TypeOutil"].ToString()));
                        i++;
                    }
                    i = 1;
                    foreach (DataRow rowGamme in dtGammes.Rows)
                    {
                        DropDownGamme.Items.Insert(i, new ListIte(rowGamme["GammeOutil"].ToString(), rowGamme["GammeOutil"].ToString()));
                        i++;
                    }

                    //Gestion des boutons de commande
                    if (((string)Session["Utilisateur"]) == "cons" || ((string)Session["Utilisateur"]) == "ggi")
                    {
                        /*NumFam.Enabled = false;
                        DescrFamFr.Enabled = false;
                        SsFam.Enabled = false;
                        SsFamFR.Enabled = false;
                        ddlPropr.Enabled = false;
                        Taux.Enabled = false;
                        SsFam.Enabled = false;*/
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

                    ddlPropr.Items.Insert(0, new ListIte(string.Empty));
                    ddlPropr.Items.Insert(1, new ListIte("ANV"));
                    ddlPropr.Items.Insert(2, new ListIte("BXL"));
                    ddlPropr.Items.Insert(3, new ListIte("DCB"));
                    ddlPropr.Items.Insert(4, new ListIte("MAN"));

                    DropDownIntVerif.Items.Insert(0, new ListIte(string.Empty));
                    DropDownIntVerif.Items.Insert(1, new ListIte("03"));
                    DropDownIntVerif.Items.Insert(2, new ListIte("06"));
                    DropDownIntVerif.Items.Insert(3, new ListIte("12"));
                    DropDownIntVerif.Items.Insert(4, new ListIte("24"));
                    DropDownIntVerif.Items.Insert(4, new ListIte("NA"));

                    DataTable dt = null;
                    try
                    {
                        //Remplit les données liées à l'outil
                        Connexion con = Connexion.Instance;
                        con.setQuery("SELECT codefamille AS fam, idSousFamille AS IdSousFam, NumSousFamille AS SubFam, DescriptionFamilleFR AS Descr_F_FR, DescriptionSousFamilleFr AS Descr_Ss_F_FR , " +
            "DescriptionFamilleNL AS Descr_F_NL, DescriptionSousFamilleNL AS Descr_Ss_F_NL, TauxSousFamille AS Taux , PeriodeControle AS Ctr, " +
            //"TauxOutil, NumOutil, ProprietaireOutil " +
            "tblpriOutil.* " +
            "FROM (tblpriFamilles LEFT JOIN tblpriSousFamilles ON (tblpriFamilles.codeFamille = tblpriSousFamilles.refFamille)) INNER JOIN tblpriOutil ON (tblpriOutil.refSousFamille) = tblpriSousFamilles.idSousFamille WHERE tblpriOutil.idOutil = @Outil");
                        //string x = Request.QueryString["id"];
                        Session["Outil"] = Request.QueryString["id"];
                        con.setParam("@Outil", Request.QueryString["id"]);
                        dt = con.getDataTable();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    DataRow row = dt.Rows[0];

                    //Alimentation de la fiche
                    NumFam.Text = row["fam"].ToString();
                    DescrFamFr.Text = row["Descr_F_FR"].ToString();
                    ddlPropr.SelectedValue = row["ProprietaireOutil"].ToString();
                    Taux.Text = row["TauxOutil"].ToString();
                    //Etat contrôle
                    SsFam.Text = row["SubFam"].ToString();
                    SsFamFR.Text = row["Descr_Ss_F_FR"].ToString();
                    NOutil.Text = row["NumOutil"].ToString();
                    //Qté Totale
                    //Etat vérif
                    IdOutil.Text = row["idOutil"].ToString();
                    refSousFamille.Text = row["refSousFamille"].ToString();

                    //1er onglet Caract. techniques
                    DropDownMarque.SelectedValue = row["MarqueOutil"].ToString();
                    DropDownType.SelectedValue = row["TypeOutil"].ToString();
                    NSerie.Text = row["NumSerie"].ToString();
                    DropDownGamme.SelectedValue = row["GammeOutil"].ToString();
                    //Proc technique

                    //2ème onglet Commande
                    Fournisseur.Text = row["FournisseurOutil"].ToString();
                    DateCommande.Text = row["DateCommande"].ToString();
                    NCommande.Text = row["NumCommande"].ToString();
                    Montant.Text = row["MontantCommande"].ToString();

                    //3ème onglet Divers
                    Annee.Text = row["Utilisation"].ToString();
                    Notes.Text = row["NotesOutil"].ToString();
                    DropDownIntVerif.SelectedValue = row["IntervalleVerifOutil"].ToString();
                    Accessoires.Text = row["AccessoireOutil"].ToString();

                    //Alimentation de l'onglet Positionnements
                    string sqlPos = "";
                    sqlPos = "SELECT DateTransfert AS DatePos, NumDocTransfert AS Document, PositionOrigine AS ImpFrom, QuantitePosition AS Qty, Position AS ImpTo, NomChantier AS Imput " +
                          "FROM ((dbo.tblpriPositionnements INNER JOIN dbo.tblpriOutil ON (refOutil = idOutil)) LEFT JOIN dbo.tblpriImputations ON Position = codeImputation) " +
                          "WHERE dbo.tblpriPositionnements.refOutil = @idOutil ORDER BY DateTransfert DESC, idPositionnement DESC";
                    try
                    {
                        Connexion con = Connexion.Instance;
                        con.setQuery(sqlPos);
                        con.setParam("@idOutil", IdOutil.Text);
                        dtOutilPos = con.getDataTable();
                        //Lien avec Gridview des positionnements     
                        gridviewPos.DataSource = dtOutilPos; //tableau =gridviewdata
                        gridviewPos.Visible = true;
                        gridviewPos.DataBind();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    //Alimentation de l'onglet Contrôles
                    string sqlCtrl = "";
                    sqlCtrl = "SELECT DateControle AS DateCtrl, NumDocControle AS Document, ImputationControle AS Imputation, CommentaireControle AS Comment " +
                          "FROM (dbo.tblpriControles INNER JOIN dbo.tblpriOutil ON (refOutil = idOutil)) " +
                          "WHERE dbo.tblpriControles.refOutil = @idOutil ORDER BY DateControle DESC, idcontrole DESC";
                    try
                    {
                        Connexion con = Connexion.Instance;
                        con.setQuery(sqlCtrl);
                        con.setParam("@idOutil", IdOutil.Text);
                        dtOutilCtrl = con.getDataTable();
                        //Lien avec Gridview des positionnements     
                        gridviewCtrl.DataSource = dtOutilCtrl; //tableau =gridviewdata
                        gridviewCtrl.Visible = true;
                        gridviewCtrl.DataBind();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    //Alimentation de l'onglet Entretiens - Réparations
                    string sqlEntr = "";
                    sqlEntr = "SELECT DateEntretienReparation AS DateCtrl, NumDocEntretienReparation AS Document, ImputationEntretienReparation AS Imputation, CommentaireEntretienReparation AS Comment " +
                          "FROM (dbo.tblpriEntretiensReparations INNER JOIN dbo.tblpriOutil ON (refOutil = idOutil)) " +
                          "WHERE dbo.tblpriEntretiensReparations.refOutil = @idOutil ORDER BY DateEntretienReparation DESC, idEntretienReparation DESC";
                    try
                    {
                        Connexion con = Connexion.Instance;
                        con.setQuery(sqlEntr);
                        con.setParam("@idOutil", IdOutil.Text);
                        dtOutilEntr = con.getDataTable();
                        //Lien avec Gridview des positionnements     
                        gridviewEntr.DataSource = dtOutilEntr; //tableau =gridviewdata
                        gridviewEntr.Visible = true;
                        gridviewEntr.DataBind();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    //Alimentation de l'onglet Vérifications
                    string sqlVerif = "";
                    sqlVerif = "SELECT DateVerif AS DateVerif, NumDocVerif AS Document, NomLaboratoire AS Labo, ResultatVerif AS Result " +
                          "FROM ((dbo.tblpriVerifications INNER JOIN dbo.tblpriOutil ON (refOutil = idOutil)) LEFT JOIN dbo.tblsecLaboratoires ON refLaboratoire = idLaboratoire) " +
                          "WHERE dbo.tblpriVerifications.refOutil = @idOutil ORDER BY DateVerif DESC, idVerification DESC";
                    try
                    {
                        Connexion con = Connexion.Instance;
                        con.setQuery(sqlVerif);
                        con.setParam("@idOutil", IdOutil.Text);
                        dtOutilVerif = con.getDataTable();
                        //Lien avec Gridview des positionnements     
                        gridviewVerif.DataSource = dtOutilVerif; //tableau =gridviewdata
                        gridviewVerif.Visible = true;
                        gridviewVerif.DataBind();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }
                }
            }
        }

        protected void Ajout_Click(object sender, EventArgs e)
        {
            //Gestion des contrôles
            ddlPropr.Enabled = true;
            NOutil.Enabled = true;
            Taux.Enabled = true;
            DropDownMarque.Enabled = true;
            DropDownType.Enabled = true;
            NSerie.Enabled = true;
            DropDownGamme.Enabled = true;
            Fournisseur.Enabled = true;
            DateCommande.Enabled = true;
            NCommande.Enabled = true;
            Montant.Enabled = true;
            Annee.Enabled = true;
            Notes.Enabled = true;
            DropDownIntVerif.Enabled = true;
            Accessoires.Enabled = true;

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
                lblMessage.Text = "Vous êtes occupé à ajouter un outil !";
            }
            else
            {
                lblMessage.Text = "U bent bezig een gereedschap te toevoegen!";
            }
        }

        protected void EnregistrerAjout_Click(object sender, EventArgs e)
        {
            try
            {
                //Tests préalables sur le numéro d'outil

                //Taille du numéro d'outil (5 caractères)
                if (NOutil.Text.Length != 5)
                {
                    lblMessage.Visible = true;
                    if (Session["Langue"] == "FR")
                    {
                        lblMessage.Text = "Impossible d'enregistrer l'ajout car le numéro d'outil doit comporter 5 caractères !";
                    }
                    else
                    {
                        lblMessage.Text = "Onmogelijk actie ! De gereedschap nummer moet 5 karakters zijn !";
                    }
                    return;
                }

                //Test premier caractère
                char NOutilCar1; //= NOutil.Text.Substring(0, 1);
                bool resulttestNOutilCar1 = char.TryParse(NOutil.Text.Substring(0, 1), out NOutilCar1);
                if (resulttestNOutilCar1 == true)
                {
                    if (!((NOutilCar1 >= '0' && NOutilCar1 <= '9') || (NOutilCar1 >= 'A' && NOutilCar1 <= 'Z')))
                    {
                        //1er caractère n'est pas un chiffre ni une lettre majuscule
                        lblMessage.Visible = true;

                        lblMessage.Visible = true;
                        if (Session["Langue"] == "FR")
                        {
                            lblMessage.Text = "Impossible d'enregistrer l'ajout car le premier caractère doit être une lettre majuscule ou un chiffre !";
                        }
                        else
                        {
                            lblMessage.Text = "Onmogelijk actie ! De eerste karakter moet een hoofdletter of een cijfer zijn !";
                        }
                        return;
                    }
                }

                //Test 4 autres caractères numériques
                string NOutilSuite = NOutil.Text.Substring(1, 4);
                int testNOutilSuite = 0;

                bool resulttestNOutilSuite = int.TryParse(NOutilSuite, out testNOutilSuite);

                if (resulttestNOutilSuite == false)
                {
                    lblMessage.Visible = true;
                    if (Session["Langue"] == "FR")
                    {
                        lblMessage.Text = "Impossible d'enregistrer l'ajout car les 4 derniers caractères du numéro d'outils doivent être des chiffres !";
                    }
                    else
                    {
                        lblMessage.Text = "Onmogelijk actie ! De vier laatste karakters moeten cijfers zijn !";
                    }
                    return;
                }

                lblMessage.Visible = true;
                if (Session["Langue"] == "FR")
                {
                    lblMessage.Text = NOutil.Text + " est prêt à être enregistré ... ";
                }
                else
                {
                    lblMessage.Text = NOutil.Text + " is klaar voor recording ... ";
                }

                //Outil existe --> message d'erreur
                con.setQuery("SELECT dbo.tblpriOutil.NumOutil " +
                    "FROM  dbo.tblpriOutil INNER JOIN dbo.tblpriSousFamilles ON dbo.tblpriOutil.refSousFamille = dbo.tblpriSousFamilles.idSousFamille " +
                    "WHERE (dbo.tblpriSousFamilles.NumSousFamille = @NumSsFam) AND (dbo.tblpriSousFamilles.refFamille = @NumFam) AND (dbo.tblpriOutil.NumOutil = @NumOutil)");
                con.setParam("@NumFam", NumFam.Text);
                con.setParam("@NumSsFam", SsFam.Text);
                con.setParam("@NumOutil", NOutil.Text);
                dtOutil = con.getDataTable();
                if (dtOutil.Rows.Count != 0)
                {
                    //Gestion des contrôles
                    ddlPropr.Enabled = false;
                    NOutil.Enabled = false;
                    Taux.Enabled = false;
                    DropDownMarque.Enabled = false;
                    DropDownType.Enabled = false;
                    NSerie.Enabled = false;
                    DropDownGamme.Enabled = false;
                    Fournisseur.Enabled = false;
                    DateCommande.Enabled = false;
                    NCommande.Enabled = false;
                    Montant.Enabled = false;
                    Annee.Enabled = false;
                    Notes.Enabled = false;
                    DropDownIntVerif.Enabled = false;
                    Accessoires.Enabled = false;

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
                        lblMessage.Text = "Impossible d'enregistrer l'ajout car cet outil existe déjà";
                    }
                    else
                    {
                        lblMessage.Text = "Onmogelijk actie ! Deze gereedschap bestaat al";
                    }
                    return;
                }
                else
                {
                    //Sinon on crée l'outil

                    //Retrouver le nouvel id de l'outil
                    con.setQuery("SELECT MAX(idOutil) AS MaxId FROM dbo.tblpriOutil");
                    dtOutil = con.getDataTable();
                    DataRow rowOutil = dtOutil.Rows[0];
                    int intNewIdOutil;
                    intNewIdOutil = (int)rowOutil["MaxId"] + 1;

                    //Ajouter l'outil
                    con = Connexion.Instance;
                    con.setQuery("INSERT INTO tblpriOutil (idOutil, refSousFamille, ProprietaireOutil, NumOutil, TauxOutil, MarqueOutil, TypeOutil, NumSerie, GammeOutil, FournisseurOutil, DateCommande, NumCommande, MontantCommande, Utilisation, NotesOutil, AccessoireOutil, IntervalleVerifOutil, Position0) " +
                                 "SELECT idOutil = @idOutil, refSousFamille = @refSousFamille, ProprietaireOutil = @PROPR, NumOutil = @NumOutil, TauxOutil = @TauxOutil, MarqueOutil = @MarqueOutil, TypeOutil = @TypeOutil, NumSerie = @NumSerie, GammeOutil = @GammeOutil, FournisseurOutil = @FournisseurOutil, DateCommande = @DateCommande, NumCommande = @NumCommande, MontantCommande = @MontantCommande, Utilisation = @Utilisation, NotesOutil = @NotesOutil, AccessoireOutil = @AccessoireOutil, IntervalleVerifOutil = @IntervalleVerifOutil, '098510'");
                    con.setParam("@idOutil", intNewIdOutil.ToString());
                    con.setParam("@refSousFamille", refSousFamille.Text);
                    con.setParam("@PROPR", ddlPropr.SelectedValue.ToString());
                    con.setParam("@NumOutil", NOutil.Text);
                    con.setParam("@TauxOutil", Taux.Text);
                    con.setParam("@MarqueOutil", DropDownMarque.SelectedValue.ToString());
                    con.setParam("@TypeOutil", DropDownType.SelectedValue.ToString());
                    con.setParam("@NumSerie", NSerie.Text);
                    con.setParam("@GammeOutil", DropDownGamme.SelectedValue.ToString());
                    con.setParam("@FournisseurOutil", Fournisseur.Text);
                    con.setParam("@DateCommande", DateCommande.Text);
                    con.setParam("@NumCommande", NCommande.Text);
                    con.setParam("@MontantCommande", Montant.Text);
                    con.setParam("@Utilisation", Annee.Text);
                    con.setParam("@NotesOutil", Notes.Text);
                    con.setParam("@AccessoireOutil", Accessoires.Text);
                    con.setParam("@IntervalleVerifOutil", DropDownIntVerif.SelectedValue.ToString());
                    con.getExecuteNonQuery();

                    //Gestion des contrôles
                    ddlPropr.Enabled = false;
                    Taux.Enabled = false;
                    DropDownMarque.Enabled = false;
                    DropDownType.Enabled = false;
                    NSerie.Enabled = false;
                    DropDownGamme.Enabled = false;
                    Fournisseur.Enabled = false;
                    DateCommande.Enabled = false;
                    NCommande.Enabled = false;
                    Montant.Enabled = false;
                    Annee.Enabled = false;
                    Notes.Enabled = false;
                    DropDownIntVerif.Enabled = false;
                    Accessoires.Enabled = false;

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
                        lblMessage.Text = "Vous venez de créer cet outil !";
                    }
                    else
                    {
                        lblMessage.Text = "Deze gereedschap is gemaakt !";
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
            ddlPropr.Enabled = true;
            Taux.Enabled = true;
            DropDownMarque.Enabled = true;
            DropDownType.Enabled = true;
            NSerie.Enabled = true;
            DropDownGamme.Enabled = true;
            Fournisseur.Enabled = true;
            DateCommande.Enabled = true;
            NCommande.Enabled = true;
            Montant.Enabled = true;
            Annee.Enabled = true;
            Notes.Enabled = true;
            DropDownIntVerif.Enabled = true;
            Accessoires.Enabled = true;

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
                lblMessage.Text = "Vous êtes occupé à modifier un outil !";
            }
            else
            {
                lblMessage.Text = "U bent bezig een gereedschap te wijzigen !";
            }
        }

        protected void EnregistrerModif_Click(object sender, EventArgs e)
        {

            try
            {
                //Gérer l'enregistrement des modifications
                Connexion con = Connexion.Instance;
                con.setQuery("UPDATE tblpriOutil SET ProprietaireOutil = @PROPR, TauxOutil = @TauxOutil, MarqueOutil = @MarqueOutil, TypeOutil = @TypeOutil, NumSerie = @NumSerie, GammeOutil = @GammeOutil, FournisseurOutil = @FournisseurOutil, DateCommande = @DateCommande, NumCommande = @NumCommande, MontantCommande = @MontantCommande, Utilisation = @Utilisation, NotesOutil = @NotesOutil, AccessoireOutil = @AccessoireOutil, IntervalleVerifOutil = @IntervalleVerifOutil WHERE idOutil = @idOutil");
                con.setParam("@PROPR", ddlPropr.SelectedValue.ToString());
                con.setParam("@TauxOutil", Taux.Text);
                con.setParam("@MarqueOutil", DropDownMarque.SelectedValue.ToString());
                con.setParam("@TypeOutil", DropDownType.SelectedValue.ToString());
                con.setParam("@NumSerie", NSerie.Text);
                con.setParam("@GammeOutil", DropDownGamme.SelectedValue.ToString());
                con.setParam("@FournisseurOutil", Fournisseur.Text);
                con.setParam("@DateCommande", DateCommande.Text);
                con.setParam("@NumCommande", NCommande.Text);
                con.setParam("@MontantCommande", Montant.Text);
                con.setParam("@Utilisation", Annee.Text);
                con.setParam("@NotesOutil", Notes.Text);
                con.setParam("@AccessoireOutil", Accessoires.Text);
                con.setParam("@IntervalleVerifOutil", DropDownIntVerif.SelectedValue.ToString());
                con.setParam("@idOutil", IdOutil.Text);
                con.getExecuteNonQuery();

                //Gestion des contrôles
                ddlPropr.Enabled = false;
                Taux.Enabled = false;
                DropDownMarque.Enabled = false;
                DropDownType.Enabled = false;
                NSerie.Enabled = false;
                DropDownGamme.Enabled = false;
                Fournisseur.Enabled = false;
                DateCommande.Enabled = false;
                NCommande.Enabled = false;
                Montant.Enabled = false;
                Annee.Enabled = false;
                Notes.Enabled = false;
                DropDownIntVerif.Enabled = false;
                Accessoires.Enabled = false;

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
                    lblMessage.Text = "Vous venez de modifier cet outil !";
                }
                else
                {
                    lblMessage.Text = "U bent bezig een gereedschap te wijzigen !";
                }

                //Response.Redirect("famille.aspx");
                Response.Redirect("OutilFiche.aspx?id=" + Session["Outil"]);
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }
        }

        protected void Annuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("OutilFiche.aspx?id=" + Session["Outil"]);
        }

        protected void Supprimer_Click(object sender, EventArgs e)
        {
            // Response.Redirect("FamilleFiche.aspx?id=" + sf);
            //Gestion du message
            lblMessage.Visible = true;
            if (Session["Langue"] == "FR")
            {
                lblMessage.Text = "Etes vous certain de vouloir supprimer cet outil ? Cet outil sera définitivement perdu. Ne souhaitez-vous pas plutôt le déclasser ? Pour se faire il suffit de le positionner sur l'imputation 111111. Prière de confirmer votre demande.";
            }
            else
            {
                lblMessage.Text = "Bent u zeker dat u deze gereedschap willen verwijderen ? Deze gereedschap zal permanent verloren zijn. Wil je niet liever willen downgraden ? Om dat te doen kan U plaatsen deze gereedschap op imputatie 111111. Bevestig uw aanvraag.";
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
            //Supprimer l'outil et tout son historique (positionnements, contrôles, etc ...)
            con.setQuery("DELETE dbo.tblpriOutil WHERE dbo.tblpriOutil.idOutil = @idOutil");
            con.setParam("@idOutil", IdOutil.Text);
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
                lblMessage.Text = "Vous venez de supprimer cet outil !";
            }
            else
            {
                lblMessage.Text = "Deze gereedschap is verwijderd !";
            }

        }

        protected void DropDownSFamille_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void DropDownSFamilleFR_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void DropDownSFamilleNL_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void btnCarTechn_Click(object sender, EventArgs e)
        { }

        protected void btnCde_Click(object sender, EventArgs e)
        { }

        protected void btnDivers_Click(object sender, EventArgs e)
        { }

        protected void DropDownMarque_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void DropDownMarqueChoix_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void DropDownTypeChoix_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void DropDownGammeChoix_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void DropDownProcTechnChoix_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void DropDownAnneeChoix_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void Position_Click(object sender, EventArgs e)
        { }

        protected void Controle_Click(object sender, EventArgs e)
        { }

        protected void Entr_Click(object sender, EventArgs e)
        { }

        protected void Etalonnage_Click(object sender, EventArgs e)
        { }

        protected void AjoutPos_Click(object sender, EventArgs e)
        { }

        protected void EnregistrerAjoutPos_Click(object sender, EventArgs e)
        { }

        protected void ModifierPos_Click(object sender, EventArgs e)
        { }

        protected void EnregistrerModifPos_Click(object sender, EventArgs e)
        { }

        protected void SupprimerPos_Click(object sender, EventArgs e)
        { }

        protected void ConfirmerSuppressionPos_Click(object sender, EventArgs e)
        { }

        protected void ddlImputPosOrigine_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void ddlImputPosOrigineTri_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void ddlImputPosTri_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void AjoutCtrl_Click(object sender, EventArgs e)
        { }

        protected void EnregistrerAjoutCtrl_Click(object sender, EventArgs e)
        { }

        protected void ModifierCtrl_Click(object sender, EventArgs e)
        { }

        protected void EnregistrerModifCtrl_Click(object sender, EventArgs e)
        { }
        protected void SupprimerCtrl_Click(object sender, EventArgs e)
        { }

        protected void ConfirmerSuppressionCtrl_Click(object sender, EventArgs e)
        { }

        protected void ddlImputCtrlTri_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void AjoutEntr_Click(object sender, EventArgs e)
        { }
        protected void EnregistrerAjoutEntr_Click(object sender, EventArgs e)
        { }
        protected void ModifierEntr_Click(object sender, EventArgs e)
        { }

        protected void EnregistrerModifEntr_Click(object sender, EventArgs e)
        { }
        protected void SupprimerEntr_Click(object sender, EventArgs e)
        { }

        protected void ConfirmerSuppressionEntr_Click(object sender, EventArgs e)
        { }

        protected void ddlImputEntrTri_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void AjoutVerif_Click(object sender, EventArgs e)
        { }
        protected void EnregistrerAjoutVerif_Click(object sender, EventArgs e)
        { }
        protected void ModifierVerif_Click(object sender, EventArgs e)
        { }

        protected void EnregistrerModifVerif_Click(object sender, EventArgs e)
        { }
        protected void SupprimerVerif_Click(object sender, EventArgs e)
        { }

        protected void ConfirmerSuppressionVerif_Click(object sender, EventArgs e)
        { }
    }
}