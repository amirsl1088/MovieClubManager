using FluentAssertions;
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

    [Scenario("حذف کردن فیلم")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = "فیلمی را حذف کنم ",
InOrderTo = "ان فیلم مذکور را از فهرست فیلم ها حذف کنم")]
    public class DeleteMovieTest:BusinessIntegrationTest
    {
        private readonly MovieManagerService _sut;
        private Movie _movie;
        public DeleteMovieTest()
        {
            _sut = MovieManagerSerciceFactory.Create(SetupContext);
        }
        [Given("یک فیلم در فهرست فیلم ها با نام اینتراستلار وجود دارد")]
        [And("")]
        private void Given()
        {
            var genre = new GenreBuilder().Build();
            Save(genre);
            _movie = new MovieBuilder().WithName("اینتراستلار").WithGenreId(genre.Id).Build();
            Save(_movie);
        }

        [When("من میخواهم فیلم با نام اینتراستلار را از فهرست فیلم ها حذف کنم")]
        private async Task When()
        {
            await _sut.Delete(_movie.Id);

        }

        [Then(" فیلم با نام اینتراستلار از فهرست فیلم ها باید حذف شده باشد")]
        private void Then()
        {
            var actual = ReadContext.Movies.FirstOrDefault(_ => _.Id == _movie.Id);
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
