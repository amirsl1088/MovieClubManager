using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.Service.Genres
{
    public class GenreAppService : GenreService
    {
        private readonly GenreManagerRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public GenreAppService(GenreManagerRepository repository
            , UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetGenreDto>?> GetAll()
        {
           return await _repository.GetAll();
        }
    }
}