using MovieClubManager.Service.Rents.Contracts.Dtos;

namespace MovieClubManager.Service.Rents.Contracts
{
    public interface RentManagerService
    {
        Task Delete(int id);
        Task<List<GetRentDto>?> GetAll();
    }
}
