using System.Text.RegularExpressions;
using Gtk;

namespace PARSminexmr
{
    public static class DConversion
    {
        public static string GetQuantity (string entry)
        {
            return Regex.Match(entry, @"([0-9 \,\.]+):").Groups[1].Value;
        }


        public static string GetCurrency1(string entry)
        {
            
            return Regex.Match(entry, @":([A-Z a-z]+):").Groups[1].Value;
        }
        
        public static string GetCurrency2(string entry)
        {
            
            return Regex.Match(entry, Regex.Match(entry, @":([A-Z a-z]+):").Groups[1].Value+@":([A-Z a-z]+)").Groups[1].Value;
        }


    }
}