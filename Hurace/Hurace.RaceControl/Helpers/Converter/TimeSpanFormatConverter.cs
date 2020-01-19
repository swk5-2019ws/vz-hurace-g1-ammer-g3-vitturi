using System;
using Windows.UI.Xaml.Data;

namespace Hurace.RaceControl.Helpers.Converter
{
    internal class TimeSpanFormatConverter : IValueConverter
    {
        public string StringFormat { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var result = "";

            if (value == null) return null;

            if (value is TimeSpan timeSpan)
            {
                result = timeSpan.ToString(StringFormat);
                if (timeSpan.TotalMilliseconds < 0) result = "-" + result;
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}