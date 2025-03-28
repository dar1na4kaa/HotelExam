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
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        private readonly string _selectedLogin;
        private readonly UserService _userService;
        public EditUserWindow(string selectedLogin)
        {
            InitializeComponent();

            _selectedLogin = selectedLogin;
            _userService = new UserService();

            LoadData();

        }

        private void LoadData()
        {
            var user = _userService.GetUserByLogin(_selectedLogin);

            try
            {
                if (user != null)
                {
                    LastNameBox.Text = user.LastName;
                    NameBox.Text = user.FirstName;
                    LoginBox.Text = user.Login;
                    BlockedSpace.Visibility = (bool) user.IsBlocked ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Выбранного пользователя не существует. Попробуйте снова");

                    AdministratorWindow administratorWindow = new AdministratorWindow();
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при выгрузке данных. Попробуйте снова");

                AdministratorWindow administratorWindow = new AdministratorWindow();
                this.Close();
            }
        }
        private void UnnbannedUserClick(object sender, RoutedEventArgs e)
        {
            string result = _userService.UnbannedUser(_selectedLogin);

            MessageBox.Show(result);

            if (result.Equals("Пользователь успешно разблокирован"))
            {
                AdministratorWindow administratorWindow = new AdministratorWindow();
                administratorWindow.Show();
                this.Close();
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if(!DataChecker.CheckLogin(LoginBox.Text.Trim()) || !DataChecker.CheckText(NameBox.Text.Trim()) ||
               !DataChecker.CheckText(LastNameBox.Text.Trim()))
            {
                string result = _userService.UpdateUserInformation(LoginBox.Text.Trim(), LastNameBox.Text.Trim(), NameBox.Text.Trim(), _selectedLogin);

                MessageBox.Show(result);

                if (result.Equals("Успешно обновлены данные пользователя"))
                {
                    AdministratorWindow administratorWindow = new AdministratorWindow();
                    administratorWindow.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Введите все данные и уберите запрещенные символы");
            }
        }
    }
}
