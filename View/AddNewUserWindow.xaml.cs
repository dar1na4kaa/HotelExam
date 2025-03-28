using System.Windows;
using HotelPairs.Services;

namespace HotelPairs.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewUserWindow.xaml
    /// </summary>
    public partial class AddNewUserWindow : Window
    {
        private readonly UserService _userService;
        public AddNewUserWindow()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void RegInClick(object sender, RoutedEventArgs e)
        {
            if (!DataChecker.CheckText(LastNameBox.Text.Trim()) || !DataChecker.CheckText(NameBox.Text.Trim()) ||
                !DataChecker.CheckLogin(LoginBox.Text.Trim()) || !DataChecker.CheckPassword(PasswordBox.Text.Trim()) ||
                !DataChecker.CheckText(RoleComboBox.Text.Trim()))
            {
                MessageBox.Show("Неправильное заполнение данных. " +
                                "Ввведите все данные и уберите запрещенные символы." +
                                "Пароль должен быть больше 4 символов");
            }
            else
            {
                var result = _userService.RegistrateUser(LoginBox.Text.Trim(),
                                                         NameBox.Text.Trim(),
                                                         LoginBox.Text.Trim(),
                                                         PasswordBox.Text.Trim(),
                                                         RoleComboBox.Text.Trim());
                MessageBox.Show(result);

                AdministratorWindow administratorWindow = new AdministratorWindow();
                administratorWindow.Show();
                this.Close();
            }
        }
    }
}
