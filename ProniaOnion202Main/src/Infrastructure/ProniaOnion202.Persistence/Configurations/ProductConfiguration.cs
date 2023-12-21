using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrinaOnion202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Price).IsRequired().HasColumnType("decimal(6,2)");
            builder.Property(c => c.Description).IsRequired(false).HasColumnType("text");
            builder.Property(c => c.SKU).IsRequired().HasMaxLength(100);
        }
    }
}
