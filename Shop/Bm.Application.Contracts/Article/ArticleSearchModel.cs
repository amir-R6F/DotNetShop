using System.Collections.Generic;
using Bm.Application.Contracts.ArticleCategory;

namespace Bm.Application.Contracts.Article
{
    public class ArticleSearchModel
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public List<ArticleCategoryViewModel> Categoires { get; set; }
    }
}