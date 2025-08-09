using System.Collections.Generic;
using Bm.Application.Contracts.Article;
using Shop.Domain;

namespace Bm.Domain.ArticleAgg
{
    public interface IArticleRepository : IBaseRepository<long, Article>
    {
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
        EditArticle GetDetails(long Id);

        Article GetWithCategory(long id);


    }
}