using System.Collections.Generic;

namespace Sm.Application.Contracts.Order
{
    public class myCart
    {
        public myCart()
        {
            Items = new List<CartItem>();
        }

        public void Add(CartItem cartItem)
        {
            Items.Add(cartItem);
            TotalAmount += cartItem.TotalItemsPrice;
            DiscountAmount += cartItem.DiscountAmount;
            PayAmount += cartItem.ItemPayAmount;
        }

        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }
        public List<CartItem> Items { get; set; }
    }
}