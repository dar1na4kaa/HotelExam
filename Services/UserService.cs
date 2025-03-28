using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelPairs.Services
{
    public class UserService
    {
        private readonly HotelContext _context;

        public UserService()
        {
            _context = new HotelContext();
        }
        public List<User> GetUsers()
        {
            return _context.Users
                .Where(b => b.RoleID == 2)
                .ToList();
        }
        public User GetUserByLogin(string login)
        {
            return _context.Users.FirstOrDefault(b => b.Login.Equals(login));
        }
        public User AddUser(string login, string passwordHash, string lastName, string firstName, int roleID)
        {
            return new User
            {
                Login = login,
                PasswordHash = passwordHash,
                LastName = lastName,
                FirstName = firstName,
                RoleID = roleID
            };
        }
        public string RegistrateUser(string login, string password, string lastname, string name, string role)
        {
            var user = AddUser(login, password, lastname, name, role.Equals("Администратор") ? 1 : 2);

            try
            {
                if (GetUserByLogin(login) != null)
                    return "Пользователь с таким логином уже зарегистрирован";

                _context.Users.Add(user);
                _context.SaveChanges();

                return $"Пользователь {lastname} {name} успешно зарегистрирован";
            }
            catch
            {
                return "Произошла ошибка при регистрации пользователя, повторите попытку";
            }
        }
        public string LogUser(string login, string password)
        {
            var user = GetUserByLogin(login);

            if (user == null)
                return "Пользователя с таким логиным не существует. Пожалуйста, проверьте ещё раз введенные данные.";

            if ((bool)user.IsBlocked)
                return "Вы заблокированы.Вы ввели 3 раза неправильный пароль. Обратитесь к администратору.";

            if (VerifyPassword(password, user.PasswordHash))
            {
                user.FailedLoginAttempts = 0;
                user.LastLoginAttempt = DateTime.Now;
                _context.SaveChanges();

                return "Вы успешно авторизовались!";
            }
            else
            {
                user.FailedLoginAttempts++;
                if (user.FailedLoginAttempts >= 3)
                {
                    user.IsBlocked = true;
                }
                _context.SaveChanges();
            }
            return "Вы ввели неверный пароль. Пожалуйста, проверьте ещё раз введенные данные.";
        }
        public string UpdateUserInformation(string oldLogin, string lastname, string name,string newLogin)
        {
            var existingUser = GetUserByLogin(oldLogin);

            try
            {
                if (existingUser == null) return "Выбранного пользователя не существует. Пожалуйста, попробуйте снова";

                if (!oldLogin.Equals(newLogin))
                {
                    if (GetUserByLogin(newLogin) != null) return "Пользователь с таким логином уже зарегистрирован";
                }

                existingUser.LastName = lastname;
                existingUser.FirstName = name;
                existingUser.Login = newLogin;
                _context.SaveChanges();

                return "Успешно обновлены данные пользователя";
            }
            catch
            {
                return "Произошла ошибка при обновлении данных. Пожалуйста, попробуйте снова";
            }
        }
        public string UnbannedUser(string login)
        {
            var existingUser = GetUserByLogin(login);

            try
            {
                if (existingUser == null) return "Выбранного пользователя не существует. Пожалуйста, попробуйте снова";

                existingUser.IsBlocked = false;
                existingUser.FailedLoginAttempts = 0;
                _context.SaveChanges();

                return "Пользователь успешно разблокирован";
            }
            catch
            {
                return "Произошла ошибка при обновлении данных. Пожалуйста, попробуйте снова";
            }
        }
        public string ChangePasswordUser(string login, string oldPassword, string newPassword)
        {
            var user = GetUserByLogin(login);

            if (VerifyPassword(oldPassword, user.PasswordHash))
            {
                user.PasswordHash = newPassword;
                user.LastLoginAttempt = DateTime.Now;
                _context.SaveChanges();

                return "Вы успешно поменяли пароль";
            }
            else
            {
                return "Вы ввели неверный старый пароль. Пожалуйста, проверьте ещё раз введенные данные";
            }
        }

        public bool IsNewUser(string login)
        {
            return !_context.Users.First(u => u.Login == login).LastLoginAttempt.HasValue;
        }
        private string HashPassword(string inputPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
        public bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            string inputPasswordHash = HashPassword(inputPassword);
            return /*inputPasswordHash == storedPasswordHash*/ true;
        }
    }
}
