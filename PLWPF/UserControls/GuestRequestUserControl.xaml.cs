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
        public GuestRequest gs = new GuestRequest() {EntryDate = DateTime.Today, ReleaseDate = DateTime.Today.AddDays(1) };
        public GuestRequestUserControl()
        {
            DataContext = gs;
            InitializeComponent();

            AreaBox.ItemsSource = Enum.GetValues(typeof(Areas));
            SubAreaBox.ItemsSource = Enum.GetValues(typeof(Request_SubArea));
            TypeBox.ItemsSource = Enum.GetValues(typeof(Type_Unit));



            EntryDateDP.DisplayDateStart = DateTime.Today;
            EntryDateDP.DisplayDateEnd = DateTime.Today.AddYears(1).AddDays(-2);
            //EntryDateDP.SelectedDate = DateTime.Today;

            LeaveDateDP.DisplayDateStart = DateTime.Today.AddDays(1);
            LeaveDateDP.DisplayDateEnd = DateTime.Today.AddYears(1).AddDays(-1);
            //EntryDateDP.SelectedDate = DateTime.Today;
        }

        

        private void EntryDateDP_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LeaveDateDP == null)
                return;
            LeaveDateDP.DisplayDateStart = EntryDateDP.SelectedDate.Value.AddDays(1);
            LeaveDateDP.SelectedDate = null;
            LeaveDateDP.IsDropDownOpen = true;
            LeaveDateDP.Focus();
        }
    }
}
