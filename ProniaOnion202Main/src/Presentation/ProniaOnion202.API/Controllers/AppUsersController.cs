using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos.Users;

namespace ProniaOnion202.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IAuthService _service;

        public AppUsersController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost]
        public async  Task<IActionResult> Register([FromForm]RegisterDto dto)
        {
            await _service.Register(dto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
