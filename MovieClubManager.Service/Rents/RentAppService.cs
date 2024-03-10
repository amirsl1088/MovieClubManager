using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Rents.Contracts.Dtos;
using MovieClubManager.Service.Rents.Exceptions;
using MovieClubManager.Service.Users.Contrcts;

namespace MovieClubManager.Service.Rents
{
    public class RentAppService : RentService
    {
        private readonly RentRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private UserRepository _userRepository;
        private MovieManagerRepository _movieRepository;
        private DateTimeService _dateTimeService;
        public RentAppService(RentRepository repository,
            UnitOfWork unitOfWork,UserRepository userRepository
            ,MovieManagerRepository movieManagerRepository
            ,DateTimeService dateTimeService)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _movieRepository=movieManagerRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task Add(AddRentDto dto)
        {
            
            var user =await _userRepository.FindUserById(dto.UserId);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var movie =await _movieRepository.FindMovieById(dto.MovieId);
            if (movie == null)
            {
                throw new MovieNotFoundException();
            }
            var result = _repository.IsExistMovieForRent(movie.Id);
            if (result == true)
            {
                throw new MovieIsAlreadyRentedException();
            }
            var rent = new Rent
            {
                UserId = dto.UserId,
                MovieId = dto.MovieId,
                DailyPriceRent = movie.DailyPriceRent,
                DelayPenalty = movie.DelayPenalty,
                RentedAt = _dateTimeService.Now()
            };
            _repository.Add(rent);
            await _unitOfWork.Complete();
        }
    }
}
