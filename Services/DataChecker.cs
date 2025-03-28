using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace HotelPairs.Services
{
    public static class DataChecker
    {
        public static bool CheckText(string data)
        {
            if(string.IsNullOrWhiteSpace(data)) return false;
            if(data.Trim().Length < 2 && data.Trim().Length > 50) return false;
            if(!Regex.IsMatch(data, @"[\s\d\w]]")) return false;

            return true;
        }
        public static bool CheckLogin(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;
            if (data.Trim().Length < 2 && data.Trim().Length > 50) return false;
            if (!Regex.IsMatch(data, @"[\s\d\w]")) return false;

            return true;
        }
        public static bool CheckPassword(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return false;
            if (data.Trim().Length < 2 && data.Trim().Length > 50) return false;

            return true;
        }

    }
}
