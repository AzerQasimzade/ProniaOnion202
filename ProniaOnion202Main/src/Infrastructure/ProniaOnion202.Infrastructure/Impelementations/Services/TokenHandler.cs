using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrinaOnion202.Domain.Entities;
using ProniaOnion202.Application.Abstractions.Services;
using ProniaOnion202.Application.Dtos.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProniaOnion202.Infrastructure.Impelementations.Services
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResponseDto CreateToken(AppUser user, int minutes)
        {
            
            ICollection<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Surname,user.Surname)
            };
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(minutes),
                claims: userClaims,
                signingCredentials: credentials
                );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(handler.WriteToken(token),user.UserName,token.ValidTo);
        }
    }
}
