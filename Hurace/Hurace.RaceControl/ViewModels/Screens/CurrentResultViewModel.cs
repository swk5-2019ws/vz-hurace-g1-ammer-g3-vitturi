using System.Linq;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels.Screens
{
    public class CurrentResultViewModel : MvxViewModel
    {
        private Gender _gender;
        private Location _location;
        private string _name;
        private string _pictureUrl;
        private readonly IRaceService _raceService;
        private RaceType _raceType;
        private int _runNumber;
        private readonly IRunService _runService;
        private Skier _lastSkier;

        public CurrentResultViewModel(IRaceService raceService, IRunService runService)
        {
            _raceService = raceService;
            _runService = runService;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public Location Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public RaceType RaceType
        {
            get => _raceType;
            set => SetProperty(ref _raceType, value);
        }

        public int RunNumber
        {
            get => _runNumber;
            set => SetProperty(ref _runNumber, value);
        }

        public string PictureUrl
        {
            get => _pictureUrl;
            set => SetProperty(ref _pictureUrl, value);
        }

        public Skier LastSkier
        {
            get => _lastSkier;
            set => SetProperty(ref _lastSkier, value);
        }


        public MvxObservableCollection<Run> Runs { get; set; } = new MvxObservableCollection<Run>();

        public override async void Prepare()
        {
            base.Prepare();

            var currentRace = await _raceService.GetCurrentRace();
            Name = currentRace.Name;
            Gender = currentRace.Gender;
            Location = currentRace.Location;
            RunNumber = currentRace.CompletedRuns >= 1 ? 2 : 1;
            RaceType = currentRace.RaceType;
            PictureUrl = currentRace.PictureUrl;

            var runs = await _runService.GetLeaderBoard(currentRace, RunNumber);
            Runs.SwitchTo(runs);

            _runService.LeaderBoardUpdated += (race, runNumber, leaderboardRuns) =>
            {
                RunNumber = runNumber;
                var runsList = leaderboardRuns.ToList();
                for (int i = 0; i < runsList.Count(); i++)
                {
                    if (i > 0)
                    {
                        runsList[i].TotalTime = runsList[i].TotalTime - runsList[0].TotalTime;
                    }
                }
                Runs.SwitchTo(leaderboardRuns);
            };

            _runService.RunStatusChanged += (race, number, skier, status) =>
            {
                if (status == RunStatus.Completed)
                {
                    LastSkier = skier;

                }
            };
        }
    }
}