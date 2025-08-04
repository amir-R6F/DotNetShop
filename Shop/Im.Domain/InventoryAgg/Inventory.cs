using System;
using System.Collections.Generic;
using System.Linq;
using Im.Domain.InventoryAgg;
using Shop.Domain;

namespace Im.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }

        public double UnitPrice { get; private set; }

        public bool InStock { get; private set; }

        public List<InventoryOperations> InvOperations { get; private set; }

        public Inventory(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            InStock = false;
            InvOperations = new List<InventoryOperations>();
        }

        public void Edit(long productId, double unitPrice)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
        }

        public long CalculateCurrentCount()
        {
            var sum = InvOperations.Where(x => x.Operation).Sum(x => x.Count);
            var minus = InvOperations.Where(x => !x.Operation).Sum(x => x.Count);

            return sum - minus;
        }

        public void Increase(long count, long operatorId, string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperations(true, count, operatorId, currentCount, description, 0, Id);
            InvOperations.Add(operation);
            InStock = currentCount > 0;
        }

        public void Decrease(long count, long operatorId, string description, long orderId)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperations(false, count, operatorId, currentCount, description, orderId, Id);
            InvOperations.Add(operation);
            InStock = currentCount > 0;
        }
    }
}