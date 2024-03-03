using Microsoft.EntityFrameworkCore;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.Persistence.EF.Genres
{
    public class EFGenreManagerRepository : GenreManagerRepository
    {
        private EFDataContext _context;

        public EFGenreManagerRepository(EFDataContext context)
        {
            _context = context;
        }

        public void Add(Genre genre)
        {
            _context.Genres.Add(genre);
        }

        public void Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
        }

        public async Task<Genre?> FindGenreById(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetGenreDto>> GetAll()
        {
            return await _context.Genres.Select(_ => new GetGenreDto
            {
                Id = _.Id,
                Title = _.Title,
                Rate = _.Rate
            }).ToListAsync();
        }
    }
}