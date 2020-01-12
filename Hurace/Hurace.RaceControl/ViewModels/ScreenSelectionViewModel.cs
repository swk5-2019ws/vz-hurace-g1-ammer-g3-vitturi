using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.ViewModels.Screens;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hurace.Core.Interface.Services;
using Hurace.Domain;
using Hurace.RaceControl.Helpers.MvvmCross;

namespace Hurace.RaceControl.ViewModels
{
    public class ScreenSelectionViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private IRaceService _raceService;
        private Race _currentRace = null;
        private IDialogService _dialogService;

        public ScreenSelectionViewModel(IMvxNavigationService navigationService, IRaceService raceService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _raceService = raceService;
            _dialogService = dialogService;
        }

        public IEnumerable<ScreenType> ScreenTypes => Enum.GetValues(typeof(ScreenType)).Cast<ScreenType>();

        public MvxCommand<ScreenType> ScreenSelectCommand { get; set; }

        public override async void Prepare()
        {
            base.Prepare();
            ScreenSelectCommand = new MvxCommand<ScreenType>(ScreenSelected);
            _raceService.RaceStatusChanged += async (race, status) => await UpdateCurrentRace();
            await UpdateCurrentRace();
        }

        private async Task UpdateCurrentRace()
        {
            _currentRace = await _raceService.GetCurrentRace();
        }

        private void ScreenSelected(ScreenType screenType)
        {
            if (_currentRace == null)
            {
                _dialogService.Alert(DialogEvent.NoRaceInProgress);
                return;
            }

            switch (screenType)
            {
                case ScreenType.CurrentResult:
                    _navigationService.Navigate<CurrentResultViewModel>();
                    break;
                case ScreenType.CurrentSkier:
                    _navigationService.Navigate<CurrentSkierViewModel>();
                    break;
                default:
                    break;
            }
        }
    }
}