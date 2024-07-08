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

namespace wpf_modbus
{
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : Page
    {
        Modbus sawConnection;
        Modbus trolleyConnection;

        public ControlPanel(Modbus inputSawConnection, Modbus inputTrolleyConnection)
        {
            InitializeComponent();
            DataContext = this;
            sawConnection = inputSawConnection;
            trolleyConnection = inputTrolleyConnection;
            
            //pass connections
            Bench.SawConnection = inputSawConnection;
            Bench.TrolleyConnection = inputTrolleyConnection;
            ManualControl.SawConnection = inputSawConnection;
            ManualControl.TrolleyConnection = inputTrolleyConnection;
        }

        private void NinetyDeg_OnClick(object sender, RoutedEventArgs e)
        {
            sawConnection.WriteSingleRegister(1, 90);
        }
    }
}
