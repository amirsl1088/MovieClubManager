﻿using MovieClubManager.Persistence.EF.Genres;
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
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Movies.Builders;
using MovieClubManager.Test.Tools.Movies.Factories;

namespace MovieClubManager.Services.Unit.Tests.Movies.Get
{
    public class MovieManagerServiceGetTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly MovieManagerService _sut;
        public MovieManagerServiceGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = new MovieManagerAppService(new EFMovieManagerRepository(_context), new EFUnitOfWork(_context), new EFGenreManagerRepository(_context));
        }
        [Fact]
        public async Task Get_gets_all_movies_information_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .Build();
            _context.Save(movie);
            var filterDto = GetMovieFilterDtoFactory.Create();

            var actual = await _sut.GetAll(filterDto);


            actual.First().Id.Should().Be(movie.Id);
            actual.First().Name.Should().Be(movie.Name);
            actual.First().PublishYear.Should().Be(movie.PublishYear);
            actual.First().DailyPriceRent.Should().Be(movie.DailyPriceRent);
            actual.First().DelayPenalty.Should().Be(movie.DelayPenalty);
            actual.First().AgeLimit.Should().Be(movie.AgeLimit);
            actual.First().Director.Should().Be(movie.Director);
            actual.First().Duration.Should().Be(movie.Duration);
            actual.First().GenreId.Should().Be(movie.GenreId);
        }
        [Theory]
        [InlineData("friends","fri")]
        [InlineData("friends","friends")]
        public async Task Get_gets_movies_with_filter_properly(string name,string filter)
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder().WithGenreId(genre.Id)
                .WithName(name).Build();
            _context.Save(movie);
            var filterDto = GetMovieFilterDtoFactory.Create(filter);

            var actual = await _sut.GetAll(filterDto);

            actual.First().Id.Should().Be(movie.Id);
            actual.First().Name.Should().Be(movie.Name);
            actual.First().PublishYear.Should().Be(movie.PublishYear);
            actual.First().DailyPriceRent.Should().Be(movie.DailyPriceRent);
            actual.First().DelayPenalty.Should().Be(movie.DelayPenalty);
            actual.First().AgeLimit.Should().Be(movie.AgeLimit);
            actual.First().Director.Should().Be(movie.Director);
            actual.First().Duration.Should().Be(movie.Duration);
            actual.First().GenreId.Should().Be(movie.GenreId);
        }
    }
}
