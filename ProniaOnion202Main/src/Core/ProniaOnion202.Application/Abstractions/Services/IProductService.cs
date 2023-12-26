using ProniaOnion202.Application.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductItemDto>> GetAllAsync(int page, int take);
        Task<ProductGetDto> GetByIdAsync(int id);

        Task CreateAsync(ProductCreateDto dto);
        Task UpdateAsync(int id, ProductUpdateDto dto);
        Task SoftDeleteAsync(int id);
        Task DeleteAsync(int id);
        Task ReverseDeleteAsync(int id);

    }
}
