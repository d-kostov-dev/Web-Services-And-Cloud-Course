namespace Application.Services.Tests
{
    using System;
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
    public class GamesControllerTests
    {
        private static readonly Random RandomNumber = new Random();

        [TestMethod]
        public void GetAllAvailableGamesTest()
        {
            var itemsCollection = this.GenerateValidItems(15);

            var currentRepository = Mock.Create<IRepository<Game>>();
            Mock.Arrange(() => currentRepository.All()).Returns(() => itemsCollection.AsQueryable());

            var dataProvider = Mock.Create<IDataProvider>();
            Mock.Arrange(() => dataProvider.Games).Returns(() => currentRepository);

            var userProvider = Mock.Create<IUserIdProvider>();
            Mock.Arrange(() => userProvider.GetUserId()).Returns(() => "1");

            var controller = new GamesController(dataProvider, userProvider);
            ControllerSetup.SetupController(controller, "games");

            var actionResult = controller.GetAll();
            var response = actionResult.ExecuteAsync(CancellationToken.None).Result;

            var actual = response.Content.ReadAsAsync<IEnumerable<GameSimpleModel>>()
                .Result.Select(x => x.Id).ToList();

            var expected = itemsCollection.AsQueryable()
                            .Where(x => x.GameState == GameState.WaitingForOpponent)
                            .OrderBy(x => x.GameState)
                            .ThenBy(x => x.Name)
                            .ThenBy(x => x.DateCreated)
                            .ThenBy(x => x.Red.UserName)
                            .Take(10)
                            .Select(x => x.Id)
                            .ToList();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        private Game[] GenerateValidItems(int itemsCount = 15)
        {
            Game[] itemsSet = new Game[itemsCount];

            for (int i = 0; i < itemsCount; i++)
            {
                var currentItem = new Game()
                {
                    Id = i * 5,
                    Name = string.Format("Test Name: {0}", i),
                    DateCreated = DateTime.Now.AddDays(RandomNumber.Next(10)),
                    Red = new User() { UserName = string.Format("Test Name: {0}", i) },
                    GameState = GameState.WaitingForOpponent,
                    Blue = new User(),
                };

                itemsSet[i] = currentItem;
            }

            return itemsSet;
        }
    }
}
