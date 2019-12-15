using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Helpers
{
    public class BaseApplicationMvxPage<TViewModel> : MvxWindowsPage<TViewModel> where TViewModel : MvxViewModel
    {
    }
}