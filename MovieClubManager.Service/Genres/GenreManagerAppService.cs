﻿using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;
using MovieClubManager.Service.Genres.Exceptions;

namespace MovieClubManager.Service.Genres
{
    public class GenreManagerAppService : GenreManagerService
    {
        private readonly GenreManagerRepository _repository;
        private UnitOfWork _unitOfWork;

        public GenreManagerAppService(GenreManagerRepository eFGenreManagerRepository
            , UnitOfWork eFUnitOfWork)
        {
            _repository = eFGenreManagerRepository;
            _unitOfWork = eFUnitOfWork;
        }

        public async Task Add(AddGenreDto dto)
        {
            var genre = new Genre
            {
                Title = dto.Title
            };
            _repository.Add(genre);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var genre = await _repository.FindGenreById(id);
            if (genre == null)
            {
                throw new GenreIdNotFoundException();
            }
            _repository.Delete(genre);
            await _unitOfWork.Complete();

        }

        public async Task<List<GetGenreDto>?> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Update(int id, UpdateGenreDto dto)
        {
            var genre =await _repository.FindGenreById(id);
            if (genre == null)
            {
                throw new GenreIdNotFoundException();
            }
            genre.Title = dto.Title;
            await _unitOfWork.Complete();
        }
    }
}