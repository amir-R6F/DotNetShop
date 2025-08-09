using System.Collections.Generic;
using Bm.Application.Contracts.ArticleCategory;
using Shop.Domain;

namespace Bm.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IBaseRepository<long, ArticleCategory>
    {
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        EditArticleCategory GetDetails(long Id);
        string GetSlugBy(long id);
        
        List<ArticleCategoryViewModel> GetCategories();
    }
}