using System;
using System.IO;
using GLib;
using Gtk;
using System.Threading;
using System.Threading.Tasks;
using UI = Gtk.Builder.ObjectAttribute;
using PARSminexmr.Parsaddres;
using PARSminexmr.Convert_to_fiat_money;
using Application = Gtk.Application;
using DateTime = System.DateTime;
using Thread = System.Threading.Thread;
using PARSminexmr.Data;
using Task = System.Threading.Tasks.Task;
using  PARSminexmr.Initialization;



namespace PARSminexmr
{
    class MainWindow : Window
    {

        
        [UI] private Label label = null;
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

        async private void Button1_Clicked(object sender, EventArgs a)
        {
            PARS_ALL_data allData = new PARS_ALL_data();

            
            await Task.Run(()  => {
          
                allData.Datetime = DateTime.Now.ToString();
                allData.XMR = ConvertPool.Convert(ParsPool.Pars(Entry.Text));
                allData.fiat =  Convert_to_fiat.Currency(allData.XMR,Hdata.initD.Currency);
                
                label.Text = allData.XMR + "|" + allData.fiat + "|" + allData.Datetime;

                Gtk.Application.Invoke(delegate
                {
                textView.Buffer.Text += label.Text+"\n";
                File.WriteAllText("History.txt",textView.Buffer.Text);
                });
               
                
            });

            
            




        }
    }

   
}
