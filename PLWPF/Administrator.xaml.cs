using BE;
using PLWPF.GuestOptions;
using PLWPF.HostingUnitOptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        private List<BE.HostingUnit> unitL = null;
        private List<GuestRequest> guestL = null;
        private List<Order> orderL = null;

        public bool DeleteButtonEnable { get; set; } = true;

        public Administrator()
        {
            InitializeComponent();

            DataContext = this;

            RefreshData();
        }

        public void RefreshUnits()
        {
            unitL = MainWindow.BL.Lunit();
            UnitsListGrid.ItemsSource = unitL;
            UnitsListGrid.Items.Refresh();
        }

        public void RefreshGuests()
        {
            guestL = MainWindow.BL.LGrequest();
            GuestsListGrid.ItemsSource = guestL;
            GuestsListGrid.Items.Refresh();
        }

        public void RefreshOrders()
        {
            orderL = MainWindow.BL.Lorder();
            OrdersListGrid.ItemsSource = orderL;
            OrdersListGrid.Items.Refresh();
        }

        public void RefreshData()
        {
            RefreshUnits();
            RefreshGuests();
            RefreshOrders();
        }

        



        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            //List of columns which not gonna be displayed on the data grid
            List<String> columnsToExclude = new List<String>
            {
                "Owner", "Diary", "AllOrders", "picture"
            };

            //Rename Headers of Columns
            Dictionary<String, String> Headers = new Dictionary<String, String>
            {
                { "HostingUnitKey", "Key" },
                { "HostingUnitName", "Name" },
                { "ChildrensAttractions", "Attractions" },


            };

            //If current column is not gonna be on display grid data - than cancel
            if (columnsToExclude.Contains(propertyDescriptor.DisplayName))
                e.Cancel = true;
            else //If Rename key exists - than get new Display name, else get default Display name
                e.Column.Header = (Headers.ContainsKey(propertyDescriptor.DisplayName) ? Headers[propertyDescriptor.DisplayName] : propertyDescriptor.DisplayName);

        }

        private void PreviewKeyDownHandler(object sender, KeyEventArgs e)
        {
            var grid = (DataGrid)sender;
            if (Key.Delete == e.Key)
            {
                e.Handled = true;
                DeleteSelectedItems(grid.SelectedItems);
            }
        }

        private void ViewSelectedItem(object selectedItem)
        {
            if (selectedItem is BE.HostingUnit)
            {
                ViewUnitWindow viewUnit = new ViewUnitWindow();
                viewUnit.unitUserControl.DataContext = (BE.HostingUnit)selectedItem;
                viewUnit.ShowDialog();
            }
            else if (selectedItem is GuestRequest)
            {
                ViewGuestWindow viewGuest = new ViewGuestWindow();
                viewGuest.guestUserControl.DataContext = (GuestRequest)selectedItem;
                viewGuest.ShowDialog();
            }
            else if (selectedItem is Order)
                MessageBox.Show("Order");
        }

        private void EditSelectedItem(object selectedItem)
        {
            if (selectedItem is BE.HostingUnit)
            {
                UpdateUnitWindow editUnit = new UpdateUnitWindow();
                editUnit.unitUserControl.DataContext = (BE.HostingUnit)selectedItem;
                editUnit.ShowDialog();
            }
            else if (selectedItem is Order)
                MessageBox.Show("Order");
        }

        private void DeleteSelectedItems(object selectedItems)
        {
            IList<object> items = selectedItems as IList<object>;
            if (items[0] is BE.HostingUnit)
            {
                bool needRefresh = false;
                String Message = "";

                List<String> SuccessMessages = new List<string>();
                List<String> ErrorMessages = new List<string>();


                foreach (var row in items)
                {
                    try
                    {
                        MainWindow.BL.DeleteHostingUnit((BE.HostingUnit)row);
                        needRefresh = true;
                        SuccessMessages.Add("Deleteing unit number[" + ((BE.HostingUnit)row).HostingUnitKey + "]");

                    }
                    catch (Exception err)
                    {
                        ErrorMessages.Add(err.Message);

                    }
                }

                if (needRefresh)
                    RefreshUnits();


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
            
        }

        private void ListGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid grid = (DataGrid)sender;
            ViewSelectedItem(grid.SelectedItem);
        }

        private DataGrid GetRelatedGridOfButton(Button b)
        {
            DataGrid grid = null;
            //Finding the grid that belongs to that button
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(b.Parent); i++)
            {
                var child = VisualTreeHelper.GetChild(b.Parent, i);
                if (child is DataGrid)
                {
                    grid = (DataGrid)child;
                    break;
                }
            }
            return grid;
        }

        //View Button Click
        private void ViewBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            DataGrid grid = GetRelatedGridOfButton((Button)sender);
            if (grid == null)
                return;

            if (grid.SelectedItems.Count <= 0)
                MessageBox.Show("Please select Item to view");
            else if (grid.SelectedItems.Count >= 2)
                MessageBox.Show("Please select only one item to view");
            else
                ViewSelectedItem(grid.SelectedItem);
        }

        //Edit Button Click
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            DataGrid grid = GetRelatedGridOfButton((Button)sender);
            if (grid == null)
                return;

            if (grid.SelectedItems.Count <= 0)
                MessageBox.Show("Please select Item to edit");
            else if (grid.SelectedItems.Count >= 2)
                MessageBox.Show("Please select only one item to edit");
            else
                EditSelectedItem(grid.SelectedItem);
        }

        //Delete Button Click
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            DataGrid grid = GetRelatedGridOfButton((Button)sender);
            if (grid == null)
                return;

            if (grid.SelectedItems.Count <= 0)
                MessageBox.Show("Please select Item to edit");
            else
                DeleteSelectedItems(grid.SelectedItems);
        }
    }
}
