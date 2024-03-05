using Microsoft.EntityFrameworkCore;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.Persistence.EF.Genres
{
    public class EFGenreManagerRepository : GenreManagerRepository
    {
        private readonly DbSet<Genre> _genre;

        public EFGenreManagerRepository(EFDataContext context)
        {
            _genre = context.Genres;
        }

        public void Add(Genre genre)
        {
            _genre.Add(genre);
        }

        public void Delete(Genre genre)
        {
            _genre.Remove(genre);
        }

        public async Task<Genre?> FindGenreById(int id)
        {
            return await _genre.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<List<GetGenreDto>> GetAll(GetGenreFilterDto? filterDto)
        {
            var genre = await _genre.Select(_ => new GetGenreDto
            {
                Id = _.Id,
                Title = _.Title,
                Rate = _.Rate
            }).ToListAsync();

            if (filterDto.Title !=null)
            {
                genre = genre.Where(_ => _.Title.Contains(filterDto.Title)).ToList();
                return genre;
            }
            return genre;
        }
    }
}