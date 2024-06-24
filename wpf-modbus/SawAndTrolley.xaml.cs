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
using Windows.UI.ViewManagement;

namespace wpf_modbus
{
    /// <summary>
    /// Interaction logic for SawAndTrolley.xaml
    /// </summary>
    public partial class SawAndTrolley : UserControl
    {
        public string Title {  
            get {
                return ComponentsTitle.Text;
            }
            set {
                ComponentsTitle.Text = value;
            } 
        }

        public SawAndTrolley()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
