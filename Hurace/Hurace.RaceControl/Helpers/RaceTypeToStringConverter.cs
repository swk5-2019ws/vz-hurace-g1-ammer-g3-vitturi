using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Hurace.Domain;

namespace Hurace.RaceControl.Helpers
{
    class RaceTypeToStringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((RaceType)value)
            {
                case RaceType.Slalom:
                    return "Slalom";
                case RaceType.SuperSlalom:
                    return "Super Slalom";
                default:
                    throw new NotImplementedException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
