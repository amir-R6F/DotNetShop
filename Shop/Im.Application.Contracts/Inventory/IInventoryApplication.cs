using System.Collections.Generic;
using Shop.Application;

namespace Im.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(DecreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> command);
        List<InventoryOprationViewModel> GetLog(long inventoryId);

    }
}