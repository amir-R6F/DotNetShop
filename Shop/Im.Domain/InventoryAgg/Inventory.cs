using System;
using System.Collections.Generic;
using Shop.Domain;

namespace Im.Domain.InventoryAgg
{
    public class Inventory : EntityBase
    {
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public bool InStock { get; private set; }
        public List<InventoryOperations> InvOperations { get; private set; }
    }

    public class InventoryOperations
    {
        public long Id { get; private set; }
        public bool Operation { get; private set; }
        public long Count { get; private set; }
        public long OperatorId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long CurrentCount { get; private set; }
        public string Description { get; private set; }
        public long OrderId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }
        
    }
}