using Sm.Application.Contracts.Product;

namespace Sm.Application.Contracts.Order
{
    public class OrderItemViewModel
    {
        
        public long Id { get;  set; }
        public long ProductId { get;  set; }
        public int Count { get;  set; }
        public double UnitPrice{ get;  set; }
        public int DiscountRate{ get;  set; }
        public long OrderId{ get;  set; }
        public string Product { get; set; }
        
    }
}