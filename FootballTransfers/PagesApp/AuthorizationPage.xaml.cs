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
using FootballTransfers.PagesApp.PagesAdmin;
using FootballTransfers.PagesApp.PagesCouch;

namespace FootballTransfers.PagesApp
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void RegistrationNavigate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void Authorization(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && pbPassword.Password != "")
            {
                var account = App.Connection.Accounts.FirstOrDefault(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);
                if (account != null)
                {
                    if(account.Users.Role_Id == 1)
                    {
                        NavigationService.Navigate(new MainAdminPage());
                    }
                    
                    if(account.Users.Role_Id == 2)
                    {
                        NavigationService.Navigate(new MainCouchPage(App.Connection.Users.FirstOrDefault(x => x.User_Id == account.User_Id)));
                    }
                }
                else
                {
                    MessageBox.Show("Not have user with written login!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Fill in login and password!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
