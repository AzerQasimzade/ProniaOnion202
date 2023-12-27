using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrinaOnion202.Domain.Entities;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository,ICategoryRepository categoryRepository,IColorRepository colorRepository,IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductItemDto>> GetAllAsync(int page,int take)
        { 
            return _mapper.Map<IEnumerable<ProductItemDto>>(await _repository.GetAllWhere(skip: ((page - 1) * take), take: take).ToListAsync());
        }

        public async Task<ProductGetDto> GetByIdAsync(int id)
        {
            Product product=await _repository.GetByIdAsync(id,includes:nameof(Product.Category));
            if (product is null) throw new Exception("The product you are looking for is no longer available"); 
            return _mapper.Map<ProductGetDto>(product);
        }
        public async Task CreateAsync(ProductCreateDto dto)
        {
            if (await _repository.IsExistAsync(p => p.Name == dto.Name)) throw new Exception($"there is a product with the same {dto.Name}");
            if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategoryId)) throw new Exception("The product you are looking for is no longer available");

            Product product=_mapper.Map<Product>(dto);

            product.ProductColors=new List<ProductColor>();

            if (dto.ColorIds is not null)
            {
                foreach (var colId in dto.ColorIds)
                {
                    if (!await _colorRepository.IsExistAsync(c => c.Id == colId)) throw new Exception($"Could not find {colId}");
                        product.ProductColors.Add(new ProductColor { ColorId = colId });
                }
            }
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync(); 
        }
        public async Task UpdateAsync(int id,ProductUpdateDto dto)
        {
            Product existed=await _repository.GetByIdAsync(id,true,includes:new string[] {nameof(Product.ProductColors),nameof(Product.ProductTags)});
            if (existed.Name!=dto.Name)
                if (await _repository.IsExistAsync(p => p.Name == dto.Name)) throw new Exception($"there is a product with the same {dto.Name}");
            
            if (dto.CategoryId != existed.CategoryId)
                if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategoryId)) throw new Exception("The product you are looking for is no longer available");
            existed = _mapper.Map(dto, existed);

            // 1 2 3      1 2 4 

            if (dto.ColorIds is not null)
            {
                foreach (var colorId in dto.ColorIds)
                {
                    if (!existed.ProductColors.Any(pc => pc.ColorId == colorId))
                    {
                        if (!await _colorRepository.IsExistAsync(c => c.Id == colorId)) throw new Exception($"Could not find {colorId}");
                        existed.ProductColors.Add(new ProductColor { ColorId = colorId });
                    }
                }
                existed.ProductColors = existed.ProductColors.Where(pc => dto.ColorIds.Any(colId => pc.ColorId == colId)).ToList();
            }
            else
                existed.ProductColors=new List<ProductColor>();
            foreach (var tagId in dto.TagIds)
            {
                if (!existed.ProductColors.Any(pc => pc.TagId == tagId))
                {
                    if (!await _colorRepository.IsExistAsync(c => c.Id == tagId)) throw new Exception($"Could not find {tagId}");
                    existed.ProductColors.Add(new ProductColor { TagId = tagId });
                }
            }
            existed.ProductTags = existed.ProductTags.Where(pc => dto.TagIds.Any(tagId => pc.TagId == tagId)).ToList();
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _repository.GetByIdAsync(id);
            if (product == null) throw new Exception("Product cant found");
            _repository.Delete(product);
            await _repository.SaveChangesAsync();
        }
        public async Task SoftDeleteAsync(int id)
        {
            Product existed = await _repository.GetByIdAsync(id,true,includes:nameof(Product.ProductColors));
            if (existed == null) throw new Exception("Product cant found");
            ICollection<ProductColor> productColors=existed.ProductColors.Where(x=>x.ProductId==id).ToList();
            if (productColors is not null)
            {
                foreach (var color in productColors)
                {
                    color.IsDeleted = true;
                }
            } 
            _repository.SoftDelete(existed);
            await _repository.SaveChangesAsync();
        }
        public async Task ReverseDeleteAsync(int id)
        {
            Product existed = await _repository.GetByIdAsync(id,true,true, includes: nameof(Product.ProductColors));
            if (existed == null) throw new Exception("Product cant found");
            ICollection<ProductColor> productColors = existed.ProductColors.Where(x => x.ProductId == id).ToList();
            if (productColors is not null)
            {
                foreach (var color in productColors)
                {
                    color.IsDeleted = false;
                }
            }
            _repository.ReverseDelete(existed);
            await _repository.SaveChangesAsync();
        }
    }
}
