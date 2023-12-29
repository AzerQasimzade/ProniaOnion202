using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Dtos.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Abstractions.Services
{
    public interface ITokenHandler
    {
        TokenResponseDto CreateToken(AppUser user, int minutes);
    }
}
