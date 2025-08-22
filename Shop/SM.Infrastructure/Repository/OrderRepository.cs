using System.Collections.Generic;
using System.Linq;
using Am.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using Shop.Infrastructure;
using Sm.Application.Contracts;
using Sm.Application.Contracts.Order;
using Sm.Domain.OrderAgg;

namespace SM.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<long, Order>, IOrderRepository
    {
        private readonly SmContext _context;
        private readonly AccountContext _accountContext;

        public OrderRepository(SmContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.Select(x => new { x.PayAmount, x.Id }).FirstOrDefault(x => x.Id == id);
            if (order != null)
                return order.PayAmount;
            return 0;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel)
        {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.Fullname }).ToList();
            
            var query = _context.Orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                AccountId = x.Id,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                IssueTrackingNum = x.IssueTrackingNum,
                PayAmount = x.PayAmount,
                PaymentMethodId = x.PaymentMethod,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi()
            });
            query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0)
                query = query.Where(x => x.AccountId == searchModel.AccountId);

            var orders = query.OrderByDescending(x => x.Id).ToList();
            foreach (var order in orders)
            {
                order.AccountFullName = accounts.FirstOrDefault(x => x.Id == order.AccountId)?.Fullname;
                order.PaymentMethod = PaymentMethod.GetBy(order.PaymentMethodId).Name;
            }

            return orders;
        }

        public List<OrderItemViewModel> GetItems(long orderId)
        {
            var products = _context.Products.Select(x => new { x.Id, x.Name }).ToList();
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
                return new List<OrderItemViewModel>();

            var items = order.Items.Select(x => new OrderItemViewModel
            {
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                OrderId = x.OrderId,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).ToList();

            foreach (var item in items)
            {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            }

            return items;
        }
    }
}