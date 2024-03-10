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
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Movies.Factories;

namespace MovieClubManager.Services.Unit.Tests.Movies.Add
{
    public class MovieManagerServiceAddTests:BusinessUnitTest
    {
       
        private readonly MovieManagerService _sut;
        public MovieManagerServiceAddTests()
        {
           
            _sut = MovieManagerSerciceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Add_adds_one_new_movie_properly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var dto = AddMovieDtoFactory.Create(genre.Id);

            await _sut.Add(dto);

            var actual = ReadContext.Movies.Single();
            actual.Name.Should().Be(dto.Name);
            actual.PublishYear.Should().Be(dto.PublishYear);
            actual.DailyPriceRent.Should().Be(dto.DailyPriceRent);
            actual.AgeLimit.Should().Be(dto.AgeLimit);
            actual.DelayPenalty.Should().Be(dto.DelayPenalty);
            actual.Duration.Should().Be(dto.Duration);
            actual.GenreId.Should().Be(dto.GenreId);

        }
        [Fact]
        public async Task Add_throws_exception_whwn_genreid_not_found_exception()
        {
            var dummyid = 5;
            var dto = AddMovieDtoFactory.Create(dummyid);

            var actual = ()=> _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<GenreIdNotFoundException>();

        }
    }
}
