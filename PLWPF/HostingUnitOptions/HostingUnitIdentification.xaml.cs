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
    /// Interaction logic for HostingUnitIdentification.xaml
    /// </summary>
    public partial class HostingUnitIdentification : Window
    {
        BE.HostingUnit TheUnit;

        public HostingUnitIdentification()
        {
            InitializeComponent();
        }

        private void UnitPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Unit number must be 8 digits
            if (UnitPassword.Password.Length < 8)
            {
                //if a char is deleted after a unit is approved
                Continue_button.IsEnabled = false;
                return;
            }
            

            //checks if the number is correct
            foreach (var number in UnitPassword.Password)
            {
                if (!char.IsDigit(number))
                {
                    WrongNumber("Invalid number");
                    return;
                }
            }

            try
            {
                TheUnit = MainWindow.BL.GetHostingUnit(Convert.ToInt32(UnitPassword.Password));
                Continue_button.IsEnabled = true;
            }
            catch (Exception err)
            {
                //the unit not exist
                WrongNumber(err.Message);
            }
        }

        private void WrongNumber(string ErrorMessage)
        {
            MessageBox.Show(ErrorMessage);
            UnitPassword.Password = "";
        }

        private void Continue_button_Click(object sender, RoutedEventArgs e)
        {
            new PrivateAreaWindow(TheUnit).ShowDialog();
        }
    }
}
