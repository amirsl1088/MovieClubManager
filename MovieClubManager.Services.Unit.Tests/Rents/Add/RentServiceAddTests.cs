using FluentAssertions;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Movies;
using MovieClubManager.Persistence.EF.Rents;
using MovieClubManager.Persistence.EF.Users;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Rents;
using MovieClubManager.Service.Rents.Contracts.Dtos;
using MovieClubManager.Service.Rents.Exceptions;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Rents.Builders;
using MovieClubManager.Test.Tools.Rents.Fctories;
using MovieClubManager.Test.Tools.Users.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.Rents.Add
{
    public class RentServiceAddTests:BusinessUnitTest
    {
       
        private readonly RentAppService _sut;
        private readonly DateTime _faketime;
        public RentServiceAddTests()
        {
           
            _faketime = new DateTime(2024, 03, 10);
            _sut = RentServiceFactory.Create(SetupContext, _faketime);
        }
        [Fact]
        public async Task Add_adds_one_new_rent_properly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            DbContext.Save(movie);
            var user = new UserBuilder().Build();
            DbContext.Save(user);
            var dto = AddRentDtoFactory.Create(user.Id, movie.Id);

            await _sut.Add(dto);

            var actual = ReadContext.Rents.Single();
            actual.UserId.Should().Be(dto.UserId);
            actual.MovieId.Should().Be(dto.MovieId);
            actual.DailyPriceRent.Should().Be(movie.DailyPriceRent);
            actual.DelayPenalty.Should().Be(movie.DelayPenalty);
            actual.RentedAt.Should().Be(_faketime);
        }
        [Fact]
        public async Task Add_throws_exception_when_userid_not_found_exception()
        {
            var dummyUserId = 4;
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            DbContext.Save(movie);
            var dto = AddRentDtoFactory.Create(dummyUserId, movie.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<UserNotFoundException>();
        }
        [Fact]
        public async Task Add_throws_exception_when_movieid_not_found_exception()
        {
            var dummyMovieId = 5;
            var user = new UserBuilder().Build();
            DbContext.Save(user);
            var dto = AddRentDtoFactory.Create(user.Id, dummyMovieId);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<MovieNotFoundException>();
        }
        [Fact]
        public async Task Add_throws_exception_when_movie_is_already_rented_exception()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            DbContext.Save(movie);
            var user = new UserBuilder().Build();
            DbContext.Save(user);
            var rent = new RentBuilder()
                .WithUserId(user.Id)
                .WithMovieId(movie.Id)
                .WithDailyPriceRent(movie.DailyPriceRent)
                .WithDelayPenalty(movie.DelayPenalty)
                .Build();
            DbContext.Save(rent);
            var dto = AddRentDtoFactory.Create(user.Id, movie.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<MovieIsAlreadyRentedException>();
        }
    }
}
