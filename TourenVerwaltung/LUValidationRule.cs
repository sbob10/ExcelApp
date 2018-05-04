using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;


namespace TourenVerwaltung
{
    class LUValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            LUEntry entry = (value as BindingGroup).Items[0] as LUEntry;
            if (string.IsNullOrEmpty(entry.Datum))
            {
                return new ValidationResult(false, "Datum darf nicht leer sein!");
            }

            if (string.IsNullOrEmpty(entry.RechnNr))
            {
                return new ValidationResult(false, "Rechnungsnummer darf nicht leer sein!");
            }
           
           return ValidationResult.ValidResult;        
        }
    }
}
