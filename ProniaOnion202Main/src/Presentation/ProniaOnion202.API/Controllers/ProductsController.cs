using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos.Products;

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
        public async Task<IActionResult> GetAll(int page=1,int take=10)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            return StatusCode(StatusCodes.Status200OK, await _service.GetByIdAsync(id));          
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]ProductCreateDto dto)
        {
            await _service.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id,ProductUpdateDto dto)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await  _service.UpdateAsync(id, dto);
            return StatusCode(StatusCodes.Status204NoContent);    
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.DeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.SoftDeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch("recovery/{id}")]
        public async Task<IActionResult> ReverseDelete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.ReverseDeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }

    }
}
