using System.Collections.Generic;
using System.Linq;
using Bm.Application.Contracts.Article;
using Bm.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Shop.Application;
using Shop.Infrastructure;

namespace Bm.Infrastructure.Repository
{
    public class ArticleRepository : BaseRepository<long, Article>, IArticleRepository
    {
        private readonly BlogContext _context;
        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _context.Articles.Select(x => new ArticleViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Picture = x.Picture,
                CategoryId = x.CategoryId,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription
            });
            
            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(x => x.Title == searchModel.Title);
            
            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditArticle GetDetails(long Id)
        {
            return _context.Articles.Select(x => new EditArticle
            {
                Id = x.Id,
                Description = x.Description,
                Keywords = x.Keywords,
                Slug = x.Slug,
                Title = x.Title,
                // Picture = x.Picture,
                CanonicalAddress = x.CanonicalAddress,
                CategoryId = x.CategoryId,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription
            }).FirstOrDefault(x => x.Id == Id);
        }

        public Article GetWithCategory(long id)
        {
            return _context.Articles.Include(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}