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
using System.Windows.Shapes;

namespace PLWPF.HostingUnitOptions
{
    /// <summary>
    /// Interaction logic for ViewUnitWindow.xaml
    /// </summary>
    public partial class ViewUnitWindow : Window
    {
        public BE.HostingUnit unit { get; set; } = new BE.HostingUnit();
        public ViewUnitWindow()
        {
            InitializeComponent();
            unitUserControl.DataContext = unit;
            unitUserControl.IsEnabled = false;
            unitUserControl.isView = true;
        }
    }
}
