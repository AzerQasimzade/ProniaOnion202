using FluentValidation;
using ProniaOnion202.Application.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Validators
{
    public class ProductCreateDtoValidator:AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is important")
                .MaximumLength(100).WithMessage("Name consist max 100 characters")
                .MinimumLength(2).WithMessage("Name consist min 2 characters");

            RuleFor(x => x.SKU)
                .NotEmpty()
                .Must(s => s.Length == 6)
                .WithMessage("SKU must contain only 6 characters");

            RuleFor(x => x.Price).Must(CheckPrice);

            RuleFor(x => x.CategoryId).Must(c => c > 0).WithMessage("Id cannot be less than 0");

            RuleForEach(x=>x.ColorIds).Must(i=>i>0).WithMessage("Id cannot be less than 0");
        }
        public bool CheckPrice(decimal price)
        {
            if (price >= 10 && price <= 999999.99m)
            {
                return true;
            }
            return false;
        }
    }
}
