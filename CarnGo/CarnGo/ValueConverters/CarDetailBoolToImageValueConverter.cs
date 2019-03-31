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
    public class CarDetailBoolToImageValueConverter:BaseValueConverter<CarDetailBoolToImageValueConverter>
    {
        public string CheckIcon { get; set; }= "\uf00c";
        public string BanIcon { get; set; } = "\uf05e";


        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool a = (bool)value;
            if (a)
                return CheckIcon;
            return BanIcon;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
