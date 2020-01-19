using System;
using Windows.UI.Xaml.Data;

namespace Hurace.RaceControl.Helpers.Converter
{
    internal class DoubleTimeFormatConverter : IValueConverter
    {
        public string StringFormat { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = "";

            if (value == null) return null;

            if (value is double time) result = TimeSpan.FromMilliseconds(time).ToString(StringFormat);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}