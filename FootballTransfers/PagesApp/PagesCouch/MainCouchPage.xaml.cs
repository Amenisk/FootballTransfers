using System;
using System.Collections.Generic;
using System.IO;
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
using FootballTransfers.ADOApp;
using FootballTransfers.PagesApp;
using FootballTransfers.PagesApp.PagesCouch;

namespace FootballTransfers.PagesApp.PagesCouch
{
    /// <summary>
    /// Логика взаимодействия для MainCouchPage.xaml
    /// </summary>
    public partial class MainCouchPage : Page
    {
        private Users _couch;
        public MainCouchPage(Users couch)
        {
            InitializeComponent();
            _couch = couch;
            imgLogo.Source = BytesToImage(couch.FootballClubs.Logo);
            tbNameOfClub.Text = couch.FootballClubs.Name;
            lvFootballers.ItemsSource = App.Connection.Footballers.
                Where(x => x.FootballClub_Id == couch.FootballClub_Id).ToList();
            tbBudget.Text += couch.FootballClubs.Budget.ToString();
        }

        private BitmapImage BytesToImage(byte[] bytes)
        {
            using(MemoryStream memoryStream = new MemoryStream(bytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memoryStream;
                image.EndInit();
                return image;
            }
        }

        private void SellFootballer(object sender, RoutedEventArgs e)
        {
            if (lvFootballers.SelectedItem != null)
            {
                NavigationService.Navigate(new FootballClubsPage(_couch.FootballClubs, (Footballers)lvFootballers.SelectedItem));
            }
            else
            {
                MessageBox.Show("Choose footballer!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BuyFootballer(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FootballClubsPage(_couch.FootballClubs));
        }

        private void GetInfo(object sender, RoutedEventArgs e)
        {
            if(lvFootballers.SelectedItem != null)
            {
                NavigationService.Navigate(new FootballerCardPage((Footballers) lvFootballers.SelectedItem));
            }
            else
            {
                MessageBox.Show("Choose footballer!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }

        private void GetListOfTransfers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FootballClubTransfersPage(_couch));
        }
    }


}
