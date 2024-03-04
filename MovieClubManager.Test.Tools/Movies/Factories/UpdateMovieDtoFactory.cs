using MovieClubManager.Entities.Genres;
using MovieClubManager.Service.Movies.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Movies.Factories
{
    public static class UpdateMovieDtoFactory
    {
        public static UpdateMovieDto Create(int? genreid=null)
        {
            return new UpdateMovieDto
            {
                Name = "svdfags",
                PublishYear = "33123",
                DailyPriceRent = 20000,
                DelayPenalty = 109,
                AgeLimit = 17,
                Director = "affdgdef",
                Duration = 2.30,
                GenreId = genreid ??1
            };
        }
    }
}
