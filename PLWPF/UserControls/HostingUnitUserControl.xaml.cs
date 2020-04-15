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
using WpfUI;

namespace PLWPF.UserControls
{
    /// <summary>
    /// Interaction logic for HostingUnitUserControl.xaml
    /// </summary>
    public partial class HostingUnitUserControl : UserControl
    {
        public static BE.HostingUnit hu = new BE.HostingUnit();
        public HostingUnitUserControl()
        {
            DataContext = hu;
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                MainWindow.BL.UpdateHostingUnit(hu);
                //MessageBox.Show(hu.ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }

        }
    }
}
