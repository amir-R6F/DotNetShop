using System.Collections.Generic;
using System.Linq;
using Im.Application.Contracts.Inventory;
using Im.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using Shop.Infrastructure;
using SM.Infrastructure;

namespace Im.Infrastructure.Repository
{
    public class InventoryRepository : BaseRepository<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly SmContext _smContext;
        
        public InventoryRepository(InventoryContext context, SmContext smContext) : base(context)
        {
            _context = context;
            _smContext = smContext;

        }

        public EditInventory GetDetails(long id)
        {
            return _context.Inventory.Select(x => new EditInventory
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var products = _smContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _context.Inventory.Select(x => new InventoryViewModel()
            {
                Id = x.Id,
                CurrentCount = x.CalculateCurrentCount(),
                InStock = x.InStock,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.InStock)
                query = query.Where(x => !x.InStock);
            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var inventoy = query.OrderByDescending(x => x.Id).ToList();
            
            inventoy.ForEach(item => 
                item.Product = products.FirstOrDefault(x=> x.Id == item.ProductId)?.Name);

            return inventoy;
        }

        public Inventory GetBy(long productId)
        {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryOprationViewModel> GetLog(long inventoryId)
        {
            var inventory = _context.Inventory.FirstOrDefault(x => x.Id == inventoryId);
            
                return inventory.InvOperations.Select(x => new InventoryOprationViewModel
            {
                Id = x.Id,
                Count = x.Count,
                OrderId = x.OrderId,
                Description = x.Description,
                Operation = x.Operation,
                CurrentCount = x.CurrentCount,
                OperatorId = x.OperatorId,
                OperatorName = "admin",
                OperationDate = x.OperationDate.ToFarsi()
                
            }).ToList();
        }
    }
}