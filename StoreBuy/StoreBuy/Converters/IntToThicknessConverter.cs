using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.Converters
{
    
    [Preserve(AllMembers = true)]
    public class IntToThicknessConverter : IValueConverter
    {
     
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int itemCount;
                int.TryParse(value.ToString(), out itemCount);
                if (itemCount >= 0)
                {
                    return new Thickness(0, -15, 0, 0);
                }
            }

            return new Thickness(0);
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
