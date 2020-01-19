using Windows.ApplicationModel;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class SettingsViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private string _versionDescription;

        public SettingsViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            OpenSimulatorCommand = new MvxCommand(OpenSimulator);
        }

        public MvxCommand OpenSimulatorCommand { get; set; }

        public string VersionDescription
        {
            get => _versionDescription;
            set => SetProperty(ref _versionDescription, value);
        }

        public override void Prepare()
        {
            base.Prepare();
            VersionDescription = GetVersionDescription();
        }

        private string GetVersionDescription()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{package.DisplayName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        public void OpenSimulator()
        {
            _navigationService.Navigate<SimulatorControlViewModel>();
        }
    }
}