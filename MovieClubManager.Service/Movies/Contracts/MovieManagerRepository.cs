using MovieClubManager.Entities.Movies;
using MovieClubManager.Service.Movies.Contracts.Dto;

namespace MovieClubManager.Service.Movies.Contracts
{
    public interface MovieManagerRepository
    {
        void Add(Movie movie);
        Task<List<GetMovieDto>> GetAll();
        Task<Movie?> FindMovieById(int id);
        void Delete(Movie movie);
    }
}
