using Microsoft.AspNetCore.Mvc;
using ShopQuery.Contracts.Product;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent : ViewComponent
    {
        private readonly IProductQuery _query;

        public LatestArrivalsViewComponent(IProductQuery query)
        {
            _query = query;
        }

        public IViewComponentResult Invoke()
        {
            var products = _query.GetLatestArrivals();
            
            return View(products);
        }
    }
}