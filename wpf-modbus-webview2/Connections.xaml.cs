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

        ControlPanel nextPage;
        Modbus sawConnection;
        Modbus trolleyConnection;

        public Connections()
        {
            InitializeComponent();
            DataContext = this;
            nextPage = new ControlPanel();
        }

        void ConnectSaw_Click(object sender, RoutedEventArgs e)
        {
            ConnectToAddress(ref sawConnection, SawOutcome, SawAddress, ref sawConnected);
            EvaluateNextPage();
        }

        void ConnectTrolley_Click(object sender, RoutedEventArgs e)
        {
            ConnectToAddress(ref trolleyConnection, TrolleyOutcome, TrolleyAddress, ref trolleyConnected);
            EvaluateNextPage();
        }

        void ConnectToAddress(ref Modbus connection, Label resultMessage, TextBox input, ref bool resultFlag)
        {
            resultMessage.Content = string.Empty;
            var addressText = input.Text;
            IPAddress address;

            try {
                address = IPAddress.Parse(addressText);
            }

            catch(Exception e) {
                var message = $"Error: {e.Message}";
                SetResult(resultMessage, Brushes.Red, message, ref resultFlag, false);
                EvaluateNextPage();
                Helpers.Get.PopupMessage(message);
                return;
            }

            if(connection == null)
            {
                var bridge = new JustWorksBridge(0, 0);
                connection = new Modbus(bridge);
                connection.Connect(address);
            }

            else
            {
                connection.Connect(address);
            }

            if(connection.IsConnected() == false)
            {
                SetResult(resultMessage, Brushes.Red, CONNECTION_FAILED, ref resultFlag, false);
            }

            else
            {
                SetResult(resultMessage, Brushes.Green, CONNECTION_SUCCESS, ref resultFlag, true);
            }
        }

        void SetResult(Label label, SolidColorBrush colour, string content, ref bool flag, bool value)
        {
            label.Content = content;
            label.Background = colour;
            flag = value;
        }

        void EvaluateNextPage()
        {
            if(sawConnected && trolleyConnected)
            {
                NextPage.Visibility = Visibility.Visible;
            }

            else
            {
                NextPage.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(nextPage);
        }
    }
}
