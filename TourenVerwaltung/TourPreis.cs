using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TourenVerwaltung
{
    public class TourPreis
    {
            public TourPreis(String tour)
            {
                Tour = tour;
                Kilometer = 0;
                Caddy = 0;
                PKW = 0;
                Bus = 0;
            }

            public string Tour { get; set; }
            public double Kilometer { get; set; }
            public double Caddy { get; set; }
            public double PKW { get; set; }
            public double Bus { get; set; }

        }
}