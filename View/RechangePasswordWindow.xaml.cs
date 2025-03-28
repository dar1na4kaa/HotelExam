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
using System.Windows.Shapes;
using HotelPairs.Services;

namespace HotelPairs.View
{
    /// <summary>
    /// Логика взаимодействия для RechangePasswordWindow.xaml
    /// </summary>
    public partial class RechangePasswordWindow : Window
    {
        private readonly string _login;
        private readonly UserService _userService;
        public RechangePasswordWindow(string login)
        {
            InitializeComponent();
            _login = login;
            _userService = new UserService(); 
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            if (ConfirmNewPasswordBox.Password.Equals(NewPasswordBox.Password))
            {
                if (!DataChecker.CheckPassword(NewPasswordBox.Password))
                {
                    MessageBox.Show("Неправильный введеный новый пароль. Пароль должен быть больше 4 символов и меньше 50");
                }
                else
                {
                    string oldPasswordText = OldPasswordBox.Text.Trim();
                    string newPasswordText = NewPasswordBox.Password.Trim();


                    string result = _userService.ChangePasswordUser(_login, oldPasswordText, newPasswordText);
                    MessageBox.Show(result);

                    if (result.Equals("Вы успешно поменяли пароль"))
                    {
                        AdministratorWindow administrator = new AdministratorWindow();
                        administrator.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }
    }
}
