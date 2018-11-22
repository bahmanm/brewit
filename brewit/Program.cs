using System;
using Gtk;
using Gdk;

namespace Brewit
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            new MainClass().Run();
        }

        public void Run() 
        {
            Application.Init();
            var win = new MainWindow();
            Application.Run();
        }
    }
}
