using Hurace.Simulator;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class SimulatorViewModel : MvxViewModel
    {
        private SimulatorRaceClock _raceClock;

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

        public MvxCommand SendTimerImpulseCommand { get; set; }

        public SimulatorViewModel(SimulatorRaceClock raceClock)
        {
            _raceClock = raceClock;
            SensorNumber = 0;
            SensorCount = 5;
            SendTimerImpulseCommand = new MvxCommand(SendTimerImpulse);
        }

        public void SendTimerImpulse()
        {
            _raceClock.SendImpulse(SensorNumber);
            SensorNumber = (SensorNumber + 1) % SensorCount;
        }
    }
}
