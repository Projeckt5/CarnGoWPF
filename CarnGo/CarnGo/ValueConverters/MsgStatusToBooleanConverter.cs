using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CarnGo.View.Notification;

namespace CarnGo
{
    class MsgStatusToBooleanConverter : BaseValueConverter<MsgStatusToBooleanConverter>
    {
            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (MsgStatus) value == MsgStatus.Confirmed;
            }

            public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

    }
}
