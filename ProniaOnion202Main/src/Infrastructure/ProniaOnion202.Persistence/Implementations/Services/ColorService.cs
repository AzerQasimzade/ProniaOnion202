using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Abstractions.Repository;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Implementations.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Color> color = await _repository.GetAllWhere(skip: (page - 1) * take, take: take).ToListAsync();
            ICollection<ColorItemDto> dtos = _mapper.Map<ICollection<ColorItemDto>>(color);
            return dtos;
        }
        public async Task Create(ColorCreateDto dto)
        {
            //await _repository.AddAsync(new Tag
            //{
            //    Name = dto.Name
            //});
            await _repository.AddAsync(_mapper.Map<Color>(dto));
            await _repository.SaveChangesAsync();
        }

        public async Task Update(int id, ColorUpdateDto colorUpdateDto)
        {
            Color color = await _repository.GetByIdAsync(id);
            if (color == null) throw new Exception("Color cant found");
            //color.Name = name;
            color = _mapper.Map(colorUpdateDto, color);
            _repository.Update(color);
            await _repository.SaveChangesAsync();

        }
        public async Task SoftDeleteAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id, true);
            if (color is null) throw new Exception("Not Found :)");
            _repository.SoftDelete(color);
            await _repository.SaveChangesAsync();
        }
     
        public async Task DeleteAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id);
            if (color == null) throw new Exception("Color cant found");
            _repository.Delete(color);
            await _repository.SaveChangesAsync();
        }

        public async Task ReverseDeleteAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id);
            if (color == null) throw new Exception("Color cant found");
            _repository.ReverseDelete(color);
            await _repository.SaveChangesAsync();
        }

    }
}
