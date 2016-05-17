using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MeetRaffle
{
    class DrawInProgressToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inProgress = (bool)value;
            return inProgress ? Brushes.LightGray : Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
