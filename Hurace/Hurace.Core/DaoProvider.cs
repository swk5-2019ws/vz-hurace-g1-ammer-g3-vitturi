using Hurace.Core.Interface.Daos;

namespace Hurace.Core
{
    public class DaoProvider
    {
        public ICountryDao CountryDao { get; set; }
        public ILocationDao LocationDao { get; set; }
        public IRaceDao RaceDao { get; set; }
        public IRunDao RunDao { get; set; }
        public ISensorMeasurementDao SensorMeasurementDao { get; set; }
        public ISkierDao SkierDao { get; set; }

        public DaoProvider(ICountryDao countryDao, ILocationDao locationDao, IRaceDao raceDao, IRunDao runDao,
            ISensorMeasurementDao sensorMeasurementDao, ISkierDao skierDao)
        {
            CountryDao = countryDao;
            LocationDao = locationDao;
            RaceDao = raceDao;
            RunDao = runDao;
            SensorMeasurementDao = sensorMeasurementDao;
            SkierDao = skierDao;
        }
    }
}