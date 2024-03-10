using FluentAssertions;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Users;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Movies;
using MovieClubManager.Persistence.EF.Rents;
using MovieClubManager.Persistence.EF.Users;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Rents;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Rents.Contracts.Dtos;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Rents.Fctories;
using MovieClubManager.Test.Tools.Users.Builders;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieClubManager.Spec.Tests.Rents.Add
{
    [Scenario("اجاره کردن فیلم")]
    [Story("",
AsA = "کاربر ",
IWantTo = " یک فیلم اجاره کنم",
InOrderTo = "فیلم را ببینم")]
    public class AddRentTest : BusinessIntegrationTest
    {
        private readonly RentService _sut;
        private Genre _genre;
        private Movie _movie;
        private User _user;
        
        public AddRentTest()
        {

            _sut = RentServiceFactory.Create(SetupContext);
        }
        [Given("یک فیلم با نام اینسپشن " +
            "و با قیمت روزانه دویست هزارتومان" +
            " و با قیمت جریمه پانزده هزار تومان " +
            "ژانر اکشن وجود دارد")]
        [And("یک کاربر با نام امیر وجود دارد")]
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


        }

        [When("من درخواست اجاره کردن فیلم با نام اینسپشن و با کاربر امیر را دارم")]
        private async Task When()
        {
            var dto = new AddRentDto
            {
                UserId = _user.Id,
                MovieId = _movie.Id
            };
            await _sut.Add(dto);

        }

        [Then("کاربر با نام امیر باید فیلم با نام اینسپشن را اجاره کرده باشد")]
        private void Then()
        {
            var actual = ReadContext.Rents.Single();
            actual.UserId.Should().Be(_user.Id);
            actual.MovieId.Should().Be(_movie.Id);
            actual.DailyPriceRent.Should().Be(_movie.DailyPriceRent);
            actual.DelayPenalty.Should().Be(_movie.DelayPenalty);
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
