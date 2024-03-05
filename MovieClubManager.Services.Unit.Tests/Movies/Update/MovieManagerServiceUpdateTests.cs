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
using MovieClubManager.Service.Genres.Exceptions;
using MovieClubManager.Service.Movies.Exceptions;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Movies.Factories;

namespace MovieClubManager.Services.Unit.Tests.Movies.Update
{
    public class MovieManagerServiceUpdateTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly MovieManagerService _sut;
        public MovieManagerServiceUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new MovieManagerAppService(new EFMovieManagerRepository(_context), new EFUnitOfWork(_context), new EFGenreManagerRepository(_context));
        }
        [Fact]
        public async Task Update_updates_movie_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var genre2 = new GenreBuilder().Build();
            _context.Save(genre2);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            _context.Save(movie);
            var dto = UpdateMovieDtoFactory.Create(genre2.Id);

            await _sut.Update(movie.Id, dto);

            var actual = _readContext.Movies.Single();
            actual.Name.Should().Be(dto.Name);
            actual.PublishYear.Should().Be(dto.PublishYear);
            actual.DailyPriceRent.Should().Be(dto.DailyPriceRent);
            actual.DelayPenalty.Should().Be(dto.DelayPenalty);
            actual.AgeLimit.Should().Be(dto.AgeLimit);
            actual.Director.Should().Be(dto.Director);
            actual.Duration.Should().Be(dto.Duration);
            actual.GenreId.Should().Be(dto.GenreId);

        }
        [Fact]
        public async Task Update_throws_exception_when_movieid_not_found_exception()
        {
            var dummyid = 5;
            var genre = new GenreBuilder().Build();
            var dto = UpdateMovieDtoFactory.Create(genre.Id);

            var actual = async () => await _sut.Update(dummyid, dto);

            await actual.Should().ThrowExactlyAsync<MovieIdNotFoundException>();

        }
        [Fact]
        public async Task Update_throws_exception_when_new_genreid_not_found_exception()
        {
            var dummyid = 5;
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            _context.Save(movie);
            var dto = UpdateMovieDtoFactory.Create(dummyid);

            var actual = async () => await _sut.Update(movie.Id, dto);

            await actual.Should().ThrowExactlyAsync<GenreIdNotFoundException>();

        }
    }
}
