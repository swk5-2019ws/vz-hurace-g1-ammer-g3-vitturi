using MvvmCross.Platforms.Uap.Core;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Helpers
{
    public class Setup : MvxWindowsSetup<Core.App>
    {
        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
    }
}