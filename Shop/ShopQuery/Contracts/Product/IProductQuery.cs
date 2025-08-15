using System.Collections.Generic;
using Sm.Application.Contracts.Order;

namespace ShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
        ProductQueryModel GetProduct(string slug);
        List<CartItem> CheckProductCount(List<CartItem> cartItems);


    }
}