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

namespace PLWPF.HostingUnitOptions
{
    /// <summary>
    /// Interaction logic for PrivateAreaWindow.xaml
    /// </summary>
    public partial class PrivateAreaWindow : Window
    {
        public BE.HostingUnit unit { get; set; } = new BE.HostingUnit();
        public PrivateAreaWindow()
        {
            InitializeComponent();

            
        }

        private void UpdateUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateUnitWindow editUnit = new UpdateUnitWindow();
            editUnit.unitUserControl.DataContext = unit;
            editUnit.ShowDialog();
        }

        private void OrdersAreaBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Orders Area");
        }

        private void DeleteUnitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.BL.DeleteHostingUnit(unit);
                MessageBox.Show("Unit has been deleted.", "System", MessageBoxButton.OK, MessageBoxImage.Hand);
                this.Close();
            }catch(Exception err)
            {
                MessageBox.Show(err.Message, "System", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
