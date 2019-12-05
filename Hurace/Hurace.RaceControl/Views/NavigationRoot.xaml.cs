using Windows.UI.Xaml.Controls;
using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.ViewModels;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    public abstract class NavigationRootAbstract : BaseApplicationMvxPage<NavigationRootViewModel>
    {
    }

    [MvxViewFor(typeof(NavigationRootViewModel))]
    public sealed partial class NavigationRoot : NavigationRootAbstract
    {
        public NavigationRoot()
        {
            InitializeComponent();
        }

        private void Navview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked) ViewModel.ShowSettings();

            switch (args.InvokedItem as string)
            {
                case "Races":
                    ViewModel.ShowHome();
                    break;
                case "Screens":
                    ViewModel.ShowScreens();
                    break;
                case "Create race":
                    ViewModel.ShowCreateRace();
                    break;
                case "Current race":
                    //_navigationService.NavigateToNotesAsync();
                    break;
            }
        }
    }
}