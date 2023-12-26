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
            CreateMap<CategoryItemDto,Category>().ReverseMap();
            CreateMap<IncludesCategoryDto,Category >().ReverseMap();
            CreateMap<Category,CategoryCreateDto>();
            CreateMap<Category,CategoryUpdateDto>().ReverseMap();
        }
    }
}
