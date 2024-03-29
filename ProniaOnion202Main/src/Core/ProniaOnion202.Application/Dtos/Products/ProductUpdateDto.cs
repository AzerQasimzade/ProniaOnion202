﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Application.Dtos.Products
{
    public record ProductUpdateDto(string Name, decimal Price, string? Description, string SKU, int CategoryId, ICollection<int>? ColorIds, ICollection<int>? TagIds);
}
