using System.Collections.Generic;
using Shop.Application;

namespace Bm.Application.Contracts.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategory command);
        OperationResult Edit(EditArticleCategory command);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);

        EditArticleCategory GetDetails(long id);

        List<ArticleCategoryViewModel> GetCategories();
    }
}