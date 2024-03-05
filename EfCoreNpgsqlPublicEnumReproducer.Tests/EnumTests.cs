using EnumBugReproducer.DAL;
using EnumBugReproducer.Models.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace EnumBugReproducer.Tests
{

    [TestClass]
    public class EnumTests
    {
        private const string ConnString = "Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=password;";

        [TestMethod]
        public void Test()
        {
            var dbContext = new TestDbContextFactory(ConnString).CreateDbContext<MyContext>();
            dbContext.Database.Migrate();
            dbContext.Entities.Add(new DbEntitySubclassOne { Id = Guid.NewGuid() });
            dbContext.Entities.Add(new DbEntitySubclassTwo { Id = Guid.NewGuid() });
            dbContext.SaveChanges();
            Assert.AreEqual(1, dbContext.SubOneEntities.Count());
        }
    }

    class TestDbContextFactory
    {
        private readonly string _dbConnectionString;

        public TestDbContextFactory(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }

        public TDbContext CreateDbContext<TDbContext>()
          where TDbContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseNpgsql(_dbConnectionString);
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
        }
    }
}
