using System;
using System.IO;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using PARSminexmr.Parsaddres;
using PARSminexmr.Convert_to_fiat_money;
using Application = Gtk.Application;
using DateTime = System.DateTime;
using Task = System.Threading.Tasks.Task;
using PARSminexmr.Initialization;
using PARSminexmr.Data;
using PARSminexmr.Settings;

namespace PARSminexmr
{
    class MainWindow : Window
    {

        
        [UI] private Label label = null;
        [UI] private ProgressBar Progres = null;
        [UI] private Button theme = null;
        [UI] private Button button = null;
        [UI] private Entry Entry = null;
        [UI] private TextView textView = null;
        
      

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {

            builder.Autoconnect(this);

            DeleteEvent += Window_DeleteEvent;
            button.Clicked += Button1_Clicked;
            theme.Clicked += theme_Clicked;            
        
            //Start Initialization
            InitStart();
        }

        
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }



        private async void theme_Clicked(object sender, EventArgs a)
        {
            Choose_style.ButtonChooseStyle();
        }






        private async void InitStart()
        {
            await Task.Run(()  =>
            {

                InitData initData = new InitData();
                    
                Progres.Visible = false;
                
                Gtk.Application.Invoke(delegate
                {
                    textView.Buffer.Text = Loading_history.Loading();
                });
                
                initData = init.SettingsFileRead();


               
                Entry.Text = initData.Currency + initData.Address;
              
                
            });
        }

       private bool _Double_click_protection = false;
        private async void Button1_Clicked(object sender, EventArgs a)
        {
            if (Entry.Text=="")
            {
                Error.Entry();
            }
            else if  (_Double_click_protection==false)
            {   
                
                Progres.Visible = true;
                _Double_click_protection = true;
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

                        label.Text = "Error";
                        Progres.Fraction = 1;
                    }


                    _Double_click_protection = false;
                  
                    

                });
                
             
                
             
                
                
                
                
            }    
            
            

            
        }
        
        
        
        
        
        
    }
}
