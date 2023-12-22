using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos.Categories;
using ProniaOnion202.Application.Dtos.Tags;

namespace ProniaOnion202.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }
        [HttpGet]

        public async Task<IActionResult> Get(int page = 1, int take = 5)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TagCreateDto tagDto)
        {
            await _service.Create(tagDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, TagUpdateDto tagUpdateDto)
        {
            await _service.Update(id,tagUpdateDto);
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.SoftDeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);

        }
    }
}
