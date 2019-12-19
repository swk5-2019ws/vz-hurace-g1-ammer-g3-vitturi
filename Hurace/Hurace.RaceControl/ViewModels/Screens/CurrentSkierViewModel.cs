using System;
using Hurace.Domain;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.ViewModels.Screens
{
    public class CurrentSkierViewModel : MvxViewModel
    {
        private string _countryCode;
        private TimeSpan _elapsedTime;
        private string _firstName;
        private string _lastName;
        private string _pictureUrl;
        private int _startNumber;

        public CurrentSkierViewModel()
        {
            PictureUrl = "https://upload.wikimedia.org/wikipedia/commons/8/8c/Marcel_Hirscher_%28Portrait%29.jpg";
            FirstName = "Marcel";
            LastName = "Hirscher";
            StartNumber = 10;
            CountryCode = "AT";
            ElapsedTime = TimeSpan.Parse("00:00:5:45");
            SensorMeasurements.Add(new SensorMeasurement {InterimTime = 0.18});
            SensorMeasurements.Add(new SensorMeasurement {InterimTime = -0.05});
            SensorMeasurements.Add(new SensorMeasurement {InterimTime = -0.33});
            Runs.Add(new Run
            {
                Skier = new Skier {FirstName = "Test1", LastName = "Test1", Country = new Country {Code = "ES"}},
                RunNumber = 1, TotalTime = 4.5
            });
            Runs.Add(new Run
            {
                Skier = new Skier {FirstName = "Test2", LastName = "Test2", Country = new Country {Code = "AT"}},
                RunNumber = 2, TotalTime = 4.6
            });
            Runs.Add(new Run
            {
                Skier = new Skier {FirstName = "Test3", LastName = "Test3", Country = new Country {Code = "US"}},
                RunNumber = 3, TotalTime = 4.7
            });
            Runs.Add(new Run
            {
                Skier = new Skier {FirstName = "Test4", LastName = "Test4", Country = new Country {Code = "EH"}},
                RunNumber = 4, TotalTime = 4.8
            });
            Runs.Add(new Run
            {
                Skier = new Skier {FirstName = "Test5", LastName = "Test5", Country = new Country {Code = "IT"}},
                RunNumber = 5, TotalTime = 4.9
            });
        }

        public string PictureUrl
        {
            get => _pictureUrl;
            set => SetProperty(ref _pictureUrl, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public int StartNumber
        {
            get => _startNumber;
            set => SetProperty(ref _startNumber, value);
        }

        public string CountryCode
        {
            get => _countryCode;
            set => SetProperty(ref _countryCode, value);
        }

        public TimeSpan ElapsedTime
        {
            get => _elapsedTime;
            set => SetProperty(ref _elapsedTime, value);
        }

        public MvxObservableCollection<SensorMeasurement> SensorMeasurements { get; } =
            new MvxObservableCollection<SensorMeasurement>();

        public MvxObservableCollection<Run> Runs { get; } = new MvxObservableCollection<Run>();
    }
}