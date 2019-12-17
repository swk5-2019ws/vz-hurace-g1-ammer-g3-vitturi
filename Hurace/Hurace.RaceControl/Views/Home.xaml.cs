using Windows.UI.Xaml.Controls;
using Hurace.Domain;
using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    public abstract class HomeAbstract : BaseApplicationMvxPage<HomeViewModel>
    {
    }

    [MvxViewFor(typeof(HomeViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class Home : HomeAbstract
    {
        public Home()
        {
            InitializeComponent();
        }

        private void HomeFeedGrid_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedRace = e.ClickedItem as Race;
            ViewModel.ShowCreateRace(selectedRace);
        }
    }
}