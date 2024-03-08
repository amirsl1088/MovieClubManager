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

    [Scenario("اویرایش کردن کاربر")]
    [Story("",
AsA = "کاربر ",
IWantTo = "یک کاربر ویرایش کنم  ",
InOrderTo = "اطلاعات آن را ویرایش کنم")]
    public class UpdateUserTest:BusinessIntegrationTest
    {
        private readonly UserService _sut;
        private User _user;
        public UpdateUserTest()
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

        [When("من درخواست ویرایش کردن کاربر مذکور به نام رضا و نام خانوادگی مهری و سن بیست و هفت و شماره \"09161502157\"و آدرس بهمان و جنسیت مرد را دارم")]
        private async Task When()
        {

            var dto = new UpdateUserDto
            {
                FirstName = "رضا ",
                LastName = "مهری  ",
                Age = 27,
                Adress = "بهمان ",
                MobileNumber = "09161502157",
                Gender = Gender.male,
                
            };
            await _sut.Update(_user.Id,dto);

        }

        [Then("کاربر مذکور باید به نام رضا و نام خانوادگی مهری و سن بیست و هفت و شماره \"09161502157\"و آدرس بهمان و جنسیت مرد ویرایش شده باشد")]
        private void Then()
        {
            var actual = ReadContext.Users.Single();
            actual.FirstName.Should().Be("رضا ");
            actual.LastName.Should().Be("مهری  ");
            actual.Age.Should().Be(27);
            actual.Adress.Should().Be("بهمان ");
            actual.MobileNumber.Should().Be("09161502157");
            actual.Gender.Should().Be(Gender.male);
            
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
