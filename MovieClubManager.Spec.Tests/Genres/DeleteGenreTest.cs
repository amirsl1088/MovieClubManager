using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieClubManager.Spec.Tests.Genres
{
    [Scenario("حذف کردن ژانر")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = "ژانری را حذف کنم  ",
InOrderTo = "آن را از فهرست ژانرها حذف کنم")]
    public class DeleteGenreTest : BusinessIntegrationTest
    {
        private readonly GenreManagerService _sut;
        private Genre _genre;
        public DeleteGenreTest()
        {
            _sut = GenreManagerServiceFactory.Create(SetupContext);
        }
        [Given("ژانری با نام کمدی وجود دارد")]
        [And("")]
        private void Given()
        {
            _genre = new GenreBuilder().WithTitle("کمدی").Build();
            Save(_genre);
        }

        [When("من میخواهم ژانری به اسم کمدی را فهرست ژانرها حذف کنم")]
        private async Task When()
        {
            await _sut.Delete(_genre.Id);

        }

        [Then("ژانری با نام کمدی نباید در لیست ژانرها وجود داشته باشد")]
        private void Then()
        {
            var actual = ReadContext.Genres.FirstOrDefault(_=>_.Id==_genre.Id);
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

