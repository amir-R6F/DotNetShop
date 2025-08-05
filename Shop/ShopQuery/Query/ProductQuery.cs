using System;
using System.Collections.Generic;
using System.Linq;
using Dm.Infrastructure;
using Im.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using ShopQuery.Contracts.Product;
using SM.Infrastructure;

namespace ShopQuery.Contracts.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly SmContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;


        public ProductQuery(SmContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice })
                .ToList();
            
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new {x.ProductId, x.DiscountRate})
                .ToList();
            
            var products =_context.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).OrderByDescending(x=> x.Id)
                .Take(6)
                .ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();
                    var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productDiscount != null)
                    {
                        int discountRate = productDiscount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return products;    
        }
    }
}