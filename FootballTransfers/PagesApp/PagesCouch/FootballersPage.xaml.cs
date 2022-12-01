using FootballTransfers.ADOApp;
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
using FootballTransfers.ADOApp;
using System.IO;

namespace FootballTransfers.PagesApp.PagesCouch
{
    /// <summary>
    /// Логика взаимодействия для FootballersPage.xaml
    /// </summary>
    public partial class FootballersPage : Page
    {
        private FootballClubs _club;
        private FootballClubs _clubOfSale;
     
        public FootballersPage(FootballClubs club, FootballClubs clubOfSale)
        {
            InitializeComponent();
            lvFootballers.ItemsSource = App.Connection.Footballers.Where
                (x => x.FootballClub_Id == clubOfSale.FootballClub_Id).ToList();
            tbNameOfClub.Text = clubOfSale.Name;
            imgLogo.Source = BytesToImage(clubOfSale.Logo);
            _club = club;
            _clubOfSale = clubOfSale;
        }

        private void GetFootballerCard(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new FootballerCardPage(_club, _clubOfSale, (Footballers)lvFootballers.SelectedItem));
        }

        private BitmapImage BytesToImage(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memoryStream;
                image.EndInit();
                return image;
            }
        }
    }
}
