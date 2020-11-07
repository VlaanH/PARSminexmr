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
        [UI] private Label label2 = null;
        [UI] private Button button = null;
        [UI] private Entry Entry = null;
        [UI] private TextView textView = null;
        
      
        private int _counter;

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
            
                Gtk.Application.Invoke(delegate
                {
                    textView.Buffer.Text = Loading_history.Loading();
                });
                
                Hdata.initD = init.SettingsFileRead();
                Entry.Text = Hdata.initD.Address;
                
            });
        }

       private bool protection = true;
        async private void Button1_Clicked(object sender, EventArgs a)
        {
            if (protection==true)
            { 
                protection = false;
                PARS_ALL_data allData = new PARS_ALL_data();
                await Task.Run(()  =>
                {

                    label2.Text = ".";
                    //Getting the current date and time
                    allData.Datetime = DateTime.Now.ToString();
                    
                    label2.Text += ".";
                    //Convert the pool integer to fractional
                    allData.XMR = ConvertPool.Convert(ParsPool.Pars(Entry.Text));
                    
                    label2.Text += ".";
                    //Convert XMR to fiat
                    allData.fiat =  Convert_to_fiat.Currency(allData.XMR,Hdata.initD.Currency);
                
                    label.Text =  allData.fiat + " | " +allData.XMR  + " XMR | " + allData.Datetime;

                    Gtk.Application.Invoke(delegate
                    {
                        label2.Text += ".";
                        textView.Buffer.Text += label.Text+"\n";
                   
                        File.WriteAllText("History.txt",textView.Buffer.Text);

                    });
                    protection = true;

                });
            }    
            
            

            
        }
        
        
        
        
        
        
    }
}
