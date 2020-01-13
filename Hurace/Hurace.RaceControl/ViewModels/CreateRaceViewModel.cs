using Hurace.Core.Interface.Services;
using Hurace.Domain;
using Hurace.RaceControl.Helpers;
using Hurace.RaceControl.Helpers.MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugins.Messenger;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Hurace.RaceControl.ViewModels
{
    public class CreateRaceViewModel : MvxViewModel<Race>
    {
        private readonly ILocationService _locationService;
        private readonly IMvxNavigationService _navigationService;
        private readonly ISkierService _skierService;
        private DateTimeOffset _date;

        private string _description;
        private readonly IDialogService _dialogService;

        private Gender _gender;
        private bool _isNewRace;

        private string _name;

        private int _numberOfSensors;

        private string _pictureUrl;
        private Race _race;
        private readonly IRaceService _raceService;

        private RaceType _raceType;
        private readonly IRunService _runService;

        private Location _selectedLocation;

        private string _skierSearchText;
        private RaceStatus _status;

        private MvxSubscriptionToken _token;

        private string _website;
        private readonly RaceValidator raceValidator = new RaceValidator();
        private bool _raceIsNotFinished;

        public CreateRaceViewModel(IMvxNavigationService navigationService, IDialogService dialogService,
            IMvxMessenger messenger,
            ILocationService locationService, ISkierService skierService, IRaceService raceService, IRunService runService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _locationService = locationService;
            _skierService = skierService;
            _raceService = raceService;
            _runService = runService;
            Messenger = messenger;
        }

        public MvxObservableCollection<Location> Locations { get; } = new MvxObservableCollection<Location>();

        public MvxObservableCollection<StartListEntryViewModel> StartListEntries { get; } =
            new MvxObservableCollection<StartListEntryViewModel>();

        public MvxObservableCollection<Skier> SearchSkiers { get; } = new MvxObservableCollection<Skier>();

        public IMvxMessenger Messenger { get; }

        public string SkierSearchText
        {
            get => _skierSearchText;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    SetProperty(ref _skierSearchText, value, async () =>
                    {
                        var skiers = await _skierService.GetSkiers(Gender, SkierSearchText);
                        var filteredSkiers = skiers.Where(skier => StartListEntries.All(i => i.Skier.Id != skier.Id));
                        SearchSkiers.SwitchTo(filteredSkiers);
                    });
            }
        }

        public int ReorderIndex { get; set; }

        public DateTimeOffset Date
        {
            get => _date;
            set => SetProperty(ref _date, value, () =>
            {
                _race.Date = Date.DateTime;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public RaceStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, () =>
            {
                _race.Name = Name;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value, () =>
            {
                _race.Gender = Gender;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public RaceType RaceType
        {
            get => _raceType;
            set => SetProperty(ref _raceType, value, () =>
            {
                _race.RaceType = RaceType;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value, () =>
            {
                _race.Description = Description;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public string Website
        {
            get => _website;
            set => SetProperty(ref _website, value, () =>
            {
                _race.Website = Website;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public string PictureUrl
        {
            get => _pictureUrl;
            set => SetProperty(ref _pictureUrl, value, () =>
            {
                _race.PictureUrl = PictureUrl;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public int NumberOfSensors
        {
            get => _numberOfSensors;
            set => SetProperty(ref _numberOfSensors, value, () =>
            {
                _race.NumberOfSensors = NumberOfSensors;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set => SetProperty(ref _selectedLocation, value, () =>
            {
                _race.Location = SelectedLocation;
                SaveRaceCommand.RaiseCanExecuteChanged();
            });
        }

        public bool RaceIsNotFinished
        {
            get => _raceIsNotFinished;
            set => SetProperty(ref _raceIsNotFinished, value);
        }

        public IEnumerable<RaceType> RaceTypes => Enum.GetValues(typeof(RaceType)).Cast<RaceType>();

        public IEnumerable<Gender> Genders => Enum.GetValues(typeof(Gender)).Cast<Gender>();

        public MvxCommand OpenRaceControlCommand { get; set; }

        public MvxCommand SaveRaceCommand { get; set; }

        public override async void Prepare(Race race)
        {
            await base.Initialize();

            if (race == null)
            {
                _isNewRace = true;
                race = new Race
                {
                    Date = DateTime.Now,
                    Gender = Gender.Male,
                    RaceType = RaceType.Slalom,
                    Status = RaceStatus.Ready,
                    NumberOfSensors = 2
                };
            }

            _race = race;
            OpenRaceControlCommand = new MvxCommand(async () =>
            {
                if (race.Status == RaceStatus.Ready)
                {
                    race.Status = RaceStatus.InProgress;
                    SaveRaceCommand.Execute();
                }
                await _navigationService.Navigate<ControlRaceViewModel, Race>(_race);
            }, () => StartListEntries.Count > 2);

            _token = Messenger.Subscribe<StartListUpdateMessage>(message =>
            {
                StartListEntries.RemoveAt(message.StartPosition - 1);
            });

            SaveRaceCommand = new MvxCommand(async () =>
            {
                var skiers = StartListEntries.Select(entry => entry.Skier).ToList();
                if (_isNewRace)
                {
                    await _raceService.CreateRace(race, skiers);
                    await _navigationService.Navigate<HomeViewModel>();
                }
                else
                {
                    await _raceService.EditRace(race);
                    await _raceService.EditStartList(race, 1, skiers);
                    _dialogService.Alert(DialogEvent.RaceEditSuccess);
                }
            }, () => raceValidator.Validate(race).IsValid);

            Name = _race.Name;
            Date = _race.Date;
            Gender = _race.Gender;
            RaceType = _race.RaceType;
            Description = _race.Description;
            Website = _race.Website;
            NumberOfSensors = _race.NumberOfSensors;
            PictureUrl = _race.PictureUrl;
            SelectedLocation = _race.Location;
            Status = _race.Status;
            RaceIsNotFinished = _race.Status != RaceStatus.Finished;

            var locations = await _locationService.GetLocations();
            var runs = (await _runService.GetAllRunsForRace(race, 1));
            if (runs.Any())
            {
                var startListEntries = runs.Select(entry =>
                    new StartListEntryViewModel(Messenger) { Skier = entry.Skier, StartPosition = entry.StartPosition, RaceStatus = Status });
                StartListEntries.SwitchTo(startListEntries);
            }
            Locations.SwitchTo(locations);
            StartListEntries.CollectionChanged += RunsOnCollectionChanged;
        }

        private void RunsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OpenRaceControlCommand.RaiseCanExecuteChanged();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    ReorderIndex = e.OldStartingIndex;
                    ReorderRuns(ReorderIndex);
                    break;
                case NotifyCollectionChangedAction.Add:
                    if (ReorderIndex == -1)
                        return;
                    ReorderRuns(ReorderIndex > e.NewStartingIndex ? e.NewStartingIndex : ReorderIndex);
                    ReorderIndex = -1;
                    break;
            }
        }

        private void ReorderRuns(int start)
        {
            for (var i = start; i < StartListEntries.Count; i++) StartListEntries[i].StartPosition = i + 1;
        }
    }
}