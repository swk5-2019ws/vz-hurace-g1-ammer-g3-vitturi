using System;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels.Screens
{
    public class CurrentSkierViewModel : MvxViewModel
    {
        private const int PreviousSkiers = 2;
        private const int LeaderboardShownSkiers = 5;
        private string _countryCode;
        private Race _currentRace;
        private readonly DispatcherTimer _dispatcherTimer;
        private TimeSpan _elapsedTime;
        private string _firstName;
        private string _lastName;
        private Run _lastRun;
        private string _pictureUrl;
        private readonly IRaceService _raceService;
        private readonly IRunService _runService;
        private int _startNumber;
        private readonly Stopwatch _stopWatch;
        private TimeSpan _totalTime;

        public CurrentSkierViewModel(IRunService runService, IRaceService raceService)
        {
            _runService = runService;
            _raceService = raceService;
            _stopWatch = new Stopwatch();
            _dispatcherTimer = new DispatcherTimer();
        }

        public TimeSpan TotalTime
        {
            get => _totalTime;
            set => SetProperty(ref _totalTime, value);
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

        public MvxObservableCollection<SensorMeasurementEntryViewModel> SensorMeasurementDiffEntries { get; } =
            new MvxObservableCollection<SensorMeasurementEntryViewModel>();

        public MvxObservableCollection<Run> Runs { get; } = new MvxObservableCollection<Run>();

        public override async void Prepare()
        {
            base.Prepare();
            _currentRace = await _raceService.GetCurrentRace();
            _runService.SensorMeasurementAdded += (race, number, skier, timeSpan) => RefreshRun();
            _runService.RunStatusChanged += (race, number, skier, status) => HandleRunStatusChange(status);
            _runService.RunStarted += RunServiceOnRunStarted;
            _dispatcherTimer.Tick += (sender, o) => ElapsedTime = TotalTime + _stopWatch.Elapsed;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            RefreshRun();
        }

        private void RunServiceOnRunStarted(Race race, int runNumber, Skier skier)
        {
            _stopWatch.Start();
            _dispatcherTimer.Start();
        }

        private void HandleRunStatusChange(RunStatus runStatus)
        {
            RefreshRun();
            if (runStatus == RunStatus.Completed || runStatus == RunStatus.Disqualified)
            {
                _stopWatch.Stop();
                _dispatcherTimer.Stop();
                _stopWatch.Reset();
                FetchLeaderBoard();
            }
        }

        private async void RefreshRun()
        {
            var currentRun = await _runService.GetCurrentRun();
            if (currentRun != null)
            {
                _lastRun = currentRun;
                PictureUrl = currentRun.Skier.PictureUrl;
                FirstName = currentRun.Skier.FirstName;
                LastName = currentRun.Skier.LastName;
                StartNumber = currentRun.StartPosition;
                CountryCode = currentRun.Skier.Country.Code;
                TotalTime = TimeSpan.FromMilliseconds(currentRun.TotalTime);
                var times = await _runService.GetInterimTimes(_currentRace, currentRun.RunNumber, currentRun.Skier);
                SensorMeasurementEntries.SwitchTo(times.Select(timeSpan => new SensorMeasurementEntryViewModel
                    {TimeSpan = timeSpan}));
                var diffTimes =
                    await _runService.GetInterimTimesDifferences(times, _currentRace, currentRun.RunNumber,
                        currentRun.Skier);
                SensorMeasurementDiffEntries.SwitchTo(diffTimes.Select(timeSpan => new SensorMeasurementEntryViewModel
                    {TimeSpan = timeSpan}));
                FetchLeaderBoard();
            }
        }

        private async void FetchLeaderBoard()
        {
            var runs = await _runService.GetLeaderBoard(_currentRace, _lastRun.RunNumber);
            var currentRunIndex = runs.Select((run, index) => new {run, index})
                .FirstOrDefault(x => x.run.Skier.Id == _lastRun.Skier.Id);
            if (currentRunIndex != null)
            {
                var leaderboardRuns = runs
                    .Skip(currentRunIndex.index <= PreviousSkiers ? 0 : currentRunIndex.index - PreviousSkiers)
                    .Take(LeaderboardShownSkiers);
                Runs.SwitchTo(leaderboardRuns);
            }
        }
    }
}