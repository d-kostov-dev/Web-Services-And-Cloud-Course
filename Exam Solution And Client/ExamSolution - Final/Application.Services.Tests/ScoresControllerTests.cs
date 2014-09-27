namespace Application.Services.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;

    using Application.Data.Interfaces;
    using Application.Models;
    using Application.WebApi.Controllers;
    using Application.WebApi.Models;
    using Application.WebApi.Providers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Telerik.JustMock;

    [TestClass]
    public class ScoresControllerTests
    {
        [TestMethod]
        public void GetTopPlayersTest()
        {
            var itemsCollection = this.GenerateValidItems(15);

            var currentRepository = Mock.Create<IRepository<User>>();
            Mock.Arrange(() => currentRepository.All()).Returns(() => itemsCollection.AsQueryable());

            var dataProvider = Mock.Create<IDataProvider>();
            Mock.Arrange(() => dataProvider.Users).Returns(() => currentRepository);

            var userProvider = Mock.Create<IUserIdProvider>();
            Mock.Arrange(() => userProvider.GetUserId()).Returns(() => "1");

            var controller = new ScoresController(dataProvider, userProvider);
            ControllerSetup.SetupController(controller, "scores");

            var actionResult = controller.GetTopPlayers();
            var response = actionResult.ExecuteAsync(CancellationToken.None).Result;

            var actual = response.Content.ReadAsAsync<IEnumerable<ScoreModel>>()
                .Result.Select(x => x.Rank).ToList();

            var expected = itemsCollection.AsQueryable().OrderByDescending(a => a.Rank).Select(a => a.Rank).Take(10).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        private User[] GenerateValidItems(int itemsCount = 15)
        {
            User[] itemsSet = new User[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                var currentItem = new User()
                {
                    UserName = string.Format("user{0}@abv.bg", i),
                    Rank = i * 100,
                };

                itemsSet[i] = currentItem;
            }

            return itemsSet;
        }
    }
}
