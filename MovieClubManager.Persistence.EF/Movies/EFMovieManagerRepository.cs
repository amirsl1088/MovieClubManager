using Microsoft.EntityFrameworkCore;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Movies.Contracts.Dto;

namespace MovieClubManager.Persistence.EF.Movies
{
    public class EFMovieManagerRepository : MovieManagerRepository
    {
        private EFDataContext _context;

        public EFMovieManagerRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Movie movie)
        {
             _context.Movies.Add(movie);
        }

        public void Delete(Movie movie)
        {
            _context.Movies.Remove(movie);
        }

        public async Task<Movie?> FindMovieById(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetMovieDto>> GetAll()
        {
            return await _context.Movies.Select(_ => new GetMovieDto
            {
                Id = _.Id,
                Name = _.Name,
                PublishYear = _.PublishYear,
                DailyPriceRent = _.DailyPriceRent,
                DelayPenalty = _.DelayPenalty,
                AgeLimit = _.AgeLimit,
                Describtion = _.Describtion,
                Director = _.Director,
                Duration = _.Duration,
                GenreId = _.GenreId
            }).ToListAsync();
        }
    }
}
