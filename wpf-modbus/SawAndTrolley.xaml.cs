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

        public SawAndTrolley()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
