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
using PLWPF.HostingUnitOptions;
using PLWPF.OrderOptions;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OrderOptions.xaml
    /// </summary>
    public partial class OrderOptionsWindow : Window
    {
        BE.HostingUnit unit;
        public OrderOptionsWindow(BE.HostingUnit h)
        {
            InitializeComponent();
            unit = h;
        }

        private void AddOrder_Button(object sender, RoutedEventArgs e)
        {
            new AddOrderWindow(unit).ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ListOrderToUpdate(unit).ShowDialog();
        }
    }
}
