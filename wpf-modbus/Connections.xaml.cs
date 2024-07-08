using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace wpf_modbus
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

        ControlPanel controlPanelPage;
        Modbus sawConnection;
        Modbus trolleyConnection;

        public Connections()
        {
            const int SERVER_ID = 12; //hard coded for the demo

            InitializeComponent();
            DataContext = this;
            var prodBridge = new ModbusBridge(SERVER_ID);
            var mockBridge = new JustWorksBridge(0, 0);
            sawConnection = new Modbus(prodBridge);
            trolleyConnection = new Modbus(mockBridge);
            controlPanelPage = new ControlPanel(sawConnection, trolleyConnection);
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
            var portText = input.Text;
            connection.Connect(portText);

            if (connection.IsConnected() == false)
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
            if (sawConnected && trolleyConnected)
            {
                NextPageButton.Visibility = Visibility.Visible;
            }

            else
            {
                NextPageButton.Visibility = Visibility.Hidden;
            }
        }

        private void ControlPanel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(controlPanelPage);
        }
    }
}
