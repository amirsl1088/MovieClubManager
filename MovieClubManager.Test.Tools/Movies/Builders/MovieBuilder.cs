using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Movies.Builders
{
    public class MovieBuilder
    {
        private readonly Movie _movie;
        public MovieBuilder()
        {
            _movie= new Movie
            {
                Name = "svgs",
                PublishYear = "123",
                DailyPriceRent = 200,
                DelayPenalty = 10,
                AgeLimit = 18,
                Director = "afef",
                Duration = 2.50,
                GenreId = 1
            };
        }
        public MovieBuilder WithName(string name)
        {
            _movie.Name = name;
            return this;
        }
        public MovieBuilder WithGenreId(int genreid)
        {
            _movie.GenreId = genreid;
            return this;
        }
        public Movie Build()
        {
            return _movie;
        }
    }
}
