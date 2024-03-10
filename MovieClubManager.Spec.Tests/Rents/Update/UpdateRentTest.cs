using FluentAssertions;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Rents.Contracts.Dtos;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Rents.Builders;
using MovieClubManager.Test.Tools.Rents.Fctories;
using MovieClubManager.Test.Tools.Users.Builders;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieClubManager.Spec.Tests.Rents.Update
{
    [Scenario("ویرایش کردن اجاره")]
    [Story("",
AsA = "کاربر ",
IWantTo = "  یک اجاره ویرایش کنم",
InOrderTo = "فیلم را تحویل دهم")]
    public class UpdateRentTest:BusinessIntegrationTest
    {
        private readonly RentService _sut;
        private Genre _genre;
        private Movie _movie;
        private User _user;
        private Rent _rent;
        private readonly DateTime _faketime;
        private decimal? _cost;
        public UpdateRentTest()
        {
            _faketime = new DateTime(2024, 03, 12);
            _sut = RentServiceFactory.Create(SetupContext,_faketime);
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
                .WithDailyPriceRent(_movie.DailyPriceRent)
                .WithDelayPenalty(_movie.DelayPenalty)
                .Build();
            
            Save(_rent);

        }

        [When("من درخواست تحویل دادن فیلم و دیدن صورت حساب را دارم")]
        private async Task When()
        {
            var dto = new UpdateRentDto
            {
                MovieRate = 4,
                GiveBack=_faketime
            };
             _cost=await _sut.Update(_rent.Id,dto);

        }

        [Then("باید فیلم تحویل داده شده باشدد")]
        [And("باید صورت حساب نمایش داده شود")]
        private void Then()
        {
            var actual = ReadContext.Rents.First();

            actual.Cost.Should().Be(_cost);
            actual.MovieRate.Should().Be(4);
            actual.GiveBack.Should().Be(_faketime);
           

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
