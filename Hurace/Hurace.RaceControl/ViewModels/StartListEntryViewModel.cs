using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Hurace.Domain;
using Hurace.RaceControl.Helpers.MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugins.Messenger;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class StartListEntryViewModel: MvxViewModel
    {
        private int _startPosition;
        private Skier _skier;
        private IMvxMessenger _messenger;
        private RaceStatus _raceStatus;

        public StartListEntryViewModel(IMvxMessenger messenger)
        {
            _messenger = messenger;
            DeleteStartListEntryCommand = new MvxCommand<int>(startPosition => _messenger.Publish(new StartListUpdateMessage(this, startPosition)));
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

        public MvxCommand<int> DeleteStartListEntryCommand { get; set; }
    }
}
