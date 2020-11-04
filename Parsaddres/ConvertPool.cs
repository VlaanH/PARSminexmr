namespace PARSminexmr.Parsaddres
{
    public static class ConvertPool
    {
        public static string Convert(double doubleP)
        {
            string stringP=default;
            if (doubleP <= 9999)
            {
                stringP = "0.000000000" + doubleP;
            }
            else if (doubleP <= 99999)
            {
                stringP = "0.00000000" + doubleP;
            }
            else  if (doubleP <= 999999)
            {
                stringP = "0.0000000" + doubleP;
            }
            else if (doubleP <= 9999999)
            {
                stringP = "0.000000" + doubleP;
            }
            else if (doubleP <= 99999999)
            {
                stringP = "0.00000" + doubleP;
            }
            else if (doubleP <= 999999999)
            {
                stringP = "0.0000" + doubleP;
            }
            else  if (doubleP <= 999999999)
            {
                stringP = "0.000" + doubleP;
            }
            else  if (doubleP <= 9999999999)
            {
                stringP = "0.00" + doubleP;
            }
            else if (doubleP <= 99999999999)
            {
                stringP = "0.0" + doubleP;
            }
            else if (doubleP <= 999999999999)
            {
                stringP = "0." + doubleP;
            }


            return stringP;
            
        }
    }
}