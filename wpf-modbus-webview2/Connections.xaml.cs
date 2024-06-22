using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        const string CONNECTION_FAILED = "Error: Connection failed.";
        const string CONNECTION_SUCCESS = "SUCCESS";

        bool sawConnected;
        bool trolleyConnected;

        public Connections()
        {
            InitializeComponent();
            DataContext = this;
        }

        void ConnectSaw_Click(object sender, RoutedEventArgs e)
        {
            SawOutcome.Content = string.Empty;
            var addressText = SawAddress.Text;
            var address = IPAddress.Parse(addressText);
            using var bridge = new ModbusBridge();
            var modbus = new Modbus(bridge);
            modbus.Connect(address);

            if(modbus.IsConnected() == false)
            {
                SawOutcome.Content = CONNECTION_FAILED;
                SawOutcome.Background = Brushes.Red;
                sawConnected = false;
                return;
            }

            else
            {
                SawOutcome.Content = CONNECTION_SUCCESS;
                SawOutcome.Background = Brushes.Green;
                sawConnected = true;
            }
            EvaluateNextPage();
        }

        void ConnectTrolley_Click(object sender, RoutedEventArgs e)
        {
            TrolleyOutcome.Content = string.Empty;
            var addressText = SawAddress.Text;
            var address = IPAddress.Parse(addressText);
            using var bridge = new ModbusBridge();
            var modbus = new Modbus(bridge);
            modbus.Connect(address);

            if (modbus.IsConnected() == false)
            {
                TrolleyOutcome.Content = CONNECTION_FAILED;
                TrolleyOutcome.Background = Brushes.Red;
                trolleyConnected = false;
            }

            else
            {
                TrolleyOutcome.Content = CONNECTION_SUCCESS;
                TrolleyOutcome.Background = Brushes.Green;
                trolleyConnected = true;
            }
            EvaluateNextPage();
        }

        void EvaluateNextPage()
        {
            if(sawConnected && trolleyConnected)
            {
                //this.NavigationService.Navigate();
            }
        }
    }
}
