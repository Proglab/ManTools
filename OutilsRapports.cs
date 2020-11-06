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
    public class OutilsRapports
    {
        public OutilsDatas OutilsDatas = null;
        public string langue = "FR";

        public OutilsRapports(OutilsDatas OutilsDatas)
        {
            this.OutilsDatas = OutilsDatas;
        }


        public Document FormatTechnique()
        {
            if (langue == "FR")
            {
                OutilsDatas.Select = "tblpriFamilles.DescriptionFamilleFr as Familles, DescriptionSousFamilleFr as 'Sous-Familles', ProprietaireOutil, NumOutil, MarqueOutil, TypeOutil, NumSerie, NotesOutil as Notes";
            }
            else
            {
                OutilsDatas.Select = "tblpriFamilles.DescriptionFamilleNL as 'Familles & beschrijving', DescriptionSousFamilleNL as 'Sub-famille & beschrijving', ProprietaireOutil as Eigen, NumOutil as Gereed, MarqueOutil as Merk, TypeOutil as Type, NumSerie as Serienummer, NotesOutil as Nota";
            }
            OutilsDatas.Search();

            Font font5 = FontFactory.GetFont(FontFactory.HELVETICA, 5);

            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 80f, 80f);
            pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

            ITextEvents PageEvent = new ITextEvents();
            PageEvent.dt = OutilsDatas.dt;
            PageEvent.langue = langue;
            PageEvent.TitleFR = "Liste des outils";
            PageEvent.SubTitleFR = "Format Technique";

            foreach (String filtre in OutilsDatas.getFiltres())
            {
                PageEvent.filtres += filtre + " ";
            }

            pdfWriter.PageEvent = PageEvent;

            pdfDoc.Open();
            pdfDoc.NewPage();


            PdfPTable table = new PdfPTable(OutilsDatas.dt.Columns.Count) { WidthPercentage = 100 };

            foreach (DataColumn c in OutilsDatas.dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            pdfDoc.Add(table);


            table = new PdfPTable(OutilsDatas.dt.Columns.Count) { WidthPercentage = 100 };

            for (int i = 0; i < OutilsDatas.dt.Rows.Count; i++)
            {
                for (int j = 0; j < OutilsDatas.dt.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(OutilsDatas.dt.Rows[i][j].ToString(), font5));
                }
            }
            pdfDoc.Add(table);
            pdfDoc.Close();

            return pdfDoc;

        }

        public Document FormatOrdre()
        {
            if (langue == "FR")
            {
                OutilsDatas.Select = "tblpriFamilles.DescriptionFamilleFr as Familles, DescriptionSousFamilleFr as 'Sous-Familles', ProprietaireOutil, NumOutil, FournisseurOutil as Fournisseur, NumCommande, DateCommande, MontantCommande as 'Montant Commande (eur)'";
            }
            else
            {
                OutilsDatas.Select = "tblpriFamilles.DescriptionFamilleNL as 'Familles & beschrijving', DescriptionSousFamilleNL as 'Sub-famille & beschrijving', ProprietaireOutil as Eigen, NumOutil as Gereed, FournisseurOutil as Leverancier, BestellingNr, 'Best.Dat.' , MontantCommande as 'Bedrag (eur)'";
            }
            OutilsDatas.Search();

            Font font5 = FontFactory.GetFont(FontFactory.HELVETICA, 5);

            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 80f, 80f);
            pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);

            ITextEvents PageEvent = new ITextEvents();
            PageEvent.dt = OutilsDatas.dt;
            PageEvent.langue = langue;
            PageEvent.TitleFR = "Liste des outils";
            PageEvent.SubTitleFR = "Format Ordre";

            foreach (String filtre in OutilsDatas.getFiltres())
            {
                PageEvent.filtres += filtre + " ";
            }

            pdfWriter.PageEvent = PageEvent;

            pdfDoc.Open();
            pdfDoc.NewPage();


            PdfPTable table = new PdfPTable(OutilsDatas.dt.Columns.Count) { WidthPercentage = 100 };

            foreach (DataColumn c in OutilsDatas.dt.Columns)
            {
                table.AddCell(new Phrase(c.ColumnName, font5));
            }

            pdfDoc.Add(table);

            float total = 0f;
            string totalString = "";
            table = new PdfPTable(OutilsDatas.dt.Columns.Count) { WidthPercentage = 100 };

            for (int i = 0; i < OutilsDatas.dt.Rows.Count; i++)
            {
                for (int j = 0; j < OutilsDatas.dt.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(OutilsDatas.dt.Rows[i][j].ToString(), font5));

                    if (j == 7 && OutilsDatas.dt.Rows[i][j].ToString() != "")
                    {
                        totalString = OutilsDatas.dt.Rows[i][j].ToString();
                        total += float.Parse(totalString);
                    }
                }
            }


            Font baseFontNormal = new Font(Font.HELVETICA, 10f, Font.NORMAL, Color.BLACK);

            PdfPCell pdfCell1 = new PdfPCell();
            PdfPCell pdfCell2 = new PdfPCell();
            PdfPCell pdfCell3 = new PdfPCell();
            PdfPCell pdfCell4 = new PdfPCell();
            PdfPCell pdfCell5 = new PdfPCell();
            PdfPCell pdfCell6 = new PdfPCell();
            PdfPCell pdfCell7 = new PdfPCell(new Phrase("TOTAL (eur)", baseFontNormal));
            PdfPCell pdfCell8 = new PdfPCell(new Phrase(total.ToString("#0.00"), baseFontNormal));

            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;
            pdfCell4.Border = 0;
            pdfCell5.Border = 0;
            pdfCell6.Border = 1;
            pdfCell7.Border = 1;
            pdfCell8.Border = 1;

            table.AddCell(pdfCell1);
            table.AddCell(pdfCell2);
            table.AddCell(pdfCell3);
            table.AddCell(pdfCell4);
            table.AddCell(pdfCell5);
            table.AddCell(pdfCell6);
            table.AddCell(pdfCell7);
            table.AddCell(pdfCell8);



            pdfDoc.Add(table);
            pdfDoc.Close();

            return pdfDoc;

        }

    }
}