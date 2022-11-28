using Microsoft.Win32;
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

namespace FootballTransfers.PageApp.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для FootballClubRegistrationPage.xaml
    /// </summary>
    public partial class FootballClubRegistrationPage : Page
    {
        private byte[] _logo;
        public FootballClubRegistrationPage()
        {
            InitializeComponent();
        }

        private void SelectLogo(object sender, RoutedEventArgs e)
        {
            try
            {
                var BtnSelect = sender as Button;
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                    _logo = File.ReadAllBytes(dialog.FileName);
                    BtnSelect.Background = Brushes.Green;
                }
            }
            catch
            {
                MessageBox.Show("Only image!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveFootballClub(object sender, RoutedEventArgs e)
        {
            if (_logo != null && tbName.Text != "" && tbBudget.Text != "")
            {
                if(!CheckNumber(tbBudget.Text))
                {
                    MessageBox.Show("Budget is number!", "Error!", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                FootballClubs newFootballClub = new FootballClubs()
                {
                    Name = tbName.Text,
                    Budget = int.Parse(tbBudget.Text),
                    Logo = _logo
                };

                App.Connection.FootballClubs.Add(newFootballClub);
                App.Connection.SaveChanges();
            }
            else
            {
                MessageBox.Show("Fill in all fields!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CheckNumber(string s)
        {
            return int.TryParse(s, out var number) && number >= 0;
        }
    }
}
