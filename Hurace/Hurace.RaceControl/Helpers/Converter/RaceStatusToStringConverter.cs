using Hurace.Domain;
using System;
using Windows.UI.Xaml.Data;

namespace Hurace.RaceControl.Helpers.Converter
{
    public class RaceStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((RaceStatus)value)
            {
                case RaceStatus.Finished:
                    return "Finished";
                case RaceStatus.InProgress:
                    return "In Progress";
                case RaceStatus.Ready:
                    return "Ready";
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