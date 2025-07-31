using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sm.Application;
using Sm.Application.Contracts.ProductCategory;
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

            service.AddDbContext<SmContext>(x => x.UseSqlServer(connection));
        }
    }
}