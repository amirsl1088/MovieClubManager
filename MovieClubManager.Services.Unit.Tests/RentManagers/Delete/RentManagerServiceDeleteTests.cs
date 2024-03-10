using FluentAssertions;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Rents;
using MovieClubManager.Service.Rents;
using MovieClubManager.Service.Rents.Contracts;
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

namespace MovieClubManager.Services.Unit.Tests.RentManagers.Delete
{
    public class RentManagerServiceDeleteTests : BusinessUnitTest
    {
        private readonly RentManagerService _sut;
        public RentManagerServiceDeleteTests()
        {
            _sut = RentManagerServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Delete_deletes_rent_form_properly()
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
                 .Build();
            DbContext.Save(rent);

            await _sut.Delete(rent.Id);

            var actual = ReadContext.Rents.FirstOrDefault(_ => _.Id == rent.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_exception_when_rentid_not_found_exception()
        {
            var dummyRentId = 5;

            var actual = () => _sut.Delete(dummyRentId);

            await actual.Should().ThrowExactlyAsync<RentIdNotFoundException>();
        }
    }
}
