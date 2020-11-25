using System.IO;
using Gtk;

namespace PARSminexmr
{
    public class Choose_style
    {
        public static void Start()
        {
            string NOxml = default;
            string Style_path = default;
            if (File.Exists("style.css") == false)
            {
                Style_path = Dialog.Dialog_Choose_style();
               
            }
            else
            {
                NOxml=File.ReadAllText("style.css");
            }
            
            
            if (Style_path=="NO")
            {
                File.WriteAllText("style.css","NO");
            }
            else  if (Style_path!=""&Style_path!=null)
            {
                File.Copy(Style_path,"style.css");  
            }

            
            if (File.Exists("style.css")==true& NOxml!="NO")
            {
                CssProvider provider = new CssProvider();
                provider.LoadFromPath("style.css");
                StyleContext.AddProviderForScreen(Gdk.Screen.Default, provider, 800);
                
            }
        }
    }
}