using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using StoreBuy.Controls;

namespace StoreBuy.Converters
{
    
    [Preserve(AllMembers = true)]
    public class PasswordStringToBooleanConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(parameter is BorderlessEntry password))
            {
                return false;
            }

            var isFocused = (bool)value;
            var isInvalidPassword = !isFocused && !CheckValidPassword(password.Text);

            return !isFocused && isInvalidPassword;
        }

   
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }

        
        private static bool CheckValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return true;
            }

            if (password.Length < 8)
                return false;
            int validConditions = 0;
            foreach (char c in password)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in password)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in password)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }

            char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };
            if (password.IndexOfAny(special) != -1)
                validConditions++;

            if (validConditions != 4)
            {
                return false;
            }

            return true;
        }
    }
}