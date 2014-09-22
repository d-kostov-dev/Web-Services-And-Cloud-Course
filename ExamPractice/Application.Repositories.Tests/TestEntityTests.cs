namespace Application.Repositories.Tests
{
    using System.Transactions;

    using Application.Data;
    using Application.Data.Interfaces;
    using Application.Data.Repositories;
    using Application.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestEntityTests
    {
        private static TransactionScope tran;
        private IDbContext databaseContext;

        public TestEntityTests()
        {
            this.databaseContext = new ApplicationDbContext();

            // this.dbContext.Database.Initialize(false);
        }

        [TestInitialize]
        public void TestInit()
        {
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void RepositoryCreateMethod_ShoudSaveInDatabase()
        {
            var itemToAdd = this.GetValidItem("Create");

            var currentRepository = new AppRepository<TestEntity>(this.databaseContext);

            currentRepository.Add(itemToAdd);
            currentRepository.SaveChanges();

            var itemInDatabase = currentRepository.Find(itemToAdd.Id);

            Assert.IsNotNull(itemInDatabase);
            Assert.AreEqual(itemToAdd.Id, itemInDatabase.Id);
        }

        [TestMethod]
        public void RepositoryUpdateMethod_ShoudUpdateItemInDatabase()
        {
            var itemToAdd = this.GetValidItem("Update");

            var currentRepository = new AppRepository<TestEntity>(this.databaseContext);

            currentRepository.Add(itemToAdd);
            currentRepository.SaveChanges();

            var itemInDatabase = currentRepository.Find(itemToAdd.Id);
            itemInDatabase.Name = "Changed Name";

            currentRepository.Update(itemInDatabase);
            currentRepository.SaveChanges();

            Assert.AreEqual(itemToAdd.Name, itemInDatabase.Name);
        }

        [TestMethod]
        public void RepositoryDeleteMethod_ShoudDeleteItemFromTheDatabase()
        {
            var itemToAdd = this.GetValidItem("Delete");

            var currentRepository = new AppRepository<TestEntity>(this.databaseContext);

            currentRepository.Add(itemToAdd);
            currentRepository.SaveChanges();

            var itemInDatabase = currentRepository.Find(itemToAdd.Id);

            currentRepository.Delete(itemInDatabase);
            currentRepository.SaveChanges();

            var deletedItem = currentRepository.Find(itemToAdd.Id);

            Assert.IsNull(deletedItem);
        }

        private TestEntity GetValidItem(string marker)
        {
            var newItem = new TestEntity()
            {
                Name = "Test name " + marker,
            };

            return newItem;
        }
    }
}
