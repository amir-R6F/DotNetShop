using System.Collections.Generic;
using Sm.Application.Contracts.Product;

namespace Im.Application.Contracts.Inventory
{
    public class CreateInventory 
    {
        public long ProductId { get; set; }

        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { get; set; }

    }
}