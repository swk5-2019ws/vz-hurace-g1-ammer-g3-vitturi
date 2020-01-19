using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Hurace.RaceControl.Helpers.Converter
{
    internal class ScreenTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (targetType == typeof(string))
                switch ((ScreenType) value)
                {
                    case ScreenType.CurrentResult:
                        return "Show the current result";
                    case ScreenType.CurrentSkier:
                        return "Show the current skier";
                    case ScreenType.FutureScreens:
                        return "More screens in the future";
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value));
                }

            if (targetType == typeof(ImageSource))
                switch ((ScreenType) value)
                {
                    case ScreenType.CurrentResult:
                        return new BitmapImage(new Uri("ms-appx:///Assets/CurrentResult.png"));
                    case ScreenType.CurrentSkier:
                        return new BitmapImage(new Uri("ms-appx:///Assets/CurrentSkier.png"));
                    case ScreenType.FutureScreens:
                        return new BitmapImage(new Uri("ms-appx:///Assets/FutureScreens.png"));
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value));
                }

            throw new ArgumentOutOfRangeException(nameof(targetType));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}