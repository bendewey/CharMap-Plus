using CharMap_Plus.Model;
using System;
using Windows.UI.Xaml.Data;

namespace CharMap_Plus.Converters
{
    public class FontCharConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value as object;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value as FontChar;
        }
    }
}
