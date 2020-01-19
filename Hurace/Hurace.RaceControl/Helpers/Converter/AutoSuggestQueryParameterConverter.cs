using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Hurace.Domain;

namespace Hurace.RaceControl.Helpers.Converter
{
    public class AutoSuggestQueryParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var args = (AutoSuggestBoxQuerySubmittedEventArgs) value;
            return (Race) args.ChosenSuggestion;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}