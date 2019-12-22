using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.RaceControl.ViewModels;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Views
{
    public abstract class SettingsAbstract : BaseApplicationMvxPage<SettingsViewModel>
    {
    }

    [MvxViewFor(typeof(SettingsViewModel))]
    [MvxSplitViewPresentation(SplitPanePosition.Content)]
    public sealed partial class Settings : SettingsAbstract
    {
        public Settings()
        {
            InitializeComponent();
        }
    }
}