using System.Collections.Generic;
using Sm.Application.Contracts.Order;

namespace ShopQuery.Contracts
{
    public interface ICartCalculateService
    {
        myCart ComputeCart(List<CartItem> cartItems);
    }
}