using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PARSminexmr.Initialization;

namespace PARSminexmr
{
   
    public static class Settings
    {
        public static async void Save(string address)
        {
            
            string currency = Regex.Match(address, @"([\w \W A-Z 0-9]+):").Groups[1].Value;
            
            string onlyAddress = Regex.Match(address, @":([\w \W A-Z 0-9]+)").Groups[1].Value;

            InitData initData = new InitData() {Address = onlyAddress, Currency = currency+":"};
            
            using (FileStream fs = new FileStream("settings.json",FileMode.OpenOrCreate))
            {
                //cleaning
                fs.SetLength(default);
                
               await JsonSerializer.SerializeAsync<InitData>(fs, initData);
            }
           
            
        }
        
        public static async Task<InitData> SettingsFileRead()
        {

            using(FileStream fs = new FileStream("settings.json",FileMode.OpenOrCreate))
            {

                try
                {
                    return await JsonSerializer.DeserializeAsync<InitData>(fs);
                }
                catch (Exception e)
                {
                    // ignored
                }
            }


            return null;
        }
        
    }
}