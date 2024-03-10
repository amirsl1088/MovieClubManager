using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Rents.Contracts.Dtos;

namespace MovieClubManager.RestApi.Controllers.Rents
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentManagersController : ControllerBase
    {
        private readonly RentManagerService _service;
        public RentManagersController(RentManagerService service)
        {
            _service = service;
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetRentDto>?> GetAll()
        {
          return  await _service.GetAll();
        }
    }
}
