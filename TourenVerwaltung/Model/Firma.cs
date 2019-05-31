using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourenVerwaltung
{
    public class Firma : BindableBase
    {
        public string Name{ get; set; }
        public string Ansprechpartner { get; set; }
        public string StraßeUndNr { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Land { get; set; }

        public int CurrentRechnungsNr { get; set; }
        public int CountForCurrentRechnungsNr { get; set; }
       

        public override string ToString()
        {
            return Name;
        }
    }
}
