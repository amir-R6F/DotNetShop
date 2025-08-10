using System.Collections.Generic;
using ShopQuery.Contracts.ArticleCategory;
using ShopQuery.Contracts.ProductCategory;
using ShopQuery.Contracts.Query;

namespace ShopQuery
{
    public class MenuModel
    {
        public List<ProductCategoryQueryModel> ProductCategories { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
    }
}