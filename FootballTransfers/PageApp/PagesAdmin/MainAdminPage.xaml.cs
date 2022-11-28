﻿using System;
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
using FootballTransfers.PageApp.PagesAdmin;

namespace FootballTransfers.PageApp.PagesAdmin
{
    /// <summary>
    /// Логика взаимодействия для MainAdminPage.xaml
    /// </summary>
    public partial class MainAdminPage : Page
    {
        public MainAdminPage()
        {
            InitializeComponent();
        }

        private void RegisterFootballClub(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FootballClubRegistrationPage());
        }

        private void RegisterFootballer(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FootballerRegistrationPage());
        }
    }
}