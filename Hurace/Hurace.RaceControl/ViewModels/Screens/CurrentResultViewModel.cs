using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurace.Core.Services;
using Hurace.Domain;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels.Screens
{
    public class CurrentResultViewModel : MvxViewModel
    {
        private string _name;
        private Gender _gender;
        private Location _location;
        private RaceType _raceType;
        private int _runNumber;
        private string _pictureUrl;
        private RunService _runService;
        private RaceService _raceService;

        public CurrentResultViewModel(RaceService raceService, RunService runService)
        {
            _raceService = raceService;
            _runService = runService;
        }

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
                Runs.SwitchTo(leaderboardRuns);
            };
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

        public MvxObservableCollection<Run> Runs { get; set; } = new MvxObservableCollection<Run>();
    }
}
