using MovieClubManager.Service.Rents.Contracts.Dtos;

namespace MovieClubManager.Service.Rents.Contracts
{
    public interface RentService
    {
        Task Add(AddRentDto dto);
        Task<decimal?> Update(int id, UpdateRentDto dto);
    }
}
