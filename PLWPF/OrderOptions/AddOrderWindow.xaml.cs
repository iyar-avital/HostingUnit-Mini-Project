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
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        private List<BE.GuestRequest> guestL = MainWindow.BL.LGrequest(item => item.StatusRequest == BE.Request_Status.Active);
        BE.Order order;

        public AddOrderWindow(BE.HostingUnit hostingUnit)
        {
            InitializeComponent();

            DataContext = this;
            AddOrder_Grid.ItemsSource = guestL;

            order = new BE.Order();
            order.HostingUnitKey = hostingUnit.HostingUnitKey;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;

            //Change date format
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM";

            //Dont show the key
            if (propertyDescriptor.DisplayName == "GuestRequestKey")
                e.Cancel = true;

            //Rename Headers of Columns
            Dictionary<String, String> Headers = new Dictionary<String, String>
            {
                { "PrivateName", "Private Name" },
                { "FamilyName", "Family Name" },
                { "MailAddress", "EMail" },
                { "StatusRequest", "Status" },
                { "RegistrationDate", "Registration" },
                { "EntryDate", "Entry Date" },
                { "ReleaseDate", "Release Date" },
                { "SubArea", "Sub Area" },
                { "ChildrensAttractions", "Attractions" }
            };

            e.Column.Header = Headers.ContainsKey(propertyDescriptor.DisplayName) ? Headers[propertyDescriptor.DisplayName] : propertyDescriptor.DisplayName;

        }

        private void AddOrder_Button_Click(object sender, RoutedEventArgs e)
        {
            if (AddOrder_Grid.SelectedItems.Count > 0)
                AddOrder();
            else
                MessageBox.Show("Please select a guest request to add an order", "System");
        }

        private void AddOrder()
        {
            string Message = "";
            List<String> SuccessMessages = new List<string>();
            List<String> ErrorMessages = new List<string>();


            foreach (var guest_request in AddOrder_Grid.SelectedItems)
            {
                try
                {
                    order.GuestRequestKey = ((BE.GuestRequest)guest_request).GuestRequestKey;
                    MainWindow.BL.AddOrder(order);
                    SuccessMessages.Add("Add order number[" + order.OrderKey + "]");

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
        }

        private void AddOrder_Grid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddOrder();
        }
    }
}
