using System.Collections.Generic;
using Shop.Domain;
using Sm.Application.Contracts.Product;
using Sm.Domain.ProductCategoryAgg;

namespace Sm.Domain.ProductAgg
{
    public interface IProductRepository : IBaseRepository<long, Product>
    {
        EditProduct GetDetails(long id);

        List<ProductViewModel> Search(ProductSearchModel searchModel);

        List<ProductViewModel> GetProducts();
        
        Product GetProductWithCategoryBy(long id);
        
        

    }
}