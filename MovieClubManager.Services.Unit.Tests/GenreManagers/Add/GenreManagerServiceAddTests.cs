using FluentAssertions;
using Moq;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Genres;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.GenreManagers.Add
{
    public class GenreManagerServiceAddTests:BusinessUnitTest
    {
       
        private readonly GenreManagerService _sut;
        public GenreManagerServiceAddTests()
        {
           
            _sut = GenreManagerServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Add_adds_one_new_genre_properly()
        {
            var dto = AddGenreDtoFactory.Create();

            await _sut.Add(dto);


            var actual = ReadContext.Genres.Single();
            actual.Title.Should().Be(dto.Title);
        }
        [Fact]
        public async Task Add_genre_with_mock_properly()
        {
            var dto = AddGenreDtoFactory.Create();
            var repositoryMock = new Mock<GenreManagerRepository>();
            var unitofworkMock = new Mock<UnitOfWork>();
            var sut = new GenreManagerAppService(repositoryMock.Object, unitofworkMock.Object);

            await sut.Add(dto);

            repositoryMock.Verify(_ => _.Add(It.Is<Genre>(_ => _.Title == dto.Title)));
            unitofworkMock.Verify(_ => _.Complete(), Times.Once);
        }
    }
}
