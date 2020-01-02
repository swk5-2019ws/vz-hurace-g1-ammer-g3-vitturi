using Hurace.Domain;
using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Hurace.RaceControl.Helpers.Converter
{
    public class RaceStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((RaceStatus)value)
            {
                case RaceStatus.Finished:
                    return new SolidColorBrush(Color.FromArgb(255, 231, 76, 60));
                case RaceStatus.InProgress:
                    return new SolidColorBrush(Color.FromArgb(255, 46, 204, 113));
                case RaceStatus.Ready:
                    return new SolidColorBrush(Color.FromArgb(255, 52, 152, 219));
                default:
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}