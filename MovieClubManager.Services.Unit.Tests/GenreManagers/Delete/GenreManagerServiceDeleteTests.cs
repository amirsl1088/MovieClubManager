using FluentAssertions;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Exceptions;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClubManager.Test.Tools.Movies.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.GenreManagers.Delete
{
    public class GenreManagerServiceDeleteTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly GenreManagerService _sut;
        public GenreManagerServiceDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreManagerServiceFactory.Create(_context);
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
        [Fact]
        public async Task Delete_throws_exception_when_genre_movies_not_empty_exception()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            _context.Save(movie);

            var actual = () => _sut.Delete(genre.Id);

            await actual.Should().ThrowExactlyAsync<CannotDeleteGenresWitchTheyHaveSomeMoviesException>();

        }
    }
}
