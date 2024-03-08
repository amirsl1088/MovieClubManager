using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Service.Movies.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Test.Tools.Movies.Builders
{
    public class AddMovieDtoBuilder
    {
        private readonly AddMovieDto _dto;
        public AddMovieDtoBuilder()
        {
            _dto= new AddMovieDto
            {
                Name = "dddffg",
                PublishYear = "124",
                DailyPriceRent = 132,
                AgeLimit = 18,
                DelayPenalty = 20,
                Duration = 1.54,
                Director = "sgsg",
                GenreId =  1

            };

        }
        public AddMovieDtoBuilder WithName(string name)
        {
            
            _dto.Name = name;
            return this;
        }
        public AddMovieDtoBuilder WithGenreId(int genreid)
        {
            _dto.GenreId = genreid;
            return this;
        }
        public AddMovieDtoBuilder WithPulishYear(string pulishyear)
        {
            _dto.PublishYear = pulishyear;
            return this;
        }
        public AddMovieDtoBuilder WithDailyPercentRent(int price)
        {
            _dto.DailyPriceRent = price;
            return this;
        }
        public AddMovieDtoBuilder WithDelayPenalty(int delaypenalty)
        {
            _dto.DelayPenalty = delaypenalty;
            return this;
        }
        public AddMovieDtoBuilder WithDirector(string director)
        {
            _dto.Director = director;
            return this;
        }
        public AddMovieDtoBuilder WithDuration(double duration)
        {
            _dto.Duration = duration;
            return this;
        }
        public AddMovieDtoBuilder WithAgeLimit(int age)
        {
            _dto.AgeLimit = age;
            return this;
        }
        public AddMovieDto Build()
        {
            return _dto;
        }
    }
}
