using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Exceptions;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Movies.Contracts.Dto;
using MovieClubManager.Service.Movies.Exceptions;
using System.Runtime.CompilerServices;

namespace MovieClubManager.Service.Movies
{
    public class MovieManagerAppService : MovieManagerService
    {
        private readonly MovieManagerRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly GenreManagerRepository _genreRepository;
        public MovieManagerAppService(MovieManagerRepository repository
            , UnitOfWork unitOfWork,GenreManagerRepository genreManagerRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _genreRepository = genreManagerRepository;
        }

        public async Task Add(AddMovieDto dto)
        {
            var genre =await _genreRepository.FindGenreById(dto.GenreId);
            if (genre == null)
            {
                throw new GenreIdNotFoundException();
            }
            var movie = new Movie
            {
                Name = dto.Name,
                PublishYear = dto.PublishYear,
                DailyPriceRent =dto.DailyPriceRent,
                DelayPenalty =dto.DelayPenalty,
                AgeLimit = dto.AgeLimit,
                Director = dto.Director,
                Duration = dto.Duration,
                GenreId = genre.Id
            };
            _repository.Add(movie);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var movie =await _repository.FindMovieById(id);
            if (movie == null)
            {
                throw new MovieIdNotFoundException();
            }
            _repository.Delete(movie);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetMovieDto>?> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Update(int id, UpdateMovieDto dto)
        {
            var movie = await _repository.FindMovieById(id);
            if (movie == null)
            {
                throw new MovieIdNotFoundException();
            }
            var genre = await _genreRepository.FindGenreById(dto.GenreId);
            if (genre == null)
            {
                throw new GenreIdNotFoundException();
            }
            movie.Name = dto.Name;
            movie.Describtion = dto.Describtion;
            movie.PublishYear = dto.PublishYear;
            movie.DailyPriceRent = dto.DailyPriceRent;
            movie.AgeLimit = dto.AgeLimit;
            movie.DelayPenalty = dto.DelayPenalty;
            movie.Duration = dto.Duration;
            movie.Director = dto.Director;
            movie.GenreId = dto.GenreId;
           await _unitOfWork.Complete();
        }
    }
}
