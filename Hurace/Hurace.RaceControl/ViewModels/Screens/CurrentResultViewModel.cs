using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hurace.Domain;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels.Screens
{
    public class CurrentResultViewModel: MvxViewModel
    {
        private string _name;
        private Gender _gender;
        private Location _location;
        private RaceType _raceType;
        private int _runNumber;
        private string _pictureUrl;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public Gender Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }

        public Location Location
        {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        public RaceType RaceType
        {
            get => _raceType;
            set => SetProperty(ref _raceType, value);
        }

        public int RunNumber
        {
            get => _runNumber;
            set => SetProperty(ref _runNumber, value);
        }

        public string PictureUrl
        {
            get => _pictureUrl;
            set => SetProperty(ref _pictureUrl, value);
        }

        public MvxObservableCollection<Run> Runs { get; set; } = new MvxObservableCollection<Run>();

        public CurrentResultViewModel()
        {
            Name = "Ski-Weltcup";
            Gender = Gender.Female;
            Location = new Location(){Name = "Sölden"};
            RunNumber = 1;
            RaceType = RaceType.SuperSlalom;
            PictureUrl =
                "https://images.unsplash.com/photo-1498576260462-eefc9d0ce9f7?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit";
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test1", LastName = "Test1", Country = new Country(){Code = "AUT"}} });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test2", LastName = "Test2", Country = new Country() { Code = "USA" } } });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test3", LastName = "Test3", Country = new Country() { Code = "NZL" } } });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test4", LastName = "Test4", Country = new Country() { Code = "AUT" } } });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test5", LastName = "Test5", Country = new Country() { Code = "AUT" } } });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test6", LastName = "Test6", Country = new Country() { Code = "GER" } } });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test7", LastName = "Test7", Country = new Country() { Code = "AUT" } } });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test8", LastName = "Test8", Country = new Country() { Code = "USA" } } });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test9", LastName = "Test9", Country = new Country() { Code = "AUT" } } });
            Runs.Add(new Run() { Skier = new Skier() { FirstName = "Test10", LastName = "Test10", Country = new Country() { Code = "AUT" } } });
        }
    }
}
