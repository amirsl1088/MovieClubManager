using FluentAssertions;
using Moq;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Users;
using MovieClubManager.Service.Users.Contrcts;
using MovieClubManager.Service.Users.Contrcts.Dto;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Users.Factories;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Contracts.Interfaces;
using Xunit;

namespace MovieClubManager.Spec.Tests.Users
{
    [Scenario("اضافه کردن کاربر")]
    [Story("",
AsA = "کاربر ",
IWantTo = "یک کاربر اضافه کنم  ",
InOrderTo = "فیلم ها را اجاره کنم")]
    public class AddUserTest:BusinessIntegrationTest
    {
        private readonly UserService _sut;
        private DateTime _faketime;
        
        public AddUserTest()
        {
            _faketime = new DateTime(2024, 03, 07);
            _sut = UserServiceFactory.Create(SetupContext, _faketime);
        }
        [Given("کاربری در فهرست کاربرها وجود ندارد")]
        [And("")]
        private void Given()
        {

        }

        [When("من درخواست اضافه کردن کاربر با نام امیر و نام خانوادگی سلامت" +
            " و سن بیست و سه" +
            " و شماره 09161502147و" +
            " آدرس فلان و جنسیت مرد را دارم")]
        private async Task When()
        {
           
            var dto = new AddUserDto
            {
                FirstName = "امیر",
                LastName = "سلامت ",
                Age = 23,
                Adress = "فلان ",
                MobileNumber = "09161502147",
                Gender = Gender.male,
                CreatedAt = _faketime
            };
            await _sut.Add(dto);

        }

        [Then("یک کاربر با نام امیرو نام خانوادگی سلامت" +
            " و سن بیست و سه " +
            "و شماره 09161502147و" +
            " آدرس فلان و جنسیت مرد باید در فهرست کاربرها وجود داشته باشد")]
        private void Then()
        {
            var actual = ReadContext.Users.Single();
            actual.FirstName.Should().Be("امیر");
            actual.LastName.Should().Be("سلامت ");
            actual.Age.Should().Be(23);
            actual.Adress.Should().Be("فلان ");
            actual.MobileNumber.Should().Be("09161502147");
            actual.Gender.Should().Be(Gender.male);
            actual.CreatedAt.Should().Be(_faketime);
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
