using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.Service.Genres.Contracts
{
    public interface GenreManagerService
    {
        Task Add(AddGenreDto dto);
        Task Delete(int id);
        Task<List<GetGenreDto>?> GetAll();
        Task Update(int id, UpdateGenreDto dto);
    }
}