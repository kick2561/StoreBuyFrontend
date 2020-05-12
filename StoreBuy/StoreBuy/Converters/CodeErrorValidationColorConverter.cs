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
    public class CodeErrorValidationColorConverter : IValueConverter
    {

        public string PageVariantParameter { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (PageVariantParameter == "0")
            {
                var emailEntry = parameter as BorderlessEntry;

                if (!(emailEntry?.BindingContext is LoginViewModel bindingContext))
                {
                    return Color.Transparent;
                }

                var isFocused = (bool)value;
                bindingContext.IsInvalidEmail = !isFocused && !CheckValidEmail(bindingContext.Email);

                if (isFocused)
                {
                    return Color.FromRgba(255, 255, 255, 0.6);
                }

                return bindingContext.IsInvalidEmail ? Color.FromHex("#FF4A4A") : Color.Transparent;

            }


            else
            {
                var emailEntry = parameter as BorderlessEntry;

                if (!(emailEntry?.BindingContext is LoginViewModel bindingContext)) return Color.FromHex("#ced2d9");

                var isFocused1 = (bool)value;
                bindingContext.IsInvalidEmail = !isFocused1 && !CheckValidEmail(bindingContext.Email);

                if (isFocused1)
                {
                    return Color.FromHex("#959eac");
                }

                return bindingContext.IsInvalidEmail ? Color.FromHex("#FF4A4A") : Color.FromHex("#ced2d9");

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }



        private bool CheckValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return true;
            }

            var regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return regex.IsMatch(email) && !email.EndsWith(".");
        }
    }
}