using System.Collections.Generic;
using System.Linq;
using Bm.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using ShopQuery.Contracts.Article;
using ShopQuery.Contracts.ArticleCategory;

namespace ShopQuery.Contracts.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _context;

        public ArticleCategoryQuery(BlogContext context)
        {
            _context = context;
        }

        public List<ArticleCategoryQueryModel> LatestCategories()
        {
            return _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ArticlesCount = x.Articles.Count
                    
                }).ToList();
        }

        public List<ArticleCategoryQueryModel> GetCategories()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryQueryModel
            {
                Name = x.Name,
                Slug = x.Slug
            }).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            var category = _context.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Slug = x.Slug,
                    Name = x.Name,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    Articles = MappArticle(x.Articles)
                }).FirstOrDefault(x => x.Slug == slug);

            category.KeywordList = category.Keywords.Split(",").ToList();

            return category;
        }

        private static List<ArticleQueryModel> MappArticle(List<Bm.Domain.ArticleAgg.Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel
            {
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi()
            }).ToList();
        }
    }
}