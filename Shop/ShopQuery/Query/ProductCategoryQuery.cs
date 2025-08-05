using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ShopQuery.Contracts.Product;
using ShopQuery.Contracts.ProductCategory;
using Sm.Application.Contracts.Product;
using SM.Infrastructure;

namespace ShopQuery.Contracts.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly SmContext _context;

        public ProductCategoryQuery(SmContext context)
        {
            _context = context;
        }

        public List<ProductCategoryQueryModel> GetCategories()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> GetCategoriesWithProducts()
        {
            return _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                }).ToList();

            
        }

        private static List<ProductQueryModel> MapProducts(List<Sm.Domain.ProductAgg.Product> products)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        } 
    //SAME RESULT WITH SELECT BUT ITS NOT CLEANE   
    //BUT IF U NEED CONDITION U WILL USE THIS
    //     var res = new List<ProductQueryModel>();
    //         foreach (var x in products)
    //     {
    //         var item = new ProductQueryModel
    //         {
    //             Id = x.Id,
    //             Name = x.Name,
    //             Category = x.Category.Name,
    //             Picture = x.Picture,
    //             PictureAlt = x.PictureAlt,
    //             PictureTitle = x.PictureTitle,
    //             Slug = x.Slug
    //         };
    //         res.Add(item);
    //     }
    //
    // return res;
    }
}