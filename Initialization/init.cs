using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PARSminexmr.Initialization
{
    public class init
    {
       public static InitData SettingsFileRead()
        {
            InitData initD =new InitData();
            if (File.Exists("Settings.txt")==true)
            {
                string file = File.ReadAllText("Settings.txt");
                
                initD.Currency = Regex.Match(file, @"<Currency>([\w \W 0-9]+)</Currency>").Groups[1].Value;
                
                initD.Address = Regex.Match(file, @"<Address>([\w \W 0-9]+)</Address>").Groups[1].Value;
                
                return initD;
            }
            else
            {
                File.Create("Settings.txt");
                return initD;
            }
            
        }
        
    }
}