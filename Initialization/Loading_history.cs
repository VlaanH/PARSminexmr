using System.IO;

namespace PARSminexmr.Initialization
{
    public static class Loading_history
    {
        

       public static string Loading()
       {
           string History = default;
           
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