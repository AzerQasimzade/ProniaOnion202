using ProniaOnion202.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take);
        //Task<GetCategoryDto> GetAsync(int id);
        Task Create(ColorCreateDto dto);
        Task Update(int id, ColorUpdateDto colorUpdateDto);
        Task SoftDeleteAsync(int id);
        Task DeleteAsync(int id);

        Task ReverseDeleteAsync(int id);


    }
}
