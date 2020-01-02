using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using Hurace.Simulator;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    public abstract class SimulatorAbstract : BaseApplicationMvxPage<SimulatorControlViewModel>
    {
    }
    [MvxViewFor(typeof(SimulatorControlViewModel))]
    [MvxWindowPresentation]
    public sealed partial class SimulatorControl : SimulatorAbstract
    {
        public SimulatorControl()
        {
            this.InitializeComponent();
            this.DataContext = new SimulatorControlViewModel(new SimulatorRaceClock());
        }
    }
}
