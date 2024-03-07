using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Users;
using MovieClubManager.Service.Users.Contrcts;
using MovieClubManager.Service.Users.Contrcts.Dto;
using MovieClubManager.Service.Users.Exceptions;
using VideoClub.Contracts.Interfaces;

namespace MovieClubManager.Service.Users
{
    public class UserAppService : UserService
    {
        private readonly UserRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly DateTimeService _dateTimeService;
        public UserAppService(UserRepository repository, UnitOfWork unitOfWork,
            DateTimeService dateTimeService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dateTimeService = dateTimeService;

        }

        public async Task Add(AddUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age,
                Adress = dto.Adress,
                MobileNumber = dto.MobileNumber,
                Gender = dto.Gender,
                CreatedAt = _dateTimeService.Now()
            };
            _repository.Add(user);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var user = await _repository.FindUserById(id);
            if (user == null)
            {
                throw new UserIdNotFoundException();
            }
            _repository.Delete(user);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetUserDto>?> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Update(int id, UpdateUserDto updateUserDto)
        {
            var user = await _repository.FindUserById(id);
            if (user == null)
            {
                throw new UserIdNotFoundException();
            }
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Age = updateUserDto.Age;
            user.Adress = updateUserDto.Adress;
            user.MobileNumber = updateUserDto.MobileNumber;
            user.Gender = updateUserDto.Gender;
            _repository.Update(user);
            await _unitOfWork.Complete();
        }
    }
}
