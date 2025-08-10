using System.Collections.Generic;
using Bm.Infrastructure;

namespace ShopQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {

        ArticleCategoryQueryModel GetArticleCategory(string slug);
        List<ArticleCategoryQueryModel> LatestCategories();
        List<ArticleCategoryQueryModel> GetCategories();
    }
}