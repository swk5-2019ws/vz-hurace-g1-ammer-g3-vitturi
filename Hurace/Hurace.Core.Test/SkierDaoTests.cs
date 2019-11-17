using System;
using Hurace.Core.Daos;
using Hurace.Core.Interface;
using Hurace.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Hurace.Core.Test
{
    public class SkierDaoTests
    {
        public static async Task<Skier> InsertSkier(ConnectionFactory connectionFactory, Country country = null)
        {
            ISkierDao skierDao = new SkierDao(connectionFactory);

            country ??= await CountryDaoTests.InsertCountry(connectionFactory);
            Skier skier = new Skier
            {
                FirstName = "John",
                LastName = "Doe",
                Birthdate = DateTime.Today,
                PictureUrl = null,
                Archived = false,
                Country = country,
                Gender = Gender.Male
            };
            skier.Id = await skierDao.Insert(skier);

            return skier;
        }

        [Fact]
        public async void TestFindAll()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ISkierDao skierDao = new SkierDao(connectionFactory);

            foreach (var _ in await skierDao.FindAll())
                Assert.True(false, "FindAll should return an empty collection");

            await InsertSkier(connectionFactory);
            foreach (var _ in await skierDao.FindAll())
                return;
            Assert.True(false, "FindAll should return a non-empty collection");
        }

        [Fact]
        public async void TestFindById()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ISkierDao skierDao = new SkierDao(connectionFactory);

            Skier skier = await InsertSkier(connectionFactory);

            Skier skierFound = await skierDao.FindById(skier.Id);
            Assert.Equal(skier.FirstName, skierFound.FirstName);
            Assert.Equal(skier.Gender, skierFound.Gender);
        }

        [Fact]
        public async void TestUpdate()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ISkierDao skierDao = new SkierDao(connectionFactory);

            Skier skier = await InsertSkier(connectionFactory);
            skier.FirstName = "Andrea";
            skier.Gender = Gender.Female;
            await skierDao.Update(skier);

            Skier skierAfter = await skierDao.FindById(skier.Id);
            Assert.Equal(skier.FirstName, skierAfter.FirstName);
            Assert.Equal(skier.Gender, skierAfter.Gender);
        }

        [Fact]
        public async void TestInsert()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);

            Skier skier = await InsertSkier(connectionFactory);
            Assert.True(skier.Id > 0);
        }

        [Fact]
        public async void TestDelete()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory(Environment.Testing);
            ISkierDao skierDao = new SkierDao(connectionFactory);

            Skier skier = await InsertSkier(connectionFactory);
            Assert.NotNull(await skierDao.FindById(skier.Id));

            await skierDao.Delete(skier.Id);
            Assert.Null(await skierDao.FindById(skier.Id));
        }
    }
}