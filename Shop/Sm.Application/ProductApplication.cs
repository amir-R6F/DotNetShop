using System.Collections.Generic;
using Shop.Application;
using Sm.Application.Contracts.Product;
using Sm.Domain.ProductAgg;
using Sm.Domain.ProductCategoryAgg;

namespace Sm.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ProductApplication(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();

            if (_productRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.Duplicate);

            var slug = command.Slug.Slugify();
            var category = _productCategoryRepository.GetSlugBy(command.CategoryId);
            
            var filename = _fileUploader.Upload(command.Picture, category.Slugify());


            var product = new Product(command.Name, command.Code, command.ShortDescription,
                command.Description, filename, command.PictureAlt,
                command.PictureTitle, command.CategoryId, slug, command.Keywords, command.MetaDescription);

            _productRepository.Create(product);
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetProductWithCategoryBy(command.Id);

            if (product == null)
                return operation.Failed(ApplicationMessages.NotFound);

            // if (_productRepository.Exists(x => x.Name == command.Name && x.Id == command.Id))
            //     return operation.Failed(ApplicationMessages.Duplicate);

            var slug = command.Slug.Slugify();
            var path = $"{product.Category.Slug.Slugify()}/{slug}";
            var FileName = _fileUploader.Upload(command.Picture, path);

            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description, FileName, command.PictureAlt,
                command.PictureTitle, command.CategoryId, slug, command.Keywords, command.MetaDescription);
            _productRepository.SaveChanges();
            return operation.Succedded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }
        
        

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }
    }
}