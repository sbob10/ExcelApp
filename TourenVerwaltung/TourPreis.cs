using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourenVerwaltung
{
    public class TourPreis
    {

        public TourPreis(String tour)
        {
            Tour = tour;
            Kilometer = 0;
            AutoTyp1 = 0;
            AutoTyp2 = 0;
            AutoTyp3 = 0;           
        }

        public string Tour { get; set; }
        public double Kilometer { get; set; }
        public double AutoTyp1 { get; set; }
        public double AutoTyp2 { get; set; }
        public double AutoTyp3 { get; set; }

    }
}
