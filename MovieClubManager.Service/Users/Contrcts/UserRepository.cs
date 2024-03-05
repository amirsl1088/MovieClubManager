using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Users.Contrcts.Dto;

namespace MovieClubManager.Service.Users.Contrcts
{
    public interface UserRepository
    {
        void Add(User user);
       Task<List<GetUserDto>> GetAll();
        void Delete(User user);
        Task<User?> FindUserById(int id);
    }
}
