using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinaOnion202.Domain.Entities
{
    public class Category:BaseNameableEntity
    {
        public ICollection<Product>? Products { get; set; }
    }
}
