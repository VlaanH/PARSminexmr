using System;
using System.Text.RegularExpressions;

namespace PARSminexmr.Convert_to_fiat_money
{
    public static class Convert_to_fiat
    {
       public static string Currency(string parsaddres=default,string Currency=default)
        {

            String Response = default;
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();

                Response = wc.DownloadString($"https://www.calc.ru/kurs-XMR-{Currency}.html?text_quantity=" + parsaddres);
            }
            catch (Exception)
            {
                Response = "er";
                
            }


            Response = Regex.Match(Response, @" <b>([0-9 \.]+) ").Groups[1].Value;
            
            return Response+ " " + Currency;
        }
    }
}