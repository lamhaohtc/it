using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Managing_Teacher_Work.Helpers
{
    public class NumberHelper
    {
        public static string GetFormattedNumber(decimal? number, string format, string cultureName = "")
        {
            try
            {
                cultureName = (String.IsNullOrEmpty(cultureName)) ? "en-US" : cultureName;
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(cultureName);


                string numberFormatted = String.Format(culture, format, number.Value);
                return numberFormatted;
            }
            catch
            {
                return number.ToString();
            }
        }
    }
}