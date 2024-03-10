using MovieClubManager.Entities.Rents;

namespace MovieClubManager.Service.Rents.Contracts
{
    public interface RentRepository
    {
        void Add(Rent rent);
        bool IsExistMovieForRent(int movieid);
    }
}
