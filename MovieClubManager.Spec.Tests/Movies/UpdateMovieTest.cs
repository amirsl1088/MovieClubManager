using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Movies.Factories;
using MovieManager.Spec.Tests;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieClubManager.Spec.Tests.Movies
{
    [Scenario("ویرایش کردن فیلم")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = "فیلمی را ویرایش کنم  ",
InOrderTo = "اطلاعات ان را را ویرایش کنم")]
    public class UpdateMovieTest:BusinessIntegrationTest
    {
        private readonly MovieManagerService _sut;
        private Movie _movie;
        private Genre _genre;
        public UpdateMovieTest()
        {
            _sut = MovieManagerSerciceFactory.Create(SetupContext);
        }
        [Given("یک فیلم در فهرست فیلم ها با اسم اینسپشن با سال انتشار دوهزار و پنج و با قیمت روزانه دیویصد هزار تومان و قیمت جریمه ده هزار تومان ومدت زمان صد و بیست دقیقه و کارگردان نولان و با ژانر کمدی وجود دارد")]
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

        [When("من درخواست ویرایش فیلم مذکور به نام اینتراستلار با سال انتشار دوهزار و ده و با قیمت روزانه دیویصد و پنجاه هزار تومان و قیمت جریمه پانزده هزار تومان ومدت زمان صد و بیست دقیقه و کارگردان نولان و با ژانر کمدی را دارم")]
        private async Task When()
        {
            var dto = UpdateMovieDtoFactory.Create(_genre.Id);
            dto.Name = "اینتراستلار";
            dto.PublishYear = "2010";
            dto.DailyPriceRent = 250;
            dto.DelayPenalty = 15;
            dto.Duration = 120;
            dto.Director = "نولان";
           
            await _sut.Update(_movie.Id, dto);
        }

        [Then("فیلم مذکور باید به نام ایتراستلاربا سال انتشار دوهزار و ده و با قیمت روزانه دیویصد و پنجاه هزار تومان و قیمت جریمه پانزده هزار تومان ومدت زمان صد و بیست دقیقه و کارگردان نولان و با ژانر کمدی ویرایش پیدا کرده باشد")]
        private void Then()
        {
            var actual = ReadContext.Movies.FirstOrDefault(_ => _.Id == _movie.Id);
            actual.Name.Should().Be("اینتراستلار");
            actual.PublishYear.Should().Be("2010");
            actual.DailyPriceRent.Should().Be(250);
            actual.DelayPenalty.Should().Be(15);
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
