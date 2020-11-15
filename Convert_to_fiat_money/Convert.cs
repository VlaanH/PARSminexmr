using System;
using System.Text.RegularExpressions;

namespace PARSminexmr.Convert_to_fiat_money
{
    public static class Convert_to_fiat
    {
       public static string Currency(string Parsaddres=default,string Currency=default)
        {

            if (Currency==default||Parsaddres==default)
            {
                return "Error";
            }
            Currency = Regex.Match(Currency, @"([\w \W A-Z 0-9]+):").Groups[1].Value;
            
            String Response = default;
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();

                Response = wc.DownloadString($"https://www.calc.ru/kurs-XMR-{Currency}.html?text_quantity="+Parsaddres);
            }
            catch (Exception)
            {
                return "Error";
            }


            Response = Regex.Match(Response, @" <b>([0-9 \.]+) ").Groups[1].Value;
            if (Response=="0")
            {
                return "Error";
            }
            
            
            return Response+ " " + Currency;
        }
    }
}