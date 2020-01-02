using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Hurace.RaceControl.Helpers.Converter
{
    class CountryCodeToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string countryCode = (string)value;
            return new BitmapImage(new Uri($"ms-appx:///Assets/CountryFlags/{countryCode}.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
