//namespace Application.Services.Tests
//{
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Net.Http;
//    using System.Threading;

//    using Application.Data.Interfaces;
//    using Application.Models;
//    using Application.WebApi.Controllers;
//    using Application.WebApi.Models;
//    using Application.WebApi.Providers;

//    using Microsoft.VisualStudio.TestTools.UnitTesting;
//    using Telerik.JustMock;

//    [TestClass]
//    public class TestEntitiesControllerTests
//    {
//        [TestMethod]
//        public void GetAllServiceTest()
//        {
//            var itemsCollection = this.GenerateValidItems(1);

//            var currentRepository = Mock.Create<IRepository<TestEntity>>();
//            Mock.Arrange(() => currentRepository.All()).Returns(() => itemsCollection.AsQueryable());

//            var dataProvider = Mock.Create<IDataProvider>();
//            Mock.Arrange(() => dataProvider.TestEntities).Returns(() => currentRepository);

//            var userProvider = Mock.Create<IUserIdProvider>();
//            Mock.Arrange(() => userProvider.GetUserId()).Returns(() => "1");

//            var controller = new TestEntitiesController(dataProvider, userProvider);
//            ControllerSetup.SetupController(controller, "testentities");

//            var actionResult = controller.GetAll();
//            var response = actionResult.ExecuteAsync(CancellationToken.None).Result;

//            var actual = response.Content.ReadAsAsync<IEnumerable<TestEntityModel>>()
//                .Result.Select(x => x.Id).ToList();

//            var expected = itemsCollection.AsQueryable().Select(a => a.Id).ToList();

//            CollectionAssert.AreEquivalent(expected, actual);
//        }

//        [TestMethod]
//        public void AddServiceTest()
//        {
//            var itemToAdd = this.CreateTestEntityModel();

//            var currentRepository = Mock.Create<IRepository<TestEntity>>();
//            Mock.Arrange(() => currentRepository.Add(Arg.IsAny<TestEntity>())).DoNothing();

//            var userRepo = Mock.Create<IRepository<User>>();
//            Mock.Arrange(() => userRepo.Find(Arg.IsAny<string>())).Returns(() => new User());

//            var dataProvider = Mock.Create<IDataProvider>();
//            Mock.Arrange(() => dataProvider.TestEntities).Returns(() => currentRepository);

//            var userProvider = Mock.Create<IUserIdProvider>();
//            Mock.Arrange(() => userProvider.GetUserId()).DoNothing();

//            var controller = new TestEntitiesController(dataProvider, userProvider);
//            ControllerSetup.SetupController(controller, "testentities");

//            var actionResult = controller.Create(itemToAdd);
//            var response = actionResult.ExecuteAsync(CancellationToken.None).Result;

//            var actual = response.Content.ReadAsAsync<TestEntityModel>().Result;
//            var expected = itemToAdd;

//            Assert.AreEqual(expected.Name, actual.Name);
//        }

//        private TestEntity[] GenerateValidItems(int itemsCount = 15)
//        {
//            TestEntity[] itemsSet = new TestEntity[itemsCount];

//            for (int i = 0; i < itemsCount; i++)
//            {
//                var testEntityItem = new TestEntity
//                {
//                    Name = "Test Name " + i,
//                    User = new User(),
//                };

//                itemsSet[i] = testEntityItem;
//            }

//            return itemsSet;
//        }

//        private TestEntityModel CreateTestEntityModel()
//        {
//            return new TestEntityModel()
//            {
//                Name = "Test Name",
//            };
//        }
//    }
//}
