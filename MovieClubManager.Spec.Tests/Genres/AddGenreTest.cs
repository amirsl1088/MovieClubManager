using FluentAssertions;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieManager.Spec.Tests;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace MovieClubManager.Spec.Tests.Genres


{
    [Scenario("اضافه کردن ژانر")]
    [Story("",
AsA = "مدیر کلاب ",
IWantTo = " ژانری به فهرست ژانر ها اضافه کنم",
InOrderTo = "به فیلم ها ژانر دهم.")]

    public class AddGenreTest : BusinessIntegrationTest
    {
        private readonly GenreManagerService _sut;

        public AddGenreTest()
        {
            _sut = GenreManagerServiceFactory.Create(SetupContext);
        }
        [Given("ژانری در فهرست ژانرها وجود ندارد.")]
        [And("")]
        private void Given()
        {
           
        }

        [When("من میخواهم ژانری به اسم کمدی به فهرست ژانرها اضافه کنم.")]
        private async Task When()
        {
            var dto = AddGenreDtoFactory.Create("کمدی");
            await _sut.Add(dto);

        }

        [Then("یک ژانر با نام کمدی باید در لیست ژانرها وجود داشته باشد.")]
        private void Then()
        {
            var actual = ReadContext.Genres.Single();
            actual.Title.Should().Be("کمدی");
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

