using System;
using System.Net;
using System.Text.RegularExpressions;

namespace PARSminexmr.Parsaddres
{
    public static class ParsPool
    {
        public static double Pars(string address=default)
        {
            
            String Response = default;

            address = Regex.Match(address, @":([\w \W A-Z 0-9]+)").Groups[1].Value;
            
            try
            {
                WebClient wc2 = new System.Net.WebClient();
                Response = wc2.DownloadString($"http://api.minexmr.com/stats_address?address={address}");
            }
            catch (Exception )
            {

            }

            
            return double.Parse(Regex.Match(Response, @"balance"":""([0-9]+)"",").Groups[1].Value);
        }
    }
}