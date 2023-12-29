using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProniaOnion202.Application.Abstractions.Services;
using a=ProniaOnion202.Infrastructure.Impelementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Infrastructure.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<ITokenHandler,a.TokenHandler>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,//(Check my Security Key)  

                    ValidAudience = configuration["Jwt:Audience"],//(it's True datas which compare from these)
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"])),
                    LifetimeValidator = (_, expired, key, _) => key != null ? expired > DateTime.UtcNow : false
                    //LifetimeValidator = (_, expired, key, _) =>
                    //{
                    //    if (key is not null)
                    //    {
                    //        if (expired>DateTime.UtcNow)
                    //        {LifetimeValidator = (_, expired, key, _) =>
                    //            return true;    
                    //        }
                    //    }
                    //    return false;
                    //}
                };
            });
            services.AddAuthorization();
            return services;
        }
    }
}
