using System.IO;
using System.Threading;
using Gtk;

namespace PARSminexmr
{
    public class Choose_style
    {
        public static void ButtonChooseStyle()
        {
            
            string NOxml = default;
            string Style_path = default;
           
            Style_path = Dialog.DialogFile();

         
            
            
            if (Style_path=="NO")
            {
                File.WriteAllText("style.css","NO");
            }
            else  if (Style_path!=""&Style_path!=null)
            {
                if (File.Exists("style.css"))
                {
                    File.Delete("style.css");
                }
                
                File.Copy(Style_path,"style.css");  
            }

            
            if (File.Exists("style.css")==true& NOxml!="NO")
            {
                CssProvider provider = new CssProvider();
                provider.LoadFromPath("style.css");
                StyleContext.AddProviderForScreen(Gdk.Screen.Default, provider, 800);
                
            }
            
        }





        public static void PreStartChoose()
        {
            string NOxml = default;
            string Style_path = default;
            if (File.Exists("style.css") == false)
            {
                Style_path = Dialog.Dialog_Choose_style_Prestart();
               
            }
            else
            {
                NOxml=File.ReadAllText("style.css");
            }
            
            
            if (Style_path=="NO")
            {
                File.WriteAllText("style.css","NO");
                Thread.Sleep(1000);
            }
            else  if (Style_path!=""&Style_path!=null)
            {
                File.Copy(Style_path,"style.css");  
            }

            
            if (File.Exists("style.css")==true)
            { 
                
                NOxml=File.ReadAllText("style.css");
                if (NOxml!="NO")
                {
                    CssProvider provider = new CssProvider();
                    provider.LoadFromPath("style.css");
                    StyleContext.AddProviderForScreen(Gdk.Screen.Default, provider, 800);     
                }
                
            }
        }
    }
}