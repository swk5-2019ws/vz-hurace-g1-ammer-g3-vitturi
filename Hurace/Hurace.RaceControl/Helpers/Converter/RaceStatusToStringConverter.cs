using System;
using Windows.UI.Xaml.Data;
using Hurace.Domain;

namespace Hurace.RaceControl.Helpers
{
    public class RaceStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((RaceStatus) value)
            {
                case RaceStatus.Finished:
                    return "Finished";
                case RaceStatus.InProgress:
                    return "In Progress";
                case RaceStatus.Ready:
                    return "Ready";
                default:
                    throw new NotSupportedException("value not supported");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}