using System;
using Gtk;

namespace PARSminexmr
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();
            var app = new Application("org.PARSminexmr.PARSminexmr", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}
