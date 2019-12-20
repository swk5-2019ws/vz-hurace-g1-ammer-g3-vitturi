using Hurace.Domain;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels
{
    public class ControlRaceViewModel : MvxViewModel
    {
        private string _name;
        private int _displayRunNumber;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public int DisplayRunNumber
        {
            get => _displayRunNumber;
            set => SetProperty(ref _displayRunNumber, value);
        }

        public MvxCommand ShowFirstRun { get; set; }

        public MvxCommand ShowSecondRun { get; set; }

        public MvxCommand StopRace { get; set; }

        public MvxCommand StartRun { get; set; }

        public MvxObservableCollection<Run> CurrentRun { get; } = new MvxObservableCollection<Run>();

        public MvxObservableCollection<Run> FinishedRuns { get; } = new MvxObservableCollection<Run>();

        public MvxObservableCollection<Run> NextRuns { get; } = new MvxObservableCollection<Run>();

        public override void Prepare()
        {
            base.Prepare();
            Name = "Kitzbühl Slalom";
            DisplayRunNumber = 1;

            CurrentRun.Add(new Run() { RunNumber = 11, Skier = new Skier() { FirstName = "Test11", LastName = "Test11", Country = new Country() { Code = "AT" } } });

            FinishedRuns.Add(new Run() { RunNumber = 1, Skier = new Skier() { FirstName = "Test1", LastName = "Test1", Country = new Country() { Code = "AT" } } });
            FinishedRuns.Add(new Run() { RunNumber = 2, Skier = new Skier() { FirstName = "Test2", LastName = "Test2", Country = new Country() { Code = "US" } } });
            FinishedRuns.Add(new Run() { RunNumber = 3, Skier = new Skier() { FirstName = "Test3", LastName = "Test3", Country = new Country() { Code = "UA" } } });
            FinishedRuns.Add(new Run() { RunNumber = 4, Skier = new Skier() { FirstName = "Test4", LastName = "Test4", Country = new Country() { Code = "AT" } } });
            FinishedRuns.Add(new Run() { RunNumber = 5, Skier = new Skier() { FirstName = "Test5", LastName = "Test5", Country = new Country() { Code = "TC" } } });
            FinishedRuns.Add(new Run() { RunNumber = 6, Skier = new Skier() { FirstName = "Test6", LastName = "Test6", Country = new Country() { Code = "TV" } } });
            FinishedRuns.Add(new Run() { RunNumber = 7, Skier = new Skier() { FirstName = "Test7", LastName = "Test7", Country = new Country() { Code = "AT" } } });
            FinishedRuns.Add(new Run() { RunNumber = 8, Skier = new Skier() { FirstName = "Test8", LastName = "Test8", Country = new Country() { Code = "US" } } });
            FinishedRuns.Add(new Run() { RunNumber = 9, Skier = new Skier() { FirstName = "Test9", LastName = "Test9", Country = new Country() { Code = "AT" } } });
            FinishedRuns.Add(new Run() { RunNumber = 10, Skier = new Skier() { FirstName = "Test10", LastName = "Test10", Country = new Country() { Code = "EH" } } });

            NextRuns.Add(new Run() { RunNumber = 12, Skier = new Skier() { FirstName = "Test12", LastName = "Test12", Country = new Country() { Code = "AT" } } });
            NextRuns.Add(new Run() { RunNumber = 13, Skier = new Skier() { FirstName = "Test13", LastName = "Test13", Country = new Country() { Code = "US" } } });
            NextRuns.Add(new Run() { RunNumber = 14, Skier = new Skier() { FirstName = "Test14", LastName = "Test14", Country = new Country() { Code = "UA" } } });
            NextRuns.Add(new Run() { RunNumber = 15, Skier = new Skier() { FirstName = "Test15", LastName = "Test15", Country = new Country() { Code = "AT" } } });
            NextRuns.Add(new Run() { RunNumber = 16, Skier = new Skier() { FirstName = "Test16", LastName = "Test16", Country = new Country() { Code = "TC" } } });
            NextRuns.Add(new Run() { RunNumber = 17, Skier = new Skier() { FirstName = "Test17", LastName = "Test17", Country = new Country() { Code = "TV" } } });
            NextRuns.Add(new Run() { RunNumber = 18, Skier = new Skier() { FirstName = "Test18", LastName = "Test18", Country = new Country() { Code = "AT" } } });
            NextRuns.Add(new Run() { RunNumber = 19, Skier = new Skier() { FirstName = "Test19", LastName = "Test19", Country = new Country() { Code = "US" } } });
            NextRuns.Add(new Run() { RunNumber = 20, Skier = new Skier() { FirstName = "Test20", LastName = "Test20", Country = new Country() { Code = "AT" } } });
            NextRuns.Add(new Run() { RunNumber = 21, Skier = new Skier() { FirstName = "Test21", LastName = "Test21", Country = new Country() { Code = "EH" } } });
        }
    }
}