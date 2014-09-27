namespace Application.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Runtime.Serialization;

    using Application.Models;

    public class CreateGameModel
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }

    public class GameDetailedModel
    {
        public GameDetailedModel()
        {
            this.YourGuesses = new HashSet<GuessModel>();
        }

        public static Expression<Func<Game, GameDetailedModel>> ViewModel
        {
            get
            {
                return x => new GameDetailedModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Red = x.Red.UserName != null ? x.Red.UserName : "Error! The game required the red player!",
                    Blue = x.Blue.UserName != null ? x.Blue.UserName : "No blue player yet",
                    GameState = x.GameState.ToString(),
                    DateCreated = x.DateCreated
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public ICollection<GuessModel> YourGuesses { get; set; }

        [DataMember(Name = "number")]
        public int RedPlayerNumber { get; set; }

        public int BluePlayerNumber { get; set; }
    }

    public class GameJoinModel
    {
        public int Number { get; set; }
    }

    public class GameSimpleModel
    {
        public GameSimpleModel()
        {
        }

        public GameSimpleModel(Game game)
        {
            this.Id = game.Id;
            this.Name = game.Name;
            this.Red = game.Red.UserName;
            this.DateCreated = game.DateCreated;
            this.GameState = game.GameState.ToString();
            this.Blue = "No blue player yet";
        }

        public static Expression<Func<Game, GameSimpleModel>> ViewModel
        {
            get
            {
                return x => new GameSimpleModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Red = x.Red.UserName != null ? x.Red.UserName : "Error! The game required the red player!",
                    Blue = x.Blue.UserName == null ? "No blue player yet" : x.Blue.UserName,
                    GameState = x.GameState.ToString(),
                    DateCreated = x.DateCreated
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string GameState { get; set; }

        public DateTime DateCreated { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }
    }
}