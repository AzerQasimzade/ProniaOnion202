using Microsoft.EntityFrameworkCore;
using PrinaOnion202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Common
{
    internal static class GlobalQueryFilter
    {
        public static void ApplyFilter<T>(this ModelBuilder builder) where T : BaseEntity, new()
        {
            builder.Entity<T>().HasQueryFilter(x => x.IsDeleted == false);
        }
        public static void ApplyQueryFilters(this ModelBuilder builder)
        {
            builder.ApplyFilter<Product>();
            builder.ApplyFilter<Category>();
            builder.ApplyFilter<Color>();
            builder.ApplyFilter<ProductColor>();
        }
    }
}
