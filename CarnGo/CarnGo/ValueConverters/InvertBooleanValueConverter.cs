using System;
using System.Globalization;

namespace CarnGo
{
    public class InvertBooleanValueConverter : BaseValueConverter<InvertBooleanValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool) value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}