using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FootballTransfers.ADOApp;
using FootballTransfers.PageApp.PagesCouch;
using FootballTransfers.PagesApp.PagesCouch;

namespace FootballTransfers.PagesApp.PagesCouch
{
    /// <summary>
    /// Логика взаимодействия для FotballClubsPage.xaml
    /// </summary>
    public partial class FootballClubsPage : Page
    {
        private Footballers _footballer;
        private FootballClubs _club;
        private int _points = 0;
        public FootballClubsPage(FootballClubs club)
        {
            InitializeComponent();
            lvClubs.ItemsSource = App.Connection.FootballClubs.
                Where(x => x.FootballClub_Id != club.FootballClub_Id).ToList();
            _club = club;
        }
        public FootballClubsPage(FootballClubs club, Footballers footballer)
        {
            InitializeComponent();
            lvClubs.ItemsSource = App.Connection.FootballClubs.
                Where(x => x.FootballClub_Id != club.FootballClub_Id).ToList();
            _footballer = footballer;
            _club = club;
        }
        private void SellFootballer(object sender, SelectionChangedEventArgs e)
        {
            if (_footballer != null)
            {
                DialogResult dialogResult = System.Windows.Forms.
                MessageBox.Show("You confirm sale?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (((FootballClubs)lvClubs.SelectedItem).Budget >= _footballer.TransferCost)
                    {
                        ((FootballClubs)lvClubs.SelectedItem).Budget -= _footballer.TransferCost;
                        _club.Budget += _footballer.TransferCost;

                        TransfersHistory transfer = new TransfersHistory()
                        {
                            Footballer_Id = _footballer.Footballer_Id,
                            Characteristic_Id = _footballer.Characterics.Characteristic_Id,
                            ClubOfSale = _footballer.FootballClubs.FootballClub_Id,
                            ClubOfPurchase = ((FootballClubs)lvClubs.SelectedItem).FootballClub_Id,
                            TransferCost = _footballer.TransferCost,
                            DateAndTime = DateTime.Now
                        };

                        ChangingCharacteristics();
                        _footballer.FootballClub_Id = ((FootballClubs)lvClubs.SelectedItem).FootballClub_Id;
                        App.Connection.TransfersHistory.Add(transfer);
                        App.Connection.SaveChanges();
                        System.Windows.MessageBox.Show("Transfer was successful");
                        NavigationService.Navigate(new MainCouchPage
                            (App.Connection.Users.FirstOrDefault(x => x.FootballClub_Id == _club.FootballClub_Id)));
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Club does not have enough budget to buy player", 
                            "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                NavigationService.Navigate(new FootballersPage(_club, (FootballClubs) lvClubs.SelectedItem));
            }
        }

        private void ChangingCharacteristics()
        {
            _footballer.Characterics.Pace
                += Cnanging(_footballer.Characterics.Pace);

            _footballer.Characterics.Shooting
                += Cnanging(_footballer.Characterics.Shooting);

            _footballer.Characterics.Passing
                += Cnanging(_footballer.Characterics.Passing);

            _footballer.Characterics.Dribbling
                += Cnanging(_footballer.Characterics.Dribbling);

            _footballer.Characterics.Deffending
               += Cnanging(_footballer.Characterics.Deffending);

            _footballer.Characterics.Physicality
               += Cnanging(_footballer.Characterics.Physicality);

            _footballer.TransferCost += _points * 100000;
            _points = 0;
        }

        private int Cnanging(int points)
        {
            Random rnd = new Random();
            int p = rnd.Next(-3, 4);

            if(points + p > 99)
                p = 0;
            
            else
                _points += p;

            return p;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainCouchPage
                (App.Connection.Users.FirstOrDefault(x => x.FootballClub_Id == _club.FootballClub_Id)));
        }
    }
}
