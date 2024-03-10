using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Rents.Builders
{
    public class RentBuilder
    {
        private Rent _rent;
        public RentBuilder()
        {
            _rent= new Rent
            {
                UserId = 1,
                MovieId = 1,
                DailyPriceRent =200,
                DelayPenalty = 15,
                RentedAt = DateTime.UtcNow
            };
        }
        public RentBuilder WithUserId(int userid)
        {
            _rent.UserId = userid;
            return this;
        }
        public RentBuilder WithMovieId(int movieid)
        {
            _rent.MovieId = movieid;
            return this;
        }
        public RentBuilder WithDailyPriceRent(decimal dailypricerent)
        {
            _rent.DailyPriceRent = dailypricerent;
            return this;
        }
        public RentBuilder WithDelayPenalty(decimal delaypenalty)
        {
            _rent.DelayPenalty = delaypenalty;
            return this;
        }
        public Rent Build()
        {
            return _rent;
        }
    }
}
