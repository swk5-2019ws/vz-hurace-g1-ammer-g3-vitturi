using Hurace.Domain;
using Hurace.RaceControl.Helpers.MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class RunEntryViewModel : MvxViewModel
    {
        private readonly IMvxMessenger _messenger;
        private Run _run;

        public RunEntryViewModel(IMvxMessenger messenger, Run run)
        {
            _messenger = messenger;
            Run = run;
            DisqualifySkierCommand =
                new MvxCommand<Run>(r => _messenger.Publish(new DisqualifySkierMessage(this, Run)));
        }

        public Run Run
        {
            get => _run;
            set => SetProperty(ref _run, value);
        }

        public bool CanDisqualify => Run.Status != RunStatus.Disqualified;

        public MvxCommand<Run> DisqualifySkierCommand { get; set; }
    }
}