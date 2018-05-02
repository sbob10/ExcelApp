using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourenVerwaltung
{
    public class ExcelManager
    {

        #region Data
        private SLDocument Leistungsuebersicht;
        private SLDocument Firmen;
        private SLDocument Fahrer;
        #endregion Data

        #region Loading Methods

        public List<LUEntry> LoadCollectionLUEntry(String month)
        {
            try
            {
                Leistungsuebersicht = new SLDocument("Leistungsübersicht.xlsx");
            }
            catch (System.IO.FileNotFoundException e)
            {
                Leistungsuebersicht = new SLDocument();
                Leistungsuebersicht.SaveAs("Leistungsübersicht.xlsx");
                Leistungsuebersicht = new SLDocument("Leistungsübersicht.xlsx");
            }
            catch (System.IO.IOException f)
            {
                throw f;
            }

            _InitWorkSheets(Leistungsuebersicht);
            Leistungsuebersicht.SelectWorksheet(month);

            List<LUEntry> tempList = new List<LUEntry>();

            for (int i = 2; i < 102; i++)
            {
                LUEntry temp = new LUEntry();
                temp.ID = Leistungsuebersicht.GetCellValueAsInt32("A" + i.ToString());
                temp.Datum = Leistungsuebersicht.GetCellValueAsString("B" + i.ToString());
                temp.RechnNr = Leistungsuebersicht.GetCellValueAsString("C" + i.ToString());
                temp.Auftragsgeber = Leistungsuebersicht.GetCellValueAsString("D" + i.ToString());
                temp.Autotyp = Leistungsuebersicht.GetCellValueAsString("E" + i.ToString());
                temp.Fahrer = Leistungsuebersicht.GetCellValueAsString("F" + i.ToString());
                temp.Beladeort = Leistungsuebersicht.GetCellValueAsString("G" + i.ToString());
                temp.Entladeort = Leistungsuebersicht.GetCellValueAsString("H" + i.ToString());
                temp.Preis_Netto = Leistungsuebersicht.GetCellValueAsDouble("I" + i.ToString());
                temp.WarteZeit = Leistungsuebersicht.GetCellValueAsDouble("J" + i.ToString());
                temp.BeEntladezeit = Leistungsuebersicht.GetCellValueAsDouble("K" + i.ToString());
                temp.Rückfracht = Leistungsuebersicht.GetCellValueAsDouble("L" + i.ToString());
                temp.Maut = Leistungsuebersicht.GetCellValueAsDouble("M" + i.ToString());
                temp.GesamtNetto = Leistungsuebersicht.GetCellValueAsDouble("N" + i.ToString());

                if (string.IsNullOrEmpty(temp.Datum) || string.IsNullOrEmpty(temp.RechnNr)) 
                {
                    break;
                }
                else
                {
                    tempList.Add(temp);
                }
            }

            return tempList;
        }

        #endregion Loading Methods

        #region Storing Methods

        public void StoreCollectionLU(List<LUEntry> collection, String month)
        {
            int i = 2, j = 1;

            Leistungsuebersicht.SelectWorksheet(month);

            Leistungsuebersicht.SetCellValue("A1", "Leistungsübersicht WLG Transporte:  Aufträge - Fahrten 2018");

            for (int x = 0; x < 100; x++)
            {
                if (x < collection.Count)
                {
                    Leistungsuebersicht.SetCellValue("A" + i.ToString(), collection.ElementAt(x).ID.ToString());
                    Leistungsuebersicht.SetCellValue("B" + i.ToString(), collection.ElementAt(x).Datum.ToString());
                    Leistungsuebersicht.SetCellValue("C" + i.ToString(), collection.ElementAt(x).RechnNr.ToString());
                    Leistungsuebersicht.SetCellValue("D" + i.ToString(), collection.ElementAt(x).Auftragsgeber.ToString());
                    Leistungsuebersicht.SetCellValue("E" + i.ToString(), collection.ElementAt(x).Autotyp.ToString());
                    Leistungsuebersicht.SetCellValue("F" + i.ToString(), collection.ElementAt(x).Fahrer.ToString());
                    Leistungsuebersicht.SetCellValue("G" + i.ToString(), collection.ElementAt(x).Beladeort.ToString());
                    Leistungsuebersicht.SetCellValue("H" + i.ToString(), collection.ElementAt(x).Entladeort.ToString());
                    Leistungsuebersicht.SetCellValue("I" + i.ToString(), collection.ElementAt(x).Preis_Netto.ToString());
                    Leistungsuebersicht.SetCellValue("J" + i.ToString(), collection.ElementAt(x).WarteZeit.ToString());
                    Leistungsuebersicht.SetCellValue("K" + i.ToString(), collection.ElementAt(x).BeEntladezeit.ToString());
                    Leistungsuebersicht.SetCellValue("L" + i.ToString(), collection.ElementAt(x).Rückfracht.ToString());
                    Leistungsuebersicht.SetCellValue("M" + i.ToString(), collection.ElementAt(x).Maut.ToString());
                    Leistungsuebersicht.SetCellValue("N" + i.ToString(), collection.ElementAt(x).GesamtNetto.ToString());
                }
                else
                {
                    Leistungsuebersicht.SetCellValue("A" + i.ToString(), j.ToString());
                    Leistungsuebersicht.SetCellValue("B" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("C" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("D" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("E" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("F" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("G" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("H" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("I" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("J" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("K" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("L" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("M" + i.ToString(), "");
                    Leistungsuebersicht.SetCellValue("N" + i.ToString(), "");
                }

                i++;
                j++;
            }
            Leistungsuebersicht.Save();
            Leistungsuebersicht = new SLDocument("Leistungsübersicht.xlsx");
        }

      

        #endregion Storing Methods

        #region Opening Methods

        public void OpenLeistungsuebersicht(String month)
        {
            System.Diagnostics.Process.Start("Leistungsübersicht.xlsx");
        }

        public void OpenExcelB()
        {
            System.Diagnostics.Process.Start("ExcelB.xlsx");
        }

        public void OpenExcelC(String month)
        {
            System.Diagnostics.Process.Start("ExcelC.xlsx");
        }

        #endregion Opening Methods

        #region Private Methods

        private void _InitWorkSheets(SLDocument slDoc)
        {
            slDoc.DeleteWorksheet("Sheet1"); //delete default worksheet created by SpreadSheetLite-API
            foreach (var item in Constants.Months)
            {
                if (!slDoc.SelectWorksheet(item))
                    slDoc.AddWorksheet(item);
            }
        }

        #endregion Private Methods


        //Setting Font:

        //  SLStyle style = slDocExcelA.CreateStyle();
        //  style.SetFont("FontName", fontSize);
        //  style.Font.Underline = DocumentFormat.OpenXml.Spreadsheet.UnderlineValues.Single;
        //  style.Font.Bold = true;
        //  slDocExcelA.SetRowStyle(RowIndex, style);
        //  slDocExcelA.SetCellStyle(stringReference, style);  

        // http://spreadsheetlight.com/sample-code/





    }
}
