using System.Collections.Generic;
using System.Linq;
using ShopQuery.Contracts.ProductCategory;
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
                Name = x.Name,
                Description = x.Description,
                Picture = x.Picture,
                Keywords = x.Keywords,
                Slug = x.Slug,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            }).ToList();
        }
    }
}