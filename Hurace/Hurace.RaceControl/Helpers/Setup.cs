using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Presenters;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Helpers
{
    public class Setup: MvxWindowsSetup<Core.App>
    {
        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
    }
}
