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

namespace FootballTransfers.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            cmbRoles.ItemsSource = App.Connection.Roles.ToList();
            cmbClubs.ItemsSource = App.Connection.FootballClubs.ToList();
        }

        private void Reverse(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && tbPassword.Text != "" 
                && tbName.Text != "" && cmbRoles.SelectedItem != null)
            {
                var newUser = new Users()
                {
                    FullName = tbName.Text,
                    Role_Id = ((Roles) cmbRoles.SelectedItem).Role_Id
                };
                var newAccount = new Accounts
                {
                    Login = tbLogin.Text,
                    Password = tbPassword.Text
                };

                if (((Roles) cmbRoles.SelectedItem).Name == "Coach")
                {
                    if(cmbClubs.SelectedItem != null)
                    {
                        newUser.FootballClub_Id = 
                            ((FootballClubs)cmbRoles.SelectedItem).FootballClub_Id;
                    }
                    else
                    {
                        MessageBox.Show("Choose football club!", "Error!", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                newUser.Accounts.Add(newAccount);
                App.Connection.Users.Add(newUser);
                App.Connection.Accounts.Add(newAccount);
                App.Connection.SaveChanges();
                MessageBox.Show("Registration success!");
            }
            else
            {
                MessageBox.Show("Fill in all fields!", "Error!",
                            MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Selection(object sender, SelectionChangedEventArgs e)
        {
            if (((Roles)cmbRoles.SelectedItem).Role_Id == 2)
            {
                tbFootballClub.Visibility = Visibility.Visible;
                cmbClubs.Visibility = Visibility.Visible;
            }
            else
            {
                tbFootballClub.Visibility = Visibility.Hidden;
                cmbClubs.Visibility = Visibility.Hidden;
            }
        }
    }
}
