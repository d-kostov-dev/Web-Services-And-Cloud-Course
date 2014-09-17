namespace MusicCatalog.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Genre { get; set; }

        public Album Album { get; set; }
    }
}
