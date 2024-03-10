using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Entities.Rents
{
    public class Rent
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public DateTime RentedAt { get; set; }
        public DateTime? GiveBack { get; set; }
        public decimal DailyPriceRent { get; set; }
        public decimal DelayPenalty { get; set; }
        public decimal? Cost { get; set; }


    }
}
