namespace Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        private ICollection<Guess> guesses;

        public Game()
        {
            this.guesses = new HashSet<Guess>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual GameState GameState { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public virtual User Red { get; set; }

        public virtual User Blue { get; set; }

        public int BluePlayerNumber { get; set; }

        [Required]
        public int RedPlayerNumber { get; set; }

        public PlayerInTurn PlayerInTurn { get; set; }

        //public virtual PlayersInGame PlayersInGame { get; set; }

        public virtual ICollection<Guess> Guesses 
        {
            get { return this.guesses; }
            set { this.guesses = value; }
        }
    }
}
