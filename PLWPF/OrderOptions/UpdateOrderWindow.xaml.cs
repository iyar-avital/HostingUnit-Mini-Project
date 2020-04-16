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
    /// Interaction logic for UpdateOrderWindow.xaml
    /// </summary>
    public partial class UpdateOrderWindow : Window
    {
        List<BE.Order> ordersToUpdate = new List<BE.Order>();
        BE.OrderStatus status;

        public UpdateOrderWindow(System.Collections.IList O)
        {
            InitializeComponent();

            foreach (var order in O)
                ordersToUpdate.Add((BE.Order)order);
         
        }

        private void CostumerRespond_button_Checked(object sender, RoutedEventArgs e)
        {
            status = BE.OrderStatus.Closed_on_customer_response;
            Update_button.Visibility = Visibility.Visible;
        }

        private void CostumerNotRespond_button_Checked(object sender, RoutedEventArgs e)
        {
            status = BE.OrderStatus.Closed_from_customer_not_respone;
            Update_button.Visibility = Visibility.Visible;
        }

        private void Update_button_Click(object sender, RoutedEventArgs e)
        {
            string Message = "";
            List<String> SuccessMessages = new List<string>();
            List<String> ErrorMessages = new List<string>();

            foreach (var order in ordersToUpdate)
            {
                try
                {
                    MainWindow.BL.UpdateOrder(order, status);
                    SuccessMessages.Add("change status order number[" + order.OrderKey + "]");
                }
                catch (Exception err)
                {
                    ErrorMessages.Add(err.Message);
                }
            }
        
            if (SuccessMessages.Count > 0)
            {
                Message += "Success:\n";
                foreach (string msg in SuccessMessages)
                    Message += " " + msg + "\n";
            }

            if (ErrorMessages.Count > 0)
            {
                Message += "Error:\n";
                foreach (string msg in ErrorMessages)
                    Message += " " + msg + "\n";
            }

            MessageBox.Show(Message, "System");
            Close();
        }
    }
}
