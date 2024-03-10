using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Entities.Movies
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublishYear { get; set; }
        public decimal DailyPriceRent { get; set; }
        public int AgeLimit { get; set; }
        public decimal DelayPenalty { get; set; }
        public decimal Duration { get; set; }
        public string Director { get; set; }
        public string? Description { get; set; }
        public Genre? Genre { get; set; }
        public int GenreId { get; set; }
        public int Count { get; set; } 
        public decimal? Rate { get; set; }
        public HashSet<Rent> Rents { get; set; }


    }
}
