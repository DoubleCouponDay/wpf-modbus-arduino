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
    /// Interaction logic for SawAndTrolley.xaml
    /// </summary>
    public partial class SawAndTrolley : UserControl
    {
        public string ComponentTitle
        {
            get
            {
                return (string)ComponentsTitle.Content;
            }
            set
            {
                if (value is string)
                {
                    ComponentsTitle.Content = value;
                }
            }
        }

        public bool Readonly
        { 
            get 
            {
                return readonlyField;
            }
            set
            {
                readonlyField = value;
                ManualTrolleyPosition.IsReadOnly = !value;
                ManualSawAngle.IsReadOnly = !value;
            }
        }
        private bool readonlyField;

        public string SawText
        {
            get
            {
                return ManualSawAngle.Text;
            }
            set
            {
                ManualSawAngle.Text = value;
            }
        }

        public string TrolleyText
        {
            get
            {
                return ManualTrolleyPosition.Text;
            }
            set
            {
                ManualTrolleyPosition.Text = value;
            }
        }

        public Modbus? SawConnection { get; set; }
        public Modbus? TrolleyConnection { get; set; }

        int sawAngle;
        int trolleyPosition;

        public SawAndTrolley()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ManualSawAngle_KeyDown(object sender, KeyEventArgs e)
        {
            if (SawConnection == null || e.Key != Key.Enter) {
                return;
            }
            short output;
            bool isInt = short.TryParse(ManualSawAngle.Text, out output);

            if (isInt == false) {
                return;
            }
            SawConnection.WriteSingleRegister(1, output);
        }
    }
}
