using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf_modbus
{
    public class Helpers
    {
        public static Helpers Get { get; private set; }

        static Helpers()
        {
            Get = new Helpers();
        }

        private Helpers() { }
        public void PopupMessage(string message)
        {
            var popup = new Window();
            popup.Width = 300;
            popup.Height = 100;
            popup.Content = message;
            popup.Show();
        }
    }
}
