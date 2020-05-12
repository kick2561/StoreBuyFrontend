using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using StoreBuy.Controls;
using StoreBuy.ViewModels.Forms;

namespace StoreBuy.Converters
{
    
    [Preserve(AllMembers = true)]
    public class PasswordErrorValidationColorConverter : IValueConverter
    {
       
        public string PageVariantParameter { get; set; }

      
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             
            if (PageVariantParameter == "0")
            {
                var passwordEntry = parameter as BorderlessEntry;

                if (!(passwordEntry?.BindingContext is LoginViewModel bindingContext))
                {
                    return Color.Transparent;
                }

                var isFocused = (bool)value;
                bindingContext.IsInvalidPassword = !isFocused && !CheckValidPassword(bindingContext.Password);

                if (isFocused)
                {
                    return Color.FromRgba(255, 255, 255, 0.6);
                }

                return bindingContext.IsInvalidPassword ? Color.FromHex("#FF4A4A") : Color.Transparent;

            }

          
            else
            {
                var passwordEntry = parameter as BorderlessEntry;

                if (!(passwordEntry?.BindingContext is LoginViewModel bindingContext)) return Color.FromHex("#ced2d9");

                var isFocused1 = (bool)value;
                bindingContext.IsInvalidPassword = !isFocused1 && !CheckValidPassword(bindingContext.Password);

                if (isFocused1)
                {
                    return Color.FromHex("#959eac");
                }

                return bindingContext.IsInvalidPassword ? Color.FromHex("#FF4A4A") : Color.FromHex("#ced2d9");

            }
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
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
            
            if (validConditions !=4)
            {
                return false;
            }

            return true;
        }
    }
}