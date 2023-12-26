using AutoMapper;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Dtos.Categories;
using ProniaOnion202.Application.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product,ProductItemDto>().ReverseMap();
            CreateMap<ProductCreateDto,Product> ();
            CreateMap<Product,ProductGetDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
        }

    }
}
