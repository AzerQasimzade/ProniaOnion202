using AutoMapper;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Dtos.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.MappingProfiles
{
    internal class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagItemDto>().ReverseMap();
            CreateMap<TagCreateDto, Tag>();
            CreateMap<TagUpdateDto, Tag>().ReverseMap();
        }

    }
}
