using Hurace.Domain;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class NavigationRootViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public NavigationRootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            ShowHome();
        }

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
            _navigationService.Navigate<ScreenViewModel>();
        }

        public void ShowCreateRace(Race race = null)
        {
            _navigationService.Navigate<CreateRaceViewModel, Race>(race);
        }
    }
}