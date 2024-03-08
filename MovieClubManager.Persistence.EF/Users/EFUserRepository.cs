using Microsoft.EntityFrameworkCore;
using MovieClubManager.Entities.Users;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Service.Users.Contrcts;
using MovieClubManager.Service.Users.Contrcts.Dto;

namespace MovieClubManager.Persistence.EF.Users
{
    public class EFUserRepository : UserRepository
    {
        private readonly DbSet<User> _user;

        public EFUserRepository(EFDataContext context)
        {
            _user = context.Users;
        }

        public void Add(User user)
        {
            _user.Add(user);
        }

        public void Delete(User user)
        {
            _user.Remove(user);
        }

        public async Task<User?> FindUserById(int id)
        {
            return await _user.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task<List<GetUserDto>> GetAll()
        {
            return _user.Select(_ => new GetUserDto
            {
                Id = _.Id,
                FirstName = _.FirstName,
                LastName = _.LastName,
                Adress = _.Adress,
                Age = _.Age,
                Gender = _.Gender,
                MobileNumber = _.MobileNumber,
                Rate = _.Rate,
                CreatedAt=_.CreatedAt
            }).ToListAsync();
        }

        public void Update()
        {
            
        }
    }
}
