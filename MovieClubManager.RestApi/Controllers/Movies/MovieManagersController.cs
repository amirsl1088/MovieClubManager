using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Movies.Contracts.Dto;

namespace MovieClubManager.RestApi.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieManagersController : ControllerBase
    {
        private readonly MovieManagerService _service;
        public MovieManagersController(MovieManagerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddMovieDto dto)
        {
            await _service.Add(dto);
        }
        [HttpGet]
        public async Task<List<GetMovieDto>?> GetAll([FromQuery]GetMovieFilterDto? filterDto)
        {
           return await _service.GetAll(filterDto);
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute]int id,[FromBody]UpdateMovieDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
           await _service.Delete(id);
        }
    }
}
