using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Dtos.Products
{
    public record ProductCreateDto(string Name,decimal Price,string? Description,string SKU,int CategoryId,ICollection<int>? ColorIds);
    
}
