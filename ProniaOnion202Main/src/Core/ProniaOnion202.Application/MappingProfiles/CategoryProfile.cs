using AutoMapper;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.MappingProfiles
{
    internal class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryItemDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }

    }
}
