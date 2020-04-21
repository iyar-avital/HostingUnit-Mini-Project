using BE;
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
            InitializeComponent();
            DataContext = hu;

            AreaBox.ItemsSource = Enum.GetValues(typeof(Areas));
            TypeBox.ItemsSource = Enum.GetValues(typeof(Type_Unit));

            InitBankDetails(); 
        }

        public void SetDataContext()
        {
            hu = (HostingUnit)DataContext;
        }

        public void InitBankDetails()
        {
            BanksList = MainWindow.BL.GetBanksList();
            cbBanksList.ItemsSource = BanksList;
            cbBranchesList.IsEnabled = false;

        }

        private void cbBanksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex < 0 || (sender as ComboBox).SelectedItem == null)
                return;
            cbBranchesList.IsEnabled = false;
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            string[] bankSplit = (sender as ComboBox).SelectedItem.ToString().Split('-');

            hu.Owner.BankBranchDetails = new BankBranch();

            BranchesByBankList = MainWindow.BL.GetBranchesList(bankSplit[1].Trim());
            cbBranchesList.ItemsSource = BranchesByBankList;

            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

            cbBranchesList.IsEnabled = true;
            if (isAdd == true)
            {
                cbBranchesList.IsDropDownOpen = true;
                cbBranchesList.Focus();
            }
            else
            {
                cbBranchesList.ItemsSource = BranchesByBankList;
            }

        }

        private void cbBranchesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsEnabled == false || cbBanksList.SelectedIndex < 0 || (sender as ComboBox).SelectedIndex < 0)
                return;

            string[] bankSplit = cbBanksList.SelectedItem.ToString().Split('-');
            string[] branchSplit = (sender as ComboBox).SelectedItem.ToString().Split('-');


            selectedBranch = MainWindow.BL.GetBranch(Convert.ToInt32(bankSplit[0]), Convert.ToInt32(branchSplit[0]));

            if (selectedBranch != null)
            {

                hu.Owner.BankBranchDetails.BankName = selectedBranch.BankName;
                hu.Owner.BankBranchDetails.BankNumber = selectedBranch.BankNumber;
                hu.Owner.BankBranchDetails.BranchAddress = selectedBranch.BranchAddress;
                hu.Owner.BankBranchDetails.BranchCity = selectedBranch.BranchCity;
                hu.Owner.BankBranchDetails.BranchNumber = selectedBranch.BranchNumber;
                hu.Owner.BankBranchDetails.BranchName = selectedBranch.BranchName;

                tbBranchCity.Text = selectedBranch.BranchCity;
                tbBranchAddress.Text = selectedBranch.BranchAddress;
            }
        }

        private void cbBanksList_DropDownOpened(object sender, EventArgs e)
        {
            cbBanksList.ItemsSource = BanksList;
        }

        private void cbBranchesList_DropDownOpened(object sender, EventArgs e)
        {
            cbBranchesList.ItemsSource = BranchesByBankList;
        }



    }
}
