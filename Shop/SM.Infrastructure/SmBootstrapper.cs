using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sm.Application;
using Sm.Application.Contracts.Product;
using Sm.Application.Contracts.ProductCategory;
using Sm.Domain.ProductAgg;
using Sm.Domain.ProductCategoryAgg;
using SM.Infrastructure.Repository;

namespace SM.Infrastructure
{
    public class SmBootstrapper
    {
        public static void configuration(IServiceCollection service, string connection)
        {
            service.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            service.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IProductApplication, ProductApplication>();

            service.AddDbContext<SmContext>(x => x.UseSqlServer(connection));
        }
    }
}