using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Genres.Contracts.Dto;

namespace MovieClubManager.RestApi.Controllers.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreManagersController : ControllerBase
    {
        private readonly GenreManagerService _service;
        public GenreManagersController(GenreManagerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddGenreDto dto)
        {
            await _service.Add(dto);
        }
        [HttpGet]
        public async Task<List<GetGenreDto>?> GetAll([FromQuery]GetGenreFilterDto? filterDto)
        {
            return await _service.GetAll(filterDto);
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute]int id,[FromBody]UpdateGenreDto dto)
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
