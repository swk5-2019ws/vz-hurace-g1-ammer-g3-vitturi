using System;
using Windows.UI.Xaml;
using Hurace.Simulator;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class SimulatorControlViewModel : MvxViewModel
    {
        private bool _automaticallySend;
        private readonly DispatcherTimer _dispatcherTimer;

        private bool _inputEnabled;
        private readonly SimulatorRaceClock _raceClock;

        private int _sendInterval;

        private int _sensorCount;

        private int _sensorNumber;

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

        public bool InputEnabled
        {
            get => _inputEnabled;
            set => SetProperty(ref _inputEnabled, value);
        }

        public int SensorNumber
        {
            get => _sensorNumber;
            set => SetProperty(ref _sensorNumber, value);
        }

        public int SensorCount
        {
            get => _sensorCount;
            set => SetProperty(ref _sensorCount, value);
        }

        public int SendInterval
        {
            get => _sendInterval;
            set => SetProperty(ref _sendInterval, value);
        }

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