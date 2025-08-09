using System.Collections.Generic;
using Shop.Application;

namespace Bm.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);

        EditArticle GetDetails(long id);
    }
}