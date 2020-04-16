using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfUI;

namespace PLWPF.OrderOptions
{
    /// <summary>
    /// Interaction logic for ListOrderToUpdate.xaml
    /// </summary>
    public partial class ListOrderToUpdate : Window
    {
        private List<BE.Order> orderL;
        BE.HostingUnit unit = new BE.HostingUnit();

        public ListOrderToUpdate(BE.HostingUnit h)
        {
            InitializeComponent();

            unit = h;
            DataContext = this;
            orderL = MainWindow.BL.Lorder(item => item.HostingUnitKey == h.HostingUnitKey);
            UpdateOrder_Grid.ItemsSource = orderL;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            //Change date format
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM";

            //Rename Headers of Columns
            Dictionary<String, String> Headers = new Dictionary<String, String>
            {
                { "OrderKey", "Key" },
                { "HostingUnitKey", "Unit" },
                { "GuestRequestKey", "Request" },
                { "CreateDate", "Create Date" },
                { "OrderDate", "Order Date" },
            };

            e.Column.Header = Headers.ContainsKey(propertyDescriptor.DisplayName) ? Headers[propertyDescriptor.DisplayName] : propertyDescriptor.DisplayName;
        }

        private void UpdateOrder_Button_Click(object sender, RoutedEventArgs e)
        {
            if (UpdateOrder_Grid.SelectedItems.Count > 0)
                UpdateOrder();
            else
                MessageBox.Show("Please select a order to update", "System");
        }

        private void UpdateOrder()
        {
            new UpdateOrderWindow(UpdateOrder_Grid.SelectedItems).ShowDialog();
            RefreshData();
        }

        private void RefreshData()
        {
            orderL = MainWindow.BL.Lorder(item => item.HostingUnitKey == unit.HostingUnitKey);
            UpdateOrder_Grid.ItemsSource = orderL;
        }

        private void UpdateOrder_Grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateOrder();
        }
    }
}
