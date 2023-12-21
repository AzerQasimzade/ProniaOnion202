using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrinaOnion202.Domain.Entities;

namespace ProniaOnion202.Persistence.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
