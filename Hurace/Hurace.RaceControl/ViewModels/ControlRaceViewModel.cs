using System.Linq;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using Hurace.RaceControl.Helpers.MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugins.Messenger;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class ControlRaceViewModel : MvxViewModel<Race>
    {
        private int _displayRunNumber;
        private string _name;
        private IMvxMessenger _messenger;
        private MvxSubscriptionToken _token;
        private readonly IMvxNavigationService _navigationService;
        private readonly IRaceService _raceService;
        private readonly IRunService _runService;

        public ControlRaceViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger, IRunService runService,
            IRaceService raceService)
        {
            _navigationService = navigationService;
            _runService = runService;
            _raceService = raceService;
            _messenger = messenger;
        }

        private Race Race { get; set; }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int DisplayRunNumber
        {
            get => _displayRunNumber;
            set => SetProperty(ref _displayRunNumber, value);
        }

        public MvxCommand ShowFirstRun { get; set; }

        public MvxCommand ShowSecondRun { get; set; }

        public MvxCommand StopRace { get; set; }

        public MvxCommand StartRun { get; set; }

        public MvxObservableCollection<RunEntryViewModel> CurrentRun { get; } = new MvxObservableCollection<RunEntryViewModel>();

        public MvxObservableCollection<RunEntryViewModel> FinishedRuns { get; } = new MvxObservableCollection<RunEntryViewModel>();

        public MvxObservableCollection<RunEntryViewModel> NextRuns { get; } = new MvxObservableCollection<RunEntryViewModel>();

        public MvxCommand ShowFirstRunCommand { get; set; }

        public MvxCommand ShowSecondRunCommand { get; set; }

        public MvxCommand StartCurrentRunCommand { get; set; }

        public MvxCommand EndRaceCommand { get; set; }

        public override async void Prepare(Race race)
        {
            base.Prepare();
            Race = race;
            Name = Race.Name;
            DisplayRunNumber = 1;

            ShowFirstRunCommand = new MvxCommand(() => SwitchRun(1));
            ShowSecondRunCommand = new MvxCommand(() => SwitchRun(2));
            StartCurrentRunCommand = new MvxCommand(async () =>
            {
                await _runService.UpdateRunStatus(Race, DisplayRunNumber, NextRuns[0].Run.Skier, RunStatus.InProgress);
                SwitchRun(DisplayRunNumber);
            }, () => NextRuns.Count > 0);
            EndRaceCommand = new MvxCommand(async () =>
            {
                Race.Status = RaceStatus.Finished;
                await _raceService.EditRace(Race);
                await _navigationService.Navigate<CreateRaceViewModel, Race>(race);
            });
            _token = _messenger.Subscribe<DisqualifySkierMessage>(async message =>
            {
                await _runService.UpdateRunStatus(message.Run.Race, DisplayRunNumber, message.Run.Skier, RunStatus.Disqualified);
                SwitchRun(DisplayRunNumber);
            });
            _runService.RunStatusChanged += (currentRace, raceNumber, skier, status) =>
                SwitchRun(race.CompletedRuns >= 1 ? 2 : 1);
            SwitchRun(DisplayRunNumber);
        }

        private async void SwitchRun(int runNumber)
        {
            var runEntries = (await _runService.GetAllRunsForRace(Race, runNumber)).Select(run => new RunEntryViewModel(_messenger, run)).ToList();
            FinishedRuns.SwitchTo(runEntries.Where(runEntry =>
                runEntry.Run.Status == RunStatus.Completed || runEntry.Run.Status == RunStatus.Disqualified ||
                runEntry.Run.Status == RunStatus.Unfinished || runEntry.Run.Status == RunStatus.NotStarted));
            NextRuns.SwitchTo(runEntries.Where(runEntry => runEntry.Run.Status == RunStatus.Ready));
            CurrentRun.SwitchTo(runEntries.Where(runEntry => runEntry.Run.Status == RunStatus.InProgress));
            DisplayRunNumber = runNumber;
            StartCurrentRunCommand.RaiseCanExecuteChanged();
        }
    }
}