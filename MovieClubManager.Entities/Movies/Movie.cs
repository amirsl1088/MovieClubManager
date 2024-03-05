using MovieClubManager.Entities.Genres;
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
        public int DailyPriceRent { get; set; }
        public int AgeLimit { get; set; }
        public int DelayPenalty { get; set; }
        public double Duration { get; set; }
        public string Director { get; set; }
        public string? Description { get; set; }
        public Genre? Genre { get; set; }
        public int GenreId { get; set; }
        public int? Count { get; set; } = 1;
        public decimal? Rate { get; set; }
       

    }
}
