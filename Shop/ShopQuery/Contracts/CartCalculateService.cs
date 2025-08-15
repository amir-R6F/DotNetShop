using System;
using System.Collections.Generic;
using System.Linq;
using Dm.Infrastructure;
using Shop.Application;
using Shop.Domain;
using Sm.Application.Contracts.Order;

namespace ShopQuery.Contracts
{
    public class CartCalculateService : ICartCalculateService
    {
        private readonly IAuthHelper _authHelper;
        private readonly DiscountContext _discountContext;

        public CartCalculateService(IAuthHelper authHelper, DiscountContext discountContext)
        {
            _authHelper = authHelper;
            _discountContext = discountContext;
        }

        public myCart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new myCart();
            var colleagueDiscounts = _discountContext.ColleagueDiscounts
                .Where(x => !x.IsRemoved)
                .Select(x => new { x.DiscountRate, x.ProductId})
                .ToList();
            
            var custommerDiscounts = _discountContext.CustomerDiscounts
                .Where(x => 
                    x.StartDate < DateTime.Now &&
                    x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId})
                .ToList();
            
            

            var CurrentAccountrole = _authHelper.CurrentAccountRole();

            foreach (var cartItem in cartItems)
            {
                if (CurrentAccountrole == Rules.Colleague)
                {
                    var ColleagueDiscount = colleagueDiscounts.FirstOrDefault(x => 
                        x.ProductId == cartItem.Id);

                    if (ColleagueDiscount != null)
                        cartItem.DiscountRate = ColleagueDiscount.DiscountRate;
                }
                else
                {
                    var CustomerDiscount = custommerDiscounts.FirstOrDefault(x =>
                        x.ProductId == cartItem.Id);
                    if (CustomerDiscount != null)
                        cartItem.DiscountRate = CustomerDiscount.DiscountRate;
                }

                cartItem.DiscountAmount = ((cartItem.TotalItemsPrice * cartItem.DiscountRate) / 100);
                cartItem.ItemPayAmount = cartItem.TotalItemsPrice - cartItem.DiscountAmount;
                cart.Add(cartItem);
            }

            return cart;
        }
    }
}