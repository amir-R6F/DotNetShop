using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Dm.Application.Contracts.CustomerDiscount;
using Dm.Domain.CustomerDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using Shop.Infrastructure;
using SM.Infrastructure;

namespace Dm.Infrastructure.Repository
{
    public class CustomerDiscountRepository : BaseRepository<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly SmContext _ShopContext;

        public CustomerDiscountRepository(DiscountContext context, SmContext shopContext) : base(context)
        {
            _context = context;
            _ShopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _context.CustomerDiscounts.Select(x => new EditCustomerDiscount
            {
                Id = x.Id,
                Reason = x.Reason,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToString(),
                StartDate = x.StartDate.ToString(),
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _ShopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _context.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                Reason = x.Reason,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                StartDate = x.StartDate.ToFarsi(),
                ProductId = x.ProductId,
                EndDateGr = x.EndDate,
                StartDateGr = x.StartDate
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
                query = query.Where(x =>
                    x.StartDateGr > searchModel.StartDate.ToGeorgianDateTime());


            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
                query = query.Where(x =>
                    x.EndDateGr < searchModel.EndDate.ToGeorgianDateTime());

            var discounts = query.OrderByDescending(x => x.Id).ToList();

            discounts.ForEach(discount =>
                discount.Product = products.FirstOrDefault(x => x.Id == discount.ProductId)?.Name);

            return discounts;
        }
    }
}