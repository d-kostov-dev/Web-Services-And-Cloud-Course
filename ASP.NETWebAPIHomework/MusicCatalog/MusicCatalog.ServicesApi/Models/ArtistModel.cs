using MusicCatalog.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MusicCatalog.ServicesApi.Models
{
    public class ArtistModel
    {
        public static Expression<Func<Artist, ArtistModel>> PrepareModel
        {
            get
            {
                return x => new ArtistModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Country = x.Country,
                    DateOfBirth = x.DateOfBirth
                };
            }
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}