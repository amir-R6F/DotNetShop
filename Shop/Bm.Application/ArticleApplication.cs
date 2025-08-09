using System;
using System.Collections.Generic;
using Bm.Application.Contracts.Article;
using Bm.Domain.ArticleAgg;
using Bm.Domain.ArticleCategoryAgg;
using Shop.Application;

namespace Bm.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _repository;
        private readonly IArticleCategoryRepository _categoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository repository, IArticleCategoryRepository categoryRepository, IFileUploader fileUploader)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if (_repository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.Duplicate);

            var categoryslug = _categoryRepository.GetSlugBy(command.CategoryId);
            var slug = command.Slug.Slugify();
            
            var path = $"{categoryslug}/{slug}";
            var FileName = _fileUploader.Upload(command.Picture, path);
            
            // var publishdate = command.PublishDate.ToGeorgianDateTime();
            
            var article = new Article(command.Title, command.ShortDescription, command.Description, FileName,
                command.PictureAlt, command.PictureTitle,
                DateTime.Now, slug, command.Keywords, command.MetaDescription, command.CanonicalAddress,
                command.CategoryId);

            _repository.Create(article);
            _repository.SaveChanges();

            return operation.Succedded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();

            var article = _repository.GetWithCategory(command.Id);
            
            if (article == null)
                return operation.Failed(ApplicationMessages.NotFound);
            
            if (_repository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.Duplicate);

            
            var slug = command.Slug.Slugify();
            var path = $"{article.Category.Slug}/{slug}";
            var FileName = _fileUploader.Upload(command.Picture, path);
            // var publishdate = command.PublishDate.ToGeorgianDateTime();
            
            article.Edit(command.Title, command.ShortDescription, command.Description, FileName,
                command.PictureAlt, command.PictureTitle,
                DateTime.Now, slug, command.Keywords, command.MetaDescription, command.CanonicalAddress,
                command.CategoryId);

            
            _repository.SaveChanges();

            return operation.Succedded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _repository.Search(searchModel);
        }

        public EditArticle GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }
    }
}