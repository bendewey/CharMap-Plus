using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace CharMap_Plus.Converters
{
    public class BoolToActiveBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; }
        public Brush FalseBrush { get; set; }

        public BoolToActiveBrushConverter()
        {
            TrueBrush = new SolidColorBrush(Colors.Black);
            FalseBrush = new SolidColorBrush(Color.FromArgb(255,32,32,32));
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool && (bool)value)
                return TrueBrush;
            else
                return FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
