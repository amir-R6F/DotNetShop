using System;
using Microsoft.EntityFrameworkCore;
using Sm.Domain.ProductAgg;
using Sm.Domain.ProductCategoryAgg;
using Sm.Domain.ProductPictureAgg;
using SM.Infrastructure.Mapping;

namespace SM.Infrastructure
{
    public class SmContext: DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<ProductPicture> ProductPictures { get; set; }


        public SmContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            base.OnModelCreating(modelBuilder);

            var assembly = typeof(ProductCategoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}