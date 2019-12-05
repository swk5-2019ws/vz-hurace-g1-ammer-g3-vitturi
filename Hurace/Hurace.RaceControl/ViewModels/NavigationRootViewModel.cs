using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class NavigationRootViewModel: MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public NavigationRootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void ShowSettings()
        {
            _navigationService.Navigate<SettingsViewModel>();
        }
    }
}
