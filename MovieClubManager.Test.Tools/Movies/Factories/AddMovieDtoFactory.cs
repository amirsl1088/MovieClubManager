using MovieClubManager.Entities.Genres;
using MovieClubManager.Service.Movies.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Movies.Factories
{
    public static class AddMovieDtoFactory
    {
        public static AddMovieDto Create(int? genreid=null)
        {
            return new AddMovieDto
            {
                Name = "dddffg",
                PublishYear = "124",
                DailyPriceRent = 132,
                AgeLimit = 18,
                DelayPenalty = 20,
                Duration = 120,
                Director = "sgsg",
                GenreId = genreid ??1

            };
        }
    }
}
