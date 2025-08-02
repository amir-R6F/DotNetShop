using Microsoft.AspNetCore.Mvc;
using ShopQuery.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _query;

        public ProductCategoryViewComponent(IProductCategoryQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _query.GetCategories();
            return View(categories);
        }
    }
}