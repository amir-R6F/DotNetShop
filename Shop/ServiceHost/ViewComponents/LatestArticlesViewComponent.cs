using Microsoft.AspNetCore.Mvc;
using ShopQuery.Contracts.Article;

namespace ServiceHost.ViewComponents
{
    public class LatestArticlesViewComponent : ViewComponent
    {
        private readonly IArticleQuery _query;

        public LatestArticlesViewComponent(IArticleQuery query)
        {
            _query = query; 
        }

        public IViewComponentResult Invoke()
        {
            var articles = _query.LatestArticles();
            
            return View(articles);
        }
    }
}