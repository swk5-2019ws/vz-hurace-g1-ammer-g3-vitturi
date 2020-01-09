﻿using Hurace.Simulator;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using Windows.UI.Xaml;

namespace Hurace.RaceControl.ViewModels
{
    public class SimulatorControlViewModel : MvxViewModel
    {
        private SimulatorRaceClock _raceClock;
        DispatcherTimer _dispatcherTimer;

        private bool _inputEnabled;
        public bool InputEnabled
        {
            get => _inputEnabled;
            set => SetProperty(ref _inputEnabled, value);
        }

        private int _sensorNumber;
        public int SensorNumber
        {
            get => _sensorNumber;
            set => SetProperty(ref _sensorNumber, value);
        }

        private int _sensorCount;
        public int SensorCount
        {
            get => _sensorCount;
            set => SetProperty(ref _sensorCount, value);
        }

        private int _sendInterval;
        public int SendInterval
        {
            get => _sendInterval;
            set => SetProperty(ref _sendInterval, value);
        }

        private bool _automaticallySend;
        public bool AutomaticallySend
        {
            get => _automaticallySend;
            set
            {
                if (value)
                {
                    InputEnabled = false;
                    _dispatcherTimer.Interval = TimeSpan.FromSeconds(_sendInterval);
                    _dispatcherTimer.Start();
                }
                else
                {
                    InputEnabled = true;
                    _dispatcherTimer.Stop();
                }

                SetProperty(ref _automaticallySend, value);
            }
        }

        public MvxCommand SendTimerImpulseCommand { get; set; }

        public SimulatorControlViewModel(SimulatorRaceClock raceClock)
        {
            _raceClock = raceClock;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += dispatcherTimer_Tick;

            SensorNumber = 0;
            SensorCount = 5;
            SendInterval = 10;
            InputEnabled = true;

            SendTimerImpulseCommand = new MvxCommand(SendTimerImpulse);
        }

        public void SendTimerImpulse()
        {
            _raceClock.SendImpulse(SensorNumber);
            SensorNumber = (SensorNumber + 1) % SensorCount;
        }

        private void dispatcherTimer_Tick(object sender, object e)
        {
            SendTimerImpulse();
        }
    }
}
