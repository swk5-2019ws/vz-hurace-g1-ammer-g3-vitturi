using Hurace.Core.Interface.Services;
using Hurace.Domain;
using MvvmCross.ViewModels;
using System;
using System.Linq;

namespace Hurace.RaceControl.ViewModels.Screens
{
    public class CurrentSkierViewModel : MvxViewModel
    {
        private const int PreviousSkiers = 2;
        private const int LeaderboardShownSkiers = 5;
        private string _countryCode;
        private TimeSpan _elapsedTime;
        private string _firstName;
        private string _lastName;
        private string _pictureUrl;
        private int _startNumber;
        private IRunService _runService;
        private IRaceService _raceService;
        private Race _currentRace;

        public CurrentSkierViewModel(IRunService runService, IRaceService raceService)
        {
            _runService = runService;
            _raceService = raceService;
        }

        public override async void Prepare()
        {
            base.Prepare();
            _currentRace = await _raceService.GetCurrentRace();
            _runService.SensorMeasurementAdded += (race, number, skier, timeSpan) => RefreshRun();
            RefreshRun();
        }

        private async void RefreshRun()
        {
            var currentRun = await _runService.GetCurrentRun();
            PictureUrl = currentRun.Skier.PictureUrl;
            FirstName = currentRun.Skier.FirstName;
            LastName = currentRun.Skier.LastName;
            StartNumber = currentRun.StartPosition;
            CountryCode = currentRun.Skier.Country.Code;
            var times = await _runService.GetInterimTimes(_currentRace, currentRun.RunNumber, currentRun.Skier);
            SensorMeasurementEntries.SwitchTo(times.Select(timeSpan => new SensorMeasurementEntryViewModel() { TimeSpan = timeSpan }));
            var runs = await _runService.GetLeaderBoard(_currentRace, currentRun.RunNumber);
            var currentRunIndex = runs.Select((run, index) => new { run, index }).First(x => x.run.Skier.Id == currentRun.Skier.Id).index;
            var leaderboardRuns = runs.Skip(currentRunIndex <= PreviousSkiers ? 0 : currentRunIndex - PreviousSkiers)
                .Take(LeaderboardShownSkiers);
            Runs.SwitchTo(leaderboardRuns);
        }

        public string PictureUrl
        {
            get => _pictureUrl;
            set => SetProperty(ref _pictureUrl, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public int StartNumber
        {
            get => _startNumber;
            set => SetProperty(ref _startNumber, value);
        }

        public string CountryCode
        {
            get => _countryCode;
            set => SetProperty(ref _countryCode, value);
        }

        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set => SetProperty(ref _elapsedTime, value);
        }

        public MvxObservableCollection<SensorMeasurementEntryViewModel> SensorMeasurementEntries { get; } =
            new MvxObservableCollection<SensorMeasurementEntryViewModel>();

        public MvxObservableCollection<Run> Runs { get; } = new MvxObservableCollection<Run>();
    }
}