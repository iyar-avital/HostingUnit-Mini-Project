using PLWPF.HostingUnitOptions;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostingUnit.xaml
    /// </summary>
    public partial class HostingUnitOptionsWindow : Window
    {
        public HostingUnitOptionsWindow()
        {
            InitializeComponent();
        }

        private void AddUnitDialog_Button(object sender, RoutedEventArgs e)
        {
            new AddUnitWindow().ShowDialog();
        }

        private void PrivateAreaDialog_Button(object sender, RoutedEventArgs e)
        {
            /*HostingUnitIdentification unitIdentification = new HostingUnitIdentification();
            
            .ShowDialog();
            */
            new HostingUnitIdentification().ShowDialog();
        }
        /*
        public void ShowMyDialogBox()
        {
            Form testDialog = new Form();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                this.txtResult.Text = testDialog.TextBox1.Text;
            }
            else
            {
                this.txtResult.Text = "Cancelled";
            }
            testDialog.Dispose();
        }
        */
    }
}
