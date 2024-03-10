using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Entities.Users;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Rents;
using MovieClubManager.Service.Rents;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Rents.Contracts.Dtos;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Rents.Builders;
using MovieClubManager.Test.Tools.Users.Builders;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieClubManager.Spec.Tests.RentManagers.Delete
{
    [Scenario("حذف کردن اجاره")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = " یک اجاره حذف کنم",
InOrderTo = "ان اجاره را لغو کنم")]
    public class DeleteRentManagerTest : BusinessIntegrationTest
    {
        private readonly RentManagerService _sut;
        private Genre _genre;
        private Movie _movie;
        private User _user;
        private Rent _rent;
        public DeleteRentManagerTest()
        {
            _sut = new RentManagerAppService(new EFRentRepository(SetupContext), new EFUnitOfWork(SetupContext));
        }
        [Given("یک اجاره با فیلم اینسپشن و کاربر امیر وجود دارد")]

        private void Given()
        {
            _genre = new GenreBuilder().WithTitle("اکشن ").Build();
            Save(_genre);
            _movie = new MovieBuilder().WithName("اینسپشن")
               .WithDailyPercentRent(200)
               .WithDelayPenalty(15)
               .WithGenreId(_genre.Id)
               .Build();
            Save(_movie);
            _user = new UserBuilder().WithFirstName("امیر").Build();
            Save(_user);
            _rent = new RentBuilder().WithMovieId(_movie.Id)
                .WithUserId(_user.Id)
                .Build();
            Save(_rent);

        }

        [When("من درخواست حذف کردن اجاره مذکور را دارم")]
        private async Task When()
        {
            await _sut.Delete(_rent.Id);

        }

        [Then("اجاره مذکور باید حذف شده باشد")]
        private void Then()
        {
            var actual = ReadContext.Rents.FirstOrDefault(_=>_.Id==_rent.Id);
            actual.Should().BeNull();
          


        }


        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then());
        }
    }
}
