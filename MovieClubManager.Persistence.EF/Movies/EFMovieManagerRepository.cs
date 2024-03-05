using Microsoft.EntityFrameworkCore;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Movies.Contracts.Dto;

namespace MovieClubManager.Persistence.EF.Movies
{
    public class EFMovieManagerRepository : MovieManagerRepository
    {
        private readonly DbSet<Movie> _movie;

        public EFMovieManagerRepository(EFDataContext context)
        {
           _movie = context.Movies;
        }

        public void Add(Movie movie)
        {
             _movie.Add(movie);
        }

        public void Delete(Movie movie)
        {
            _movie.Remove(movie);
        }

        public async Task<Movie?> FindMovieById(int id)
        {
            return await _movie.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetMovieDto>> GetAll(GetMovieFilterDto? filterDto)
        {
            var movie= await _movie.Select(_ => new GetMovieDto
            {
                Id = _.Id,
                Name = _.Name,
                PublishYear = _.PublishYear,
                DailyPriceRent = _.DailyPriceRent,
                DelayPenalty = _.DelayPenalty,
                AgeLimit = _.AgeLimit,
                Description = _.Description,
                Director = _.Director,
                Duration = _.Duration,
                GenreId = _.GenreId
            }).ToListAsync();
            if(filterDto.Name != null)
            {
                movie = movie.Where(_ => _.Name.Contains(filterDto.Name)).ToList();
                return movie;
            }
            return movie;

        }
    }
}
