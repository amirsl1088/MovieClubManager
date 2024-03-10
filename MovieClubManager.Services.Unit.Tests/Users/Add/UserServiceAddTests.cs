using FluentAssertions;
using Moq;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Users;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Users;
using MovieClubManager.Service.Users;
using MovieClubManager.Service.Users.Contrcts;
using MovieClubManager.Service.Users.Contrcts.Dto;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClubManager.Test.Tools.Users.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.Users.Add
{
    public class UserServiceAddTests:BusinessUnitTest
    {
        private readonly UserService _sut;
        private readonly DateTime _faketime;
        public UserServiceAddTests()
        {
           
            _faketime = new DateTime(2024, 03, 07);
            _sut = UserServiceFactory.Create(SetupContext, _faketime);
        }
        [Fact]
        public async Task Add_adds_user_properly()
        {
            var dto = AddUserDtoFactory.Create();

            await _sut.Add(dto);

            var actual = ReadContext.Users.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.Age.Should().Be(dto.Age);
            actual.Adress.Should().Be(dto.Adress);
            actual.MobileNumber.Should().Be(dto.MobileNumber);
            actual.Gender.Should().Be(dto.Gender);
            actual.CreatedAt.Should().Be(_faketime);
        }
        [Fact]
        public async Task Add_adds_user_with_mock_properly()
        {
            var dto = AddUserDtoFactory.Create();
            var repositoryMock = new Mock<UserRepository>();
            var unitOfWorkMock = new Mock<UnitOfWork>();
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(new DateTime(2024, 03, 07));
            var sut = new UserAppService(repositoryMock.Object, unitOfWorkMock.Object, dateTimeServiceMock.Object);

            await sut.Add(dto);

            repositoryMock.Verify(_ => _.Add(It.Is<User>(_ =>
            _.FirstName == dto.FirstName &&
            _.LastName == dto.LastName &&
            _.Age == dto.Age &&
            _.Adress==dto.Adress &&
            _.MobileNumber==dto.MobileNumber &&
            _.Gender==dto.Gender &&
            _.CreatedAt==_faketime)), Times.Once);
           
            unitOfWorkMock.Verify(_ => _.Complete(), Times.Once);

        }
    }
}
