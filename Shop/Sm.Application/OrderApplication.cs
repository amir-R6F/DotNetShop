using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Shop.Application;
using Shop.Application.ZarinPal;
using Sm.Application.Contracts.Order;
using Sm.Domain.OrderAgg;

namespace Sm.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;


        public OrderApplication(IAuthHelper authHelper, IOrderRepository orderRepository, IConfiguration configuration)
        {
            _authHelper = authHelper;
            _orderRepository = orderRepository;
            _configuration = configuration;
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
            // reduce orderItems from Inventory
            _orderRepository.SaveChanges();
            return issueTrackingNum;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }
    }
}