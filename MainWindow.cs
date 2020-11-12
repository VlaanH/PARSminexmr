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
using PARSminexmr.Settings;

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

       
       private async void initStart()
        {
            await Task.Run(()  =>
            {
                Progres.Visible = false;
                
                Gtk.Application.Invoke(delegate
                {
                    textView.Buffer.Text = Loading_history.Loading();
                });
                
                Hdata.initD = init.SettingsFileRead();

                
                if (Hdata.initD.Currency!=""&Hdata.initD.Address!="")
                {
                    Entry.Text = Hdata.initD.Currency+":"+Hdata.initD.Address;
                }
                
                
            });
        }

       private bool _Protection = false;
        private async void Button1_Clicked(object sender, EventArgs a)
        {
            if (Entry.Text=="")
            {
                Error.Entry();
            }
            else if  (_Protection==false)
            {   
                
                Progres.Visible = true;
                _Protection = true;
                Progres.Fraction = default;
                
                PARS_ALL_data allData = new PARS_ALL_data();
                await Task.Run(()  =>
                {
                 
                    //Getting the current date and time
                    allData.Datetime = DateTime.Now.ToString();
                    
                 
                  
                    
                 
                    //Convert the pool integer to fractional
                    allData.XMR = ConvertPool.Convert(ParsPool.Pars(Entry.Text));
                    
                   
                   
                    
                    Progres.Fraction = 0.5;
                    //Convert XMR to fiat
                    allData.fiat =  Convert_to_fiat.Currency(allData.XMR,Entry.Text);


                    if (allData.fiat!="Error")
                    {
                        label.Text =  allData.fiat + " | " +allData.XMR  + " XMR | " + allData.Datetime;
                        
                        Gtk.Application.Invoke(delegate
                        {
                       
                            textView.Buffer.Text += label.Text+"\n";
                      
                            File.WriteAllText("History.txt",textView.Buffer.Text);
                            Progres.Fraction = 1;
                        });
                        //saving settings to file
                        _Settings.Save(Entry.Text);
                    }
                    else
                    {
                        Error.Entry();
                        Progres.Visible = false;
                        Progres.Fraction = default;
                    }


                    _Protection = false;
                  
                    

                });
            }    
            
            

            
        }
        
        
        
        
        
        
    }
}
