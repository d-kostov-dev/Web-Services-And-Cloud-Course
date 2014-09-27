namespace Application.WebApi.Controllers
{
    using System;
    using System.Web.Http;

    using Application.Data.Interfaces;
    using Application.Models;
    using Application.WebApi.Models;
    using Application.WebApi.Providers;
    
    public class GuessesController : BaseController
    {
        public GuessesController(IDataProvider dataProvider, IUserIdProvider userIdProvider)
            : base(dataProvider, userIdProvider)
        {
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult MakeGuess(int id, MakeGuessModel guessMade)
        {
            // Check if the number is valid...no need to continue if it's not;
            if (!IsValidNumber(guessMade.Number))
            {
                return this.BadRequest("Invalid Number");
            }

            // Get the game
            var currentGame = this.data.Games.Find(id);
            if (currentGame == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "game"));
            }

            // Check if game can be played
            if (currentGame.GameState != GameState.InProgress)
            {
                return this.BadRequest("This game is not in progress! You can't play in this game!");
            }

            // Get the user
            var userId = this.userIdProvider.GetUserId();
            var currentUser = this.data.Users.Find(userId);
            if (currentUser == null)
            {
                return this.BadRequest(string.Format(INVALID_ITEM_FORMAT, "user"));
            }

            // Check if the user is in this game
            if (currentUser.UserName != currentGame.Red.UserName && currentUser.UserName != currentGame.Blue.UserName)
            {
                return this.BadRequest("This is not your game!");
            }

            // Process turn
            if (currentGame.PlayerInTurn == PlayerInTurn.Red)
            {
                if (currentUser.UserName != currentGame.Red.UserName)
                {
                    return this.BadRequest("It's not your turn!");
                }

                var result = CalculateGuessResult(guessMade.Number, currentGame.BluePlayerNumber);

                if (result.BullsCount == 4)
                {
                    // Finish the game
                    currentGame.GameState = GameState.Finished;

                    // Notifcation for both players
                    // Blue
                    var notificationForBlue = new Notification()
                    {
                        Message = string.Format("{0} beat you in game \"{1}\"", currentUser.UserName, currentGame.Name),
                        Game = currentGame,
                        Type = NotificationType.GameLost,
                        User = currentGame.Blue,
                        DateCreated = DateTime.Now,
                        State = NotificationState.Unread
                    };

                    // Calculate Rank
                    currentGame.Blue.Losses++;
                    currentGame.Blue.Rank = (currentGame.Blue.Wins * 100) + (currentGame.Blue.Losses * 15);

                    // Red
                    var notificationForRed = new Notification()
                    {
                        Message = string.Format("You beat {0} in game \"{1}\"", currentGame.Blue.UserName, currentGame.Name),
                        Game = currentGame,
                        Type = NotificationType.GameWon,
                        User = currentGame.Red,
                        DateCreated = DateTime.Now,
                        State = NotificationState.Unread
                    };

                    // Calculate Rank
                    currentGame.Red.Wins++;
                    currentGame.Red.Rank = (currentGame.Red.Wins * 100) + (currentGame.Red.Losses * 15);

                    this.data.Notifications.Add(notificationForBlue);
                    this.data.Notifications.Add(notificationForRed);

                    this.data.SaveChanges();
                }
                else
                {
                    // Change Turn Notification
                    var notification = new Notification()
                    {
                        Message = string.Format("It is your turn in game \"{0}!\"", currentGame.Name),
                        Game = currentGame,
                        Type = NotificationType.YourTurn,
                        User = currentGame.Blue,
                        DateCreated = DateTime.Now,
                        State = NotificationState.Unread
                    };

                    this.data.Notifications.Add(notification);

                    // Change turn
                    currentGame.PlayerInTurn = PlayerInTurn.Blue;

                    this.data.SaveChanges();
                }

                var guessToSave = new Guess()
                {
                    GuessNumber = guessMade.Number,
                    User = currentUser,
                    ForGame = currentGame,
                    BullsCount = result.BullsCount,
                    CowsCount = result.CowsCount,
                    DateMade = DateTime.Now,
                };

                this.data.Guesses.Add(guessToSave);
                this.data.SaveChanges();

                var guessToReturn = new GuessModel()
                {
                    UserName = currentUser.UserName,
                    BullsCount = result.BullsCount,
                    CowsCount = result.CowsCount,
                    UserId = currentUser.Id,
                    GameId = currentGame.Id,
                    DateMade = DateTime.Now,
                    Number = guessMade.Number
                };

                return this.Ok(guessToReturn);
            }
            else
            {
                if (currentUser.UserName != currentGame.Blue.UserName)
                {
                    return this.BadRequest("It's not your turn!");
                }

                var result = CalculateGuessResult(guessMade.Number, currentGame.RedPlayerNumber);

                if (result.BullsCount == 4)
                {
                    // Finish the game
                    currentGame.GameState = GameState.Finished;

                    // Notifcation for both players
                    // Blue
                    var notificationForBlue = new Notification()
                    {
                        Message = string.Format("{0} beat you in game \"{1}\"", currentUser.UserName, currentGame.Name),
                        Game = currentGame,
                        Type = NotificationType.GameLost,
                        User = currentGame.Red,
                        DateCreated = DateTime.Now,
                        State = NotificationState.Unread
                    };

                    // Calculate Rank
                    currentGame.Red.Losses++;
                    currentGame.Red.Rank = (currentGame.Red.Wins * 100) + (currentGame.Red.Losses * 15);

                    // Red
                    var notificationForRed = new Notification()
                    {
                        Message = string.Format("You beat {0} in game \"{1}\"", currentGame.Red.UserName, currentGame.Name),
                        Game = currentGame,
                        Type = NotificationType.GameWon,
                        User = currentGame.Blue,
                        DateCreated = DateTime.Now,
                        State = NotificationState.Unread
                    };

                    // Calculate Rank
                    currentGame.Blue.Wins++;
                    currentGame.Blue.Rank = (currentGame.Blue.Wins * 100) + (currentGame.Blue.Losses * 15);

                    this.data.Notifications.Add(notificationForBlue);
                    this.data.Notifications.Add(notificationForRed);

                    this.data.SaveChanges();
                }
                else
                {
                    // Change Turn Notification
                    var notification = new Notification()
                    {
                        Message = string.Format("It is your turn in game \"{0}!\"", currentGame.Name),
                        Game = currentGame,
                        Type = NotificationType.YourTurn,
                        User = currentGame.Red,
                        DateCreated = DateTime.Now,
                        State = NotificationState.Unread
                    };

                    this.data.Notifications.Add(notification);

                    // Change turn
                    currentGame.PlayerInTurn = PlayerInTurn.Red;

                    this.data.SaveChanges();
                }

                var guessToSave = new Guess()
                {
                    GuessNumber = guessMade.Number,
                    User = currentUser,
                    ForGame = currentGame,
                    BullsCount = result.BullsCount,
                    CowsCount = result.CowsCount,
                    DateMade = DateTime.Now,
                };

                this.data.Guesses.Add(guessToSave);
                this.data.SaveChanges();

                var guessToReturn = new GuessModel()
                {
                    UserName = currentUser.UserName,
                    BullsCount = result.BullsCount,
                    CowsCount = result.CowsCount,
                    UserId = currentUser.Id,
                    GameId = currentGame.Id,
                    DateMade = DateTime.Now,
                    Number = guessMade.Number
                };

                return this.Ok(guessToReturn);
            }
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

        private GuessResultMotel CalculateGuessResult(int inputNumber, int numberToGuess)
        {
            var guessResult = new GuessResultMotel();

            for (var i = 0; i < 4; i++)
            {
                var currentGuessNumber = inputNumber.ToString()[i];
                var indexInSecretNumber = numberToGuess.ToString().IndexOf(currentGuessNumber);

                if (indexInSecretNumber > -1)
                {
                    if (indexInSecretNumber == i)
                    {
                        guessResult.BullsCount++;
                    }
                    else
                    {
                        guessResult.CowsCount++;
                    }
                }
            }

            return guessResult;
        }
    }
}
