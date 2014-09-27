namespace Application.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Guess
    {
        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }

        [Required]
        public virtual Game ForGame { get; set; }

        [Required]
        public int GuessNumber { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}
