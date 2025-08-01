using System.Collections.Generic;
using Shop.Application;
using Sm.Application.Contracts.ProductPicture;
using Sm.Domain.ProductPictureAgg;

namespace Sm.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;


        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();
            if (_productPictureRepository.Exists(x =>
                x.Picture == command.Picture &&
                x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.Duplicate);

            var picture = new ProductPicture(command.ProductId, command.Picture, command.PictureAlt,
                command.PictureTitle);
            _productPictureRepository.Create(picture);
            _productPictureRepository.SaveChanges();
            return operation.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();

            var picture = _productPictureRepository.Get(command.Id);
            if (picture == null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (_productPictureRepository.Exists(x =>
                x.Picture == command.Picture &&
                x.ProductId == command.ProductId &&
                x.Id != command.Id))
                return operation.Failed(ApplicationMessages.Duplicate);

            picture.Edit(command.ProductId, command.Picture, command.PictureAlt,
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