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
    /// Логика взаимодействия для FootballerRegistrationPage.xaml
    /// </summary>
    public partial class FootballerRegistrationPage : Page
    {
        private byte[] _photo;

        public FootballerRegistrationPage()
        {
            InitializeComponent();
        }

        private void SelectPhoto(object sender, RoutedEventArgs e)
        {
            try
            {
                var BtnSelect = sender as Button;
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() != null)
                {
                    _photo = File.ReadAllBytes(dialog.FileName);
                    BtnSelect.Background = Brushes.Green;
                }
            }
            catch
            {
                MessageBox.Show("Only photo!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveFootballClub(object sender, RoutedEventArgs e)
        {
            if (СheckingForEmptiness())
            {
                if(!CheckingCorrectnessOfInput())
                {
                    return;
                }

                Footballers newFootballer = new Footballers()
                {
                    Photo = _photo,
                    FullName = tbFullName.Text,
                    TransferCost = int.Parse(tbTransferCost.Text),
                    FootballClub_Id = ((FootballClubs) cmbClubs.SelectedItem).FootballClub_Id
                };

                Characterics newCharacteristic = new Characterics()
                {
                    Pace = int.Parse(tbPace.Text),
                    Shooting = int.Parse(tbShooting.Text),
                    Passing = int.Parse(tbPassing.Text),
                    Dribbling = int.Parse(tbDribbling.Text),
                    Deffending = int.Parse(tbDeffending.Text),
                    Physicality = int.Parse(tbPhysicality.Text)
                };
                newCharacteristic.Footballers.Add(newFootballer);
                App.Connection.Characterics.Add(newCharacteristic);
                App.Connection.Footballers.Add(newFootballer);
                App.Connection.SaveChanges();
            }
            else
            {
                MessageBox.Show("Fill in all fields!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool СheckingForEmptiness()
        {
            return _photo != null && tbFullName.Text != "" && tbTransferCost.Text != ""
                && cmbClubs.SelectedItem != null 
                && tbPace.Text != "" && tbShooting.Text != "" && tbPassing.Text != "" 
                && tbDribbling.Text != "" && tbDeffending.Text != "" && tbPhysicality.Text != "";
        }
        private bool CheckNumber(string s)
        {
            return int.TryParse(s, out var number) && number >= 0;
        }

        private bool CheckingCorrectnessOfInput()
        {
            if(!CheckNumber(tbPace.Text))
            {
                MessageBox.Show("Pace is number!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckNumber(tbShooting.Text))
            {
                MessageBox.Show("Shooting is number!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckNumber(tbPassing.Text))
            {
                MessageBox.Show("Passing is number!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckNumber(tbDribbling.Text))
            {
                MessageBox.Show("Dribbling is number!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckNumber(tbDeffending.Text))
            {
                MessageBox.Show("Deffending is number!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckNumber(tbPhysicality.Text))
            {
                MessageBox.Show("Physicality is number!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!CheckNumber(tbTransferCost.Text))
            {
                MessageBox.Show("TransferCost is number!", "Error!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }


    }
}
