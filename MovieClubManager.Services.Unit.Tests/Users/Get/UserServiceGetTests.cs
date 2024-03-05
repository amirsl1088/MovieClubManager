﻿using MovieClubManager.Persistence.EF.Users;
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

namespace MovieClubManager.Services.Unit.Tests.Users.Get
{
    public class UserServiceGetTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly UserService _sut;
        public UserServiceGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new UserAppService(new EFUserRepository(_context), new EFUnitOfWork(_context));
        }
        [Fact]
        public async Task Get_gets_users_imformation_properly()
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

            var actual = await _sut.GetAll();

            actual.Count.Should().Be(1);
            actual.First().FirstName.Should().Be(user.FirstName);
            actual.First().LastName.Should().Be(user.LastName);
            actual.First().Adress.Should().Be(user.Adress);
            actual.First().Age.Should().Be(user.Age);
            actual.First().MobileNumber.Should().Be(user.MobileNumber);
            actual.First().Gender.Should().Be(user.Gender);
            actual.First().Rate.Should().Be(user.Rate);
        }
    }
}