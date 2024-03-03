using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.RestApi.Controllers.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreService _service;
        public GenreController(GenreService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<List<GetGenreDto>?> GetAll()
        {
            return await _service.GetAll();
        }
    }
}
