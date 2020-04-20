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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AdminIdentificationWindow.xaml
    /// </summary>
    public partial class AdminIdentificationWindow : Window
    {
        public AdminIdentificationWindow()
        {
            InitializeComponent();
        }

        private void UnitPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Unit number must be 8 digits
            if (UserPassword.Password.Length < MainWindow.BL.GetUserPassword().Length)
            {
                //if a char is deleted after a unit is approved
                Continue_button.IsEnabled = false;
                return;
            }

            if (UserPassword.Password == MainWindow.BL.GetUserPassword())
                Continue_button.IsEnabled = true;
            else
                WrongNumber("Incorrect password");

            
        }

        private void WrongNumber(string ErrorMessage)
        {
            MessageBox.Show(ErrorMessage);
            UserPassword.Password = "";
        }

        private void Continue_button_Click(object sender, RoutedEventArgs e)
        {
            new Administrator().ShowDialog();
        }
    }
}
