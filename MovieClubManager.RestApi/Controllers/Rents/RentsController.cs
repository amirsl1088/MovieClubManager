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
        [HttpPatch("{id}")]
        public async Task<decimal?> Update([FromRoute]int id,[FromBody]UpdateRentDto dto)
        {
            return await _service.Update(id, dto);
        }
        [HttpGet("{userid}")]
        public async Task<List<GetRentDto>?>Get([FromRoute]int userid)
        {
            return await _service.Get(userid);
        }
    }
}
