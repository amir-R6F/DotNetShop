using System.Collections.Generic;
using Im.Application.Contracts.Inventory;
using Shop.Domain;

namespace Im.Domain.InventoryAgg
{
    public interface IInventoryRepository : IBaseRepository<long, Inventory>
    {
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        Inventory GetBy(long productId);
        List<InventoryOprationViewModel> GetLog(long inventoryId);

    }
}