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
using System.IO;
using PLWPF;
using BL;
using PLWPF.GuestOptions;
using System.Threading;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IBL BL = FactoryBL.getBL();
        System.Media.SoundPlayer player = new System.Media.SoundPlayer(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"CarSongs.wav")); // to get relative location
        //System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Users\riki\Documents\לוסטיג\שנה_ב\סמסטר_א\מיני_פרוייקט\project\Project01_5982_6824_dotNet5779\CarSongs.wav");
        bool soundFlag = true;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //player.PlayLooping();

            MainWindow.BL.OrderMoreThanMonth();

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MainWindow.BL.orderThread != null)
            {

                MessageBox.Show("Bye");
                try
                {
                    MainWindow.BL.orderThread.Interrupt();
                }
                catch (Exception er) { }
            }
            else
                MessageBox.Show("Bye no abort..");
        }

        private void GuestRequest_ButtonClick(object sender, RoutedEventArgs e)
        {
            new AddGuestWindow().ShowDialog();
        }

        private void HostingUnit_Button_Click(object sender, RoutedEventArgs e)
        {
            new HostingUnitOptionsWindow().ShowDialog();
        }

        private void Administrator_Button_Click(object sender, RoutedEventArgs e)
        {
            new AdminIdentificationWindow().ShowDialog();
        }
       
        private void MoreWindow_Button_Click(object sender, RoutedEventArgs e)
        {
            new More().ShowDialog();
        }
        
        private void Pause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (soundFlag) //music is being playing
            {
                player.Stop();
                //pause.Source = new ImageSourceConverter().ConvertFromString("C:/Users/riki/Documents/לוסטיג/שנה_ב/סמסטר_א/מיני_פרוייקט/project/Project01_5982_6824_dotNet5779/WpfUI/images/play.jpg") as ImageSource;
                pause.Source = new ImageSourceConverter().ConvertFromString(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "play.jpg")) as ImageSource;  // to get relative location
                soundFlag = false;
            }
            else //music is not being playing
            {
                player.PlayLooping();
                //pause.Source = new ImageSourceConverter().ConvertFromString("C:/Users/riki/Documents/לוסטיג/שנה_ב/סמסטר_א/מיני_פרוייקט/project/Project01_5982_6824_dotNet5779/WpfUI/images/pause.jpg") as ImageSource;
                pause.Source = new ImageSourceConverter().ConvertFromString(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pause.jpg")) as ImageSource;  // to get relative location
                soundFlag = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}



