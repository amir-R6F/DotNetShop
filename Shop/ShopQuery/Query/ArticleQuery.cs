using System;
using System.Collections.Generic;
using System.Linq;
using Bm.Infrastructure;
using Cm.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using ShopQuery.Contracts.Article;
using ShopQuery.Contracts.Comment;

namespace ShopQuery.Contracts.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;


        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _context.Articles.Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription
                
            }).OrderByDescending(x => x.Id)
                .Take(4)
                .ToList();
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
            {
                Id = x.Id,
                Title = x.Title,
                CategoryName = x.Category.Name,
                CategorySlug = x.Category.Slug,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription,

            }).FirstOrDefault(x => x.Slug == slug);

            article.KeywordList = article.Keywords.Split(",").ToList();

            var comments = _commentContext.Comments
                .Where(x => !x.IsCanceled)
                .Where(x => x.IsConfirmed)
                .Where(x => x.Type == CommentsType.Article)
                .Where(x => x.OwnerId == article.Id)
                .Select(x => new CommentQueryModel
                {
                    Id = x.Id,
                    Message = x.Message,
                    Name = x.Name,
                    ParentId = x.ParentId,
                    CreationDate = x.CreationDate.ToFarsi(),
                    
                }).OrderByDescending(x => x.Id )
                .ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentId > 0)
                {
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
                }
                    
            }
                

            article.Comments = comments;
            
            return article;
        }
    }
}