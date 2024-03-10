using MovieClubManager.Entities.Rents;
using MovieClubManager.Service.Rents.Contracts.Dtos;

namespace MovieClubManager.Service.Rents.Contracts
{
    public interface RentRepository
    {
        void Add(Rent rent);
        bool IsExistMovieForRent(int movieid);
        Task<Rent?> FindRentById(int id);
        Task<List<GetRentDto>> Get();
    }
}
