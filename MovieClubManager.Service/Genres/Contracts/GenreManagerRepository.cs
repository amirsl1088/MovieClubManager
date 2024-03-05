using MovieClubManager.Entities.Genres;
using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.Service.Genres.Contracts
{
    public interface GenreManagerRepository
    {
        void Add(Genre genre);
        Task<List<GetGenreDto>> GetAll(GetGenreFilterDto? filterDto);
        Task<Genre?> FindGenreById(int id);
        void Delete(Genre genre);
    }
}