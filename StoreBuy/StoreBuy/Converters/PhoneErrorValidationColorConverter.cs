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
    public class PhoneErrorValidationColorConverter : IValueConverter
    {
       
        public string PageVariantParameter { get; set; }

       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (PageVariantParameter == "0")
            {
                var phoneEntry = parameter as BorderlessEntry;

                if (!(phoneEntry?.BindingContext is LoginViewModel bindingContext))
                {
                    return Color.Transparent;
                }

                var isFocused = (bool)value;
                bindingContext.IsInvalidPhone = !isFocused && !CheckValidPhone(bindingContext.Phone);

                if (isFocused)
                {
                    return Color.FromRgba(255, 255, 255, 0.6);
                }

                return bindingContext.IsInvalidPhone ? Color.FromHex("#FF4A4A") : Color.Transparent;

            }

          
            else
            {
                var phoneEntry = parameter as BorderlessEntry;

                if (!(phoneEntry?.BindingContext is LoginViewModel bindingContext)) return Color.FromHex("#ced2d9");

                var isFocused1 = (bool)value;
                bindingContext.IsInvalidPhone = !isFocused1 && !CheckValidPhone(bindingContext.Phone);

                if (isFocused1)
                {
                    return Color.FromHex("#959eac");
                }

                return bindingContext.IsInvalidPhone ? Color.FromHex("#FF4A4A") : Color.FromHex("#ced2d9");

            }
        }

              
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
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