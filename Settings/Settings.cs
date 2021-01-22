using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PARSminexmr.Settings
{
    public static class Settings
    {
        public static void Save(string address)
        {
            
            string currency = Regex.Match(address, @"([\w \W A-Z 0-9]+):").Groups[1].Value;
            
            string onlyAddress = Regex.Match(address, @":([\w \W A-Z 0-9]+)").Groups[1].Value;
            
            string fullSettings = "<Address>" + onlyAddress + "</Address>" + "\n" + "<Currency>" + currency + "</Currency>";

            try
            {
                File.WriteAllText("Settings.txt",fullSettings);
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}