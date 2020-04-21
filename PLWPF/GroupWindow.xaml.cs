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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        public GroupWindow()
        {
            InitializeComponent();

            ShowRequestByArea();
            ShowRequestByPeople();
            ShowHotingUnitByArea();
            ShowHostingUnitByHost();
        }

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
