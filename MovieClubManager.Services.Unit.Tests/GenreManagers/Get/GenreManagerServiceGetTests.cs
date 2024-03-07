﻿using FluentAssertions;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.GenreManagers.Get
{
    public class GenreManagerServiceGetTests
    {
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly GenreManagerService _sut;
        public GenreManagerServiceGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreManagerServiceFactory.Create(_context);
        }
        [Fact]
        public async Task Get_gets_genre_information_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var filter = GetGenreFilterDtoFactory.Create();

            var actual = await _sut.GetAll(filter);


            actual.First().Id.Should().Be(genre.Id);
            actual.First().Title.Should().Be(genre.Title);
            actual.First().Rate.Should().Be(genre.Rate);
        }
        [Theory]
        [InlineData("dream", "dre")]
        [InlineData("dream", "dream")]
        public async Task Get_gets_genre_with_filter_properly(string title, string filter)
        {
            var genre = new GenreBuilder().WithTitle(title)
                .Build();
            _context.Save(genre);
            var filterDto = GetGenreFilterDtoFactory.Create(filter);

            var actual = await _sut.GetAll(filterDto);

            actual.First().Id.Should().Be(genre.Id);
            actual.First().Title.Should().Be(genre.Title);
            actual.First().Rate.Should().Be(genre.Rate);


        }
    }
}