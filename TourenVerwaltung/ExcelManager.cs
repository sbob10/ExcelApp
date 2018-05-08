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
        private SLDocument FirmenPreise;
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
                temp.RechnNr = Leistungsuebersicht.GetCellValueAsInt32("C" + i.ToString());
                temp.Auftragsgeber = Leistungsuebersicht.GetCellValueAsString("D" + i.ToString());
                temp.Autotyp = Leistungsuebersicht.GetCellValueAsString("E" + i.ToString());
                temp.Fahrer = Leistungsuebersicht.GetCellValueAsString("F" + i.ToString());
                temp.Beladeort = Leistungsuebersicht.GetCellValueAsString("G" + i.ToString());
                temp.Entladeort = Leistungsuebersicht.GetCellValueAsString("H" + i.ToString());
                temp.Preis_Netto = Leistungsuebersicht.GetCellValueAsDouble("I" + i.ToString());
                temp.WarteZeit = Leistungsuebersicht.GetCellValueAsDouble("J" + i.ToString());
                temp.BeEntladezeit = Leistungsuebersicht.GetCellValueAsDouble("K" + i.ToString());
                temp.Rückfracht = Leistungsuebersicht.GetCellValueAsString("L" + i.ToString());
                temp.Maut = Leistungsuebersicht.GetCellValueAsDouble("M" + i.ToString());
                temp.GesamtNetto = Leistungsuebersicht.GetCellValueAsDouble("N" + i.ToString());

                if (string.IsNullOrEmpty(temp.Datum)) 
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

        public List<Fahrer> LoadCollectionFahrer()
        {
            try
            {
                Fahrer = new SLDocument("Fahrer.xlsx");
            }
            catch (System.IO.FileNotFoundException e)
            {
                Fahrer = new SLDocument();
                Fahrer.SaveAs("Fahrer.xlsx");
                Fahrer = new SLDocument("Fahrer.xlsx");
            }
            catch (System.IO.IOException f)
            {
                throw f;
            }

            List<Fahrer> tempList = new List<Fahrer>();

            for (int i = 2; i < 102; i++)
            {
                Fahrer temp = new Fahrer();
                temp.NameVorname = Fahrer.GetCellValueAsString("B" + i.ToString());
                temp.DatumSeit = Fahrer.GetCellValueAsString("C" + i.ToString());
                temp.DatumBis = Fahrer.GetCellValueAsString("D" + i.ToString());
                temp.GebDatum = Fahrer.GetCellValueAsString("E" + i.ToString());
                temp.StundenGesamt = Fahrer.GetCellValueAsDouble("F" + i.ToString());
                temp.StundenAbgerechnet = Fahrer.GetCellValueAsDouble("G" + i.ToString());
                temp.ÜberstundenVormonate = Fahrer.GetCellValueAsDouble("H" + i.ToString());
                temp.ÜberstundenDifferenz = Fahrer.GetCellValueAsDouble("I" + i.ToString());
               

                if (string.IsNullOrEmpty(temp.NameVorname))
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

        public List<Firma> LoadCollectionFirma()
        {
            try
            {
                Firmen = new SLDocument("Firmen.xlsx");
            }
            catch (System.IO.FileNotFoundException e)
            {
                Firmen = new SLDocument();
                Firmen.SaveAs("Firmen.xlsx");
                Firmen = new SLDocument("Firmen.xlsx");
            }
            catch (System.IO.IOException f)
            {
                throw f;
            }

            List<Firma> tempList = new List<Firma>();

            for (int i = 2; i < 102; i++)
            {
                Firma temp = new Firma();
                temp.Name = Firmen.GetCellValueAsString("B" + i.ToString());
                temp.Ansprechpartner = Firmen.GetCellValueAsString("C" + i.ToString());
                temp.StraßeUndNr = Firmen.GetCellValueAsString("D" + i.ToString());
                temp.PLZ = Firmen.GetCellValueAsString("E" + i.ToString());
                temp.Ort = Firmen.GetCellValueAsString("F" + i.ToString());
                temp.Land = Firmen.GetCellValueAsString("G" + i.ToString());
                temp.CurrentRechnungsNr = Firmen.GetCellValueAsInt32("H" + i.ToString());
                temp.CountForCurrentRechnungsNr = Firmen.GetCellValueAsInt32("I" + ToString());
                     
                if (string.IsNullOrEmpty(temp.Name))
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

        public Dictionary<String, List<TourPreis>> LoadHashMapFirmenPreise()
        {
            try
            {
                FirmenPreise = new SLDocument("FirmenPreise.xlsx");
            }
            catch (System.IO.FileNotFoundException e)
            {
                FirmenPreise = new SLDocument();
                FirmenPreise.SaveAs("FirmenPreise.xlsx");
                FirmenPreise = new SLDocument("FirmenPreise.xlsx");
            }
            catch (System.IO.IOException f)
            {
                throw f;
            }

            Dictionary<String, List<TourPreis>> tempHashMap = new Dictionary<String, List<TourPreis>>();

            for (int i = 2; i < 202; i++)
            {               
                String key = FirmenPreise.GetCellValueAsString("B" + i.ToString());

                if (string.IsNullOrEmpty(key))
                    continue;

                if (!tempHashMap.Keys.Contains(key))
                    tempHashMap.Add(key, new List<TourPreis>());
                TourPreis tempItem = new TourPreis(FirmenPreise.GetCellValueAsString("C" + i.ToString()));
                tempItem.Kilometer = FirmenPreise.GetCellValueAsDouble("D" + i.ToString());
                tempItem.AutoTyp1 = FirmenPreise.GetCellValueAsDouble("E" + i.ToString());
                tempItem.AutoTyp2 = FirmenPreise.GetCellValueAsDouble("F" + i.ToString());
                tempItem.AutoTyp3 = FirmenPreise.GetCellValueAsDouble("G" + i.ToString());

                tempHashMap[key].Add(tempItem);
            }

            return tempHashMap;
        }

        #endregion Loading Methods

        #region Storing Methods

        public void StoreCollectionLU(List<LUEntry> collection, String month)
        {
            int i = 2, j = 1;

            Leistungsuebersicht.SelectWorksheet(month);

            Leistungsuebersicht.SetCellValue("A1", "Leistungsübersicht WLG Transporte:  Aufträge - Fahrten 2018");

            // clear excel
            for(int x = 0; x < 100; x++)
            {
                Leistungsuebersicht.SetCellValue("A" + i.ToString(), j);
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

                i++;j++;
            }

            i = 2; j = 1;

            for (int x = 0; x < collection.Count; x++)
            {
                if (x < collection.Count)
                {
                    if(collection.ElementAt(x).ID == 0)
                        Leistungsuebersicht.SetCellValue("A" + i.ToString(), j);
                    else
                        Leistungsuebersicht.SetCellValue("A" + i.ToString(), collection.ElementAt(x).ID);
                    if(collection.ElementAt(x).Datum != null)
                        Leistungsuebersicht.SetCellValue("B" + i.ToString(), collection.ElementAt(x).Datum.ToString());
                    Leistungsuebersicht.SetCellValue("C" + i.ToString(), collection.ElementAt(x).RechnNr);
                    if (collection.ElementAt(x).Auftragsgeber != null)
                        Leistungsuebersicht.SetCellValue("D" + i.ToString(), collection.ElementAt(x).Auftragsgeber.ToString());
                    if (collection.ElementAt(x).Autotyp != null)
                        Leistungsuebersicht.SetCellValue("E" + i.ToString(), collection.ElementAt(x).Autotyp.ToString());
                    if (collection.ElementAt(x).Fahrer != null)
                        Leistungsuebersicht.SetCellValue("F" + i.ToString(), collection.ElementAt(x).Fahrer.ToString());
                    if (collection.ElementAt(x).Beladeort != null)
                        Leistungsuebersicht.SetCellValue("G" + i.ToString(), collection.ElementAt(x).Beladeort.ToString());
                    if (collection.ElementAt(x).Entladeort != null)                   
                        Leistungsuebersicht.SetCellValue("H" + i.ToString(), collection.ElementAt(x).Entladeort.ToString());
                    Leistungsuebersicht.SetCellValue("I" + i.ToString(), collection.ElementAt(x).Preis_Netto);
                    Leistungsuebersicht.SetCellValue("J" + i.ToString(), collection.ElementAt(x).WarteZeit);
                    Leistungsuebersicht.SetCellValue("K" + i.ToString(), collection.ElementAt(x).BeEntladezeit);
                    if (collection.ElementAt(x).Rückfracht != null)
                        Leistungsuebersicht.SetCellValue("L" + i.ToString(), collection.ElementAt(x).Rückfracht.ToString());
                    Leistungsuebersicht.SetCellValue("M" + i.ToString(), collection.ElementAt(x).Maut);
                    Leistungsuebersicht.SetCellValue("N" + i.ToString(), collection.ElementAt(x).GesamtNetto);

                }
                i++;
                j++;
            }

            Leistungsuebersicht.Save();
            Leistungsuebersicht = new SLDocument("Leistungsübersicht.xlsx");
        }

        public void StoreCollectionFahrer(List<Fahrer> collection)
        {
            int i = 2, j = 1;

            Fahrer.SetCellValue("A1", "Fahrer WLG Transporte:  Übersicht 2018");

            // clear excel
            for (int x = 0; x < 100; x++)
            {
                Fahrer.SetCellValue("A" + i.ToString(), j);
                Fahrer.SetCellValue("B" + i.ToString(), "");
                Fahrer.SetCellValue("C" + i.ToString(), "");
                Fahrer.SetCellValue("D" + i.ToString(), "");
                Fahrer.SetCellValue("E" + i.ToString(), "");
                Fahrer.SetCellValue("F" + i.ToString(), "");
                Fahrer.SetCellValue("G" + i.ToString(), "");

                i++; j++;
            }

            i = 2; j = 1;

            for (int x = 0; x < collection.Count; x++)
            {
                if (collection.ElementAt(x).NameVorname != null)
                    Fahrer.SetCellValue("B" + i.ToString(), collection.ElementAt(x).NameVorname);                 
                if (collection.ElementAt(x).DatumSeit != null)
                    Fahrer.SetCellValue("C" + i.ToString(), collection.ElementAt(x).DatumSeit);
                if (collection.ElementAt(x).DatumBis != null)
                    Fahrer.SetCellValue("D" + i.ToString(), collection.ElementAt(x).DatumBis);
                if (collection.ElementAt(x).GebDatum != null)
                    Fahrer.SetCellValue("E" + i.ToString(), collection.ElementAt(x).GebDatum);
                Fahrer.SetCellValue("F" + i.ToString(), collection.ElementAt(x).StundenGesamt);
                Fahrer.SetCellValue("G" + i.ToString(), collection.ElementAt(x).StundenAbgerechnet);
                Fahrer.SetCellValue("H" + i.ToString(), collection.ElementAt(x).ÜberstundenVormonate);
                Fahrer.SetCellValue("I" + i.ToString(), collection.ElementAt(x).ÜberstundenDifferenz);

                i++; j++;
            }

            Fahrer.Save();
            Fahrer = new SLDocument("Fahrer.xlsx");
        }

        public void StoreCollectionFirma(List<Firma> collection)
        {
            int i = 2, j = 1;

            Firmen.SetCellValue("A1", "Firmen WLG Transporte:  Übersicht 2018");

            // clear excel
            for (int x = 0; x < 100; x++)
            {
                Firmen.SetCellValue("A" + i.ToString(), j);
                Firmen.SetCellValue("B" + i.ToString(), "");
                Firmen.SetCellValue("C" + i.ToString(), "");
                Firmen.SetCellValue("D" + i.ToString(), "");
                Firmen.SetCellValue("E" + i.ToString(), "");
                Firmen.SetCellValue("F" + i.ToString(), "");
                Firmen.SetCellValue("G" + i.ToString(), "");
                Firmen.SetCellValue("H" + i.ToString(), "");
                Firmen.SetCellValue("I" + i.ToString(), "");
                Firmen.SetCellValue("J" + i.ToString(), "");


                i++; j++;
            }

            i = 2; 

            for (int x = 0; x < collection.Count; x++)
            {
                if (collection.ElementAt(x).Name != null)
                    Firmen.SetCellValue("B" + i.ToString(), collection.ElementAt(x).Name);
                if (collection.ElementAt(x).Ansprechpartner != null)
                    Firmen.SetCellValue("C" + i.ToString(), collection.ElementAt(x).Ansprechpartner);
                if (collection.ElementAt(x).StraßeUndNr != null)
                    Firmen.SetCellValue("D" + i.ToString(), collection.ElementAt(x).StraßeUndNr);
                if (collection.ElementAt(x).PLZ != null)
                    Firmen.SetCellValue("E" + i.ToString(), collection.ElementAt(x).PLZ);
                if (collection.ElementAt(x).PLZ != null)
                    Firmen.SetCellValue("F" + i.ToString(), collection.ElementAt(x).Ort);
                if (collection.ElementAt(x).PLZ != null)
                    Firmen.SetCellValue("G" + i.ToString(), collection.ElementAt(x).Land);
                Firmen.SetCellValue("H" + i.ToString(), collection.ElementAt(x).CurrentRechnungsNr);
                Firmen.SetCellValue("I" + i.ToString(), collection.ElementAt(x).CountForCurrentRechnungsNr);

                i++; 
            }



            Firmen.Save();
            Firmen = new SLDocument("Firmen.xlsx");

        }

        public void StoreHashMapFirmenPreise(Dictionary<String, List<TourPreis>> hashMap)
        {
            int i = 2, j = 1;

            Firmen.SetCellValue("A1", "Tourpreise WLG Transporte:  Übersicht 2018");

            // clear excel
            for (int x = 0; x < 200; x++)
            {
                Firmen.SetCellValue("A" + i.ToString(), j);
                Firmen.SetCellValue("B" + i.ToString(), "");
                Firmen.SetCellValue("C" + i.ToString(), "");
                Firmen.SetCellValue("D" + i.ToString(), "");
                Firmen.SetCellValue("E" + i.ToString(), "");
                Firmen.SetCellValue("F" + i.ToString(), "");
                Firmen.SetCellValue("G" + i.ToString(), "");

                i++; j++;
            }

            i = 2; 


            foreach(String item in hashMap.Keys)
            {
                foreach(TourPreis tp in hashMap[item])
                {
                    FirmenPreise.SetCellValue("B" + i.ToString(), item);
                    FirmenPreise.SetCellValue("C" + i.ToString(), tp.Tour);
                    FirmenPreise.SetCellValue("D" + i.ToString(), tp.Kilometer);
                    FirmenPreise.SetCellValue("E" + i.ToString(), tp.AutoTyp1);
                    FirmenPreise.SetCellValue("F" + i.ToString(), tp.AutoTyp2);
                    FirmenPreise.SetCellValue("G" + i.ToString(), tp.AutoTyp3);
                    i++;
                }
            }

            FirmenPreise.Save();
            FirmenPreise = new SLDocument("FirmenPreise.xlsx");

        }

        #endregion Storing Methods

        #region Opening Methods

        public void OpenLeistungsuebersicht(String month)
        {
            System.Diagnostics.Process.Start("Leistungsübersicht.xlsx");
        }

        public void CreateAndOpenRechnungFahrer(List<LUEntry> collection)
        {

        }

        public void CreateAndOpenRechnungFirma(List<LUEntry> collection)
        {

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
