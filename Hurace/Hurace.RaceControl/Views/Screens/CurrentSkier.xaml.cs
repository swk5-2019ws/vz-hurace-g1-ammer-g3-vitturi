using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels.Screens;
using MvvmCross.ViewModels;


namespace Hurace.RaceControl.Views.Screens
{
    public abstract class CurrentSkierAbstract : BaseApplicationMvxPage<CurrentSkierViewModel>
    {
    }

    [MvxViewFor(typeof(CurrentSkierViewModel))]
    [MvxWindowPresentation]
    public sealed partial class CurrentSkier : CurrentSkierAbstract
    {
        public CurrentSkier()
        {
            this.InitializeComponent();
        }
    }
}
