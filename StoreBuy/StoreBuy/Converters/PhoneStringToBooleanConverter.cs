using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using StoreBuy.Controls;

namespace StoreBuy.Converters
{
   
    [Preserve(AllMembers = true)]
    public class PhoneStringToBooleanConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(parameter is BorderlessEntry phone))
            {
                return false;
            }

            var isFocused = (bool)value;
            var isInvalidPhone = !isFocused && !CheckValidPhone(phone.Text);

            return !isFocused && isInvalidPhone;
        }

       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }

       
        private static bool CheckValidPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return true;
            }

            var regex = new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$");
            return regex.IsMatch(phone) && !phone.EndsWith(".");
        }
    }
}