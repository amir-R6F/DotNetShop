using Microsoft.AspNetCore.Mvc;
using ShopQuery;
using ShopQuery.Contracts.ArticleCategory;
using ShopQuery.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IProductCategoryQuery _productCategoryQuery;

        public MenuViewComponent(IArticleCategoryQuery articleCategoryQuery, IProductCategoryQuery productCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var model = new MenuModel
            {
                ProductCategories = _productCategoryQuery.GetCategories(),
                ArticleCategories = _articleCategoryQuery.GetCategories()
            };
            
            
            return View(model);
        }
    }
}