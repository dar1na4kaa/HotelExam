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
using HotelPairs.Services;

namespace HotelPairs.View
{
    /// <summary>
    /// Логика взаимодействия для AdministratorWindow.xaml
    /// </summary>
    public partial class AdministratorWindow : Window
    {
        private readonly string _login;
        private readonly UserService _userService;
        public AdministratorWindow(string login)
        {
            InitializeComponent();
            _login = login; 
            _userService = new UserService();

            LoadUsers();
        }
        private void LoadUsers()
        {
            var users = _userService.GetUsers();
            listViewUsers.ItemsSource = users;  
        }

        private void OpenAddUserWindow_Click(object sender, RoutedEventArgs e)
        {
            AddNewUserWindow addNewUserWindow = new AddNewUserWindow();
            addNewUserWindow.Show();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        private void OpenEditUserWindow_Click(object sender, MouseButtonEventArgs e)
        {
            if (listViewUsers.SelectedItem is User selectedUser)
            {
                EditUserWindow editWindow = new EditUserWindow(selectedUser.Login);
                editWindow.ShowDialog();  // Открываем окно редактирования
            }
        }

    }
}
