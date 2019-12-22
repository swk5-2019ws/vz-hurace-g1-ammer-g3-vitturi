using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using Hurace.Simulator;

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
