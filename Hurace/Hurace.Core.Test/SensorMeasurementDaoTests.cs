using Hurace.Core.Daos;
using Hurace.Core.Interface.Daos;
using Hurace.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class SensorMeasurementDaoTests
    {
        public static async Task<SensorMeasurement> InsertSensorMeasurement(ConnectionFactory connectionFactory,
            Run run = null)
        {
            ISensorMeasurementDao sensorMeasurementDao = new SensorMeasurementDao(connectionFactory);

            run ??= await RunDaoTests.InsertRun(connectionFactory);
            SensorMeasurement sensorMeasurement = new SensorMeasurement
            {
                SensorId = 1,
                Timestamp = 1576850015.1111,
                Run = run,
            };
            sensorMeasurement.Id = await sensorMeasurementDao.Insert(sensorMeasurement);

            return sensorMeasurement;
        }

        [Fact]
        public async void TestFindAll()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ISensorMeasurementDao sensorMeasurementDao = new SensorMeasurementDao(connectionFactory);

            foreach (var _ in await sensorMeasurementDao.FindAll())
                Assert.True(false, "FindAll should return an empty collection");

            await InsertSensorMeasurement(connectionFactory);
            foreach (var _ in await sensorMeasurementDao.FindAll())
                return;
            Assert.True(false, "FindAll should return a non-empty collection");
        }

        [Fact]
        public async void TestFindById()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ISensorMeasurementDao sensorMeasurementDao = new SensorMeasurementDao(connectionFactory);

            SensorMeasurement sensorMeasurement = await InsertSensorMeasurement(connectionFactory);

            SensorMeasurement sensorMeasurementFound = await sensorMeasurementDao.FindById(sensorMeasurement.Id);
            Assert.Equal(sensorMeasurement.Timestamp, sensorMeasurementFound.Timestamp);
        }

        [Fact]
        public async void TestUpdate()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ISensorMeasurementDao sensorMeasurementDao = new SensorMeasurementDao(connectionFactory);

            SensorMeasurement sensorMeasurement = await InsertSensorMeasurement(connectionFactory);
            sensorMeasurement.Timestamp = 1576850015.2222;
            await sensorMeasurementDao.Update(sensorMeasurement);

            SensorMeasurement sensorMeasurementAfter = await sensorMeasurementDao.FindById(sensorMeasurement.Id);
            Assert.Equal(sensorMeasurement.Timestamp, sensorMeasurementAfter.Timestamp);
        }

        [Fact]
        public async void TestInsert()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);

            SensorMeasurement sensorMeasurement = await InsertSensorMeasurement(connectionFactory);
            Assert.True(sensorMeasurement.Id > 0);
        }

        [Fact]
        public async void TestDelete()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ISensorMeasurementDao sensorMeasurementDao = new SensorMeasurementDao(connectionFactory);

            SensorMeasurement sensorMeasurement = await InsertSensorMeasurement(connectionFactory);
            Assert.NotNull(await sensorMeasurementDao.FindById(sensorMeasurement.Id));

            await sensorMeasurementDao.Delete(sensorMeasurement.Id);
            Assert.Null(await sensorMeasurementDao.FindById(sensorMeasurement.Id));
        }
    }
}