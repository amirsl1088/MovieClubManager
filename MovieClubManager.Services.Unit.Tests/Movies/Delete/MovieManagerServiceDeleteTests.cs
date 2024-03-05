using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Persistence.EF.Movies;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Movies;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MovieClubManager.Service.Movies.Exceptions;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Movies.Builders;

namespace MovieClubManager.Services.Unit.Tests.Movies.Delete
{
    public class MovieManagerServiceDeleteTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly MovieManagerService _sut;
        public MovieManagerServiceDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new MovieManagerAppService(new EFMovieManagerRepository(_context), new EFUnitOfWork(_context), new EFGenreManagerRepository(_context));
        }
        [Fact]
        public async Task Delete_delets_movie_from_table_movies_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            _context.Save(movie);

            await _sut.Delete(movie.Id);

            var actual = _readContext.Movies.FirstOrDefault(_ => _.Id == movie.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_exception_when_movieid_not_found_exception()
        {
            var dummyid = 5;

            var actual = async () => await _sut.Delete(dummyid);

            await actual.Should().ThrowExactlyAsync<MovieIdNotFoundException>();
        }
    }
}
