using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieClubManager.Spec.Tests.Genres.Update
{
    [Scenario("ویرایش کردن ژانر")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = "ژانری را ویرایش کنم ",
InOrderTo = "اطلاعات آن را بروز کنم")]
    public class UpdateGenreTest : BusinessIntegrationTest
    {

        private readonly GenreManagerService _sut;
        private Genre _genre;

        public UpdateGenreTest()
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

        [When("من میخواهم ژانر با نام کمدی را به ژانر با نام درام ویرایش کنم")]
        private async Task When()
        {
            var dto = UpdateGenreDtoFactory.Create("درام");
            await _sut.Update(_genre.Id, dto);

        }

        [Then("باید ژانر کمدی به ژانر درام ویرایش پیدا کرده باشد")]
        private void Then()
        {
            var actual = ReadContext.Genres.Single();
            actual.Title.Should().Be("درام");
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

