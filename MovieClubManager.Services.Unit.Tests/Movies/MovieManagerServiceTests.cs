using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Persistence.EF.Movies;
using MovieClubManager.Service.Genres.Exceptions;
using MovieClubManager.Service.Movies;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Movies.Contracts.Dto;
using MovieClubManager.Service.Movies.Exceptions;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Movies.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.Movies
{
    public class MovieManagerServiceTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly MovieManagerService _sut;
        public MovieManagerServiceTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new MovieManagerAppService(new EFMovieManagerRepository(_context), new EFUnitOfWork(_context), new EFGenreManagerRepository(_context));
        }
        [Fact]
        public async Task Add_adds_one_new_movie_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dto = AddMovieDtoFactory.Create(genre.Id);

            await _sut.Add(dto);

            var actual = _readContext.Movies.Single();
            actual.Name.Should().Be(dto.Name);
            actual.PublishYear.Should().Be(dto.PublishYear);
            actual.DailyPriceRent.Should().Be(dto.DailyPriceRent);
            actual.AgeLimit.Should().Be(dto.AgeLimit);
            actual.DelayPenalty.Should().Be(dto.DelayPenalty);
            actual.Duration.Should().Be(dto.Duration);
            actual.GenreId.Should().Be(dto.GenreId);

        }
        [Fact]
        public async Task Add_throws_exception_whwn_genreid_not_found_exception()
        {
            var dummyid = 5;
            var dto = AddMovieDtoFactory.Create(dummyid);

            var actual = async () => await _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<GenreIdNotFoundException>();

        }
        [Fact]
        public async Task Get_gets_all_movies_information_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            _context.Save(movie);

            await _sut.GetAll();

            var actual = _readContext.Movies.First();
            actual.Id.Should().Be(movie.Id);
            actual.Name.Should().Be(movie.Name);
            actual.PublishYear.Should().Be(movie.PublishYear);
            actual.DailyPriceRent.Should().Be(movie.DailyPriceRent);
            actual.DelayPenalty.Should().Be(movie.DelayPenalty);
            actual.AgeLimit.Should().Be(movie.AgeLimit);
            actual.Director.Should().Be(movie.Director);
            actual.Duration.Should().Be(movie.Duration);
            actual.GenreId.Should().Be(movie.GenreId);
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

           var actual=async()=> await _sut.Delete(dummyid);

            await actual.Should().ThrowExactlyAsync<MovieIdNotFoundException>();
        }
    }
}
