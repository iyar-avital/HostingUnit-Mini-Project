using BE;
using PLWPF.GuestOptions;
using PLWPF.HostingUnitOptions;
using PLWPF.OrderOptions;
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
        private List<BE.Order> orderL = null;

        public bool DeleteButtonEnable { get; set; } = true;

        public Administrator()
        {
            InitializeComponent();

            DataContext = this;

            RefreshData();

            ShowRequestByArea();
            ShowRequestByPeople();
            ShowHotingUnitByArea();
            ShowHostingUnitByHost();
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
                "Owner", "Diary","DiaryDto", "AllOrders", "picture", "OrderDate"
            };

            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM";

            //Rename Headers of Columns
            Dictionary<String, String> Headers = new Dictionary<String, String>
            {
                { "HostingUnitKey", "Unit Key" },
                { "OrderKey", "Order Key" },
                { "GuestRequestKey", "Request Key" },
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
            if (selectedItem is HostingUnit)
            {
                HostingUnit h = (HostingUnit)selectedItem;
                ViewUnitWindow viewUnit = new ViewUnitWindow();
                viewUnit.unitUserControl.DataContext = h;
                viewUnit.unitUserControl.cbBranchesList.ItemsSource = MainWindow.BL.GetBranchesList(h.Owner.BankBranchDetails.BankName);
                viewUnit.unitUserControl.cbBranchesList.SelectedValue = h.Owner.BankBranchDetails.BranchNumber + " - " + h.Owner.BankBranchDetails.BranchName;

                viewUnit.ShowDialog();
            }
            else if (selectedItem is GuestRequest)
            {
                ViewGuestWindow viewGuest = new ViewGuestWindow();
                viewGuest.guestUserControl.DataContext = (GuestRequest)selectedItem;
                viewGuest.ShowDialog();
            }
             
        }

        private void EditSelectedItem(object selectedItem)
        {
            if (selectedItem is HostingUnit)
            {
                UpdateUnitWindow editUnit = new UpdateUnitWindow(((HostingUnit)selectedItem).HostingUnitKey);
                editUnit.ShowDialog();
                RefreshUnits();
            }
            else if (selectedItem is Order)
            {
                List<Order> myOrder = new List<Order>() { (Order)selectedItem};

                UpdateOrderWindow upOrW = new UpdateOrderWindow(myOrder);
                upOrW.ShowDialog();
                RefreshOrders();
            }

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



        /** GROUPING DATA **/


        private void ShowRequestByArea()
        {
            IEnumerable<IGrouping<Areas, GuestRequest>> GroupRequest = MainWindow.BL.GroupByArea();
            List<Areas> ListAreaKeys = GroupRequest.KeysInGroup();
            List<GuestRequest> ItemFoeKey;
            int NumberAllRequest = MainWindow.BL.LGrequest().Count;
            double NumberRequestKey;
            double Height;
            string Tooltip = "";
            for (int i = 0; i < ListAreaKeys.Count; i++)
            {
                RequestByArea_Grid.ColumnDefinitions.Add(new ColumnDefinition());
                ItemFoeKey = GroupRequest.ItemsInKey(ListAreaKeys[i]);
                NumberRequestKey = ItemFoeKey.Count;
                Tooltip = MakeToolTip(ListAreaKeys[i], NumberRequestKey, ref ItemFoeKey);
                Height = NumberRequestKey / NumberAllRequest * 400;
                MakeBorder(Height, i, Tooltip, RequestByArea_Grid);
                MakeLable(ListAreaKeys[i], i, RequestByArea_Grid);
            }
        }

        private void ShowRequestByPeople()
        {
            IEnumerable<IGrouping<int, GuestRequest>> GroupRequest = MainWindow.BL.GroupByPeople();
            List<int> ListAreaKeys = GroupRequest.KeysInGroup();
            List<GuestRequest> ItemFoeKey;
            int NumberAllRequest = MainWindow.BL.LGrequest().Count;
            double NumberRequestKey;
            double Height;
            string Tooltip = "";
            for (int i = 0; i < ListAreaKeys.Count; i++)
            {
                RequestByPeople_Grid.ColumnDefinitions.Add(new ColumnDefinition());
                ItemFoeKey = GroupRequest.ItemsInKey(ListAreaKeys[i]);
                NumberRequestKey = ItemFoeKey.Count;
                Tooltip = MakeToolTip(ListAreaKeys[i], NumberRequestKey, ref ItemFoeKey);
                Height = NumberRequestKey / NumberAllRequest * 400;
                MakeBorder(Height, i, Tooltip, RequestByPeople_Grid);
                MakeLable(ListAreaKeys[i], i, RequestByPeople_Grid);
            }
        }

        private void ShowHotingUnitByArea()
        {
            IEnumerable<IGrouping<Areas, HostingUnit>> GroupUnits = MainWindow.BL.GroupByAreaOfUnit();
            List<Areas> ListAreaKeys = GroupUnits.KeysInGroup();
            List<HostingUnit> ItemFoeKey;
            int NumberAllRequest = MainWindow.BL.Lunit().Count;
            double NumberUnitKey;
            double Height;
            string Tooltip = "";
            for (int i = 0; i < ListAreaKeys.Count; i++)
            {
                UnitByArea_Grid.ColumnDefinitions.Add(new ColumnDefinition());
                ItemFoeKey = GroupUnits.ItemsInKey(ListAreaKeys[i]);
                NumberUnitKey = ItemFoeKey.Count;
                Tooltip = MakeToolTip(ListAreaKeys[i], NumberUnitKey, ref ItemFoeKey);
                Height = NumberUnitKey / NumberAllRequest * 400;
                MakeBorder(Height, i, Tooltip, UnitByArea_Grid);
                MakeLable(ListAreaKeys[i], i, UnitByArea_Grid);
            }
        }

        private void ShowHostingUnitByHost()
        {
            IEnumerable<IGrouping<int, Host>> GroupUnits = MainWindow.BL.GroupHostByNumOfUnit();
            List<int> ListAreaKeys = GroupUnits.KeysInGroup();
            List<Host> ItemFoeKey;
            int NumberAllRequest = MainWindow.BL.Lunit().Count;
            double NumberUnitKey;
            double Height;
            string Tooltip = "";
            for (int i = 0; i < ListAreaKeys.Count; i++)
            {
                UnitByArea_Grid.ColumnDefinitions.Add(new ColumnDefinition());
                ItemFoeKey = GroupUnits.ItemsInKey(ListAreaKeys[i]);
                NumberUnitKey = ItemFoeKey.Count;
                Tooltip = MakeToolTip(ListAreaKeys[i], NumberUnitKey, ref ItemFoeKey);
                Height = NumberUnitKey / NumberAllRequest * 400;
                MakeBorder(Height, i, Tooltip, UnitByHost_Grid);
                MakeLable(ListAreaKeys[i], i, UnitByHost_Grid);
            }
        }

        private void MakeBorder(double h, int i, string tool, Grid g)
        {
            Border b = new Border();
            b.Background = Brushes.Brown;
            b.Padding = new Thickness(5);
            b.CornerRadius = new CornerRadius(7);
            b.Height = h;
            b.Width = 70;
            b.ToolTip = tool;
            b.HorizontalAlignment = HorizontalAlignment.Center;
            b.VerticalAlignment = VerticalAlignment.Bottom;
            g.Children.Add(b);
            b.SetValue(Grid.ColumnProperty, i);
        }

        private void MakeLable(Object obj, int i, Grid g)
        {
            Label l = new Label();
            l.FontSize = 15;
            l.Content = obj.ToString();
            l.HorizontalAlignment = HorizontalAlignment.Center;
            l.VerticalAlignment = VerticalAlignment.Top;
            g.Children.Add(l);
            l.SetValue(Grid.ColumnProperty, i);
        }

        private string MakeToolTip(Areas areas, double numberRequestKey, ref List<GuestRequest> itemFoeKey)
        {
            string tool = "Number of request \nin " + areas.ToString() + " = " + numberRequestKey + "\n";
            foreach (var request in itemFoeKey)
                tool += "\n\n Name: " + request.PrivateName + " " + request.FamilyName + "\n Entry Date: " + request.EntryDate.ToString("dd/MM") +
                    "\n Release Date: " + request.ReleaseDate.ToString("dd/MM");
            return tool;
        }

        private string MakeToolTip(int people, double numberRequestKey, ref List<GuestRequest> itemFoeKey)
        {
            string tool = "Number of request \n with " + people.ToString() + " people = " + numberRequestKey + "\n";
            foreach (var request in itemFoeKey)
                tool += "\n\n Name: " + request.PrivateName + " " + request.FamilyName + "\n Entry Date: " + request.EntryDate.ToString("dd/MM") +
                    "\n Release Date: " + request.ReleaseDate.ToString("dd/MM");
            return tool;
        }

        private string MakeToolTip(Areas areas, double numberRequestKey, ref List<HostingUnit> itemFoeKey)
        {
            string tool = "Number of Unit \nin " + areas.ToString() + " = " + numberRequestKey + "\n";
            foreach (var unit in itemFoeKey)
                tool += "\n\n Name: " + unit.HostingUnitName + "\n Type: " + unit.Type;
            return tool;
        }

        private string MakeToolTip(int units, double numberRequestKey, ref List<Host> itemFoeKey)
        {
            string tool = "Number of host \n with " + units.ToString() + " units = " + numberRequestKey + "\n";
            foreach (var host in itemFoeKey)
                tool += "\n\n Name: " + host.PrivateName + " " + host.FamilyName + "\nPhone: " + host.PhoneNumber +
                    "\n Email: " + host.MailAddress;
            return tool;
        }

    }
}
