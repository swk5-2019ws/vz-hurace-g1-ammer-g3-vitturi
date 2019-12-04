using Windows.UI.Xaml.Controls;
using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Views;
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
            this.InitializeComponent();
        }

        private void Navview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ViewModel.ShowSettings();
            }

            switch (args.InvokedItem as string)
            {
                case "Races":
                    //_navigationService.NavigateToPodcastsAsync();
                    break;
                case "Screens":
                    //_navigationService.NavigateToNowPlayingAsync();
                    break;
                case "Create race":
                    //_navigationService.NavigateToFavoritesAsync();
                    break;
                case "Current race":
                    //_navigationService.NavigateToNotesAsync();
                    break;
            }
        }
    }
}
