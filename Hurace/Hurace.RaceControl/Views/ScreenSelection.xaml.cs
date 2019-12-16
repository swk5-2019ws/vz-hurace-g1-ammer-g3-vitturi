using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    [MvxViewFor(typeof(ScreenSelectionViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class ScreenSelection : MvxWindowsPage
    {
        public ScreenSelection()
        {
            InitializeComponent();
        }
    }
}