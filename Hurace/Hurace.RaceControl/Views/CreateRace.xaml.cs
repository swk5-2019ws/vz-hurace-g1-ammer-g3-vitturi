using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    [MvxViewFor(typeof(CreateRaceViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class CreateRace : MvxWindowsPage
    {
        public CreateRace()
        {
            InitializeComponent();
        }
    }
}