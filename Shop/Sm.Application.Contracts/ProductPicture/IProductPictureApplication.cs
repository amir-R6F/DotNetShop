using System.Collections.Generic;
using Shop.Application;
using Sm.Application.Contracts.Product;

namespace Sm.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);

        OperationResult Edit(EditProductPicture command);

        OperationResult Remove(long id);
        OperationResult Restore(long id);

        EditProductPicture GetDetails(long id);

        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);    

    }
}