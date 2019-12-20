using Windows.Devices.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    public abstract class ControlRaceAbstract : BaseApplicationMvxPage<ControlRaceViewModel>
    {
    }

    [MvxViewFor(typeof(ControlRaceViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class ControlRace : ControlRaceAbstract
    {
        public ControlRace()
        {
            InitializeComponent();
        }

        private void Run_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse ||
                e.Pointer.PointerDeviceType == PointerDeviceType.Pen)
                VisualStateManager.GoToState(sender as Control, "HoverButtonsShown", true);
        }

        private void RunEntry_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as Control, "HoverButtonsHidden", true);
        }
    }
}