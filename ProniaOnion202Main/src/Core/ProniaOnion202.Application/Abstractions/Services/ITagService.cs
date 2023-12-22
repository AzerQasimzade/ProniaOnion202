using ProniaOnion202.Application.Dtos.Categories;
using ProniaOnion202.Application.Dtos.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<ICollection<TagItemDto>> GetAllAsync(int page, int take);
        //Task<GetCategoryDto> GetAsync(int id);
        Task Create(TagCreateDto dto);
        Task Update(int id, TagUpdateDto tagUpdateDto);
        Task SoftDeleteAsync(int id);


    }
}
