using System;
using System.IO;
using System.Net;
using Gtk;
using PARSminexmr;


namespace PARSminexmr.Initialization
{
    public static class InitIcon 
    {
        
       public static void download_icon()
       {
           
             if (File.Exists("ico/800.png")==false)
             {
                 Directory.CreateDirectory("ico");
                 try
                 {
                     using var c = new WebClient();
                     c.DownloadFile("https://i.ibb.co/QF7YTKm/800.png", "ico/800.png");
                 }
                 catch (Exception e)
                 {
                     Error.icon();
                 } 
               
             }
            
            
       }
       
    }
}