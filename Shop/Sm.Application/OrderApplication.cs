using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Shop.Application;
using Shop.Application.ZarinPal;
using Sm.Application.Contracts.Order;
using Sm.Domain.OrderAgg;
using Sm.Domain.Services;

namespace Sm.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;
        private readonly IShopInventoryAcl _acl;
        


        public OrderApplication(IAuthHelper authHelper, IOrderRepository orderRepository, IConfiguration configuration, IShopInventoryAcl acl)
        {
            _authHelper = authHelper;
            _orderRepository = orderRepository;
            _configuration = configuration;
            _acl = acl;
        }

        public long PlaceOrder(myCart cart)
        {
            var currentAccountId = _authHelper.CurrentAccountId();
            
            var order = new Order(currentAccountId, cart.PaymentMethod, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);
            foreach (var cartItem in cart.Items)
            {
                var price = Regex.Replace(cartItem.UnitPrice, @"\D", "");
                var orderItem = new OrderItem(cartItem.Id, cartItem.Count, Double.Parse(price), cartItem.DiscountRate);
                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            var symbol = _configuration.GetValue<string>("Symbol");
            var issueTrackingNum = CodeGenerator.Generate(symbol);
            order.SetIssueTrackingNum(issueTrackingNum);

            if (_acl.ReduceFromInventory(order.Items))
            {
                _orderRepository.SaveChanges();
                return issueTrackingNum;                
            }

            return "";

        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }
    }
}