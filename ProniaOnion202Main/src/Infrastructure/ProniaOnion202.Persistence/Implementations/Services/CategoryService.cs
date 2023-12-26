using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Abstractions.Repository;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ICollection<CategoryItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();
            ICollection<CategoryItemDto> dtos = _mapper.Map<ICollection<CategoryItemDto>>(categories); 
            return dtos;
        } 

        public async Task Create(CategoryCreateDto dto)
        {
            //await _repository.AddAsync(new Category
            //{
            //    Name = dto.Name
            //});
            await _repository.AddAsync(_mapper.Map<Category>(dto));
            await _repository.SaveChangesAsync();
        }

        public async Task Update(int id, CategoryUpdateDto categoryUpdateDto)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Category cant found");
            //category.Name = name;
            category = _mapper.Map(categoryUpdateDto, category);
            _repository.Update(category);
            await _repository.SaveChangesAsync();

        }
        public async Task SoftDeleteAsync(int id)
        {
            Category category=await _repository.GetByIdAsync(id,true);
            if (category is null) throw new Exception("Not Found :)");
            _repository.SoftDelete(category);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Category cant found");
            _repository.Delete(category);
            await _repository.SaveChangesAsync();
        }

        public async Task ReverseDeleteAsync(int id)
        {
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) throw new Exception("Category cant found");
            _repository.ReverseDelete(category);
            await _repository.SaveChangesAsync();
        }




        //public async Task<GetCategoryDto> GetAsync(int id)
        //{
        //    Category category = await _repository.GetByIdAsync(id);
        //    if (category == null) throw new Exception("Not Found");

        //    return new GetCategoryDto
        //    {
        //        Id = category.Id,
        //        Name = category.Name
        //    };
        //}
    }
}
