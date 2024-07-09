using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Timers;

namespace wpf_modbus
{
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : Page
    {
        Modbus sawConnection;
        Modbus trolleyConnection;
        System.Timers.Timer timer;

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

            Setup_ModbusRead();
        }

        private void NinetyDeg_OnClick(object sender, RoutedEventArgs e)
        {
            sawConnection.WriteSingleRegister(1, 90);
        }

        void Setup_ModbusRead()
        {
            timer = new System.Timers.Timer();

            timer.Elapsed += OnOneSecond;
            timer.Interval = 1000;
            timer.Start();
        }

        void OnOneSecond(object? sender, ElapsedEventArgs e)
        {
            //Span<int> sawAngle = sawConnection.ReadHoldingRegisters<int>(1, 1);
            //Bench.SawText = sawAngle.ToString();
        }
    }
}
