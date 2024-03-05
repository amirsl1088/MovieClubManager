using FluentAssertions;
using MovieClubManager.Entities.Users;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Users;
using MovieClubManager.Service.Users;
using MovieClubManager.Service.Users.Contrcts;
using MovieClubManager.Service.Users.Contrcts.Dto;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.Users.Add
{
    public class UserServiceAddTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly UserService _sut;
        public UserServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new UserAppService(new EFUserRepository(_context), new EFUnitOfWork(_context));
        }
        [Fact]
        public async Task Add_adds_user_properly()
        {
            var dto = new AddUserDto
            {
                FirstName = "hkj",
                LastName = "juhkurg",
                Age = 18,
                Adress = "uhku.j",
                MobileNumber = "ukhjh",
                Gender = Gender.male
            };

            await _sut.Add(dto);

            var actual = _readContext.Users.Single();
            actual.FirstName.Should().Be(dto.FirstName);
            actual.LastName.Should().Be(dto.LastName);
            actual.Age.Should().Be(dto.Age);
            actual.Adress.Should().Be(dto.Adress);
            actual.MobileNumber.Should().Be(dto.MobileNumber);
            actual.Gender.Should().Be(dto.Gender);
        }
    }
}
