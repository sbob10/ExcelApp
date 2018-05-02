using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourenVerwaltung
{
    public class LUEntry : BindableBase
    {

        public int ID { get; set; }
        public string Datum { get; set; }
        public string RechnNr { get; set; }
        public string Auftragsgeber { get; set; }
        public string Autotyp { get; set; }
        public string Fahrer { get; set; }
        public string Beladeort { get; set; }
        public string Entladeort { get; set; }
        public double Preis_Netto { get; set; }
        public double WarteZeit { get; set; }
        public double BeEntladezeit { get; set; }
        public double Rückfracht { get; set; }
        public double Maut { get; set; }
        public double GesamtNetto { get; set; }
    }
}
