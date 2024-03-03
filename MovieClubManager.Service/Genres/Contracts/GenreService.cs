using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.Service.Genres.Contracts
{
    public interface GenreService
    {
        Task<List<GetGenreDto>?> GetAll();
    }
}