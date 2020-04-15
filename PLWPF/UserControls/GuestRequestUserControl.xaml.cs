using BE;
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
    /// Interaction logic for GuestRequestUserControl.xaml
    /// </summary>
    public partial class GuestRequestUserControl : UserControl
    {
        public static GuestRequest gs = new GuestRequest();
        public GuestRequestUserControl()
        {
            DataContext = gs;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                MainWindow.BL.AddClientRequest(gs);
                MessageBox.Show(gs.ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }

        }
    }
}
