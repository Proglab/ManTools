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
    public partial class Outils : System.Web.UI.Page
    {
        //Déclaratives des datatables
        protected DataTable dt = null;
        protected DataTable dt1 = null;
        protected DataTable dtPropr = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Utilisateur"] == null)
            {//reponse et redirection
                Response.Redirect("default.aspx");
            }
            else
            {
                if (!IsPostBack) //si post
                {
                    Propr.Items.Insert(0, new ListIte(string.Empty));
                    Propr.Items.Insert(1, new ListIte("ANV"));
                    Propr.Items.Insert(2, new ListIte("BXL"));
                    Propr.Items.Insert(3, new ListIte("DCB"));
                    Propr.Items.Insert(4, new ListIte("MAN"));
                }
            }
        }

        //Exécute la sélection sur base du filtre
        protected void Rechercher_Click(object sender, EventArgs e)
        {
            Connexion con = Connexion.Instance;

            string total = "";


            try
            {
                con.setQuery("SELECT count(*) AS TotalOutils FROM tblpriOutil");
                dt1 = con.getDataTable();
                total = dt1.Rows[0]["TotalOutils"].ToString();
            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }

            OutilsDatas outilsTechnique = new OutilsDatas();
            outilsTechnique.DropDownFamille = DropDownFamille.SelectedItem.ToString();
            outilsTechnique.DropDownFamilleFR = DropDownFamilleFR.SelectedItem.ToString();
            outilsTechnique.DropDownFamilleNL = DropDownFamilleNL.SelectedItem.ToString();
            outilsTechnique.Propr = Propr.Text;
            outilsTechnique.BonTransf = BonTransf.Text;
            outilsTechnique.NumOutil = NumOutil.Text;
            outilsTechnique.DropDownPosition = DropDownPosition.Text;

            dt = outilsTechnique.Search();

            //Variable Session pour le pdf
            Session["dataTable"] = outilsTechnique;

            //Lien avec Gridview liste des Outils     
            gridviewdata.DataSource = dt; //tableau =gridviewdata
            gridviewdata.Visible = true;
            gridviewdata.DataBind();

            //    //Lien avec label nombre d'items sélectionnés sur le nbre d'items total
            Lblresults.Visible = true;
            Lblresults.Text = "il y a " + dt.Rows.Count + " Outil(s) sur " + total;

            //lien href (bouton détail du tableau) vers la fiche détaillée
            foreach (System.Web.UI.WebControls.GridViewRow row in gridviewdata.Rows) // Pour toute les lignes du row prend chaque ligneS !! Typer ces variable
            {
                System.Web.UI.WebControls.TableCell cell = new System.Web.UI.WebControls.TableCell(); //new céllule
                System.Web.UI.WebControls.LinkButton lien = new System.Web.UI.WebControls.LinkButton(); // le lien boutton
                lien.Text = "Details";// text
                lien.PostBackUrl = "~/OutilFiche.aspx?id=" + row.Cells[0].Text;//href selection la valeur 2ieme colone
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
                OutilsDatas outilsTechnique = (OutilsDatas)Session["dataTable"];
                OutilsRapports rapport = new OutilsRapports(outilsTechnique);

                Document pdfDoc = rapport.FormatOrdre();

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

        public void DropDownFamille_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void DropDownFamilleFR_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void DropDownFamilleNL_SelectedIndexChanged(object sender, EventArgs e)
        { }

        public void DropDownSFamille_SelectedIndexChanged(object sender, EventArgs e)
        { }

        public void DropDownSFamilleFR_SelectedIndexChanged(object sender, EventArgs e)
        { }

        public void DropDownSFamilleNL_SelectedIndexChanged(object sender, EventArgs e)
        { }

        public void DropDownMarque_SelectedIndexChanged(object sender, EventArgs e)
        { }

        public void TriPos_SelectedIndexChanged(object sender, EventArgs e)
        { }
        public void SupprimerCritères_Click(object sender, EventArgs e)
        { }

        public void AjouterPremierOutil_Click(object sender, EventArgs e)
        { }
    }
}