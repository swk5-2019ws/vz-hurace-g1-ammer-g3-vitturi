using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class SensorMeasurementEntryViewModel: MvxViewModel
    {
        private TimeSpan _timeSpan;

        public TimeSpan TimeSpan
        {
            get => _timeSpan;
            set => SetProperty(ref _timeSpan, value);
        }
    }
}
