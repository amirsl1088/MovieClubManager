using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.RestApi.Controllers.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GenreService _service;
        public GenresController(GenreService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<List<GetGenreDto>?> GetAll([FromQuery]GetGenreFilterDto?filterDto)
        {
            return await _service.GetAll(filterDto);
        }
    }
}
