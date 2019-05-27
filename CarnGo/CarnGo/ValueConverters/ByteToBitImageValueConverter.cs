using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;

namespace CarnGo
{
    public class ByteToBitImageValueConverter : BaseValueConverter<ByteToBitImageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is byte[] image))
                return default;

            var bm = new BitmapImage();
            using (var stream = new MemoryStream(image))
            {
                bm.BeginInit();
                bm.StreamSource = stream;
                bm.CacheOption = BitmapCacheOption.OnLoad;
                bm.EndInit();
                bm.Freeze();
            }

            return bm;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}