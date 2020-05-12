using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StoreBuy.Converters
{
    [Preserve(AllMembers = true)]
    public class BooleanToStringConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                if (parameter.ToString() == "0")
                {
                    if ((bool)value)
                    {
                        return "\ue72f";
                    }
                    else
                    {
                        return "\ue734";
                    }
                }
                else if (parameter.ToString() == "1")
                {
                    if ((bool)value)
                    {
                        return "\ue732";
                    }
                    else
                    {
                        return "\ue701";
                    }
                }
                else if (parameter.ToString() == "2")
                {
                    if ((bool)value)
                    {
                        return "+";
                    }
                    else
                    {
                        return "-";
                    }
                }
            }

            return string.Empty;
        }

     
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
