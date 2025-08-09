using System;
using System.Collections.Generic;
using System.Linq;
using Bm.Application.Contracts.ArticleCategory;
using Bm.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;
using Shop.Domain;
using Shop.Infrastructure;

namespace Bm.Infrastructure.Repository
{
    public class ArticleCategoryRepository : BaseRepository<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;
        
        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Picture = x.Picture,
                ShowOrder = x.ShowOrder
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name == searchModel.Name);
            
            return query.OrderByDescending(x => x.Id).ToList();
        }

        public EditArticleCategory GetDetails(long Id)
        {
            return _context.ArticleCategories.Select(x => new EditArticleCategory
            {
                Id = x.Id,
                Description = x.Description,
                Keywords = x.Keywords,
                Name = x.Name,
                // Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                CanonicalAddress = x.CanonicalAddress,
                MetaDescription = x.MetaDescription,
                ShowOrder = x.ShowOrder
            }).FirstOrDefault(x => x.Id == Id);
        }
        
        public string GetSlugBy(long id)
        {
            var category = _context.ArticleCategories
                .Select(x => new { x.Id, x.Slug })
                .FirstOrDefault(x => x.Id == id);

            if (category == null)
                throw new Exception($"Article category with ID {id} not found.");

            return category.Slug;
        }

        public List<ArticleCategoryViewModel> GetCategories()
        {
            return _context.ArticleCategories.Select(x => new ArticleCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}