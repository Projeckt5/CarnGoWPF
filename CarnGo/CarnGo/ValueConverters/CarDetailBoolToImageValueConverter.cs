using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Controls;
using UserControl = System.Windows.Controls.UserControl;

namespace CarnGo
{
    public class CarDetailBoolToImageValueConverter:MarkupExtension,IValueConverter
    {
        public string CheckIcon { get; set; }
        public string BanIcon { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if ((bool)value)
                return CheckIcon;
            else        
                return BanIcon;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ApplicationPageValueConverter();
        }
    }
}
