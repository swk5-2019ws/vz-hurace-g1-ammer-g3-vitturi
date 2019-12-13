using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Hurace.Core;
using Hurace.Core.Daos;
using Hurace.Core.Interface;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Hurace.RaceControl.Core
{
    public class App: MvxApplication
    {
        public override void Initialize()
        {
            var connectionString = ConfigurationReader.GetConnectionString(Environment.Production);
            var databaseName = GetDatabaseName(connectionString);
            var task = Task.Run(async () => await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists)).Result;
            var databasePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName);
            var customConnectionString = connectionString.Replace(databaseName, databasePath, StringComparison.CurrentCulture);

            var connectionFactory = new ConnectionFactory(Environment.Production, customConnectionString);
            Mvx.IoCProvider.RegisterSingleton<IRaceDao>(new RaceDao(connectionFactory));
            Mvx.IoCProvider.RegisterSingleton<ILocationDao>(new LocationDao(connectionFactory));
            Mvx.IoCProvider.RegisterSingleton<ISkierDao>(new SkierDao(connectionFactory));

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
