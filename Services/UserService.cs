using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        public string LogUser(string login, string password)
        {
            var user = _context.Users
                               .FirstOrDefault(u => u.Login == login);

            if (user == null)
                return "Пользователя с таким логиным не существует. Пожалуйста, проверьте ещё раз введенные данные.";

            if ((bool)user.IsBlocked)
                return "Вы заблокированы. Обратитесь к администратору.";

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
        public void UpdateUser(string login,string lastname, string name, bool isBlocked)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Login == login);

            if (existingUser != null)
            {
                existingUser.LastName = lastname;
                existingUser.FirstName = name;
                existingUser.Login = login;
                existingUser.IsBlocked = isBlocked;

                _context.SaveChanges();
            }
        }
        public string ChangePasswordUser(string login, string oldPassword, string newPassword)
        {
            var user = _context.Users
                               .FirstOrDefault(u => u.Login == login);


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

        public bool isNewUser(string login)
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
