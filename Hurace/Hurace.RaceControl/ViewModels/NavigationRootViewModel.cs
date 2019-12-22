using System.Threading.Tasks;
using Hurace.Core.Services;
using Hurace.Domain;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class NavigationRootViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private RaceService _raceService;
        private bool _isRaceActive;
        private Race _currentRace;

        public bool IsRaceActive
        {
            get => _isRaceActive;
            set => SetProperty(ref _isRaceActive, value);
        }

        public NavigationRootViewModel(IMvxNavigationService navigationService, RaceService raceService)
        {
            _navigationService = navigationService;
            _raceService = raceService;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            ShowHome();
        }

        public override async void Prepare()
        {
            base.Prepare();
            _raceService.RaceStatusChanged += async (race, status) => await UpdateCurrentRace();
            OpenCurrentRaceCommand = new MvxCommand(() => ShowCurrentRace(_currentRace));
            await UpdateCurrentRace();
        }

        private async Task UpdateCurrentRace()
        {
            var race = await _raceService.GetCurrentRace();
            if (race != null)
            {
                IsRaceActive = true;
                _currentRace = race;
            }
            else
            {
                IsRaceActive = false;
            }
        }

        public MvxCommand OpenCurrentRaceCommand { get; set; }

        public void ShowSettings()
        {
            _navigationService.Navigate<SettingsViewModel>();
        }

        public void ShowHome()
        {
            _navigationService.Navigate<HomeViewModel>();
        }

        public void ShowScreens()
        {
            _navigationService.Navigate<ScreenSelectionViewModel>();
        }

        public void ShowCreateRace(Race race = null)
        {
            _navigationService.Navigate<CreateRaceViewModel, Race>(race);
        }

        public void ShowCurrentRace(Race race)
        {
            _navigationService.Navigate<ControlRaceViewModel, Race>(race);
        }
    }
}