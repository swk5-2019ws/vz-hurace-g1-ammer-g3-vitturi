using System;
using Hurace.Core.Daos;
using Hurace.Core.Interface;

namespace Hurace.Core.Test
{
    public static class DaoProviderHelper
    {
        public static DaoProvider GetPartialDaoProvider(ICountryDao countryDao = null, ILocationDao locationDao = null,
            IRaceDao raceDao = null, IRunDao runDao = null,
            ISensorMeasurementDao sensorMeasurementDao = null, ISkierDao skierDao = null)
        {
            // var connectionFactory = new ConnectionFactory(Environment.Testing);
            //
            // ICountryDao countryDao = new CountryDao(connectionFactory);
            // ILocationDao locationDao = new LocationDao(connectionFactory);
            // IRaceDao raceDao = new RaceDao(connectionFactory);
            // IRunDao runDao = new RunDao(connectionFactory);
            // ISensorMeasurementDao sensorMeasurementDao = new SensorMeasurementDao(connectionFactory);
            // ISkierDao skierDao = new SkierDao(connectionFactory);

            return new DaoProvider(countryDao, locationDao, raceDao, runDao, sensorMeasurementDao, skierDao);
        }
    }
}