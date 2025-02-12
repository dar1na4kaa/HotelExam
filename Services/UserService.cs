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
        private readonly HotelDbContext _context;

        public UserService()
        {
            _context = new HotelDbContext();
        }

        public string LogIn(string login, string password)
        {
            var user = _context.Users
                               .FirstOrDefault(u => u.Login == login && VerifyPassword(password,u.PasswordHash));

            if (user != null)
            {
                if (user.IsBlocked)
                {
                    return "Вы заблокированы. Обратитесь к администратору.";
                }

                user.LastLogin = DateTime.Now;
                user.FailedLoginAttempt = 0;
                _context.SaveChanges();

                return "Вы успешно авторизовались!";
            }
            else
            {
                var failedUser = _context.Users.FirstOrDefault(u => u.Login == login);
                if (failedUser != null)
                {
                    failedUser.FailedLoginAttempt++;
                    if (failedUser.FailedLoginAttempt >= 3)
                    {
                        failedUser.IsBlocked = true;


                    }
                    _context.SaveChanges();
                }

                return "Вы ввели неверный логин или пароль. Пожалуйста, проверьте ещё раз введенные данные.";
            }
        }
        private string HashPassword(string inputPassword)
        {
            using(SHA256  sha256 = SHA256.Create()) {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
        public bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            string inputPasswordHash = HashPassword(inputPassword);
            return inputPasswordHash == storedPasswordHash;
        }
    }
}
