using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Persistence.EF.Movies;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Movies;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MovieClubManager.Service.Genres.Exceptions;
using MovieClubManager.Service.Movies.Exceptions;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Movies.Factories;

namespace MovieClubManager.Services.Unit.Tests.Movies.Update
{
    public class MovieManagerServiceUpdateTests:BusinessUnitTest
    {
       
        private readonly MovieManagerService _sut;
        public MovieManagerServiceUpdateTests()
        {
           
            _sut = MovieManagerSerciceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Update_updates_movie_properly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var genre2 = new GenreBuilder().Build();
            DbContext.Save(genre2);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            DbContext.Save(movie);
            var dto = UpdateMovieDtoFactory.Create(genre2.Id);

            await _sut.Update(movie.Id, dto);

            var actual = ReadContext.Movies.Single();
            actual.Name.Should().Be(dto.Name);
            actual.PublishYear.Should().Be(dto.PublishYear);
            actual.DailyPriceRent.Should().Be(dto.DailyPriceRent);
            actual.DelayPenalty.Should().Be(dto.DelayPenalty);
            actual.AgeLimit.Should().Be(dto.AgeLimit);
            actual.Director.Should().Be(dto.Director);
            actual.Duration.Should().Be(dto.Duration);
            actual.GenreId.Should().Be(dto.GenreId);

        }
        [Fact]
        public async Task Update_throws_exception_when_movieid_not_found_exception()
        {
            var dummyid = 5;
            var genre = new GenreBuilder().Build();
            var dto = UpdateMovieDtoFactory.Create(genre.Id);

            var actual = async () => await _sut.Update(dummyid, dto);

            await actual.Should().ThrowExactlyAsync<MovieIdNotFoundException>();

        }
        [Fact]
        public async Task Update_throws_exception_when_new_genreid_not_found_exception()
        {
            var dummyid = 5;
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            DbContext.Save(movie);
            var dto = UpdateMovieDtoFactory.Create(dummyid);

            var actual = async () => await _sut.Update(movie.Id, dto);

            await actual.Should().ThrowExactlyAsync<GenreIdNotFoundException>();

        }
    }
}
