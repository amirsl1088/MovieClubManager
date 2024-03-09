using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
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
    [Scenario("عدم امکان ویرایش کردن فیلم")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = "فیلمی را ویرایش کنم  ",
InOrderTo = "اطلاعات ان را را ویرایش کنم")]
    public class UpdateMovieExceptionTest:BusinessIntegrationTest
    {
        private readonly MovieManagerService _sut;
        private Movie _movie;
        private Genre _genre;
        private Func<Task>? _actual;
        public UpdateMovieExceptionTest()
        {
            _sut = MovieManagerSerciceFactory.Create(SetupContext);
        }
        [Given("یک فیلم در فهرست فیلم ها با اسم اینسپشن" +
            " با سال انتشار دوهزار و پنج " +
            "و با قیمت روزانه دویست هزار تومان" +
            " و قیمت جریمه ده هزار تومان" +
            " ومدت زمان صد و بیست دقیقه" +
            " و کارگردان نولان" +
            "  و با ژانر کمدی وجود دارد" +
            "و در فهرست ژانرها فقط کمدی وجود دارد")]
        [And("")]
        private void Given()
        {
            _genre = new GenreBuilder().WithTitle("کمدی").Build();
            Save(_genre);
            _movie = new MovieBuilder().WithName("اینسپشن")
                .WithGenreId(_genre.Id)
                .WithPulishYear("2005")
                .WithDailyPercentRent(200)
                .WithDelayPenalty(10)
                .WithDuration(120)
                .WithDirector("نولان")
                .Build();
            Save(_movie);
        }

        [When("من درخواست ویرایش فیلم مذکور به نام اینتراستلار" +
            " با سال انتشار دوهزار و ده " +
            "و با قیمت روزانه دویست" +
            " و پنجاه هزار تومان" +
            " و قیمت جریمه پانزده هزار تومان" +
            " ومدت زمان صد و بیست دقیقه " +
            "و کارگردان نولان" +
            " و با ژانر ترسناک را دارم")]
        private void When()
        {
            var dummyid = 5;
            var dto = UpdateMovieDtoFactory.Create(dummyid);
            dto.Name = "اینتراستلار";
            dto.PublishYear = "2010";
            dto.DailyPriceRent = 250;
            dto.DelayPenalty = 15;
            dto.Duration = 120;
            dto.Director = "نولان";

            _actual =  ()=> _sut.Update(_movie.Id, dto);
        }

        [Then("فیلم مذکور نباید به نام ایتراستلار" +
            " با سال انتشار دوهزار و ده" +
            " و با قیمت روزانه دویست" +
            " و پنجاه هزار تومان " +
            "و قیمت جریمه پانزده هزار تومان " +
            "ومدت زمان صد و بیست دقیقه " +
            "و کارگردان نولان" +
            " و با ژانر کمدی ویرایش پیدا کرده باشد")]
        [And("باید خطای عدم پیدا کردن ژانر نمایش داده شود")]
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

