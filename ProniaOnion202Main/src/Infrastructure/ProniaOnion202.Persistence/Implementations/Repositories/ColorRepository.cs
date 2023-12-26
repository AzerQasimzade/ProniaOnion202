using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Abstractions.Repository;
using ProniaOnion202.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Implementations.Repositories
{
    public class ColorRepository : Repository<Color>, IColorRepository
    {

        public ColorRepository(AppDbContext context):base(context) 
        {
        }
    }
    
}
