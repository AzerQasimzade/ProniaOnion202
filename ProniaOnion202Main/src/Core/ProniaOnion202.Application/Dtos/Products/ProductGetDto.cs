using ProniaOnion202.Application.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Dtos.Products
{
    public record ProductGetDto(int id, string name, decimal price, string SKU,string? Description,int CategoryId,IncludesCategoryDto Category);
    
}
