﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace FootballTransfers.PagesApp.PagesCouch
{
    /// <summary>
    /// Логика взаимодействия для FotballerCardPage.xaml
    /// </summary>
    public partial class FootballerCardPage : Page
    {

        private Footballers _footballer;
        private FootballClubs _club;
        private FootballClubs _clubOfSale;
        private int _points = 0;

        public FootballerCardPage(Footballers footballer)
        {
            InitializeComponent();
            imgPhoto.Source = BytesToImage(footballer.Photo);
            tbFullName.Text += footballer.FullName;
            tbPosition.Text += footballer.Position;
            tbTransferCost.Text += footballer.TransferCost.ToString();
            tbFootballClub.Text += App.Connection.FootballClubs.
                FirstOrDefault(x => x.FootballClub_Id == footballer.FootballClub_Id).Name;
            tbNationally.Text += footballer.Nationality;
            tbPace.Text += footballer.Characterics.Pace;
            tbShooting.Text += footballer.Characterics.Shooting;
            tbPassing.Text += footballer.Characterics.Passing;
            tbDribbling.Text += footballer.Characterics.Dribbling;
            tbDeffending.Text += footballer.Characterics.Deffending;
            tbPhysicality.Text += footballer.Characterics.Physicality;

            _footballer = footballer;
        }

        public FootballerCardPage(FootballClubs club, FootballClubs clubOfSale, Footballers footballer) : this(footballer)
        {
            _club = club;
            _clubOfSale = clubOfSale;
            
            btnBuy.Visibility = Visibility.Visible;
            tbBalance.Visibility = Visibility.Visible;
            tbBalanceValue.Visibility = Visibility.Visible;
            tbBalanceValue.Text = club.Budget.ToString();   
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

        private void BuyFootballer(object sender, RoutedEventArgs e)
        {
            DialogResult dialogResult = System.Windows.Forms.
                MessageBox.Show("You confirm purchase?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (_club.Budget >= _footballer.TransferCost)
                {
                    _clubOfSale.Budget += _footballer.TransferCost;
                    _club.Budget -= _footballer.TransferCost;

                    TransfersHistory transfer = new TransfersHistory()
                    {
                        Footballer_Id = _footballer.Footballer_Id,
                        Characteristic_Id = _footballer.Characterics.Characteristic_Id,
                        ClubOfSale = _clubOfSale.FootballClub_Id,
                        ClubOfPurchase = _club.FootballClub_Id,
                        TransferCost = _footballer.TransferCost,
                        DateAndTime = DateTime.Now
                    };

                    ChangingCharacteristics();
                    _footballer.FootballClub_Id = _club.FootballClub_Id;
                    App.Connection.TransfersHistory.Add(transfer);
                    App.Connection.SaveChanges();
                    System.Windows.MessageBox.Show("Transfer was successful");
                    NavigationService.Navigate(new MainCouchPage
                        (App.Connection.Users.FirstOrDefault(x => x.FootballClub_Id == _club.FootballClub_Id)));
                }
                else
                {
                    System.Windows.MessageBox.Show("Your club does not have enough budget to buy player",
                        "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

            if (points + p > 99)
                p = 0;

            else
                _points += p;

            return p;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if(_clubOfSale != null)
            {
                NavigationService.Navigate(new FootballersPage(_club, _clubOfSale));
            }
            else
            {
                NavigationService.Navigate(new MainCouchPage(App.Connection.Users.FirstOrDefault
                    (x => x.FootballClub_Id == _footballer.FootballClub_Id)));
            }
        }
    }
}
