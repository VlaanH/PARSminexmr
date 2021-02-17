using System;
using System.Net;
using System.Text.RegularExpressions;

namespace PARSminexmr.Parsaddres
{
    public static class ParsPool
    {
        public static double Pars(string address=default)
        {
            
            String response = default;
            double pars = default;
            address = Regex.Match(address, @":([\w \W A-Z 0-9]+)").Groups[1].Value;
            
            try
            {
                WebClient wc2 = new System.Net.WebClient();
                response = wc2.DownloadString($"http://api.minexmr.com/stats_address?address={address}");
                pars = double.Parse(Regex.Match(response, @"balance"":""([0-9]+)"",").Groups[1].Value);
            }
            catch (Exception)
            {
                // ignored
            }


            if (pars!=default)
            {
                return pars;  
            }
            else
            {
                return 0; 
            }
            
          
            
       
            
            
            
            
        }
    }
}