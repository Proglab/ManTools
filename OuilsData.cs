using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;

namespace ManTools2020
{
    public class OutilsDatas
    {
        public DataTable dt = null;
        List<String> filtres = new List<String>();

        #region Paramètres

        public String DropDownFamille = "";
        public String DropDownFamilleFR = "";
        public String DropDownFamilleNL = "";
        public String Propr = "";
        public String BonTransf = "";
        public String NumOutil = "";
        public String DropDownPosition = "";
        public String Quantifiable = "";
        public String Select = "idOutil AS Id, codeFamille AS Fam, NumSousFamille AS SsFam, DescriptionFamilleFr AS Nom_Fam_Fr, " +
            "DescriptionSousFamilleFr AS Nom_SsFam_Fr, " +
            "ProprietaireOutil AS Propr, NumOutil AS Outil, QTE AS Qte, Position AS Pos, NomChantier AS Chantier";

        #endregion


        public OutilsDatas()
        {

        }


        public DataTable Search()
        {
            filtres.Clear();


            //Déclaration des variables locales
            string sql = "";
            string sqlWhereClause = " tblpriPositionnements.idPositionnement IN (SELECT MAX(idPositionnement) as idPositionnement " +
            "FROM tblpriPositionnements " +
            "GROUP BY refOutil) ";

            //    //Préparation du filtre via la variable sqlWhereClause
            if (DropDownFamille != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriFamilles.codeFamille = @famille";
            }

            if (DropDownFamilleFR != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriFamilles.DescriptionFamilleFr = @DescrfamilleFr";
            }


            if (DropDownFamilleNL != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriFamilles.DescriptionFamilleNl = @DescrfamilleNl";
            }

            if (Propr != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriOutil.ProprietaireOutil = @Propr";
            }
            if (BonTransf != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriOutil.NumDocTran0 = @NumDocTran";
            }
            if (NumOutil != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriOutil.NumOutil = @NumOutil";
            }

            if (DropDownPosition != "")
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriPositionnements.Position = @PosAct";
            }
            else
            {
                if (sqlWhereClause != "")
                {
                    sqlWhereClause += " AND";
                }
                sqlWhereClause += " tblpriPositionnements.Position <> '111111'";
            }

            sql = "SELECT " + Select + " " +
            "FROM tblpriPositionnements " +
            "JOIN tblpriOutil ON (tblpriPositionnements.refOutil = tblpriOutil.idOutil) " +
            "JOIN dbo.tblpriSousFamilles ON (tblpriOutil.refSousFamille = tblpriSousFamilles.idSousFamille) " +
            "JOIN dbo.tblpriFamilles ON (tblpriSousFamilles.refFamille = tblpriFamilles.codeFamille) " +
            "JOIN dbo.tblpriImputations ON (dbo.tblpriImputations.codeImputation = dbo.tblpriPositionnements.Position) ";

            //Ajout du filtre
            if (sqlWhereClause != "")
            {
                sql += " WHERE";
                sql += sqlWhereClause;

            }

            //    //Paramétrage du filtre + initialisation du datable principal (liste) + 
            //    //initialisation des données liées au label sur le nombre d'items sélectionnés sur le nombre total de sous-familles 
            try
            {
                Connexion con = Connexion.Instance;
                con.setQuery(sql);
                if (DropDownFamille != "")
                {
                    con.setParam("@famille", DropDownFamille);
                    filtres.Add("Famille = " + DropDownFamille);
                }

                if (DropDownFamilleFR != "")
                {
                    con.setParam("@DescrfamilleFr", DropDownFamilleFR);
                    filtres.Add("FamilleFR = " + DropDownFamilleFR);
                }

                if (DropDownFamilleNL != "" && sqlWhereClause != "")
                {
                    con.setParam("@DescrfamilleNl", DropDownFamilleNL);
                    filtres.Add("FamilleNL = " + DropDownFamilleNL);
                }

                if (Propr != "")
                {
                    con.setParam("@Propr", Propr);
                    filtres.Add("Propr = " + Propr);
                }
                if (BonTransf != "")
                {
                    con.setParam("@NumDocTran", BonTransf);
                    filtres.Add("BonTransf = " + BonTransf);
                }
                if (NumOutil != "")
                {
                    con.setParam("@NumOutil", NumOutil);
                    filtres.Add("NumOutil = " + NumOutil);
                }
                if (DropDownPosition != "")
                {
                    con.setParam("@PosAct", DropDownPosition);
                    filtres.Add("Position = " + DropDownPosition);
                }
                else
                {
                    filtres.Add("Position <> " + "111111");
                }
                //        if (Quantifiable == true)
                //        {
                //            con.setParam("@Quant", "true");
                //        }


                dt = con.getDataTable();

                return dt;

            }
            catch (SqlException exp)
            {
                throw new InvalidOperationException("Data could not be read", exp);
            }

        }

        public List<String> getFiltres()
        {
            return filtres;
        }
    }
}