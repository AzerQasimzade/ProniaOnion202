﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Abstractions.Repository;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<TagItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();
            ICollection<TagItemDto> dtos = _mapper.Map<ICollection<TagItemDto>>(tags);
            return dtos;
        }
        public async Task Create(TagCreateDto dto)
        {
            //await _repository.AddAsync(new Tag
            //{
            //    Name = dto.Name
            //});
            await _repository.AddAsync(_mapper.Map<Tag>(dto));
            await _repository.SaveChangesAsync();
        }

        public async Task Update(int id,TagUpdateDto tagUpdateDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Tag cant found");
            //tag.Name = name;
            tag = _mapper.Map(tagUpdateDto, tag);
            _repository.Update(tag);
            await _repository.SaveChangesAsync();

        }
        public async Task SoftDeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id,true);
            if (tag is null) throw new Exception("Not Found :)");
            _repository.SoftDelete(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Tag cant found");
            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task ReverseDeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag == null) throw new Exception("Category cant found");
            _repository.ReverseDelete(tag);
            await _repository.SaveChangesAsync();
        }

    }
}
