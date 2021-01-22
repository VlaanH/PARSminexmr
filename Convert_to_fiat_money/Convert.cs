using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace PARSminexmr.Convert_to_fiat_money
{
    public static class ConvertToFiat
    {
        public static string Kraken(string quantity = default,string currency1 = default,string currency2 = default)
        {
            string APIresponse = default;
            double multiplyValue = default;
            string parsData = default;
            string roundedMultipleValues = default; 
          
            WebClient wc = new WebClient();

            APIresponse = wc.DownloadString($"https://api.kraken.com/0/public/Ticker?pair="+currency1+currency2);
            try
            {
                string ParsPatern = @":{""a"":\[""([\w \W ]+)\],""b"":\[";
                parsData = Regex.Match(APIresponse, ParsPatern).Groups[1].Value;
                for (int i = 0; i < 2; i++)
                    parsData = Regex.Match(parsData, @"([\w \W ]+)"",""").Groups[1].Value;

                //remove spaces in line
                string withoutSpaces = parsData.Replace(" ", "");
              
                multiplyValue = double.Parse(withoutSpaces, new CultureInfo("en-us")) *
                                double.Parse(quantity, new CultureInfo("en-us"));
            }
            catch (Exception e)
            {
                parsData = "Error";                
            }


            
            if (parsData=="Error") 
            {   //An attempt to change a currency pair
                APIresponse = wc.DownloadString($"https://api.kraken.com/0/public/Ticker?pair="+currency2+currency1);
                try
                {
                    string ParsPatern = @":{""a"":\[""([\w \W ]+)\],""b"":\[";
                    parsData = Regex.Match(APIresponse, ParsPatern).Groups[1].Value;
                    for (int i = 0; i < 2; i++)
                        parsData = Regex.Match(parsData, @"([\w \W ]+)"",""").Groups[1].Value;
                    
                    //remove spaces in line
                    string withoutSpaces = parsData.Replace(" ", "");

                  
                    multiplyValue = double.Parse(quantity, new CultureInfo("en-us")) /
                                        double.Parse(withoutSpaces, new CultureInfo("en-us"));
                    
                }
                catch (Exception e)
                {
                    if (multiplyValue == 0)
                        return "Error";      
                }

            }
              
            //rounding to 8 decimal places
            roundedMultipleValues = Math.Round(multiplyValue, 8).ToString("0.##########");
           
            
            return roundedMultipleValues + " " + currency2.ToUpper();
        }





        public static string CalcDotRu(string quantity=default,string currency1 = default,string currency2 = default,bool parsCurrency=default)
        {
            string roundedMultipleValues = default;
            
            if(parsCurrency==true)
                currency2 = Regex.Match(currency2, @"([\w \W A-Z 0-9]+):").Groups[1].Value;
            
            
            if (currency2==default||quantity==default)
            {
                return "Error";
            }
         
            
            String response = default;
            try
            {
                WebClient wc = new WebClient();

                response = wc.DownloadString($"https://www.calc.ru/kurs-{currency1.ToUpper()}-{currency2.ToUpper()}.html?text_quantity="+quantity);
            }
            catch (Exception)
            {
                return "Error";
            }


            response = Regex.Match(response, @" <b>([0-9 \.]+) ").Groups[1].Value;
            if (response=="0")
            {
                return "Error";
            }
            
           
            
           
            
            //remove spaces in line
            string withoutSpaces = response.Replace(" ", "");

            //rounding to 8 decimal places
            roundedMultipleValues = Math.Round(double.Parse(withoutSpaces,new CultureInfo("en-us")), 8).ToString("0.##########");
            
            return roundedMultipleValues+ " " + currency2.ToUpper();
      
        }
    }
}