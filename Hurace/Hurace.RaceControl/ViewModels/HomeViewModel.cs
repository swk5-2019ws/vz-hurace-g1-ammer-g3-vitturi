using System.Threading.Tasks;
using Hurace.Core.Interface;
using Hurace.Domain;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private const int SHOWN_RACES = 10;
        private readonly IMvxNavigationService _navigationService;

        private readonly IRaceDao _raceDao;

        public HomeViewModel(IMvxNavigationService navigationService, IRaceDao raceDao)
        {
            _navigationService = navigationService;
            _raceDao = raceDao;
        }

        public MvxObservableCollection<Race> Races { get; } = new MvxObservableCollection<Race>();

        public override async Task Initialize()
        {
            await base.Initialize();
            var races = await _raceDao.GetLastRaces(SHOWN_RACES);
            Races.AddRange(races);
        }

        public void ShowCreateRace(Race race = null)
        {
            _navigationService.Navigate<CreateRaceViewModel, Race>(race);
        }
    }
}