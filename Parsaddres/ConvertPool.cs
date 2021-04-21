using System;

namespace PARSminexmr.Parsaddres
{
    public static class ConvertPool
    {
        public static string Convert_(double doubleP)
        {
            string stringP = default;

            switch (doubleP)
            {   
                     //1
                    case <= 999:
                        
                        stringP = "0.000000000" + doubleP;
                        break;
                    //2
                    case  <= 9999:
                        
                        stringP = "0.00000000"+ doubleP;
                        break;
                    //3   
                    case  <= 99999: 
                            
                        stringP = "0.0000000"+ doubleP;
                        break; 
                    //4
                    case  <= 999999:
                        
                        stringP = "0.000000" + doubleP;
                        break;
                    //5
                    case  <= 9999999:
                        
                        stringP = "0.00000" + doubleP;
                        break; 
                    //6
                    case  <= 99999999:
                        
                        stringP = "0.0000" + doubleP;
                        break; 
                    //7
                    case  <= 999999999:
                        
                        stringP = "0.000" + doubleP; 
                        break;
                    //8
                    case  <= 9999999999:
                        
                        stringP = "0.00" + doubleP; 
                        break;
                    //9 
                    case  <= 99999999999:
                        
                        stringP = "0.0" + doubleP; 
                        break;
                    case  <= 999999999999:
                        
                        stringP = "0." + doubleP; 
                        break;
            }
                
            return stringP;
            
        }
    }
}