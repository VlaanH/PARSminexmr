using System;
using System.IO;
using GLib;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using PARSminexmr.Parsaddres;
using PARSminexmr.Convert_to_fiat_money;
using Application = Gtk.Application;
using DateTime = System.DateTime;
using PARSminexmr.Data;
using Task = System.Threading.Tasks.Task;
using PARSminexmr.Initialization;
namespace PARSminexmr
{
    class MainWindow : Window
    {

        
        [UI] private Label label = null;
        [UI] private ProgressBar Progres = null;
        [UI] private Button button = null;
        [UI] private Entry Entry = null;
        [UI] private TextView textView = null;
        
      

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {

            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            button.Clicked += Button1_Clicked;
            //Start_Initialization
            initStart();
        }

        
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

       
       async private void initStart()
        {
            await Task.Run(()  =>
            {
                Progres.Visible = false;
                Gtk.Application.Invoke(delegate
                {
                    textView.Buffer.Text = Loading_history.Loading();
                });
                
                Hdata.initD = init.SettingsFileRead();
                Entry.Text = Hdata.initD.Address;
                
            });
        }

       private bool protection = false;
        async private void Button1_Clicked(object sender, EventArgs a)
        {
            if (protection==false)
            {   
                
                Progres.Visible = true;
                protection = true;
                Progres.Fraction = default;
                
                PARS_ALL_data allData = new PARS_ALL_data();
                await Task.Run(()  =>
                {
                 
                    
                
             
                 
                    //Getting the current date and time
                    allData.Datetime = DateTime.Now.ToString();
                    
                 
                  
                    
                 
                    //Convert the pool integer to fractional
                    allData.XMR = ConvertPool.Convert(ParsPool.Pars(Entry.Text));
                    
                   
                   
                    //Convert XMR to fiat
                    Progres.Fraction += 0.5;
                    allData.fiat =  Convert_to_fiat.Currency(allData.XMR,Hdata.initD.Currency);
                
             
                    label.Text =  allData.fiat + " | " +allData.XMR  + " XMR | " + allData.Datetime;

                    Gtk.Application.Invoke(delegate
                    {
                       
                        textView.Buffer.Text += label.Text+"\n";
                      
                        File.WriteAllText("History.txt",textView.Buffer.Text);
                        Progres.Fraction += 1;
                    });
                    protection = false;
                  



                });
            }    
            
            

            
        }
        
        
        
        
        
        
    }
}
