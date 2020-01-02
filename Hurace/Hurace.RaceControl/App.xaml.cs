using Hurace.RaceControl.Helpers;
using MvvmCross.Platforms.Uap.Views;

namespace Hurace.RaceControl
{
    sealed partial class App
    {
        public App()
        {
            this.InitializeComponent();
        }
    }

    public abstract class RaceControlApp : MvxApplication<Setup, Core.App>
    {
    }
}
