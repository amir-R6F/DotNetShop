using System.Collections.Generic;
using Shop.Application;
using Sm.Application.Contracts.ProductCategory;

namespace Sm.Application.Contracts.Product
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
        EditProduct GetDetails(long id);
        OperationResult InStock(long id);
        OperationResult NotInStock(long id);

        List<ProductViewModel> GetProducts();
    }
}