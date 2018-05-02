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

    }
}
