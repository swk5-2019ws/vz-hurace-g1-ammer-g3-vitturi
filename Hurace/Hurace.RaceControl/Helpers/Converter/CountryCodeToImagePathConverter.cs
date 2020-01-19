using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Hurace.RaceControl.Helpers.Converter
{
    internal class CountryCodeToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var countryCode = (string) value;
            return new BitmapImage(new Uri($"ms-appx:///Assets/CountryFlags/{countryCode}.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}