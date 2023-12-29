using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos.Tokens;
using ProniaOnion202.Application.Dtos.Users;
using ProniaOnion202.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenHandler _handler;

        public AuthService(UserManager<AppUser> userManager,IMapper mapper,ITokenHandler handler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _handler = handler;
        }

        public async Task<TokenResponseDto> Login(LoginDto dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);
                if (user is null) throw new Exception("Email,Username or Password is incorrect");
            }
            if (!await _userManager.CheckPasswordAsync(user, dto.Password)) throw new Exception("Email,Username or Password is incorrect");

            return _handler.CreateToken(user, 60);
        }

        public async Task Register(RegisterDto dto)
        {
            if (await _userManager.Users.AnyAsync(u => u.UserName == dto.UserName || u.Email == dto.Email)) throw new Exception("There is already a user in this email or username");
            AppUser user= _mapper.Map<AppUser>(dto);
            var result=await _userManager.CreateAsync(user,dto.Password);

            if (!result.Succeeded)
            {
                StringBuilder builder= new StringBuilder();
                foreach (var error in result.Errors)
                {
                    builder.AppendLine(error.Description);
                }
                throw new Exception(builder.ToString());
            }
        }
    }
}
