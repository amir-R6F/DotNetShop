using Sm.Application.Contracts.Order;

namespace Sm.Application
{
    public class CartService : ICartService
    {
        public myCart Cart { get; set; }
        
        public void Set(myCart cart)
        {
            Cart = cart;
        }

        public myCart Get()
        {
            return Cart;
        }
    }
}