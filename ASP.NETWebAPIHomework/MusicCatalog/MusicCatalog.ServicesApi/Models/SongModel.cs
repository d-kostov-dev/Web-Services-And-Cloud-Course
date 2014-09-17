using MusicCatalog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MusicCatalog.ServicesApi.Models
{
    public class SongModel
    {
        public static Expression<Func<Song, SongModel>> PrepareModel
        {
            get
            {
                return x => new SongModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Year = x.Year,
                    Genre = x.Genre
                };
            }
        }

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
    }
}