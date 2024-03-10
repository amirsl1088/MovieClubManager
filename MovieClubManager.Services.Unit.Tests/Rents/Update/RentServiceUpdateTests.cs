using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Rents.Contracts;
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

namespace MovieClubManager.Services.Unit.Tests.Rents.Update
{
    public class RentServiceUpdateTests:BusinessUnitTest
    {
        private readonly RentService _sut;
        private readonly DateTime _faketime;
        public RentServiceUpdateTests()
        {
            _faketime = new DateTime(2024, 03, 12);
            _sut = RentServiceFactory.Create(SetupContext, _faketime);
        }
        [Fact]
        public async Task Update_updates_rent_form_properly()
        {
            var genre = new GenreBuilder().WithTitle("اکشن ").Build();
            DbContext.Save(genre);
            var movie = new MovieBuilder().WithName("اینسپشن")
            .WithDailyPercentRent(200)
               .WithDelayPenalty(15)
            .WithGenreId(genre.Id)
               .Build();
            DbContext.Save(movie);
            var user = new UserBuilder().WithFirstName("امیر").Build();
            DbContext.Save(user);
           var rent = new RentBuilder().WithMovieId(movie.Id)
                .WithUserId(user.Id)
                .WithDailyPriceRent(movie.DailyPriceRent)
                .WithDelayPenalty(movie.DelayPenalty)
                .Build();

            DbContext.Save(rent);
            var dto = UpdateRentDtoFactory.Create(4, _faketime);
            var cost = await _sut.Update(rent.Id, dto);

            var actual = ReadContext.Rents.First();
            actual.MovieRate.Should().Be(dto.MovieRate);
            actual.GiveBack.Should().Be(dto.GiveBack);
            actual.Cost.Should().Be(cost);
        }
        [Fact]
        public async Task Update_throws_exception_when_rentid_not_found_exception()
        {
            var dummyRentId = 5;
            var dto = UpdateRentDtoFactory.Create(4, _faketime);

            var actual = ()=> _sut.Update(dummyRentId, dto);

           await actual.Should().ThrowExactlyAsync<RentIdNotFoundException>();
        }
        [Fact]
        public async Task Update_throws_exception_when_giveback_datetime_befor_than_today_exception()
        {
            var genre = new GenreBuilder().WithTitle("اکشن ").Build();
            DbContext.Save(genre);
            var movie = new MovieBuilder().WithName("اینسپشن")
            .WithDailyPercentRent(200)
               .WithDelayPenalty(15)
            .WithGenreId(genre.Id)
               .Build();
            DbContext.Save(movie);
            var user = new UserBuilder().WithFirstName("امیر").Build();
            DbContext.Save(user);
            var rent = new RentBuilder().WithMovieId(movie.Id)
                 .WithUserId(user.Id)
                 .WithDailyPriceRent(movie.DailyPriceRent)
                 .WithDelayPenalty(movie.DelayPenalty)
                 .Build();

            DbContext.Save(rent);
            var dto = UpdateRentDtoFactory.Create(4, new DateTime(2023, 10, 12));

            var actual = () => _sut.Update(rent.Id, dto);

          await  actual.Should().ThrowExactlyAsync<DateTimeCannotBeforThanTodayException>();
        }
    }
}
