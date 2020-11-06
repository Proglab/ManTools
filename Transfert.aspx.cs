using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTools2020
{
    public partial class Transfert : System.Web.UI.Page
    {

        protected DataTable dtSource;
        protected DataTable dtDestination;
        protected DataTable dtDatagrid;
        protected DataTable dtResumeDatagrid = new DataTable();



        protected void Page_Load(object sender, EventArgs e)
        {
            //Vérification de connexion de l'utilisateur
            if (Session["Utilisateur"] == null)
            {
                Response.Redirect("default.aspx");
            }
            else
            {

                if (dtResumeDatagrid.Columns.Count == 0)
                {
                    //initialisation du datagrid de validation
                    dtResumeDatagrid.Columns.Add("caseChecked", typeof(string));
                    dtResumeDatagrid.Columns.Add("idOutil", typeof(string));
                    dtResumeDatagrid.Columns.Add("Quantifiable", typeof(string));
                    dtResumeDatagrid.Columns.Add("codeFamille", typeof(string));
                    dtResumeDatagrid.Columns.Add("DescriptionFamilleFr", typeof(string));
                    dtResumeDatagrid.Columns.Add("NumSousFamille", typeof(string));
                    dtResumeDatagrid.Columns.Add("DescriptionSousFamilleFr", typeof(string));
                    dtResumeDatagrid.Columns.Add("NumOutil", typeof(string));
                    dtResumeDatagrid.Columns.Add("Position0", typeof(string));
                }

                //si la page n'a pas été postée
                if (!IsPostBack)
                {
                    try
                    {
                        Connexion.Instance.setQuery("SELECT codeImputation, CONCAT(codeImputation, ' - ',NomChantier) AS Imputation FROM  dbo.tblpriImputations ORDER BY codeImputation");
                        dtSource = Connexion.Instance.getDataTable();
                        dtDestination = Connexion.Instance.getDataTable();
                    }
                    catch (SqlException exp)
                    {
                        throw new InvalidOperationException("Data could not be read", exp);
                    }
                    //remplissage des source et destination 
                    DropDownSource.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    DropDownDestination.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                    int i = 1;
                    foreach (DataRow row in dtSource.Rows)
                    {
                        DropDownSource.Items.Insert(i, new ListItem(row["Imputation"].ToString(), row["codeImputation"].ToString()));
                        i++;
                    }

                    i = 1;
                    foreach (DataRow row in dtSource.Rows)
                    {
                        DropDownDestination.Items.Insert(i, new ListItem(row["Imputation"].ToString(), row["codeImputation"].ToString()));
                        i++;
                    }

                    //selection de la date du calendrier par défaut
                    DateTransfert.SelectedDate = DateTime.Today;
                }


            }

        }

        protected void search_Click(object sender, EventArgs e)
        {

            //si la Page est valide
            if (Page.IsValid)
            {
                Connexion.Instance.setQuery("SELECT dbo.tblpriOutil.idOutil, dbo.tblpriFamilles.codeFamille, dbo.tblpriFamilles.DescriptionFamilleFr, dbo.tblpriSousFamilles.NumSousFamille, dbo.tblpriSousFamilles.DescriptionSousFamilleFr, dbo.tblpriSousFamilles.Quantifiable, dbo.tblpriOutil.NumOutil, dbo.tblpriOutil.Position0 " +
                    "FROM  dbo.tblpriFamilles INNER JOIN " +
                    "dbo.tblpriSousFamilles ON dbo.tblpriFamilles.codeFamille = dbo.tblpriSousFamilles.refFamille INNER JOIN " +
                    "dbo.tblpriOutil ON dbo.tblpriSousFamilles.idSousFamille = dbo.tblpriOutil.refSousFamille " +
                    "WHERE (dbo.tblpriOutil.Position0 = '" + DropDownSource.SelectedItem.Value + "') " +
                    "ORDER BY dbo.tblpriFamilles.DescriptionFamilleFr, dbo.tblpriSousFamilles.DescriptionSousFamilleFr");
                dtDatagrid = Connexion.Instance.getDataTable();

                //remplir le datagrid
                Grid.DataSource = dtDatagrid;
                Grid.DataBind();

                //pour chaque ligne vérifier le quantifiable
                foreach (DataGridItem dataGridItem in Grid.Items)
                {
                    Boolean Quantifiable = dataGridItem.Cells[2].Text == "False" ? false : true;
                    TextBox InputText = (TextBox)dataGridItem.FindControl("Nombre");

                    //si tout les outils
                    if (type.SelectedItem.Value != "0")
                    {
                        CheckBox caseChecked = (CheckBox)dataGridItem.FindControl("caseChecked");
                        caseChecked.Checked = true;
                    }

                    if (Quantifiable == true)
                    {
                        //quantité autorisée
                        InputText.Enabled = true;
                    }
                    else
                    {
                        //quantité non autorisée
                        InputText.Enabled = false;
                    }
                }

                //titre
                origin.Text = DropDownSource.SelectedItem.Text;
                destination.Text = DropDownDestination.SelectedItem.Text;

                //navigation entre les panels pour empecher la modification du formulaire
                Datagrid.Visible = true;
                Form.Visible = false;
            }
        }

        protected void transfertBouton_Click(object sender, EventArgs e)
        {
            //titre
            source.Text = DropDownSource.SelectedItem.Text;
            dest.Text = DropDownDestination.SelectedItem.Text;


            //récupération des différentes variables
            String Source = DropDownSource.SelectedItem.Value;
            String destination = DropDownDestination.SelectedItem.Value;
            String BonTransfert = BonTransfertNumber.Text;
            String DateTranfert = ((Calendar)DateTransfert).SelectedDate.ToString("yyyy-MM-dd");
            String TypeTranfert = type.Text;

            //récupération du max positionnements car pas de auto increment
            Connexion.Instance.setQuery("SELECT Max(idPositionnement) as idPositionnement FROM tblpriPositionnements");
            int max = int.Parse(Connexion.Instance.getExecuteScalar());
            max++;


            //si on sélectionne que quelques outils :
            foreach (DataGridItem dataGridItem in Grid.Items)
            {
                Boolean check = ((CheckBox)dataGridItem.FindControl("caseChecked")).Checked;
                String idOutil = dataGridItem.Cells[1].Text;
                String Nombre = ((TextBox)dataGridItem.FindControl("Nombre")).Text;
                Boolean Quantifiable = dataGridItem.Cells[2].Text == "False" ? false : true;

                if (check == true)
                {
                    //si la case a cocher est cochée
                    String sql = "INSERT INTO dbo.tblpriPositionnements " +
                        "VALUES ( '" + max.ToString() + "', '" + idOutil + "', '" + Source + "', '" + destination + "', '" + Nombre + "', '" + BonTransfert + "', '" + DateTranfert + "');";

                    Connexion con = Connexion.Instance;

                    con.setQuery(sql);
                    con.getExecuteNonQuery();

                    max++;

                    //remplissage du datagrid de confirmation
                    DataRow NewRow = dtResumeDatagrid.NewRow();
                    NewRow[0] = check.ToString();
                    NewRow[1] = idOutil;
                    NewRow[2] = Quantifiable.ToString();
                    NewRow[3] = dataGridItem.Cells[3].Text;
                    NewRow[4] = dataGridItem.Cells[4].Text;
                    NewRow[5] = dataGridItem.Cells[5].Text;
                    NewRow[6] = dataGridItem.Cells[6].Text;
                    NewRow[7] = dataGridItem.Cells[7].Text;
                    NewRow[8] = Nombre;
                    dtResumeDatagrid.Rows.Add(NewRow);
                }
            }
            //binding
            GridResume.DataSource = dtResumeDatagrid;
            GridResume.DataBind();

            //vues
            Datagrid.Visible = false;
            Form.Visible = false;
            ok.Visible = true;
        }


        protected void Annuler_Click(object sender, EventArgs e)
        {
            Datagrid.Visible = false;
            Form.Visible = true;
        }

        protected void retour_Click(object sender, EventArgs e)
        {
            Response.Redirect("Transfert.aspx");

        }

        protected void TriChantierOrigine_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void TriChantierDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ReinitCrit_Click(object sender, EventArgs e)
        {
        }

        protected void PDF_Click(object sender, EventArgs e)
        {
        }

        protected void CSV_Click(object sender, EventArgs e)
        { }
    }
}