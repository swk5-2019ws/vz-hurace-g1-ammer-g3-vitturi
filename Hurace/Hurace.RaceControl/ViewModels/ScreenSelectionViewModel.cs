using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.ViewModels.Screens;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hurace.RaceControl.ViewModels
{
    public class ScreenSelectionViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public ScreenSelectionViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IEnumerable<ScreenType> ScreenTypes => Enum.GetValues(typeof(ScreenType)).Cast<ScreenType>();

        public MvxCommand<ScreenType> ScreenSelectCommand { get; set; }

        public override void Prepare()
        {
            base.Prepare();
            ScreenSelectCommand = new MvxCommand<ScreenType>(ScreenSelected);
        }

        private void ScreenSelected(ScreenType screenType)
        {
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