using Gtk;

namespace PARSminexmr
{
    public static class Error
    {
        public static void icon()
        {
            Gtk.Application.Invoke (delegate {
                using (var md = new MessageDialog (null, DialogFlags.Modal, MessageType.Error, ButtonsType.Close,"Error download icon")) {
                    md.Title = "Error";
                    md.Run ();
                    md.Destroy ();
                }
            });
        }

        public static void Entry()
        {
            Gtk.Application.Invoke (delegate {
                using (var md = new MessageDialog (null, DialogFlags.Modal, MessageType.Error, ButtonsType.Close,"Fill in the address field correctly: Currency:Address")) {
                    md.Title = "Error";
                    md.Run ();
                    md.Destroy ();
                    
                }
            });
        }
        
        
    }
}