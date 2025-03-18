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
using System.Windows.Shapes;

namespace HotelPairs
{
    /// <summary>
    /// Логика взаимодействия для RechangePasswordWindow.xaml
    /// </summary>
    public partial class RechangePasswordWindow : Window
    {
        private readonly string _login;
        public RechangePasswordWindow(string login)
        {
            InitializeComponent();
            _login = login;
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
