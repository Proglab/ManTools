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

    public partial class Famille : System.Web.UI.Page
    {
        //Déclaratives des datatables
        protected DataTable dt = null;
        protected DataTable dt1 = null;
        protected DataTable dtCodesFamilles = null;
        protected DataTable dtDescrFamillesFR = null;
        protected DataTable dtDescrFamillesNL = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            int i;

            //Test utilisateur loggé
            if (Session["Utilisateur"] == null)
            {
                Response.Redirect("default.aspx");
            }
            else
            {
                //Test Post
                if (!IsPostBack)
                {
                    //Préparation des datatables pour l'alimentation des dropdownlists
                    try
                    {
                        Connexion con = Connexion.Instance;
                        con.setQuery("SELECT codeFamille FROM dbo.tblpriFamilles ORDER BY codeFamille");
                        dtCodesFamilles = con.getDataTable();
                        con.setQuery("SELECT DescriptionFamilleFr FROM dbo.tblpriFamilles ORDER BY DescriptionFamilleFr");
                        dtDescrFamillesFR = con.getDataTable();
                        con.setQuery("SELECT DescriptionFamilleNl FROM dbo.tblpriFamilles ORDER BY DescriptionFamilleNl");
                        dtDescrFamillesNL = con.getDataTable();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }

                    //Insertion d'un champ vide en première ligne des dropdown
                    DropDownFamille.Items.Insert(0, new ListIte(string.Empty, string.Empty));
                    DropDownFamilleFR.Items.Insert(0, new ListIte(string.Empty, string.Empty));
                    DropDownFamilleNL.Items.Insert(0, new ListIte(string.Empty, string.Empty));

                    //Alimentation manuelle des dropdownlists
                    i = 1;
                    foreach (DataRow row in dtCodesFamilles.Rows)
                    {
                        DropDownFamille.Items.Insert(i, new ListIte(row["codeFamille"].ToString(), row["codeFamille"].ToString()));
                        i++;
                    }

                    i = 1;
                    foreach (DataRow row in dtDescrFamillesFR.Rows)
                    {
                        DropDownFamilleFR.Items.Insert(i, new ListIte(row["DescriptionFamilleFr"].ToString(), row["DescriptionFamilleFr"].ToString()));
                        i++;
                    }
                    i = 1;
                    foreach (DataRow row in dtDescrFamillesNL.Rows)
                    {
                        DropDownFamilleNL.Items.Insert(i, new ListIte(row["DescriptionFamilleNl"].ToString(), row["DescriptionFamilleNl"].ToString()));
                        i++;
                    }
                }
            }
        }

        protected void Download_Csv(object sender, EventArgs e)
        {
            if (Session["Utilisateur"] == null)
            {
                Response.Redirect("default.aspx");
            }

            this.writeCSV(gridviewdata, DateTime.Now.ToString("yyyyMMdd"));
        }

        protected void Download_Pdf(object sender, EventArgs e)
        {
            if (Session["Utilisateur"] == null)
            {
                Response.Redirect("default.aspx");
            }

            try
            {
                DataTable dt = (DataTable)Session["dataTable"];

                Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();
                pdfDoc.NewPage();

                iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);

                PdfPTable table = new PdfPTable(dt.Columns.Count);

                foreach (DataColumn c in dt.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font5));
                }

                PdfPTable pdftable = new PdfPTable(dt.Columns.Count);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        table.AddCell(new Phrase(dt.Rows[i][j].ToString(), font5));
                    }
                }
                pdfDoc.Add(table);
                pdfDoc.Close();

                Response.ContentType = "application/pdf";

                //Set default file Name as current datetime
                Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");

                System.Web.HttpContext.Current.Response.Write(pdfDoc);

                Response.Flush();
                Response.End();
            }
            catch (DocumentException ex)
            {
                Response.Write(ex.ToString());
            }

        }

        protected void DropDownFamille_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownFamille.Text != "")
            {
                Connexion con = Connexion.Instance;
                con.setQuery("SELECT codeFamille, DescriptionFamilleFr, DescriptionFamilleNl FROM dbo.tblpriFamilles WHERE codeFamille = '" + DropDownFamille.Text + "'");
                dtCodesFamilles = con.getDataTable();
                DataRow row = dtCodesFamilles.Rows[0];
                DropDownFamilleFR.Text = row["DescriptionFamilleFr"].ToString();
                DropDownFamilleNL.Text = row["DescriptionFamilleNl"].ToString();
            }
            else
            {
                //DropDownFamilleFR.SelectedItem.Value = "";
                DropDownFamilleFR.Text = "";
                DropDownFamilleNL.Text = "";
            }
        }

        protected void DropDownFamilleFR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownFamilleFR.Text != "")
            {
                Connexion con = Connexion.Instance;
                con.setQuery("SELECT codeFamille, DescriptionFamilleFr, DescriptionFamilleNl FROM dbo.tblpriFamilles WHERE DescriptionFamilleFr = '" + DropDownFamilleFR.Text + "'");
                dtCodesFamilles = con.getDataTable();
                DataRow row = dtCodesFamilles.Rows[0];
                DropDownFamille.Text = row["codeFamille"].ToString();
                DropDownFamilleNL.Text = row["DescriptionFamilleNl"].ToString();
            }
            else
            {
                //DropDownFamilleFR.SelectedItem.Value = "";
                DropDownFamille.Text = "";
                DropDownFamilleNL.Text = "";
            }
        }

        protected void DropDownFamilleNL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownFamilleNL.Text != "")
            {
                Connexion con = Connexion.Instance;
                con.setQuery("SELECT codeFamille, DescriptionFamilleFr, DescriptionFamilleNl FROM dbo.tblpriFamilles WHERE DescriptionFamilleNl = '" + DropDownFamilleNL.Text + "'");
                dtCodesFamilles = con.getDataTable();
                DataRow row = dtCodesFamilles.Rows[0];
                DropDownFamille.Text = row["codeFamille"].ToString();
                DropDownFamilleFR.Text = row["DescriptionFamilleFr"].ToString();
            }
            else
            {
                //DropDownFamilleFR.SelectedItem.Value = "";
                DropDownFamille.Text = "";
                DropDownFamilleFR.Text = "";
            }
        }

        //Exécute la sélection sur base du filtre
        protected void Rechercher_Click(object sender, EventArgs e)
        {
            //Déclaration des variables locales
            string sql = "";
            string sqlWhereClause = "";
            string total = "";

            //Test au moins un critère : suspendu temporairement suite demande Dirk reste problème de design
            //if (DropDownFamille.Text == "" && DropDownFamilleFR.Text == "" && DropDownFamilleNL.Text == "" && PrCtrl.Text == "" && IdTaux.Text == "" && Quantifiable.Checked == false)
            //{
            //    Lblresults.Visible = true;
            //    if (Session["Langue"] == "FR")
            //    {
            //        Lblresults.Text = "il faut au moins un critère de recherche";
            //    }
            //    else
            //    {
            //        Lblresults.Text = "Ten minste één zoekcriterium is nodig";
            //    }               
            //    return;            
            //}

            //Préparation du filtre via la variable sqlWhereClause
            if (DropDownFamille.SelectedItem.ToString() != "")
            {
                sqlWhereClause += " tblpriFamilles.codeFamille = @famille";
            }
            if (DropDownFamilleFR.SelectedItem.ToString() != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriFamilles.DescriptionFamilleFr = @DescrfamilleFr";
            }
            if (DropDownFamilleNL.SelectedItem.ToString() != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriFamilles.DescriptionFamilleNl = @DescrfamilleNl";
            }
            if (PrCtrl.Text != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriFamilles.PeriodeControle = @PerCtrl";
            }
            if (IdTaux.Text != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriSousFamilles.TauxSousFamille = @Taux";
            }
            if (Quantifiable.Checked == true)
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriSousFamilles.Quantifiable = @Quant";
            }

            //Préparation de la requête SQL principale sans le filtre
            sql = "SELECT idSousFamille AS Id, codeFamille AS Fam, NumSousFamille AS S_Fam, DescriptionFamilleFr AS Nom_Fam_Fr, DescriptionSousFamilleFr AS Nom_SsFam_Fr, DescriptionFamilleNl AS Naam_Fam_Nl, " +
            "DescriptionSousFamilleNl AS Naam_SbFam_Nl, TauxSousFamille AS Tx_Trf, PeriodeControle AS Control " +
            "FROM dbo.tblpriFamilles INNER JOIN dbo.tblpriSousFamilles ON (codeFamille = refFamille) ";

            //Ajout du filtre
            if (sqlWhereClause != "")
            {
                sql += " WHERE";
                sql += sqlWhereClause;
            }

            //Paramétrage du filtre + initialisation du datable principal (liste) + 
            //initialisation des données liées au label sur le nombre d'items sélectionnés sur le nombre total de sous-familles 
            try
            {
                Connexion con = Connexion.Instance;
                con.setQuery(sql);
                if (DropDownFamille.SelectedItem.ToString() != "")
                {
                    con.setParam("@famille", DropDownFamille.SelectedItem.ToString());
                }
                if (DropDownFamilleFR.SelectedItem.ToString() != "")
                {
                    con.setParam("@DescrfamilleFr", DropDownFamilleFR.SelectedItem.ToString());
                }
                if (DropDownFamilleNL.SelectedItem.ToString() != "" && sqlWhereClause != "")
                {
                    con.setParam("@DescrfamilleNl", DropDownFamilleNL.SelectedItem.ToString());
                }
                if (PrCtrl.Text != "")
                {
                    con.setParam("@PerCtrl", PrCtrl.Text);
                }
                if (IdTaux.Text != "")
                {
                    con.setParam("@Taux", IdTaux.Text);
                }
                if (Quantifiable.Checked == true)
                {
                    con.setParam("@Quant", "true");
                }
                dt = con.getDataTable();

                con.setQuery("SELECT count(*) AS TotalSsFam FROM tblpriSousFamilles");
                dt1 = con.getDataTable();
                total = dt1.Rows[0]["TotalSsFam"].ToString();
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }

            //Variable Session pour le pdf
            Session["dataTable"] = dt;

            //Lien avec Gridview liste des familles     
            gridviewdata.DataSource = dt; //tableau =gridviewdata
            gridviewdata.Visible = true;
            gridviewdata.DataBind();

            //Lien avec label nombre d'items sélectionnés sur le nbre d'items total
            Lblresults.Visible = true;
            if (Session["Langue"] == "FR")
            {
                Lblresults.Text = "il y a " + dt.Rows.Count + " famille(s) sur " + total;
            }
            else
            {
                Lblresults.Text = "Er is (zijn) " + dt.Rows.Count + " familie(s) uit " + total;
            }
            //lien href (bouton détail du tableau) vers la fiche détaillée
            foreach (System.Web.UI.WebControls.GridViewRow row in gridviewdata.Rows) // Pour toute les lignes du row prend chaque ligneS !! Typer ces variable
            {
                System.Web.UI.WebControls.TableCell cell = new System.Web.UI.WebControls.TableCell(); //new céllule
                System.Web.UI.WebControls.LinkButton lien = new System.Web.UI.WebControls.LinkButton(); // le lien boutton
                lien.Text = "Detail";// text
                lien.PostBackUrl = "~/familleFiche.aspx?id=" + row.Cells[0].Text;//href selction la valeur 2ieme colone
                cell.Controls.Add(lien);// prend la cellule le contenu et rajoute le lien
                row.Cells.Add(cell); // prends les cellules de la ligne en cort et ajoute lui la céllule
            }

            //Visibilité des boutons l'impression PDF et le Download Excel
            if (dt.Rows.Count > 0)
            {
                btnDownloadPDF.Visible = true;
                btnDownloadCSV.Visible = true;
            }
            else
            {
                btnDownloadPDF.Visible = false;
                btnDownloadCSV.Visible = false;
            }
        }

        //Supprime le filtre
        protected void SupprimerCritères_Click(object sender, EventArgs e)
        {
            DropDownFamille.Text = "";
            DropDownFamilleFR.Text = "";
            DropDownFamilleNL.Text = "";
            PrCtrl.Text = "";
            IdTaux.Text = "";

            gridviewdata.Visible = false;
            Lblresults.Text = "";

            btnDownloadPDF.Visible = false;
            btnDownloadCSV.Visible = false;
        }

        //spécifique au csv
        public void writeCSV(GridView gridIn, string fileName)
        {
            //Eet the resulting file attachment name to the name of the report...
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
            Response.Charset = "UTF-8";
            Response.ContentType = "application/text";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            // Get the header row text form the sortable columns
            LinkButton headerLink = new LinkButton();
            string headerText = string.Empty;

            for (int k = 0; k < gridIn.HeaderRow.Cells.Count; k++)
            {
                //add separator
                headerText = gridIn.HeaderRow.Cells[k].Text;
                sb.Append(headerText + ";");
            }

            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < gridIn.Rows.Count; i++)
            {
                for (int k = 0; k < gridIn.HeaderRow.Cells.Count; k++)
                {
                    //add separator and strip "," values from returned content...
                    sb.Append(gridIn.Rows[i].Cells[k].Text.Replace(";", "") + ";");
                }
                //append new line
                sb.Append("\r\n");
            }
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }


    }
}