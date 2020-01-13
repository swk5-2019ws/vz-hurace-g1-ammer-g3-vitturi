using Hurace.Core;
using Hurace.Core.Daos;
using Hurace.Core.Interface.Services;
using Hurace.Core.Services;
using Hurace.RaceControl.Helpers.MvvmCross;
using Hurace.Simulator;
using MvvmCross;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;

namespace Hurace.RaceControl.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            var connectionString = ConfigurationReader.GetConnectionString(Environment.Production);
            var databaseName = GetDatabaseName(connectionString);
            var task = Task.Run(async () => await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists)).Result;
            var databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName);
            var customConnectionString = connectionString.Replace(databaseName, databasePath, StringComparison.CurrentCulture);

            var connectionFactory = new ConnectionFactory(Environment.Production, customConnectionString);
            var locationDao = new LocationDao(connectionFactory);
            var skierDao = new SkierDao(connectionFactory);
            var countryDao = new CountryDao(connectionFactory);
            var raceDao = new RaceDao(connectionFactory);
            var runDao = new RunDao(connectionFactory);
            var sensorMeasurementDao = new SensorMeasurementDao(connectionFactory);

            var daoProvider = new DaoProvider(countryDao, locationDao, raceDao, runDao, sensorMeasurementDao, skierDao);
            var messengerHub = new MvxMessengerHub();
            var simulatorRaceClock = new SimulatorRaceClock();
            Mvx.IoCProvider.RegisterSingleton<IMvxMessenger>(messengerHub);
            Mvx.IoCProvider.RegisterSingleton<IDialogService>(new DialogService(messengerHub));
            Mvx.IoCProvider.RegisterSingleton<IRaceService>(new RaceService(daoProvider));
            Mvx.IoCProvider.RegisterSingleton<ILocationService>(new LocationService(daoProvider));
            Mvx.IoCProvider.RegisterSingleton<ISkierService>(new SkierService(daoProvider));
            Mvx.IoCProvider.RegisterSingleton<IRunService>(new RunService(daoProvider, simulatorRaceClock));
            Mvx.IoCProvider.RegisterSingleton<SimulatorRaceClock>(simulatorRaceClock);

            RegisterAppStart<ViewModels.NavigationRootViewModel>();
        }

        private string GetDatabaseName(string connectionString)
        {
            Regex regex = new Regex("(?<key>[^=;,]+)=(?<val>[^;,]+(,\\d+)?)", RegexOptions.IgnoreCase);
            Match match = regex.Match(connectionString);
            return match.Groups["val"].Value;
        }
    }
}
