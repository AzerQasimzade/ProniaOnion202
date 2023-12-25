using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion202.Application.Abstractions.Repository;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Implementations.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAllAsync(int page,int take)
        { 
            return _mapper.Map<IEnumerable<ProductItemDto>>(await _repository.GetAllWhere(skip: ((page - 1) * take), take: take).ToListAsync());
        }
    }
}
