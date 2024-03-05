using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClubManager.Service.Users.Contrcts;
using MovieClubManager.Service.Users.Contrcts.Dto;

namespace MovieClubManager.RestApi.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        public UsersController(UserService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddUserDto dto)
        {
            await _service.Add(dto);
        }
        [HttpGet]
        public async Task<List<GetUserDto>?> GetAll()
        {
            return await _service.GetAll();
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute]int id,[FromBody]UpdateUserDto dto)
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
