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
using System.Windows.Shapes;
using WpfUI;

namespace PLWPF.HostingUnitOptions
{
    /// <summary>
    /// Interaction logic for UpdateUnitWindow.xaml
    /// </summary>
    public partial class UpdateUnitWindow : Window
    {
        private HostingUnit unit { get; set; } = new HostingUnit();
        public UpdateUnitWindow(int HKey)
        {
            InitializeComponent();
            unit = MainWindow.BL.GetHostingUnit(HKey);
            unitUserControl.hu = unit;
            unitUserControl.DataContext = unit;
            unitUserControl.IsEnabled = true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*var result = Validation.GetErrors(TextBox.);
            if (result.Count > 0) // has errors.
            {
                //write your logic here.
            }*/


            try
            {
                MainWindow.BL.UpdateHostingUnit(unitUserControl.hu);
                MessageBoxResult result = MessageBox.Show("[Key:" + unitUserControl.hu.HostingUnitKey + "] Unit has been updated.\n\nWould you like to close this window?", "System", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (result.Equals(MessageBoxResult.Yes))
                    this.Close();
                //MessageBox.Show(hu.ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "System", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void unitUserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }


}
