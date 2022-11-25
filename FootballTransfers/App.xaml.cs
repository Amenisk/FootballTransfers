using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FootballTransfers.ADO;

namespace FootballTransfers
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static FootballTransfersEntities Connection = new FootballTransfersEntities();
    }
}
