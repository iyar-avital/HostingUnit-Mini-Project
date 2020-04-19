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
    /// Interaction logic for HostingUnitUserControl.xaml
    /// </summary>
    public partial class HostingUnitUserControl : UserControl
    {
        public HostingUnit hu = new HostingUnit() {Diary = new bool[13,32], Owner = new Host() { BankBranchDetails = new BankBranch() } };
        public HostingUnitUserControl()
        {
            DataContext = hu;
            InitializeComponent();

            AreaBox.ItemsSource = Enum.GetValues(typeof(Areas));
            TypeBox.ItemsSource = Enum.GetValues(typeof(Type_Unit));
        }

        
    }
}
