namespace Application.WebApi.Models
{
    using System;
    using System.Linq.Expressions;

    using Application.Models;

    public class GuessModel
    {
        public static Expression<Func<Guess, GuessModel>> ViewModel
        {
            get
            {
                return x => new GuessModel()
                {
                    Id = x.Id,
                    UserName = x.User.UserName,
                    UserId = x.User.Id,
                    GameId = x.ForGame.Id,
                    DateMade = x.DateMade,
                    CowsCount = x.CowsCount,
                    BullsCount = x.BullsCount,
                    Number = x.GuessNumber,
                };
            }
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public int GameId { get; set; }

        public int Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }

    public class MakeGuessModel
    {
        public int Number { get; set; }
    }

    public class GuessResultMotel
    {
        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}