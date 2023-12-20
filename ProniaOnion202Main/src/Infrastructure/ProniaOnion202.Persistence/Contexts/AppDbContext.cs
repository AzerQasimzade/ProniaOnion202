using Microsoft.EntityFrameworkCore;
using PrinaOnion202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion202.Persistence.Contexts
{
    internal class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Product>().Property(c => c.Price).IsRequired().HasColumnType("decimal(6,2)");
            modelBuilder.Entity<Product>().Property(c => c.Description).IsRequired(false).HasColumnType("text");
            modelBuilder.Entity<Product>().Property(c => c.SKU).IsRequired().HasMaxLength(100);
            base.OnModelCreating(modelBuilder);
        }


    }
}
