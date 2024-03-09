using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Service.Genres.Exceptions;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Test.Tools.Genres.Builders;
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

    [Scenario("عدم اضافه شدن فیلم بدون ژانر")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = " فیلمی به فهرست فیلم ها اضافه کنم",
InOrderTo = "فیلم ها را اجاره دهم")]
    public class AddMovieExceptionTest:BusinessIntegrationTest
    {
        private readonly MovieManagerService _sut;
        private Func<Task>? _actual;
        
        public AddMovieExceptionTest()
        {
            _sut = MovieManagerSerciceFactory.Create(SetupContext);
        }
        [Given("فیلمی در فهرست فیلم ها وجود ندارد و ژانری در فهرست ژانرها وجود ندارد")]
        [And("")]
        private void Given()
        {

        }

        [When("من درخواست اضافه کردن فیلم به کلاب با اسم اینسپشن " +
            "با سال انتشار دوهزار و پنج " +
            "و با قیمت روزانه دویست هزار تومان " +
            "و قیمت جریمه ده هزار تومان " +
            "ومدت زمان صد و بیست دقیقه " +
            "و کارگردان نولان " +
            "و بدون ژانر  به فهرست فیلم ها را دارم")]
        private void When()
        {
            var dummyid = 7;
            var dto = new AddMovieDtoBuilder().WithName("اینسپشن")
                .WithPulishYear("2005")
                .WithDailyPercentRent(200)
                .WithDelayPenalty(10)
                .WithDuration(120)
                .WithDirector("نولان")
                .WithGenreId(dummyid)
                .Build();
            _actual=()=> _sut.Add(dto);

        }

        [Then("خطای عدم امکان ثبت فیلم بدون ژانر باید نشان داده شود")]
        private async Task Then()
        {
           await _actual.Should().ThrowExactlyAsync<GenreIdNotFoundException>();
        }


        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When(),
                _ => Then().Wait());
        }
    }
}
