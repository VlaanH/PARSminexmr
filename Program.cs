using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using Application = Gtk.Application;
using PARSminexmr.Initialization;
using Gtk;


namespace PARSminexmr
{
     class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            //Download icon
            InitIcon.download_icon();

            var app = new Application("org.PARSminexmr.PARSminexmr", GLib.ApplicationFlags.None);

            
            
            
            Choose_style.Start();
                    
                    
                    
            app.Register(GLib.Cancellable.Current);
      
     
            
            var win = new MainWindow();
            app.AddWindow(win);

            
           
           
            
            win.Show();
            Application.Run();
            
        }
    }
}
