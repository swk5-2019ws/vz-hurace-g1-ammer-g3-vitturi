using System;
using Windows.UI.Xaml.Data;
using Hurace.Domain;

namespace Hurace.RaceControl.Helpers
{
    internal class RaceTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((RaceType) value)
            {
                case RaceType.Slalom:
                    return "Slalom";
                case RaceType.SuperSlalom:
                    return "Super Slalom";
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