using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Hurace.RaceControl.Helpers.Converter
{
    public class StringToAbbreviationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string name = (string) value;
            return $"{name.ToUpper()[0]}.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}