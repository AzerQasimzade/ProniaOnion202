using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion202.Application.Abstractions.Services;

namespace ProniaOnion202.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll(int page,int take)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }
    }
}
