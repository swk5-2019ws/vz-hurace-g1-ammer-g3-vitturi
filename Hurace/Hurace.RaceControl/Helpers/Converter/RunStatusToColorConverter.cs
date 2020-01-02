using Hurace.Domain;
using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Hurace.RaceControl.Helpers.Converter
{
    class RunStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((RunStatus)value)
            {
                case RunStatus.Disqualified:
                case RunStatus.NotStarted:
                case RunStatus.Unfinished:
                    return new SolidColorBrush(Color.FromArgb(255, 52, 152, 219));
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
