using MovieClubManager.Service.Movies.Contracts.Dto;

namespace MovieClubManager.Service.Movies.Contracts
{
    public interface MovieManagerService
    {
        Task Add(AddMovieDto dto);
        Task Delete(int id);
        Task<List<GetMovieDto>?> GetAll(GetMovieFilterDto? filterDto);
        Task Update(int id, UpdateMovieDto dto);
    }
}
