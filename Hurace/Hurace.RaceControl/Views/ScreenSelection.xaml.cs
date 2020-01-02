using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    public abstract class ScreenSelectionAbstract : BaseApplicationMvxPage<ScreenSelectionViewModel>
    {
    }

    [MvxViewFor(typeof(ScreenSelectionViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class ScreenSelection : ScreenSelectionAbstract
    {
        public ScreenSelection()
        {
            InitializeComponent();
        }
    }
}