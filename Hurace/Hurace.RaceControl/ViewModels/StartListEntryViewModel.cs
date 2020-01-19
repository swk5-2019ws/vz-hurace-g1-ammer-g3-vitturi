using Hurace.Domain;
using Hurace.RaceControl.Helpers.MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class StartListEntryViewModel : MvxViewModel
    {
        private readonly IMvxMessenger _messenger;
        private RaceStatus _raceStatus;
        private Skier _skier;
        private int _startPosition;

        public StartListEntryViewModel(IMvxMessenger messenger)
        {
            _messenger = messenger;
            DeleteStartListEntryCommand =
                new MvxCommand(() => _messenger.Publish(new StartListUpdateMessage(this, StartPosition)));
        }

        public int StartPosition
        {
            get => _startPosition;
            set => SetProperty(ref _startPosition, value);
        }

        public Skier Skier
        {
            get => _skier;
            set => SetProperty(ref _skier, value);
        }

        public RaceStatus RaceStatus
        {
            get => _raceStatus;
            set => SetProperty(ref _raceStatus, value);
        }

        public MvxCommand DeleteStartListEntryCommand { get; set; }
    }
}