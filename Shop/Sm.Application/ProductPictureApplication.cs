using System;
using System.Collections.Generic;
using Shop.Application;
using Sm.Application.Contracts.ProductPicture;
using Sm.Domain.ProductAgg;
using Sm.Domain.ProductPictureAgg;

namespace Sm.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetProductWithCategoryBy(command.ProductId);

            var path = $"{product.Category.Slug.Slugify()}//{product.Slug.Slugify()}";
            var fileName = _fileUploader.Upload(command.Picture, path);

            var picture = new ProductPicture(command.ProductId, fileName, command.PictureAlt,
                command.PictureTitle);
            
            _productPictureRepository.Create(picture);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();

            var picture = _productPictureRepository.GetProductPictureWithProductBy(command.Id);
            
            if (picture == null)
                return operation.Failed(ApplicationMessages.NotFound);

            var path = $"{picture.Product.Category.Slug.Slugify()}//{picture.Product.Slug.Slugify()}";
            Console.WriteLine("wwwwwwwwwwwwwwwww");
            Console.WriteLine(path);
            var fileName = _fileUploader.Upload(command.Picture, path);

            picture.Edit(command.ProductId, fileName, command.PictureAlt,
                command.PictureTitle);
            
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();

            var picture = _productPictureRepository.Get(id);
            if (picture == null)
                return operation.Failed(ApplicationMessages.NotFound);
            
            picture.Remove();
            
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();

            var picture = _productPictureRepository.Get(id);
            if (picture == null)
                return operation.Failed(ApplicationMessages.NotFound);
            
            picture.Restore();
            
            _productPictureRepository.SaveChanges();
            return operation.Succedded();        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}