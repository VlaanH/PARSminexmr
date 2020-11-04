using System;
using System.Net;
using System.Text.RegularExpressions;

namespace PARSminexmr.Parsaddres
{
    public static class ParsPool
    {
        public static double Pars(string addres)
        {
            String Response = default;


            try
            {
                WebClient wc2 = new System.Net.WebClient();
                Response = wc2.DownloadString($"http://api.minexmr.com/stats_address?address={addres}");
            }
            catch (Exception )
            {

            }

      
     
       
   
        
            double.TryParse(Regex.Match(Response, @"balance"":""([0-9]+)"",").Groups[1].Value, out double pp);
            return pp;
        }
    }
}