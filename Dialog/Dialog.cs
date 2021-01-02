using System;
using System.IO;
using Gtk;

namespace PARSminexmr
{
    public class Dialog
    {
        public static string DialogFile(string heading=default)
        {
            string result = null;
            Gtk.FileChooserDialog OpenDialog = new Gtk.FileChooserDialog(heading, null, Gtk.FileChooserAction.Save, "Open", Gtk.FileChooserAction.Open);
            OpenDialog.SetPosition(Gtk.WindowPosition.CenterAlways);
            if (OpenDialog.Run() == (int)Gtk.FileChooserAction.Open)
            {
                result = OpenDialog.Filename;
                OpenDialog.Dispose();

               
            }
            OpenDialog.Dispose();
            return result;

        }

        public static string Dialog_Choose_style_Prestart()
        {
            string StylePathFail = default;
            var md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.YesNo, "Choose a Custom GTK Theme?");
            md.SetPosition(Gtk.WindowPosition.CenterAlways);
            md.Title = "Error";
            int res = md.Run();        
            
            if (res==(int)Gtk.ResponseType.Yes)
            {
                StylePathFail = Dialog.DialogFile("Path:{Project}/CSS/Style.css");

            } 
            else if (res==(int)Gtk.ResponseType.No)
            {   
                md.Dispose();
                
                var _NO = new MessageDialog(null, DialogFlags.Modal, MessageType.Question, ButtonsType.YesNo, "Don't ask again?");
                _NO.SetPosition(Gtk.WindowPosition.CenterAlways);
                _NO.Title = "?";
                int NO =_NO.Run();
                if (NO==(int)Gtk.ResponseType.Yes)
                {
                    StylePathFail = "NO";

                }
                _NO.Dispose();

            }
            md.Dispose();

            return StylePathFail;
        }

        


    }

}