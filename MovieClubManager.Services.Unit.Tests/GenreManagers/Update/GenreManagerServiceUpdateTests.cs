using FluentAssertions;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Exceptions;
using MovieClubManager.Test.Tools.Genres.Builders;
using MovieClubManager.Test.Tools.Genres.Factories;
using MovieClubManager.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieClubManager.Services.Unit.Tests.GenreManagers.Update
{
    public class GenreManagerServiceUpdateTests:BusinessUnitTest
    {

        private readonly GenreManagerService _sut;
        public GenreManagerServiceUpdateTests()
        {
           
            _sut = GenreManagerServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Update_updates_title_of_genre_properly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var dto = UpdateGenreDtoFactory.Create();

            await _sut.Update(genre.Id, dto);

            var actual = ReadContext.Genres.Single();
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
    }
}
