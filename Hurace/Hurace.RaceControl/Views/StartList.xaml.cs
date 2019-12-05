using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    [MvxViewFor(typeof(StartListViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class StartList : MvxWindowsPage
    {
        public StartList()
        {
            InitializeComponent();
        }
    }
}