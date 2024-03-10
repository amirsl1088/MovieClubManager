using FluentAssertions;
using MovieClubManager.Service.Rents;
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

namespace MovieClubManager.Services.Unit.Tests.Rents.Get
{
    public class RentServiceGetTests : BusinessUnitTest
    {
        private readonly RentAppService _sut;
        private readonly DateTime _faketime;
        public RentServiceGetTests()
        {
            _faketime = new DateTime(2024, 03, 10);
            _sut = RentServiceFactory.Create(SetupContext, _faketime);
        }
        [Fact]
        public async Task Get_gets_user_movie_rents_properly()
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

            var actual = await _sut.Get(user.Id);

            actual.First().Id.Should().Be(rent.Id);
            actual.First().MovieId.Should().Be(movie.Id);
            actual.First().UserId.Should().Be(user.Id);
            actual.First().DailyPriceRent.Should().Be(rent.DailyPriceRent);
            actual.First().DelayPenalty.Should().Be(rent.DelayPenalty);
            actual.First().RentedAt.Should().Be(rent.RentedAt);
            actual.First().GiveBack.Should().Be(rent.GiveBack);
            actual.First().MovieRate.Should().Be(rent.MovieRate);
            actual.First().Cost.Should().Be(rent.Cost);

        }
    }
}
