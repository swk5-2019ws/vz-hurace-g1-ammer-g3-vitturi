using System.Threading.Tasks;
using Hurace.Core.Interface;
using Hurace.Core.Services;
using Hurace.Domain;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private const int SHOWN_RACES = 15;
        private readonly IMvxNavigationService _navigationService;

        private readonly RaceService _raceService;

        private string _raceSearchText;

        public HomeViewModel(IMvxNavigationService navigationService, RaceService raceService)
        {
            _navigationService = navigationService;
            _raceService = raceService;
        }

        public MvxObservableCollection<Race> Races { get; } = new MvxObservableCollection<Race>();

        public MvxObservableCollection<Race> SearchRaces { get; } = new MvxObservableCollection<Race>();

        public MvxCommand<Race> RaceSearchQueryCommand { get; set; }

        public string RaceSearchText
        {
            get => _raceSearchText;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    SetProperty(ref _raceSearchText, value, async () =>
                    {
                        var races = await _raceService.SearchRaces(RaceSearchText);
                        SearchRaces.SwitchTo(races);
                    });
            }
        }


        public override async Task Initialize()
        {
            await base.Initialize();
            var races = await _raceService.GetLastRaces(SHOWN_RACES);
            Races.AddRange(races);
            RaceSearchQueryCommand = new MvxCommand<Race>(ShowCreateRace);
        }

        public void ShowCreateRace(Race race = null)
        {
            _navigationService.Navigate<CreateRaceViewModel, Race>(race);
        }
    }
}