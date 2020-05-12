using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.Converters
{
    
    [Preserve(AllMembers = true)]
    public class DynamicResourceToColorConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DynamicResource dynamicResource))
            {
                return value;
            }

            Application.Current.Resources.TryGetValue(dynamicResource.Key, out var color);
            return (Color)color;
        }

            
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}