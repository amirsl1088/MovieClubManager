using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Movies.Factories;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieClubManager.Spec.Tests.Movies
{

    [Scenario("اضافه کردن فیلم")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = " فیلمی به فهرست فیلم ها اضافه کنم",
InOrderTo = "فیلم ها را اجاره دهم")]
    public class AddMovieTest:BusinessIntegrationTest
    {
        private readonly MovieManagerService _sut;
        private Genre _genre;
        public AddMovieTest()
        {
            _sut = MovieManagerSerciceFactory.Create(SetupContext);
        }
        [Given("فیلمی در فهرست فیلم ها وجود ندارد ولی ژانری با نام کمدی وجود دارد")]
        [And("")]
        private void Given()
        {
            _genre = new GenreBuilder().WithTitle("کمدی").Build();
            Save(_genre);
        }

        [When("من درخواست اضافه کردن فیلم به کلاب با اسم اینسپشن" +
            " با سال انتشار دوهزار و پنج " +
            "و با قیمت روزانه دویست هزار تومان" +
            " و قیمت جریمه ده هزار تومان " +
            "ومدت زمان صد و بیست دقیقه" +
            " و کارگردان نولان" +
            " و با ژانر کمدی  به فهرست فیلم ها اضافه مینکنم")]
        private async Task When()
        {
            var dto = new AddMovieDtoBuilder().WithName("اینسپشن")
                .WithPulishYear("2005")
                .WithDailyPercentRent(200)
                .WithDelayPenalty(10)
                .WithDuration(120)
                .WithDirector("نولان")
                .WithGenreId(_genre.Id)
                .Build();
            await _sut.Add(dto);

        }

        [Then("یک فیلم با اسم اینسپشن" +
            " با سال انتشار دوهزار و پنج " +
            "و با قیمت روزانه دویست هزار تومان" +
            " و قیمت جریمه ده هزار تومان" +
            " ومدت زمان صد و بیست دقیقه" +
            " و کارگردان نولان" +
            " و با ژانر کمدی  باید فهرست فیلم ها اضافه شده باشد")]
        private void Then()
        {
            var actual = ReadContext.Movies.Single();
            actual.Name.Should().Be("اینسپشن");
            actual.PublishYear.Should().Be("2005");
            actual.DailyPriceRent.Should().Be(200);
            actual.DelayPenalty.Should().Be(10);
            actual.Duration.Should().Be(120);
            actual.Director.Should().Be("نولان");
            actual.GenreId.Should().Be(_genre.Id);
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
