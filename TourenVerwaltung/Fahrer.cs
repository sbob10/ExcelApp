using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourenVerwaltung
{
    public class Fahrer : BindableBase
    {

        public string NameVorname { get; set; }
        public string DatumSeit { get; set; }
        public string DatumBis { get; set; }
        public string GebDatum { get; set; }
        public double StundenGesamt { get; set; }
        public double StundenAbgerechnet { get; set; }
        public double ÜberstundenVormonate { get; set; }
        public double ÜberstundenDifferenz { get; set; }


        public override string ToString()
        {
            return NameVorname;
        }
    }
}
