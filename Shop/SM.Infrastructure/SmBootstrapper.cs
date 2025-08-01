using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sm.Application;
using Sm.Application.Contracts.Product;
using Sm.Application.Contracts.ProductCategory;
using Sm.Application.Contracts.ProductPicture;
using Sm.Domain.ProductAgg;
using Sm.Domain.ProductCategoryAgg;
using Sm.Domain.ProductPictureAgg;
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

            service.AddTransient<IProductPictureApplication, ProductPictureApplication>();
            service.AddTransient<IProductPictureRepository, ProductPictureRepository>();

            service.AddDbContext<SmContext>(x => x.UseSqlServer(connection));
        }
    }
}