using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_modbus_webview2
{
    /// <summary>
    /// Interaction logic for Connections.xaml
    /// </summary>
    public partial class Connections : Page
    {
        public string SawOutcome { get; private set; } = string.Empty;
        public string TrolleyOutcome { get; private set; } = string.Empty;

        public Connections()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void ConnectSaw_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ConnectTrolley_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
