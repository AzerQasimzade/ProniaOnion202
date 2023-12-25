using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Dtos.Products
{
    public record ProductItemDto(int id,string name,decimal price,string SKU);
   
}
