using FluentAssertions;
using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Users.Contrcts;
using MovieClubManager.Service.Users.Contrcts.Dto;
using MovieClubManager.Test.Tools.Users.Factories;
using MovieManager.Spec.Tests;
using MovieManagerClub.Tests.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieClubManager.Spec.Tests.Users
{

    [Scenario("حذف کردن کردن کاربر")]
    [Story("",
AsA = "کاربر ",
IWantTo = " یک کاربر حذف کنم  ",
InOrderTo = "اطلاعات آن را حذف کنم")]
    public class DeleteUserTest:BusinessIntegrationTest
    {
        private readonly UserService _sut;
        private User _user;
        public DeleteUserTest()
        {
            _sut = UserServiceFactory.Create(SetupContext);
        }
        [Given("کاربری  با نام امیرو نام خانوادگی سلامت و سن بیست و سه و شماره \"09161502147\"و آدرس فلان و جنسیت مرد وجود دارد")]
        [And("")]
        private void Given()
        {
            _user = new User
            {
                FirstName = "امیر",
                LastName = "سلامت ",
                Age = 23,
                Adress = "فلان ",
                MobileNumber = "09161502147",
                Gender = Gender.male,
                CreatedAt = DateTime.UtcNow
            };
            Save(_user);
        }

        [When(" من درخواست حذف کردن کاربر مذکور دارم")]
        private async Task When()
        {

            await _sut.Delete(_user.Id);

        }

        [Then("کاربر مذکور باید حذف شده باشد")]
        private void Then()
        {
            var actual = ReadContext.Users.FirstOrDefault(_=>_.Id==_user.Id);
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
