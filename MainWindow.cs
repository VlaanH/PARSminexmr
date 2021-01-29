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
using Init = PARSminexmr.Initialization.Init;

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
        [UI] private Button ConversionButton = null;
        [UI] private Gtk.Dialog DialogConversion = null;
        [UI] private Button CloseDialogConversion = null;
       
        [UI] private Button ConvertDialogButton = null;
        [UI] private Label ConvertLabel = null;
        [UI] private Entry ConvertEntry = null;
        [UI] private CheckButton CheckKraken = null;
        [UI] private CheckButton CheckCalcDotRu = null;



        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {

            builder.Autoconnect(this);
    
            DeleteEvent += Window_DeleteEvent;
            button.Clicked += Button1_Clicked;
            theme.Clicked += theme_Clicked;
            ConversionButton.Clicked += Conversion_Clicked;
            CloseDialogConversion.Clicked += CloseDialogConversion_Clicked;
            ConvertDialogButton.Clicked += ConvertDialogButton_Clicked;
            CheckKraken.Clicked += CheckKraken_Clicked;
            CheckCalcDotRu.Clicked += CheckCalcDotRu_Clicked;
            
            //Start Initialization
            InitStart();
        }

        
        private void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            Application.Quit();
        }
        
        private void CheckKraken_Clicked(object sender, EventArgs a)
        {
            if (CheckCalcDotRu.Active == true)
                CheckCalcDotRu.Active = false;
        }
        private void CheckCalcDotRu_Clicked(object sender, EventArgs a){
           
            if (CheckKraken.Active == true)
                CheckKraken.Active = false;
            
        }
        
        
        
        private void theme_Clicked(object sender, EventArgs a){
            Choose_style.ButtonChooseStyle();
        }

        private void CloseDialogConversion_Clicked(object sender, EventArgs a) {
            DialogConversion.HideOnDelete();
        }

        private void ConvertDialogButton_Clicked(object sender, EventArgs a)
        {
            string quantity = dialogConversion.GetQuantity(ConvertEntry.Text);
            string currency1 = dialogConversion.GetCurrency1(ConvertEntry.Text);
            string currency2 = dialogConversion.GetCurrency2(ConvertEntry.Text);


            if (CheckKraken.Active == true)
            {
                string responseToRequest = ConvertToFiat.Kraken(quantity, currency1, currency2);

                if (responseToRequest != "Error")
                    ConvertLabel.Text = quantity + currency1 + " = " + responseToRequest;
                else
                { 
                    Gtk.Application.Invoke(delegate
                    {
                        ConvertLabel.Text = responseToRequest;
                    });
                   
                }
            }

           

            if (CheckCalcDotRu.Active == true)
            {
                string responseToRequest = ConvertToFiat.CalcDotRu(quantity, currency1, currency2, false);
                
                if (responseToRequest != "Error")
                    ConvertLabel.Text = quantity + currency1 + " = " + responseToRequest;
                else
                { 
                    Gtk.Application.Invoke(delegate
                    {
                        ConvertLabel.Text = responseToRequest;
                    });
                   
                }
            }
               
           
        }

        

        private async void InitStart()
        {
            await Task.Run(()  =>
            {

                InitData initData = new InitData();
                    
                Progres.Visible = false;
                
                Gtk.Application.Invoke(delegate
                {
                    textView.Buffer.Text = LoadingHistory.Loading();
                });
                
                initData = Init.SettingsFileRead();


               
                Entry.Text = initData.Currency + initData.Address;
              
                
            });
        }



        private void Conversion_Clicked(object sender, EventArgs a) {
            DialogConversion.Run();
           
        }
        
        
       
     





        private bool _doubleClickProtection = false;
        private async void Button1_Clicked(object sender, EventArgs a)
        {
            if (Entry.Text=="")
            {
                Error.Entry();
            }
            else if  (_doubleClickProtection==false)
            {   
                
                Progres.Visible = true;
                _doubleClickProtection = true;
                Progres.Fraction = default;
                
                ParsAllData allData = new ParsAllData();
                
                await Task.Run(()  =>
                {
                 
                    //Getting the current date and time
                    allData.Datetime = DateTime.Now.ToString();
                    
                 
                  
                    
                 
                    //Convert the pool integer to fractional
                    allData.XMR = ConvertPool.Convert(ParsPool.Pars(Entry.Text));
                    
                   
                   
                    
                    Progres.Fraction = 0.5;
                    //Convert XMR to fiat
                    allData.Fiat =  ConvertToFiat.CalcDotRu(allData.XMR,"XMR",Entry.Text,true);
                
                    

                    if (allData.Fiat!="Error")
                    {
                        label.Text =  allData.Fiat + " | " +allData.XMR  + " XMR | " + allData.Datetime;
                        
                        Gtk.Application.Invoke(delegate
                        {
                       
                            textView.Buffer.Text += label.Text+"\n";

                            File.WriteAllText("History.txt",textView.Buffer.Text);
                           
                            Progres.Fraction = 1;
                            
                        });

                  
                        //saving settings to file
                        Settings.Settings.Save(Entry.Text);
                        
                    }
                    else
                    {
                        Error.Entry();

                        label.Text = "Error";
                        Progres.Fraction = 1;
                    }


                    _doubleClickProtection = false;
                  
                    

                });
                
             
                
             
                
                
                
                
            }    
            
            

            
        }
        
        
        
        
        
        
    }
}
