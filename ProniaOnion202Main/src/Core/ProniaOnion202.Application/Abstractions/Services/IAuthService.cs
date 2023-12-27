using ProniaOnion202.Application.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task Register(RegisterDto dto);
        Task Login(LoginDto dto);

    }
}
