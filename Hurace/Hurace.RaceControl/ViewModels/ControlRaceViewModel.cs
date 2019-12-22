using System.Linq;
using Hurace.Core.Services;
using Hurace.Domain;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class ControlRaceViewModel : MvxViewModel<Race>
    {
        private string _name;
        private int _displayRunNumber;
        private RunService _runService;
        private RaceService _raceService;
        private IMvxNavigationService _navigationService;

        private Race Race { get; set; }

        public ControlRaceViewModel(IMvxNavigationService navigationService, RunService runService, RaceService raceService)
        {
            _navigationService = navigationService;
            _runService = runService;
            _raceService = raceService;
        }

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

        public MvxObservableCollection<Run> CurrentRun { get; } = new MvxObservableCollection<Run>();

        public MvxObservableCollection<Run> FinishedRuns { get; } = new MvxObservableCollection<Run>();

        public MvxObservableCollection<Run> NextRuns { get; } = new MvxObservableCollection<Run>();

        public MvxCommand ShowFirstRunCommand { get; set; }

        public MvxCommand ShowSecondRunCommand { get; set; }

        public MvxCommand StartCurrentRunCommand { get; set; }

        public MvxCommand EndRaceCommand { get; set; }

        public MvxCommand<Skier> DisqualifySkierCommand { get; set; }

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
                    await _runService.UpdateRunStatus(Race, DisplayRunNumber, NextRuns[0].Skier, RunStatus.InProgress);
                    SwitchRun(DisplayRunNumber);
                }, () => NextRuns.Count > 0);
            EndRaceCommand = new MvxCommand(async () =>
            {
                Race.Status = RaceStatus.Finished;
                await _raceService.EditRace(Race);
                await _navigationService.Navigate<CreateRaceViewModel, Race>(race);
            });
            DisqualifySkierCommand = new MvxCommand<Skier>(async skier =>
            {
                await _runService.UpdateRunStatus(Race, DisplayRunNumber, skier, RunStatus.Disqualified);
                SwitchRun(DisplayRunNumber);
            });
            _runService.RunStatusChanged += (currentRace, raceNumber, skier, status) => SwitchRun(race.CompletedRuns >= 1 ? 2 : 1);
            SwitchRun(DisplayRunNumber);
        }

        private async void SwitchRun(int runNumber)
        {
            var runs = await _runService.GetAllRunsForRace(Race, runNumber);
            FinishedRuns.SwitchTo(runs.Where(run => run.Status == RunStatus.Completed || run.Status == RunStatus.Disqualified || run.Status == RunStatus.Unfinished || run.Status == RunStatus.NotStarted));
            NextRuns.SwitchTo(runs.Where(run => run.Status == RunStatus.Ready));
            CurrentRun.SwitchTo(runs.Where(run => run.Status == RunStatus.InProgress));
            StartCurrentRunCommand.RaiseCanExecuteChanged();
        }
    }
}