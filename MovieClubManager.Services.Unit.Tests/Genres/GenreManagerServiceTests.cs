using FluentAssertions;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Service.Genres;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;
using MovieClubManager.Service.Genres.Exceptions;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System.Reflection;

namespace MovieClubManager.Services.Unit.Tests.Genres
{
    public class GenreManagerServiceTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly GenreManagerService _sut;
        public GenreManagerServiceTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreManagerServiceFactory.Create(_context);
        }

        [Fact]
        public async Task Add_adds_one_new_genre_properly()
        {
            var dto = AddGenreDtoFactory.Create();

            await _sut.Add(dto);


            var actual = _readContext.Genres.Single();
            actual.Title.Should().Be(dto.Title);
        }
        [Fact]
        public async Task Get_gets_genre_information_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);

            await _sut.GetAll();

            var actual = _readContext.Genres.First();
            actual.Id.Should().Be(genre.Id);
            actual.Title.Should().Be(genre.Title);
            actual.Rate.Should().Be(genre.Rate);
        }
        [Fact]
        public async Task Update_updates_title_of_genre_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dto = UpdateGenreDtoFactory.Create();

            await _sut.Update(genre.Id, dto);

            var actual = _readContext.Genres.Single();
            actual.Title.Should().Be(dto.Title);
        }
        [Fact]
        public async Task Update_throws_exception_when_genreid_not_found_exception()
        {
            var dummyid = 5;
            var dto = UpdateGenreDtoFactory.Create();

            var actual = async () => await _sut.Update(dummyid, dto);

            await actual.Should().ThrowExactlyAsync<GenreIdNotFoundException>();
        }
        [Fact]
        public async Task Delete_delets_genre_from_table_genres_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);

            await _sut.Delete(genre.Id);

            var actual = _readContext.Genres.FirstOrDefault(_ => _.Id == genre.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_exception_when_genreid_not_found_exception()
        {
            var dummyid = 4;

            var actual = async () => await _sut.Delete(dummyid);

            await actual.Should().ThrowExactlyAsync<GenreIdNotFoundException>();
        }
    }
}