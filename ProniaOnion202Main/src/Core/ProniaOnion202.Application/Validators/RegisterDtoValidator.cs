using FluentValidation;
using ProniaOnion202.Application.Abstractions.Repository.Common.Extensions;
using ProniaOnion202.Application.Dtos.Users;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(256)
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(256)
                .MinimumLength(8);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3)
                .Matches(@"^[a-zA-Z\s]*$");
            RuleFor(x => x.Surname)
               .NotEmpty()
               .MaximumLength(50)
               .MinimumLength(3)
               .Matches(@"^[a-zA-Z\s]*$");
            RuleFor(x => x)
                .Must(x => x.ConfirmPassword == x.Password);
        }       
    }   
}

