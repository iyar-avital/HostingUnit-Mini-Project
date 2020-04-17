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
using WpfUI;

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
            RefreshOrders();
        }

        private void AddOrder_Button(object sender, RoutedEventArgs e)
        {
            new AddOrderWindow(unit).ShowDialog();
            RefreshOrders();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ListOrderToUpdate(unit).ShowDialog();
        }

        private void RefreshOrders()
        {
            List<BE.Order> orders = MainWindow.BL.Lorder(order => order.HostingUnitKey == unit.HostingUnitKey);
            if (orders.Count == 0)
                UpdateOrder_button.Visibility = Visibility.Collapsed;
            else
                UpdateOrder_button.Visibility = Visibility.Visible;
        }
    }
}
