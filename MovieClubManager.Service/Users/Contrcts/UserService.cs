using MovieClubManager.Service.Users.Contrcts.Dto;

namespace MovieClubManager.Service.Users.Contrcts
{
    public interface UserService
    {
        Task Add(AddUserDto dto);
        Task Delete(int id);
        Task<List<GetUserDto>?> GetAll();
        Task Update(int id, UpdateUserDto dto);
    }
}
