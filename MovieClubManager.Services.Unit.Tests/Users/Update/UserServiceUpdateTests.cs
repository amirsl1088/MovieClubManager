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

namespace MovieClubManager.Services.Unit.Tests.Users.Update
{
    public class UserServiceUpdateTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly UserService _sut;
        public UserServiceUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = UserServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Update_updates_users_imformation_properly()
        {
            var user = new UserBuilder().Build();
            _context.Save(user);
            var dto = UpdateUserDtoFactory.Create();

            await _sut.Update(user.Id, dto);

            var actual = _readContext.Users.Single();
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
    }
}
