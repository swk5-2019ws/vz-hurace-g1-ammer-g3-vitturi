﻿using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Hurace.RaceControl.Helpers.Converter
{
    class TimeSpanToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double time = (double)value;
            var brush = new AcrylicBrush();
            brush.BackgroundSource = AcrylicBackgroundSource.Backdrop;
            brush.TintOpacity = 0.6;

            if (time > 0.0)
            {
                brush.TintColor = Color.FromArgb(255, 255, 40, 37);
                brush.FallbackColor = Color.FromArgb(255, 255, 40, 37);
            }
            else
            {
                brush.TintColor = Color.FromArgb(255, 0, 179, 65);
                brush.FallbackColor = Color.FromArgb(255, 0, 179, 65);
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
