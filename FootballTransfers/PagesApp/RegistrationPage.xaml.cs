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

namespace FootballTransfers.PagesApp
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
            var clubs = new List<FootballClubs>();
            var allClubs = App.Connection.FootballClubs.ToList();
            var coaches = App.Connection.Users.Where(x => x.FootballClub_Id != null).ToList();
            var idClubs = new List<int>();

            foreach(var coach in coaches)
            {
                idClubs.Add(Convert.ToInt32(coach.FootballClub_Id));
            }

            foreach(var club in allClubs)
            {
                if (idClubs.Contains(club.FootballClub_Id))
                    continue;

                clubs.Add(club);
;            }

            cmbClubs.ItemsSource = clubs;
        }

        private void Reverse(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && tbPassword.Text != "" 
                && tbFullName.Text != "" && cmbRoles.SelectedItem != null)
            {
                if(!CheckNewLogin(tbLogin.Text))
                {
                    MessageBox.Show("User with this login already exists!", "Error!",
                           MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var newUser = new Users()
                {
                    FullName = tbFullName.Text,
                    Role_Id = ((Roles) cmbRoles.SelectedItem).Role_Id
                };
                var newAccount = new Accounts
                {
                    Login = tbLogin.Text,
                    Password = tbPassword.Text
                };

                if (((Roles) cmbRoles.SelectedItem).Role_Id == 2)
                {
                    if(cmbClubs.SelectedItem != null)
                    {
                        newUser.FootballClub_Id = 
                            ((FootballClubs)cmbClubs.SelectedItem).FootballClub_Id;
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
                ClearForm();
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
            if (cmbRoles.SelectedItem != null && ((Roles)cmbRoles.SelectedItem).Role_Id == 2)
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

        private void ClearForm()
        {
            tbFullName.Text = "";
            tbLogin.Text = "";
            tbPassword.Text = "";
            cmbRoles.SelectedItem = null;
            cmbClubs.SelectedItem = null;
        }

        private bool CheckNewLogin(string login)
        {
            var user = App.Connection.Accounts.FirstOrDefault(x => x.Login == login);

            return user == null;
        }
    }
}
