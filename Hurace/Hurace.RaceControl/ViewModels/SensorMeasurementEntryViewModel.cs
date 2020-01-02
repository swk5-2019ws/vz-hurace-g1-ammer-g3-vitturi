﻿using MvvmCross.ViewModels;
using System;

namespace Hurace.RaceControl.ViewModels
{
    public class SensorMeasurementEntryViewModel : MvxViewModel
    {
        private TimeSpan _timeSpan;

        public TimeSpan TimeSpan
        {
            get => _timeSpan;
            set => SetProperty(ref _timeSpan, value);
        }
    }
}
