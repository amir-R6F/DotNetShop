using System.Collections.Generic;

namespace ShopQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetCategories();

    }
}