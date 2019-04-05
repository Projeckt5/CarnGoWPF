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
    class MessageConverter : BaseValueConverter<MessageConverter>
    {
            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value == null)
                    return 0;
                switch ((MessageType) value)
                {
                case MessageType.LessorMessage: 
                    return new LessorItemView();
                case MessageType.RenterMessage:
                    return new RenterItemView();
                default:
                    throw new ArgumentException("The value to convert was not a Messagetype");
            }
            }

            public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }

    }
}
