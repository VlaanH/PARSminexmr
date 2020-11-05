using System.IO;

namespace PARSminexmr.Initialization
{
    public static class Loading_history
    {
       static string History = default;
       public static string Loading()
       {
           
           
            if (File.Exists("History.txt"))
            {
                History= File.ReadAllText("History.txt");
               
                
                return History;
            }
            else
            {
                File.Create("History.txt");
                return default;
            }
            
            
            
       }

    }
}