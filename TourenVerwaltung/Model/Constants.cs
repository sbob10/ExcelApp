using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourenVerwaltung
{
    static class Constants
    {
        public static readonly List<String> Months = new List<String>(new String[] { "Januar", "Februar", "März", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Dezember" });
        public static readonly List<String> Years = new List<String>(new String[] {"2018", "2019", "2020"});

        public static String getCurrentDateMonth()
        {
            int temp = DateTime.Now.Month;

            switch (temp)
            {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "März";
                case 4:
                    return "April";
                case 5:
                    return "Mai";
                case 6:
                    return "Juni";
                case 7:
                    return "Juli";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "Oktober";
                case 11:
                    return "November";
                case 12:
                    return "Dezember";
                default:
                    return "Januar";
            }
        }

        public static String getCurrentDateYear()
        {
            int temp = DateTime.Now.Year;

            switch (temp)
            {
                case 2018:
                    return "2018";
                case 2019:
                    return "2019";
                case 2020:
                    return "2020";           
                default:
                    return "2020";
            }
        }

        public static String getStringOfAutotyp(Autotyp typ)
        {
            switch (typ)
            {
                case Autotyp.Bus:
                    return "Bus";
                case Autotyp.Caddy:
                    return "Caddy";
                case Autotyp.PKW:
                    return "PKW";
                default:
                    return "";
            }
        }

        public static Autotyp getAutotypOfString(String typ)
        {
            switch (typ)
            {
                case "Bus":
                    return Autotyp.Bus;
                case "Caddy":
                    return Autotyp.Caddy;
                case "PKW":
                    return Autotyp.PKW;
                default:
                    return Autotyp.Bus;
            }
        }

    }
}
