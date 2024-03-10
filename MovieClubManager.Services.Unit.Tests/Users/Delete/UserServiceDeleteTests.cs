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
using MovieClubManager.Test.Tools.Users.Builders;
using MovieClubManager.Test.Tools.Users.Factories;

namespace MovieClubManager.Services.Unit.Tests.Users.Delete
{
    public class UserServiceDeleteTests:BusinessUnitTest
    {
       
        private readonly UserService _sut;
        public UserServiceDeleteTests()
        {
           
            _sut = UserServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Delete_delets_user_properly()
        {
            var user = new UserBuilder().Build();
            DbContext.Save(user);

            await _sut.Delete(user.Id);

            var actual = ReadContext.Users.FirstOrDefault(_ => _.Id == user.Id);
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
