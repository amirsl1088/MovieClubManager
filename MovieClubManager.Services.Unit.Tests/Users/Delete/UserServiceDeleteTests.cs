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
using FluentAssertions;
using MovieClubManager.Service.Users.Exceptions;

namespace MovieClubManager.Services.Unit.Tests.Users.Delete
{
    public class UserServiceDeleteTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly UserService _sut;
        public UserServiceDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new UserAppService(new EFUserRepository(_context), new EFUnitOfWork(_context));
        }
        [Fact]
        public async Task Delete_delets_user_properly()
        {
            var user = new User
            {
                FirstName = "jyhgkj,",
                LastName = "ukghjlk",
                Age = 22,
                Adress = "jyghjlk",
                MobileNumber = "rfssyt",
                Gender = Gender.female
            };
            _context.Save(user);

            await _sut.Delete(user.Id);

            var actual = _readContext.Users.FirstOrDefault(_ => _.Id == user.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_exception_when_userid_not_found_exception()
        {
            var dummyid = 5;

            var actual=()=> _sut.Delete(dummyid);

            await actual.Should().ThrowExactlyAsync<UserIdNotFoundException>();

        }
    }
}
