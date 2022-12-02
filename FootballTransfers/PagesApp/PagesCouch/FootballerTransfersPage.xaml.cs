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

namespace FootballTransfers.PagesApp.PagesCouch
{
    /// <summary>
    /// Логика взаимодействия для FootballerTransfersPage.xaml
    /// </summary>
    public partial class FootballerTransfersPage : Page
    {
        private Footballers _footballer;
        private FootballClubs _club;
        private FootballClubs _clubOfSale;
        public FootballerTransfersPage(Footballers footballer)
        {
            InitializeComponent();
            _footballer = footballer;
            tbFullName.Text = footballer.FullName;
            lvTransfers.ItemsSource = App.Connection.TransfersHistory.
                Where(x => x.Footballer_Id == _footballer.Footballer_Id).ToList();
        }

        public FootballerTransfersPage(FootballClubs club, 
            FootballClubs clubOfSale, Footballers footballer) : this(footballer)
        {
            _club = club;
            _clubOfSale = clubOfSale;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if(_club != null)
            {
                NavigationService.Navigate(new FootballerCardPage(_club, _clubOfSale, _footballer));
            }
            else
            {
                NavigationService.Navigate(new FootballerCardPage(_footballer));
            }
            
        }
    }
}
