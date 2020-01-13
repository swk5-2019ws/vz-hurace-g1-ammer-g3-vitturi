using Hurace.Domain;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Hurace.RaceControl.Helpers.Converter
{
    public class RaceStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var element = (string)parameter;
            switch ((RaceStatus)value)
            {
                case RaceStatus.Finished:
                    switch (element)
                    {
                        case "SkierAutoSuggestBox":
                        case "StartButton":
                        case "ControlButton":
                            return Visibility.Collapsed;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(value));
                    }
                case RaceStatus.InProgress:
                    switch (element)
                    {
                        case "SkierAutoSuggestBox":
                        case "StartButton":
                            return Visibility.Collapsed;
                        case "ControlButton":
                            return Visibility.Visible;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(value));
                    }
                case RaceStatus.Ready:
                    switch (element)
                    {
                        case "SkierAutoSuggestBox":
                        case "StartButton":
                            return Visibility.Visible;
                        case "ControlButton":
                            return Visibility.Collapsed;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(value));
                    }
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
