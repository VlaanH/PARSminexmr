using System;
using Application = Gtk.Application;
using PARSminexmr.Initialization;

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

       
                
            Choose_style.PreStartChoose();
                    
                    
                    
            app.Register(GLib.Cancellable.Current);
      
     
            
            var win = new MainWindow();
            app.AddWindow(win);

            
           
           
            
            win.Show();
            Application.Run();
            
        }
    }
}
