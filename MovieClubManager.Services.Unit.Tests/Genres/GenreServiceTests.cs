using FluentAssertions;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Service.Genres;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.Genres
{
    public class GenreServiceTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly GenreService _sut;
        public GenreServiceTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new GenreAppService(new EFGenreManagerRepository(_context), new EFUnitOfWork(_context));
        }
        [Fact]
        public async Task Get_gets_all_genre_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);

            await _sut.GetAll();

            var actual = _readContext.Genres.First();
            actual.Id.Should().Be(genre.Id);
            actual.Title.Should().Be(genre.Title);
            actual.Rate.Should().Be(genre.Rate);
        }

    }
}