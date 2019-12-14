using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Hurace.Core.Interface;
using Hurace.Domain;
using Hurace.RaceControl.Helpers;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class CreateRaceViewModel : MvxViewModel<Race>
    {
        private readonly ILocationDao _locationDao;
        private readonly ISkierDao _skierDao;
        private DateTimeOffset _date;

        private string _description;

        private Gender _gender;

        private string _name;

        private int _numberOfSensors;

        private string _pictureUrl;
        private Race _race;

        private RaceType _raceType;

        private Location _selectedLocation;

        private string _skierSearchText;

        private string _website;

        public CreateRaceViewModel(ILocationDao locationDao, ISkierDao skierDao)
        {
            _locationDao = locationDao;
            _skierDao = skierDao;
        }

        public MvxObservableCollection<Location> Locations { get; } = new MvxObservableCollection<Location>();

        public MvxObservableCollection<StartListEntryViewModel> StartListEntries { get; } =
            new MvxObservableCollection<StartListEntryViewModel>();

        public MvxObservableCollection<Skier> SearchSkiers { get; } = new MvxObservableCollection<Skier>();

        public string SkierSearchText
        {
            get => _skierSearchText;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    SetProperty(ref _skierSearchText, value, async () =>
                    {
                        var skiers = await _skierDao.SearchSkiers(SkierSearchText, null, Gender);
                        var filteredSkiers = skiers.Where(skier => StartListEntries.All(i => i.Skier.Id != skier.Id));
                        SearchSkiers.SwitchTo(filteredSkiers);
                    });
            }
        }

        public int ReorderIndex { get; set; }

        public DateTimeOffset Date
        {
            get => _date;
            set => SetProperty(ref _date, value, () => { _race.Date = Date.DateTime; });
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, () => { _race.Name = Name; });
        }

        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value, () => { _race.Gender = Gender; });
        }

        public RaceType RaceType
        {
            get => _raceType;
            set => SetProperty(ref _raceType, value, () => { _race.RaceType = RaceType; });
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value, () => { _race.Description = Description; });
        }

        public string Website
        {
            get => _website;
            set => SetProperty(ref _website, value, () => { _race.Website = Website; });
        }

        public string PictureUrl
        {
            get => _pictureUrl;
            set => SetProperty(ref _pictureUrl, value, () => { _race.PictureUrl = PictureUrl; });
        }

        public int NumberOfSensors
        {
            get => _numberOfSensors;
            set => SetProperty(ref _numberOfSensors, value, () => { _race.NumberOfSensors = NumberOfSensors; });
        }

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set => SetProperty(ref _selectedLocation, value, () => { _race.Location = SelectedLocation; });
        }

        public IEnumerable<Tuple<Enum, string>> RaceTypes => EnumHelper.GetAllValuesAndDescriptions<RaceType>();

        public IEnumerable<Tuple<Enum, string>> Genders => EnumHelper.GetAllValuesAndDescriptions<Gender>();

        public override async void Prepare(Race race)
        {
            await base.Initialize();

            _race = race;
            Name = _race.Name;
            Date = _race.Date;
            Gender = _race.Gender;
            RaceType = _race.RaceType;
            Description = _race.Description;
            Website = _race.Website;
            NumberOfSensors = _race.NumberOfSensors;
            PictureUrl = _race.PictureUrl;
            SelectedLocation = _race.Location;

            var locations = await _locationDao.FindAll();
            Locations.SwitchTo(locations);
            StartListEntries.CollectionChanged += RunsOnCollectionChanged;
        }

        private void RunsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    ReorderIndex = e.OldStartingIndex;
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