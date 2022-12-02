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

namespace FootballTransfers.PagesApp.PagesCouch
{
    /// <summary>
    /// Логика взаимодействия для FootballClubTransfersPage.xaml
    /// </summary>
    public partial class FootballClubTransfersPage : Page
    {
        private Users _couch;
        private FootballClubs _club;
        public FootballClubTransfersPage(Users couch)
        {
            InitializeComponent();
            _couch = couch;
            _club = App.Connection.FootballClubs.FirstOrDefault
                (x => x.FootballClub_Id == _couch.FootballClub_Id);
            tbNameOfClub.Text = _club.Name;
            imgLogo.Source = BytesToImage(_club.Logo);
            lvTransfers.ItemsSource = App.Connection.TransfersHistory.
                Where(x => x.ClubOfPurchase == _club.FootballClub_Id 
                || x.ClubOfSale == _club.FootballClub_Id).ToList();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainCouchPage(_couch));
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
