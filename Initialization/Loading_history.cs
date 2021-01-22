using System.IO;

namespace PARSminexmr.Initialization
{
    public static class LoadingHistory
    {
       static string _history = default;
       public static string Loading()
       {
           
           
            if (File.Exists("History.txt"))
            {
                
                _history= File.ReadAllText("History.txt");
                
                return _history;
                
            }
       
            
            
            return "";
            
       }

    }
}