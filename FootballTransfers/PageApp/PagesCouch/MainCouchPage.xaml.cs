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

namespace FootballTransfers.PageApp.PagesCouch
{
    /// <summary>
    /// Логика взаимодействия для MainCouchPage.xaml
    /// </summary>
    public partial class MainCouchPage : Page
    {
        public MainCouchPage(Users couch)
        {
            InitializeComponent();
            imgLogo.Source = couch.FootballClubs.Logo;
        }
    }


}
