using System.Collections.Generic;

namespace ShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
    }
}