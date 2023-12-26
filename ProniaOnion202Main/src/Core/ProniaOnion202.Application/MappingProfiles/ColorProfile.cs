using AutoMapper;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.MappingProfiles
{
    internal class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, ColorItemDto>().ReverseMap();
            CreateMap<ColorCreateDto, Color>();
            CreateMap<ColorUpdateDto, Color>().ReverseMap();
        }

    }
}
