using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels.Screens;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views.Screens
{
    public abstract class CurrentResultAbstract : BaseApplicationMvxPage<CurrentResultViewModel>
    {
    }

    [MvxViewFor(typeof(CurrentResultViewModel))]
    [MvxWindowPresentation]
    public sealed partial class CurrentResult : CurrentResultAbstract
    {
        public CurrentResult()
        {
            InitializeComponent();
        }
    }
}