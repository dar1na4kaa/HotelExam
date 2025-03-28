﻿using HotelPairs.Services;
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

namespace HotelPairs.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserService _userService;
        public LoginWindow()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void LogInClick(object sender, RoutedEventArgs e)
        {
            if (!DataChecker.CheckLogin(LoginBox.Text.Trim()) || !DataChecker.CheckPassword(PasswordBox.Text))
            {
                MessageBox.Show("Неправильные введеные данные. Ввведите все данные и уберите запрещенные символы");
            }
            else
            {
                string loginText = LoginBox.Text.Trim();
                string passwordText = PasswordBox.Text.Trim();

                string result = _userService.LogUser(loginText, passwordText);
                MessageBox.Show(result);

                if (result.Equals("Вы успешно авторизовались!"))
                {
                    if (_userService.IsNewUser(loginText))
                    {
                        RechangePasswordWindow rechangePasswordWindow = new RechangePasswordWindow(loginText);
                        rechangePasswordWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        AdministratorWindow administrator = new AdministratorWindow();
                        administrator.Show();
                        this.Close();
                    }
                }
            }   
        }
    }
}
