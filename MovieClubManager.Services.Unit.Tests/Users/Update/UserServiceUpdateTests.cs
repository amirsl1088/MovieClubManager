using MovieClubManager.Persistence.EF.Users;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Users.Contrcts;
using MovieClubManager.Service.Users;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Users.Contrcts.Dto;
using FluentAssertions;
using MovieClubManager.Service.Users.Exceptions;
using MovieClubManager.Test.Tools.Users.Builders;
using MovieClubManager.Test.Tools.Users.Factories;
using Moq;
using MovieClubManager.Contracts.Interfaces;

namespace MovieClubManager.Services.Unit.Tests.Users.Update
{
    public class UserServiceUpdateTests:BusinessUnitTest
    {
       
        private readonly UserService _sut;
        public UserServiceUpdateTests()
        {
          
            _sut = UserServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Update_updates_users_imformation_properly()
        {
            var user = new UserBuilder().Build();
            DbContext.Save(user);
            var dto = UpdateUserDtoFactory.Create();

            await _sut.Update(user.Id, dto);

            var actual = ReadContext.Users.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.Adress.Should().Be(dto.Adress);
            actual.MobileNumber.Should().Be(dto.MobileNumber);
            actual.Age.Should().Be(dto.Age);
            actual.Gender.Should().Be(dto.Gender);
        }
        [Fact]
        public async Task Update_throws_exception_when_userid_not_found_exception()
        {
            var dummyid = 5;
            var dto = UpdateUserDtoFactory.Create();

            var actual = () => _sut.Update(dummyid, dto);

            await actual.Should().ThrowExactlyAsync<UserIdNotFoundException>();
        }

        [Fact]
        public async Task Update_updates_user_properly_with_mock()
        {
            var user = new UserBuilder().Build();
            DbContext.Save(user);
            var dto = UpdateUserDtoFactory.Create();
            var repository = new Mock<UserRepository>();
            var unitOfWork = new Mock<UnitOfWork>();
            var sut = UserServiceFactory.Create(SetupContext,userRepository: repository.Object,unitOfWork: unitOfWork.Object);
            repository.Setup(_ => _.FindUserById(It.Is<int>(_ => _ == user.Id))).ReturnsAsync(user);

            await sut.Update(user.Id, dto);

            
            unitOfWork.Verify(_ => _.Complete(), Times.Once);
        }
    }
}
