using System.IO;
using System.Text.RegularExpressions;

namespace PARSminexmr.Settings
{
    public static class _Settings
    {
        public static void Save(string address)
        {
            
            string Currency = Regex.Match(address, @"([\w \W A-Z 0-9]+):").Groups[1].Value;
            
            string only_address = Regex.Match(address, @":([\w \W A-Z 0-9]+)").Groups[1].Value;
            
            string Full_Settings = "<Address>" + only_address + "</Address>" + "\n" + "<Currency>" + Currency + "</Currency>";
            

            File.WriteAllText("Settings.txt",Full_Settings);
        }
    }
}