using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Rents.Contracts.Dtos;

namespace MovieClubManager.RestApi.Controllers.Rents
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentsController : ControllerBase
    {
        private readonly RentService _service;
        public RentsController(RentService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add(AddRentDto dto)
        {
            await _service.Add(dto);
        }
    }
}
