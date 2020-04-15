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

namespace PLWPF.GuestOptions
{
    /// <summary>
    /// Interaction logic for ViewGuestWindow.xaml
    /// </summary>
    public partial class ViewGuestWindow : Window
    {
        public GuestRequest guest { get; set; } = new GuestRequest();
        public ViewGuestWindow()
        {
            InitializeComponent();
            guestUserControl.DataContext = guest;
            guestUserControl.IsEnabled = false;
        }
    }
}
