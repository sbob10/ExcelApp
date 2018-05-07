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
        public int RechnNr { get; set; }

        private String _Auftragsgeber;

        public String Auftragsgeber
        {
            get { return _Auftragsgeber; }
            set { SetProperty(ref _Auftragsgeber, value, () => Auftragsgeber); if (OnAuftragsgeberChanged != null) OnAuftragsgeberChanged.Invoke(value, this); }
        }

        public string Autotyp { get; set; }
        public string Fahrer { get; set; }
        public string Beladeort { get; set; }
        public string Entladeort { get; set; }
        public double Preis_Netto { get; set; }
        public double WarteZeit { get; set; }
        public double BeEntladezeit { get; set; }
        public string Rückfracht { get; set; }
        public double Maut { get; set; }
        public double GesamtNetto { get; set; }

        public Func<String, LUEntry, String> OnAuftragsgeberChanged; 

    }
}
