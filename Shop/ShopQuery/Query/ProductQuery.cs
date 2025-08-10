using System;
using System.Collections.Generic;
using System.Linq;
using Cm.Infrastructure;
using Dm.Infrastructure;
using Im.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using ShopQuery.Contracts.Comment;
using ShopQuery.Contracts.Product;
using ShopQuery.Contracts.ProductPicture;
using SM.Infrastructure;

namespace ShopQuery.Contracts.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly SmContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;


        public ProductQuery(SmContext context, InventoryContext inventoryContext, DiscountContext discountContext,
            CommentContext commentContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice })
                .ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToList();

            var products = _context.Products
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
                }).OrderByDescending(x => x.Id)
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

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice })
                .ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .ToList();

            var query = _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    ShortDescription = x.ShortDescription
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            var products = query.OrderByDescending(x => x.Id).ToList();

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
                        product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                        var discountAmount = Math.Round((price * discountRate) / 100);
                        product.PriceWithDiscount = (price - discountAmount).ToMoney();
                    }
                }
            }

            return products;
        }

        public ProductQueryModel GetProduct(string slug)
        {
            var inventory = _inventoryContext.Inventory
                .Select(x => new { x.ProductId, x.UnitPrice, x.InStock })
                .ToList();

            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .ToList();

            var product = _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    ShortDescription = x.ShortDescription,
                    Pictures = PictureMapping(x.ProductPictures),
                    Description = x.Description,
                    Code = x.Code,
                    Keywords = x.Keywords,
                    // Comments = CommentMapping(x.Comments)
                }).FirstOrDefault(x => x.Slug == slug);


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
                    product.IsInStock = productInventory.InStock;
                    product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            product.Comments = _commentContext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentsType.Product)
                .Where(x => x.OwnerId == product.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                }).OrderByDescending(x => x.Id )
                .ToList();

            return product;
        }

        private static List<ProductPictureQueryModel> PictureMapping(
            List<Sm.Domain.ProductPictureAgg.ProductPicture> Pictures)
        {
            return Pictures.Select(x => new ProductPictureQueryModel
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                IsRemoved = x.IsRemoved
            }).ToList();
        }

        // private static List<CommentQueryModel> CommentMapping(List<Sm.Domain.CommentAgg.Comment> Comments)
        // {
        //     return Comments
        //         .Where(x => !x.IsCanceled)
        //         .Where(x => x.IsConfirmed)
        //         .Select(x => new CommentQueryModel
        //     {
        //         Email = x.Email,
        //         Message = x.Message,
        //         Name = x.Name,
        //     }).ToList();
        // }
    }
}