using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Rents.Contracts.Dtos;
using MovieClubManager.Service.Rents.Exceptions;

namespace MovieClubManager.Service.Rents
{
    public class RentManagerAppService : RentManagerService
    {
        private readonly RentRepository _repository;
        private readonly UnitOfWork _unitofwork;
        public RentManagerAppService(RentRepository repository, UnitOfWork unitofwork)
        {
            _repository = repository;
            _unitofwork = unitofwork;
        }

        public async Task Delete(int id)
        {
            var rent =await _repository.FindRentById(id);
            if (rent == null)
            {
                throw new RentIdNotFoundException();
            }
            _repository.Delete(rent);
            await _unitofwork.Complete();
        }

        public async Task<List<GetRentDto>?> GetAll()
        {
            return await _repository.Get();
        }
    }
}
