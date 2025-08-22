using System.Collections.Generic;
using Sm.Domain.OrderAgg;

namespace Sm.Domain.Services
{
    public interface IShopInventoryAcl
    {
        bool ReduceFromInventory(List<OrderItem> items);
        
    }
}