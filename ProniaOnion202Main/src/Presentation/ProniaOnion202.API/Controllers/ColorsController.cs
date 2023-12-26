using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos;
using ProniaOnion202.Application.Dtos.Tags;

namespace ProniaOnion202.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _service;

        public ColorsController(IColorService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 5)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ColorCreateDto colorDto)
        {
            await _service.Create(colorDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, ColorUpdateDto colorUpdateDto)
        {
            await _service.Update(id, colorUpdateDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.SoftDeleteAsync(id);
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
