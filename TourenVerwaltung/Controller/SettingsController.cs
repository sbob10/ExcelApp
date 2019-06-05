using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourenVerwaltung.Controller
{

    public static class SettingsController
    {
        private static string _SettingsPath                     = System.Reflection.Assembly.GetExecutingAssembly().CodeBase + "\\Settings" + "\\";

        private static string _SettingsFileNameFahrer           = "Fahrer.json";
        private static string _SettingsFileNameFirmen           = "Firmen.json";
        private static string _SettingsFileNameTourPreise       = "TourPreise.json";
        private static string _SettingsFileNameGeneralSettings  = "GeneralSettings.json";

        //Cell Definitions

        //Celltype - Rechnung
        public static Tuple<int, string> _CellTypeRechnungListBegining;                 
        public static Tuple<int, string> _CellTypeRechnungListEnding;
        public static Tuple<int, string> _CellTypeRechnungFirmenName;
        public static Tuple<int, string> _CellTypeRechnungAdresseStraße;
        public static Tuple<int, string> _CellTypeRechnungAdressePLZOrt;
        public static Tuple<int, string> _CellTypeRechnungAnsprechPartner;


        //Celltype - LU
        public static Tuple<int, string> _CellTypeLUListBegining;
        public static Tuple<int, string> _CellTypeLUListEnding;

        //Celltype - LN
        public static Tuple<int, string> _CellTypeLNListBegining;
        public static Tuple<int, string> _CellTypeLNListEnding;



        public static List<Fahrer> LoadFahrerList()
        {

            return new List<Fahrer>();                              
        }

        public static void StoreFahrerList(List<Fahrer> fahrerList)
        {

        }

        public static List<Firma> LoadFirmenList()
        {

            return new List<Firma>();
        }

        public static void StoreFirmenList(List<Firma> firmenList)
        {

        }

        public static List<TourPreis> LoadTourPreisList()
        {

            return new List<TourPreis>();
        }

        public static void StoreTourPreisList(List<TourPreis> tourPreisList)
        {

        }


    }
}
