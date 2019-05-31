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
        public double RechnNr { get; set; }

        private String _Auftragsgeber;

        public String Auftragsgeber
        {
            get { return _Auftragsgeber; }
            set
            {
                SetProperty(ref _Auftragsgeber, value, () => Auftragsgeber);
                if (OnAuftragsgeberChanged != null)
                    OnAuftragsgeberChanged.Invoke(value, this);            
            }
        }

        public Autotyp Autotyp { get; set; }
        public string Fahrer { get; set; }


        private String _Beladeort;

        public String Beladeort
        {
            get { return _Beladeort; }
            set
            {
                SetProperty(ref _Beladeort, value, () => Beladeort);               
            }
        }

        private String _Entladeort;

        public String Entladeort
        {
            get { return _Entladeort; }
            set
            {
                SetProperty(ref _Entladeort, value, () => Entladeort);               
            }
        }

        public double Preis_Netto { get; set; }
        public double WarteZeit { get; set; }
        public double BeEntladezeit { get; set; }
        public double Rückfracht { get; set; }
        public double Maut { get; set; }
        public double GesamtNetto { get; set; }

        public Func<String, LUEntry, String> OnAuftragsgeberChanged;

    }
}
