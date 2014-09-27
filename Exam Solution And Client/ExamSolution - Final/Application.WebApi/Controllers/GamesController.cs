namespace Application.WebApi.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using Application.Data.Interfaces;
    using Application.Models;
    using Application.WebApi.Models;
    using Application.WebApi.Providers;

    public class GamesController : BaseController
    {
        private const int ItemsPerPage = 10;

        private static readonly Random RandomNumber = new Random();

        public GamesController(IDataProvider dataProvider, IUserIdProvider userIdProvider)
            : base(dataProvider, userIdProvider)
        {
        }

        // Public
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return this.GetAll(0);
        }

        // Public And Authorized
        [HttpGet]
        public IHttpActionResult GetAll(int page)
        {
            var allItems = this.data.Games.All();

            var userId = this.userIdProvider.GetUserId();
            var currentUser = this.data.Users.Find(userId);

            // If the user is logged in show all his games or the games he can join. Else show only games available for play
            if (currentUser != null)
            {
                allItems = allItems
                    .Where(x => 
                        ((x.Red.UserName == currentUser.UserName || x.Blue.UserName == currentUser.UserName) && x.GameState == GameState.InProgress) ||
                            x.GameState == GameState.WaitingForOpponent)
                    .AsQueryable();
            }
            else
            {
                allItems = allItems
                    .Where(x => x.GameState == GameState.WaitingForOpponent)
                    .AsQueryable();
            }

            var finalItems = allItems
                            .OrderBy(x => x.GameState)
                            .ThenBy(x => x.Name)
                            .ThenBy(x => x.DateCreated)
                            .ThenBy(x => x.Red.UserName)
                            .Skip(ItemsPerPage * page)
                            .Take(ItemsPerPage)
                            .Select(GameSimpleModel.ViewModel);

            return this.Ok(finalItems);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetById(int id)
        {
            // Get the game
            var currentGame = this.data.Games.Find(id);
            if (currentGame == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "game"));
            }

            // Check if game has both players (Returns the game details about a game that is currently played)
            if (currentGame.Blue == null || currentGame.GameState == GameState.Finished)
            {
                return this.BadRequest("This game is not in play");
            }

            // Get the user
            var userId = this.userIdProvider.GetUserId();
            var currentUser = this.data.Users.Find(userId);
            if (currentUser == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "user"));
            }

            // Check if the user has permisions for this game
            if (currentGame.Red.UserName != currentUser.UserName && currentGame.Blue.UserName != currentUser.UserName)
            {
                return this.BadRequest("You are not allowed to see this game details.");
            }

            var guesses = currentGame.Guesses.AsQueryable().Select(GuessModel.ViewModel).ToList();

            var itemToReturn = new GameDetailedModel()
            {
                Id = currentGame.Id,
                Name = currentGame.Name,
                DateCreated = currentGame.DateCreated,
                GameState = currentGame.GameState.ToString(),
                Red = currentGame.Red.UserName,
                Blue = currentGame.Blue.UserName,
                YourGuesses = guesses,
            };

            return this.Ok(itemToReturn);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult CreateGame(CreateGameModel item)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var userId = this.userIdProvider.GetUserId();
            var currentUser = this.data.Users.Find(userId);

            // Checks if the current user exists. It's a token bug.
            if (currentUser == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "user"));
            }

            if (!this.IsValidNumber(item.Number))
            {
                return this.BadRequest("Invalid Number");
            }

            var newItem = new Game()
            {
                Name = item.Name,
                GameState = GameState.WaitingForOpponent,
                DateCreated = DateTime.Now,
                Red = currentUser,
                RedPlayerNumber = item.Number
            };

            this.data.Games.Add(newItem);
            this.data.SaveChanges();

            var returnItem = new GameSimpleModel(newItem);

            return this.Ok(returnItem);
        }

        [HttpPut]
        [Authorize]
        public IHttpActionResult JoinGame(int id, GameJoinModel item)
        {
            // Get the game
            var currentGame = this.data.Games.Find(id);
            if (currentGame == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "game"));
            }

            // Check if game can be played
            if (currentGame.GameState != GameState.WaitingForOpponent)
            {
                return this.BadRequest("This game is in progress. You can't join!");
            }

            // Get the user
            var userId = this.userIdProvider.GetUserId();
            var currentUser = this.data.Users.Find(userId);
            if (currentUser == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "user"));
            }

            // Check if not same player
            if (currentUser.UserName == currentGame.Red.UserName)
            {
                return this.BadRequest("You can't join your own game");
            }

            if (!this.IsValidNumber(item.Number))
            {
                return this.BadRequest("Invalid Number");
            }

            currentGame.Blue = currentUser;
            currentGame.GameState = GameState.InProgress;
            currentGame.PlayerInTurn = (PlayerInTurn)RandomNumber.Next(0, 2);
            currentGame.BluePlayerNumber = item.Number;

            // Create Notification
            var notification = new Notification()
            {
                Message = string.Format("{0} joined your game \"{1} by {2}\"", currentUser.UserName, currentGame.Name, currentGame.Red.UserName),
                Game = currentGame,
                Type = NotificationType.GameJoined,
                User = currentGame.Red,
                DateCreated = DateTime.Now,
                State = NotificationState.Unread
            };

            this.data.Notifications.Add(notification);

            this.data.SaveChanges();

            var result = string.Format("result: You joined game \"{0}\"", currentGame.Name);

            return this.Ok(result);
        }

        private bool IsValidNumber(int number)
        {
            var numberToString = number.ToString();

            if (numberToString.Length != 4)
            {
                return false;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    if (numberToString[i] == numberToString[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
