using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Hurace.Domain;

namespace Hurace.RaceControl.Helpers.Converter
{
    public class RaceStatusToEnabledConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((RaceStatus)value)
            {
                case RaceStatus.Finished:
                case RaceStatus.InProgress:
                    return false;
                case RaceStatus.Ready:
                    return true;
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
